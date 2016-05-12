﻿using StkGenCode.Code.Column;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class DbCodeFireBird : CodeBase
    {
        public static string FileName(string tablename)
        {
            return string.Format("{0}Db.cs", tablename);
        }

        public static string ClassName(string tablename)
        {
            return string.Format("{0}Db", tablename);
        }

        private string GenUsign()
        {
            string _code = "";
            _code += "using System;" + _NewLine;
            _code += "using System.Collections.Generic;" + _NewLine;
            _code += "using System.Data;" + _NewLine;
            _code += "using System.Linq;" + _NewLine;

            _code += "using System.Web.UI.WebControls;" + _NewLine;

            return _code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            string _code = "";
            //_code = "namespace XXXXX.Code.Bu" + _NewLine;
            //_code += "{" + _NewLine;
            _code += "public class  " + ClassName(_TableName) + ": DataAccess" + _NewLine;
            _code += "{" + _NewLine;

            return _code;
        }

        private string GenEndNameSpaceAndClass()
        {
            string _code = "" + _NewLine;
            _code = "}" + _NewLine;
            //_code += " }"; Close Name

            return _code;
        }

        private string GenGetAll()
        {
            string _code = "";
            _code += "public List<" + _TableName + "> Select()\r\n {";
            _code += "string _sql1 = \"SELECT * FROM " + _TableName + "\";\r\n";
            _code += " DataSet ds = Db.GetDataSet(_sql1);  \r\n ";
            _code += " return DataSetToList(ds);   \r\n";
            _code += "} \r\n";
            return _code;
        }

        private string GenSelectOne()
        {
            string _code = "";
            string sqlColumnList = ""; //"A,B,C,D,E"

            string column = _ds.Tables[0].PrimaryKey[0].ToString();
            sqlColumnList = ColumnString.GenLineString(_ds, "{0},");
            _code += "public " + _TableName + " Select(string " + column + ") " + _NewLine;
            _code += "{ " + _NewLine;

            _code += " string sql = \"SELECT " + sqlColumnList + "0 AS RecordCount FROM " + _TableName + " where " + column + " = @" + column + "; \"; " + _NewLine;
            _code += "  var prset = new List<IDataParameter>();" + _NewLine;
            _code += "  prset.Add(Db.CreateParameterDb(\"@" + column + "\", " + column + "));" + _NewLine;
            _code += "  DataSet ds = Db.GetDataSet(sql,prset);" + _NewLine;
            _code += "return DataSetToList(ds).FirstOrDefault(); " + _NewLine;
            _code += "}";
            return _code;
        }

        private string GetWithFilter()
        {
            string sql = "";
            sql += "public List<" + _TableName + "> GetWithFilter(bool sortAscending, string sortExpression)";
            sql += "{" + _NewLine;

            sql += _NotImplement + _NewLine;
            sql += "string sql = \"SELECT * FROM " + _TableName + " \"; " + _NewLine;

            bool isfirst = true;

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                if (isfirst == true)
                {
                    sql += "sql += string.Format(\"  where ((''='{0}')or(" + _DataColumn.ColumnName + "='{0}'))\", _" + _TableName + "." + _DataColumn.ColumnName + ");";
                    sql += _NewLine;
                    isfirst = false;
                }
                else
                {
                    sql += "sql += string.Format(\"  and ((''='{0}')or(" + _DataColumn.ColumnName + "='{0}'))\", _" + _TableName + "." + _DataColumn.ColumnName + ");";
                    sql += _NewLine;
                }
            }

            sql += "  if (sortExpression == null)";
            sql += "{" + _NewLine;
            sql += "sql += string.Format(\" order by " + _ds.Tables[0].Columns[0].ColumnName + " \", sortExpression);";
            sql += "}" + _NewLine;
            sql += "else" + _NewLine;
            sql += "{" + _NewLine;
            //sql += " SetSort(sortAscending, sortExpression);";
            sql += "}" + _NewLine;

            sql += _NewLine;
            sql += "DataSet ds = Db.GetDataSet(sql);";
            sql += "return DataSetToList(ds);";
            sql += "}" + _NewLine;
            return sql;
        }

        private string GenGetPageWise()
        {
            string code = "";
            string columnSql = ColumnString.GenLineString(_ds, "{0},");
            code += "  public List< " + _TableName + "> GetPageWise(int pageIndex, int PageSize, string wordFullText = \"\") " + _NewLine;
            code += "    { " + _NewLine;
            code += "        string sql = \"\"; " + _NewLine;
            code += " " + _NewLine;
            code += "        //Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [EM_ID] desc )AS RowNumber ,*  FROM [ " + _TableName + "]' + @CommandFilter; " + _NewLine;
            code += "        string ColumnSort = \"\"; " + _NewLine;
            code += "        if (_SortExpression == null) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            ColumnSort = DataKey; " + _NewLine;
            code += "        } " + _NewLine;
            code += "        else " + _NewLine;
            code += "        { " + _NewLine;
            code += "            ColumnSort = _SortExpression; " + _NewLine;
            code += "        } " + _NewLine;
            code += "        string sortCommnad = GenSort(_SortDirection, ColumnSort); " + _NewLine;
            code += " " + _NewLine;

            code += "// Non implemnet full text Search" + _NewLine;
            code += "        string whereCommnad = GenWhereformProperties(); " + _NewLine;
            code += " " + _NewLine;
            code += "        int startRow = ((pageIndex - 1) * PageSize) + 1; " + _NewLine;
            code += "        int toRow = (startRow + PageSize) - 1; " + _NewLine;
            code += "        sql = string.Format(\"SELECT  {4}," + columnSql + "  (SELECT count(*) FROM  " + _TableName + "  {1}) as RecordCount FROM  " + _TableName + " A {1} {0} ROWS {2} TO {3}; \", sortCommnad, whereCommnad, startRow, toRow, Get_row_number_command()); " + _NewLine;
            code += " " + _NewLine;
            code += "        DataSet ds = Db.GetDataSet(sql); " + _NewLine;
            code += "        return DataSetToList(ds); " + _NewLine;
            code += "    }" + _NewLine;

            //Version 1
            //code += "public List<" + _TableName + "> GetPageWise(int pageIndex, int PageSize, string Wordfilter) " + _NewLine;
            //code += "{ " + _NewLine;
            //code += "string store = \"Sp_Get" + _TableName + "PageWise\"; " + _NewLine;

            //code += "            string sql = \"\"; " + _NewLine;
            //code += " " + _NewLine;
            //code += "            //Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [EM_ID] desc )AS RowNumber ,*  FROM [" + _TableName + "]' + @CommandFilter; " + _NewLine;
            //code += "            string ColumnSort = \"\"; " + _NewLine;
            //code += "            if (_SortExpression == null) " + _NewLine;
            //code += "            { " + _NewLine;
            //code += "                ColumnSort = DataKey; " + _NewLine;
            //code += "            } " + _NewLine;
            //code += "            else " + _NewLine;
            //code += "            { " + _NewLine;
            //code += "                ColumnSort = _SortExpression; " + _NewLine;
            //code += "            } " + _NewLine;
            //code += "            string sortCommnad = GenSort(_SortDirection, ColumnSort); " + _NewLine;
            //code += "            sql = string.Format(\"insert into  #Results   SELECT ROW_NUMBER() OVER (  {0} )AS RowNumber ,*  FROM [" + _TableName + "] \", sortCommnad); " + _NewLine;
            //code += " sql += Wordfilter; " + _NewLine;

            //code += " " + _NewLine;

            //code += "var prset = new List<IDataParameter>(); " + _NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@PageIndex\", pageIndex)); " + _NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@PageSize\", PageSize)); " + _NewLine;
            //code += "" + _NewLine;

            //code += " " + _NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@CommandFilter\", sql)); " + _NewLine;
            //code += " " + _NewLine;
            //code += "DataSet ds = Db.GetDataSet(store, prset, CommandType.StoredProcedure); " + _NewLine;
            //code += "return DataSetToList(ds); " + _NewLine;
            //code += "}" + _NewLine;

            return code;
        }

        private string GenInsert()
        {
            string insercolumn = "";
            string inservalue = "";
            string insertparameter = "";

            string ReturnPrimary = "";
            // string updateCommand = "";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //              insercolumn += _DataColumn.ColumnName + "," ;
                //              inservalue += "?,";
                //insertparameter += "new FbParameter(\":"+ _DataColumn.ColumnName+"\", _obj."+ _DataColumn.ColumnName+"),";

                // updateCommand += _DataColumn.ColumnName + "=?,";
                //=====================================================================

                //ถ้า PRimary ไม่ auto ให้เลือกตัวมันเอง
                //SELECT @[name];
                if ((_DataColumn.Table.PrimaryKey[0].ToString() == _DataColumn.ColumnName.ToString()))
                {
                    if ((_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                    {
                        continue;
                    }
                    else
                    {
                        //ถ้าไม่เป็น Auto ให้เลือกตัวมันเอง Return
                        //
                        ReturnPrimary = "returning " + _DataColumn.Table.PrimaryKey[0];
                    }
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                insercolumn += _DataColumn.ColumnName + ",";
                inservalue += "@" + _DataColumn.ColumnName + ",";
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));" + _NewLine;
            }

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            string _code = "";
            _code += "public object Insert() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            _code += " VALUES (" + inservalue + ") " + ReturnPrimary + "\";" + _NewLine;
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            _code += _NewLine + insertparameter;
            _code += _NewLine;
            _code += _NewLine;

            //_code += "int output = Db.FbExecuteNonQuery(sql, prset);" + _NewLine;
            //_code += "if (output != 1){" + _NewLine;
            //_code += " throw new System.Exception(\"Insert\" + this.ToString());}   }" + _NewLine;

            _code += "object output = Db.FbExecuteScalar(sql, prset);";
            _code += "return output;";

            _code += "  }" + _NewLine;
            _code += _NewLine;
            return _code;
        }

        private string GenUpdate()
        {
            //string insercolumn = "";
            //string inservalue = "";
            string insertparameter = "";

            string updateCommand = "";

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
                if (_DataColumn.Table.PrimaryKey[0].ToString() == _DataColumn.ColumnName.ToString())
                {
                    continue;
                }

                updateCommand += _DataColumn.ColumnName + "=@" + _DataColumn.ColumnName + ",";

                //=====================================================================
                // New Version
                //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
                // insercolumn += _DataColumn.ColumnName + ",";
                //inservalue += "@" + _DataColumn.ColumnName + ",";
                //  insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
            }
            //Key

            string _code = "";
            _code += "public void Update() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            _code += _NewLine + insertparameter;

            _code += _NewLine;

            _code += "var sql = @\"UPDATE   " + _TableName + " SET  " + updateCommand.Trim(',') + " where " + _ds.Tables[0].PrimaryKey[0].ToString() + " = @" + _ds.Tables[0].PrimaryKey[0].ToString() + "\";" + _NewLine; ;
            //_code += "var prset = new List<FbParameter> { new FbParameter(\":" + _TableName + "_ID\", " + _TableName + "_ID.ToString().Trim()) };";
            _code += _NewLine;

            //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
            //int output = DB_R2.FbExecuteNonQuery(sql, prset);
            //if (output != 1)
            //{
            //    throw new System.Exception("Save " + this.ToString());
            //}
            _code += "int output = Db.FbExecuteNonQuery(sql, prset);" + _NewLine;
            _code += "if (output != 1){" + _NewLine;
            _code += " throw new System.Exception(\"Update\" + this.ToString());}   }" + _NewLine;

            _code += _NewLine;
            return _code;
        }

        private string GenDelete()
        {
            string insercolumn = "";
            string inservalue = "";
            string insertparameter = "";

            insertparameter = " prset.Add(Db.CreateParameterDb(\"@" + _ds.Tables[0].PrimaryKey[0].ToString() + "\",_" + _TableName + "." + _ds.Tables[0].PrimaryKey[0].ToString() + "));";

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            string _code = "";
            _code += "public void Delete() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            _code += _NewLine + insertparameter;

            _code += _NewLine;

            string DeleteCondition = "";

            //pyID=@pyID
            DeleteCondition = _ds.Tables[0].PrimaryKey[0].ToString() + "=@" + _ds.Tables[0].PrimaryKey[0].ToString();

            // DeleteCondition = DeleteCondition.Remove(DeleteCondition.Length - 3);

            _code += "var sql =@\"DELETE FROM " + _TableName + " where " + DeleteCondition + "\";" + _NewLine; ;
            //_code += "var prset = new List<FbParameter> { new FbParameter(\":" + _TableName + "_ID\", " + _TableName + "_ID.ToString().Trim()) };";
            _code += _NewLine;

            _code += "int output = Db.FbExecuteNonQuery(sql, prset);" + _NewLine;
            _code += "if (output != 1){" + _NewLine;
            _code += " throw new System.Exception(\"Delete\" + this.ToString());}   }" + _NewLine;

            _code += _NewLine;
            return _code;
        }

        private string GenConvertDataList()
        {
            string _code = "";
            //_code += "private List<MPO_CUSTOMER_R2> DataSetToList(DataSet ds)" + _NewLine;
            //_code += "{" + _NewLine;

            //_code += "EnumerableRowCollection < MPO_CUSTOMER_R2 > q = (from temp in ds.Tables[0].AsEnumerable()" + _NewLine;
            //_code += "select new MPO_CUSTOMER_R2" + _NewLine;
            //_code += "{" + _NewLine;

            _code += "private List<" + _TableName + "> DataSetToList(DataSet ds) \r\n";
            _code += "{\r\n";
            _code += " EnumerableRowCollection<" + _TableName + "> q = (from temp in ds.Tables[0].AsEnumerable()\r\n";
            _code += " select new " + _TableName + "\r\n";
            _code += "{\r\n";

            //Inherrit
            _code += "RecordCount = temp.Field<Int32>(\"RecordCount\"),";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                string NullType = "";
                if ((_DataColumn.DataType.ToString() == "System.String"))
                { NullType = ""; }
                else
                { NullType = "?"; }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                _code += _DataColumn.ColumnName + "= temp.Field<" + _DataColumn.DataType.Name + NullType + ">(\"" + _DataColumn.ColumnName + "\"), \r\n ";
            }

            _code += " });\r\n";

            _code += "  return q.ToList();\r\n";
            _code += "}\r\n";

            return _code;
        }

        private string GenConStance()
        {
            string _code = "";
            _code += "  public " + _TableName + " _" + _TableName + ";" + _NewLine;

            _code += "public const string DataKey = \"" + _ds.Tables[0].PrimaryKey[0].ColumnName + "\";" + _NewLine;
            _code += "public const string DataText = \"" + _ds.Tables[0].Columns[0].ColumnName + "\";" + _NewLine;
            _code += "public const string DataValue = \"" + _ds.Tables[0].Columns[1].ColumnName + "\";" + _NewLine;
            return _code;
        }

        private string Comment()
        {
            string _code = _NewLine + "//Trasaction User" + _NewLine;
            _code += "//bool output = false;" + _NewLine;
            _code += "//    try" + _NewLine;
            _code += "//    {" + _NewLine;
            _code += "//        Db.OpenFbData();" + _NewLine;
            _code += "//        Db.BeginTransaction();" + _NewLine;

            _code += "//        MPO_ORDERS o1 = new MPO_ORDERS();" + _NewLine;
            _code += "//o1 = _MPO_ORDERS;" + _NewLine;
            _code += "//        int orid = o1.Save();" + _NewLine;

            _code += "//MPO_ODERDETAILS o2 = new MPO_ODERDETAILS();" + _NewLine;
            _code += "//o2.Save(orid, ODERDETAILS);" + _NewLine;

            _code += "//        Db.CommitTransaction();" + _NewLine;
            _code += "//        OR_ID = orid;" + _NewLine;
            _code += "//        output = true;" + _NewLine;
            _code += "//    }" + _NewLine;
            _code += "//    catch (System.Exception ex)" + _NewLine;
            _code += "//    {" + _NewLine;
            _code += "//        Db.RollBackTransaction();" + _NewLine;
            _code += "//        ErrorLogging.LogErrorToLogFile(ex, \"\");" + _NewLine;
            _code += "//        throw ex;" + _NewLine;
            _code += "//    }" + _NewLine;

            _code += "//    return output;" + _NewLine;

            return _code;
        }

        private string GenUpdateColumn()
        {
            string code = "";
            //id, column, value
            code += "   public Boolean UpdateColumn(string id, string column,string value) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            var prset = new List<IDataParameter>(); " + _NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@" + _ds.Tables[0].PrimaryKey[0].ColumnName + "\", id)); " + _NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@Data\", value)); " + _NewLine;
            code += "             var sql = @\"UPDATE   " + _TableName + " SET \"+column+ \"=@Data where " + _ds.Tables[0].PrimaryKey[0].ColumnName + " = @" + _ds.Tables[0].PrimaryKey[0].ColumnName + "\"; " + _NewLine;
            code += " " + _NewLine;
            code += "            int output = Db.FbExecuteNonQuery(sql, prset); " + _NewLine;
            code += "            if (output == 1) " + _NewLine;
            code += "            { " + _NewLine;
            code += "                return true; " + _NewLine;
            code += "            } " + _NewLine;
            code += " " + _NewLine;
            code += "            return false;   " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        private string GenGetKeyWordsAllColumn()
        {
            string code = "";

            code += " public List<string> GetKeyWordsAllColumn(string Keyword) " + _NewLine;
            code += "    { " + _NewLine;
            code += "        " + _NewLine;
            //[Sp_Getfxrates_family_Autocomplete]
            code += "        string sql = \"Sp_Get" + _TableName + "_Autocomplete\"; " + _NewLine;
            code += "        var prset = new List<IDataParameter>(); " + _NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@Key_word\", Keyword)); " + _NewLine;
            code += " " + _NewLine;
            code += "        List<string> dataArray = new List<string>(); " + _NewLine;
            code += " " + _NewLine;
            code += "        DataSet ds = Db.GetDataSet(sql, prset,CommandType.StoredProcedure); " + _NewLine;
            code += "        foreach (DataRow row in ds.Tables[0].Rows) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            dataArray.Add(row[0].ToString()); " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += "        return dataArray; " + _NewLine;
            code += "    }" + _NewLine;

            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            string code = "";
            code += "  public List<string> GetKeyWordsOneColumn(string column, string keyword) " + _NewLine;
            code += "  { " + _NewLine;
            code += "          " + _NewLine;
            code += " " + _NewLine;
            code += "  string sql = \"SELECT  \" + column + \" FROM " + _TableName + " where lower(\" + column + \") like '\" + keyword.ToLower() + \"%'   group by \" + column + \" order by count(*) desc;\"; " + _NewLine;

            code += "         " + _NewLine;
            code += "         " + _NewLine;
            code += "  List<string> dataArray = new List<string>(); " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "  DataSet ds = Db.GetDataSet(sql); " + _NewLine;
            code += "  foreach (DataRow row in ds.Tables[0].Rows) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            dataArray.Add(row[0].ToString()); " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += "        return dataArray; " + _NewLine;
            code += "    } " + _NewLine;
            return code;
        }

        private string GenWhereformProperties()
        {
            string code = "";

            code += "public string GenWhereformProperties() " + _NewLine;
            code += "{" + _NewLine;
            code += "  String sql=\"\";" + _NewLine;
            code += "   sql += \"WHERE (1=1) \"; " + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                code += "            if ( _" + _TableName + "." + _DataColumn.ColumnName + "!= null) " + _NewLine;
                code += "            { " + _NewLine;
                if (_DataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += "                sql += string.Format(\" AND ((''='{0}') or (" + _DataColumn.ColumnName + "='{0}') )\", StkDate.ConvertDateEnForDb(Convert.ToDateTime( _" + _TableName + "." + _DataColumn.ColumnName + "))); " + _NewLine;
                }
                else
                {
                    code += "                sql += string.Format(\" AND ((''='{0}') or (" + _DataColumn.ColumnName + "='{0}') )\", _" + _TableName + "." + _DataColumn.ColumnName + "); " + _NewLine;
                }
                code += "            } " + _NewLine;
            }

            code += "return sql;" + _NewLine;
            code += "}" + _NewLine;
            return code;
        }

        public string GenSql()
        {
            string code = "";
            code += "  public string GenSql() " + _NewLine;
            code += "        { " + _NewLine;
            code += "            string sql = \"\"; " + _NewLine;
            code += " " + _NewLine;
            code += "            //Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [EM_ID] desc )AS RowNumber ,*  FROM [" + _TableName + "]' + @CommandFilter; " + _NewLine;
            code += "            string ColumnSort = \"\"; " + _NewLine;
            code += "            if (_SortExpression == null) " + _NewLine;
            code += "            { " + _NewLine;
            code += "                ColumnSort = DataKey; " + _NewLine;
            code += "            } " + _NewLine;
            code += "            else " + _NewLine;
            code += "            { " + _NewLine;
            code += "                ColumnSort = _SortExpression; " + _NewLine;
            code += "            } " + _NewLine;
            code += "            string sortCommnad = GenSort(_SortDirection, ColumnSort); " + _NewLine;
            code += "            sql = string.Format(\"insert into  #Results   SELECT ROW_NUMBER() OVER (  {0} )AS RowNumber ,*  FROM [" + _TableName + "] \", sortCommnad); " + _NewLine;
            code += " " + _NewLine;
            code += "            sql += GenWhereformProperties(); " + _NewLine;
            code += "            return sql; " + _NewLine;
            code += "        }" + _NewLine;
            return code;
        }

        private string GenProperties()
        {
            string code = "";
            code += " public SortDirection _SortDirection { get; set; }" + _NewLine;
            code += " public string _SortExpression { get; set; }" + _NewLine;
            return code;
        }

        private string Get_row_number_command()
        {
            string code = "";
            code += "    public string Get_row_number_command() " + _NewLine;
            code += "    { " + _NewLine;
            code += "        return \"rdb$get_context('USER_TRANSACTION', 'row#') as row_number,rdb$set_context('USER_TRANSACTION', 'row#', coalesce(cast(rdb$get_context('USER_TRANSACTION', 'row#') as integer), 0) + 1)\"; " + _NewLine;
            code += "    }" + _NewLine;
            return code;
        }

        public override void Gen()

        {
            string _code = "";
            _code = GenUsign();

            _code += GenBeginNameSpaceAndClass();
            _code += GenProperties();
            _code += GenConStance();

            _code += GenGetAll();
            _code += GenSelectOne();
            _code += GetWithFilter();

            _code += GenGetPageWise();
            _code += GenInsert();
            _code += GenUpdate();
            _code += GenDelete();
            _code += GenConvertDataList();
            _code += GenUpdateColumn();

            _code += GenGetKeyWordsAllColumn();
            _code += GenGetKeyWordsOneColumn();
            _code += GenSql();
            _code += GenWhereformProperties();
            _code += Get_row_number_command();

            _code += GenEndNameSpaceAndClass();

            _code += Comment();

            FileName name = new FileName();
            name._TableName = _TableName;
            name._ds = _ds;
            _FileCode.writeFile(FileName(_TableName), _code);
        }
    }
}