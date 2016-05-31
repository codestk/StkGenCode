using System.Data;
using StkGenCode.Code.Column;

namespace StkGenCode.Code.Template
{
    public class DbCodeFireBird : CodeBase
    {
        public new static string FileName(string tablename)
        {
            return $"{tablename}Db.cs";
        }

        public static string ClassName(string tablename)
        {
            return $"{tablename}Db";
        }

        private string GenUsign()
        {
            var code = "";
            code += "using System;" + NewLine;
            code += "using System.Collections.Generic;" + NewLine;
            code += "using System.Data;" + NewLine;
            code += "using System.Linq;" + NewLine;
            code += "using System.Web.UI.WebControls;" + NewLine;

            return code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            var code = "";
            //_code = "namespace XXXXX.Code.Bu" + _NewLine;
            //_code += "{" + _NewLine;
            code += "public class  " + ClassName(TableName) + ": DataAccess" + NewLine;
            code += "{" + NewLine;

            return code;
        }

        private string GenEndNameSpaceAndClass()
        {
            var code = "}" + NewLine;

            return code;
        }

        private string GenGetAll()
        {
            var code = "";
            var sqlColumnList = GenListColumn(); //"A,B,C,D,E"
            code += "public List<SelectInputProperties> Select()\r\n {";
            //_code += "string _sql1 = \"SELECT * FROM " + _TableName + "\";\r\n";
            code += " string sql = \"SELECT " + sqlColumnList + "0 AS RecordCount FROM " + TableName + "\";" + NewLine;
            code += " DataSet ds = Db.GetDataSet(sql);  \r\n ";
            //code += " return DataSetToList(ds);   \r\n";
            code += " return SelectInputProperties.DataSetToList(ds);";
            code += "} \r\n";
            return code;
        }

        private string GenListColumn()
        {
            return ColumnString.GenLineString(Ds, "{0},");
        }

        private string GenSelectOne()
        {
            var code = "";
            var sqlColumnList = GenListColumn(); //"A,B,C,D,E"

            var column = Ds.Tables[0].PrimaryKey[0].ToString();
            //sqlColumnList = ColumnString.GenLineString(_ds, "{0},");
            code += "public " + TableName + " Select(string " + column + ") " + NewLine;
            code += "{ " + NewLine;

            code += " string sql = \"SELECT " + sqlColumnList + "0 AS RecordCount FROM " + TableName + " where " +
                    column + " = @" + column + "; \"; " + NewLine;
            code += "  var prset = new List<IDataParameter>();" + NewLine;
            code += "  prset.Add(Db.CreateParameterDb(\"@" + column + "\", " + column + "));" + NewLine;
            code += "  DataSet ds = Db.GetDataSet(sql,prset);" + NewLine;
            code += "return DataSetToList(ds).FirstOrDefault(); " + NewLine;
            code += "}";
            return code;
        }

        private string GetWithFilter()
        {
            var sql = "";
            sql += "public List<" + TableName + "> GetWithFilter(bool sortAscending, string sortExpression)";
            sql += "{" + NewLine;

            sql += NotImplement + NewLine;
            sql += "string sql = \"SELECT * FROM " + TableName + " \"; " + NewLine;

            var isfirst = true;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (isfirst)
                {
                    sql += "sql += string.Format(\"  where ((''='{0}')or(" + dataColumn.ColumnName + "='{0}'))\", _" +
                           TableName + "." + dataColumn.ColumnName + ");";
                    sql += NewLine;
                    isfirst = false;
                }
                else
                {
                    sql += "sql += string.Format(\"  and ((''='{0}')or(" + dataColumn.ColumnName + "='{0}'))\", _" +
                           TableName + "." + dataColumn.ColumnName + ");";
                    sql += NewLine;
                }
            }

            sql += "  if (sortExpression == null)";
            sql += "{" + NewLine;
            sql += "sql += string.Format(\" order by " + Ds.Tables[0].Columns[0].ColumnName + " \", sortExpression);";
            sql += "}" + NewLine;
            sql += "else" + NewLine;
            sql += "{" + NewLine;
            //sql += " SetSort(sortAscending, sortExpression);";
            sql += "}" + NewLine;

            sql += NewLine;
            sql += "DataSet ds = Db.GetDataSet(sql);";
            sql += "return DataSetToList(ds);";
            sql += "}" + NewLine;
            return sql;
        }

        private string GenGetPageWise()
        {
            var code = "";
            var columnSql = ColumnString.GenLineString(Ds, "{0},");
            code += "  public List< " + TableName +
                    "> GetPageWise(int pageIndex, int PageSize, string wordFullText = \"\") " + NewLine;
            code += "    { " + NewLine;
            code += "        string sql = \"\"; " + NewLine;
            code += " " + NewLine;
            code +=
                "        //Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [EM_ID] desc )AS RowNumber ,*  FROM [ " +
                TableName + "]' + @CommandFilter; " + NewLine;
            code += "        string ColumnSort = \"\"; " + NewLine;
            code += "        if (_SortExpression == null) " + NewLine;
            code += "        { " + NewLine;
            code += "            ColumnSort = DataKey; " + NewLine;
            code += "        } " + NewLine;
            code += "        else " + NewLine;
            code += "        { " + NewLine;
            code += "            ColumnSort = _SortExpression; " + NewLine;
            code += "        } " + NewLine;
            code += "        string sortCommnad = GenSort(_SortDirection, ColumnSort); " + NewLine;
            code += " " + NewLine;

            code += "// Non implemnet full text Search" + NewLine;
            code += "        string whereCommnad = GenWhereformProperties(); " + NewLine;
            code += " " + NewLine;
            code += "        int startRow = ((pageIndex - 1) * PageSize) + 1; " + NewLine;
            code += "        int toRow = (startRow + PageSize) - 1; " + NewLine;
            code += "        sql = string.Format(\"SELECT  {4}," + columnSql + "  (SELECT count(*) FROM  " + TableName +
                    "  {1}) as RecordCount FROM  " + TableName +
                    " A {1} {0} ROWS {2} TO {3}; \", sortCommnad, whereCommnad, startRow, toRow, Get_row_number_command()); " +
                    NewLine;
            code += " " + NewLine;
            code += "        DataSet ds = Db.GetDataSet(sql); " + NewLine;
            code += "        return DataSetToList(ds); " + NewLine;
            code += "    }" + NewLine;

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
            var insercolumn = "";
            var inservalue = "";
            var insertparameter = "";

            var returnPrimary = "";
            // string updateCommand = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                //              insercolumn += _DataColumn.ColumnName + "," ;
                //              inservalue += "?,";
                //insertparameter += "new FbParameter(\":"+ _DataColumn.ColumnName+"\", _obj."+ _DataColumn.ColumnName+"),";

                // updateCommand += _DataColumn.ColumnName + "=?,";
                //=====================================================================

                //ถ้า PRimary ไม่ auto ให้เลือกตัวมันเอง
                //SELECT @[name];
                if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                {
                    if (Ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    {
                        continue;
                    }
                    //ถ้าไม่เป็น Auto ให้เลือกตัวมันเอง Return
                    //
                    returnPrimary = "returning " + dataColumn.Table.PrimaryKey[0];
                }

                if (dataColumn.DataType.ToString() == "System.Guid")
                {
                    continue;
                }

                insercolumn += dataColumn.ColumnName + ",";
                inservalue += "@" + dataColumn.ColumnName + ",";
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + dataColumn.ColumnName + "\",_" + TableName +
                                   "." + dataColumn.ColumnName + "));" + NewLine;
            }

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            var code = "";
            code += "public object Insert() {";
            code += NewLine + "var prset = new List<IDataParameter>();";
            code += "var sql = \"INSERT INTO " + TableName + "(" + insercolumn + ")";
            code += " VALUES (" + inservalue + ") " + returnPrimary + "\";" + NewLine;
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            code += NewLine + insertparameter;
            code += NewLine;
            code += NewLine;

            //_code += "int output = Db.FbExecuteNonQuery(sql, prset);" + _NewLine;
            //_code += "if (output != 1){" + _NewLine;
            //_code += " throw new System.Exception(\"Insert\" + this.ToString());}   }" + _NewLine;

            code += "object output = Db.FbExecuteScalar(sql, prset);";
            code += "return output;";

            code += "  }" + NewLine;
            code += NewLine;
            return code;
        }

        private string GenUpdate()
        {
            //string insercolumn = "";
            //string inservalue = "";
            var insertparameter = "";

            var updateCommand = "";

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + dataColumn.ColumnName + "\",_" + TableName +
                                   "." + dataColumn.ColumnName + "));";
                if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                {
                    continue;
                }

                updateCommand += dataColumn.ColumnName + "=@" + dataColumn.ColumnName + ",";

                //=====================================================================
                // New Version
                //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
                // insercolumn += _DataColumn.ColumnName + ",";
                //inservalue += "@" + _DataColumn.ColumnName + ",";
                //  insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
            }
            //Key

            var code = "";
            code += "public void Update() {";
            code += NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            code += NewLine + insertparameter;

            code += NewLine;

            code += "var sql = @\"UPDATE   " + TableName + " SET  " + updateCommand.Trim(',') + " where " +
                    Ds.Tables[0].PrimaryKey[0] + " = @" + Ds.Tables[0].PrimaryKey[0] + "\";" + NewLine;
            //_code += "var prset = new List<FbParameter> { new FbParameter(\":" + _TableName + "_ID\", " + _TableName + "_ID.ToString().Trim()) };";
            code += NewLine;

            //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
            //int output = DB_R2.FbExecuteNonQuery(sql, prset);
            //if (output != 1)
            //{
            //    throw new System.Exception("Save " + this.ToString());
            //}
            code += "int output = Db.FbExecuteNonQuery(sql, prset);" + NewLine;
            code += "if (output != 1){" + NewLine;
            code += " throw new System.Exception(\"Update\" + this.ToString());}   }" + NewLine;

            code += NewLine;
            return code;
        }

        private string GenDelete()
        {
            //string insercolumn = "";
            //string inservalue = "";

            var insertparameter = " prset.Add(Db.CreateParameterDb(\"@" + Ds.Tables[0].PrimaryKey[0] + "\",_" +
                                  TableName + "." + Ds.Tables[0].PrimaryKey[0] + "));";

            //insercolumn = insercolumn.TrimEnd(',');
            //inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            var code = "";
            code += "public void Delete() {";
            code += NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            code += NewLine + insertparameter;

            code += NewLine;

            //pyID=@pyID
            var deleteCondition = Ds.Tables[0].PrimaryKey[0] + "=@" + Ds.Tables[0].PrimaryKey[0];

            // DeleteCondition = DeleteCondition.Remove(DeleteCondition.Length - 3);

            code += "var sql =@\"DELETE FROM " + TableName + " where " + deleteCondition + "\";" + NewLine;
            //_code += "var prset = new List<FbParameter> { new FbParameter(\":" + _TableName + "_ID\", " + _TableName + "_ID.ToString().Trim()) };";
            code += NewLine;

            code += "int output = Db.FbExecuteNonQuery(sql, prset);" + NewLine;
            code += "if (output != 1){" + NewLine;
            code += " throw new System.Exception(\"Delete\" + this.ToString());}   }" + NewLine;

            code += NewLine;
            return code;
        }

        private string GenConvertDataList()
        {
            var code = "";
            //_code += "private List<MPO_CUSTOMER_R2> DataSetToList(DataSet ds)" + _NewLine;
            //_code += "{" + _NewLine;

            //_code += "EnumerableRowCollection < MPO_CUSTOMER_R2 > q = (from temp in ds.Tables[0].AsEnumerable()" + _NewLine;
            //_code += "select new MPO_CUSTOMER_R2" + _NewLine;
            //_code += "{" + _NewLine;

            code += "private List<" + TableName + "> DataSetToList(DataSet ds) \r\n";
            code += "{\r\n";
            code += " EnumerableRowCollection<" + TableName + "> q = (from temp in ds.Tables[0].AsEnumerable()\r\n";
            code += " select new " + TableName + "\r\n";
            code += "{\r\n";

            //Inherrit
            code += "RecordCount = temp.Field<Int32>(\"RecordCount\"),";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                var nullType = dataColumn.DataType.ToString() == "System.String" ? "" : "?";

                if (dataColumn.DataType.ToString() == "System.Guid")
                {
                    continue;
                }

                code += dataColumn.ColumnName + "= temp.Field<" + dataColumn.DataType.Name + nullType + ">(\"" +
                        dataColumn.ColumnName + "\"), \r\n ";
            }

            code += " });\r\n";

            code += "  return q.ToList();\r\n";
            code += "}\r\n";

            return code;
        }

        private string GenConStance()
        {
            var code = "";
            code += "  public " + TableName + " _" + TableName + ";" + NewLine;

            code += "public const string DataKey = \"" + Ds.Tables[0].PrimaryKey[0].ColumnName + "\";" + NewLine;
            code += "public const string DataText = \"" + Ds.Tables[0].Columns[1].ColumnName + "\";" + NewLine;
            code += "public const string DataValue = \"" + Ds.Tables[0].PrimaryKey[0].ColumnName + "\";" + NewLine;
            return code;
        }

        //private string Comment()
        //{
        //    string code = _NewLine + "//Trasaction User" + _NewLine;
        //    code += "//bool output = false;" + _NewLine;
        //    code += "//    try" + _NewLine;
        //    code += "//    {" + _NewLine;
        //    code += "//        Db.OpenFbData();" + _NewLine;
        //    code += "//        Db.BeginTransaction();" + _NewLine;

        //    code += "//        MPO_ORDERS o1 = new MPO_ORDERS();" + _NewLine;
        //    code += "//o1 = _MPO_ORDERS;" + _NewLine;
        //    code += "//        int orid = o1.Save();" + _NewLine;

        //    code += "//MPO_ODERDETAILS o2 = new MPO_ODERDETAILS();" + _NewLine;
        //    code += "//o2.Save(orid, ODERDETAILS);" + _NewLine;

        //    code += "//        Db.CommitTransaction();" + _NewLine;
        //    code += "//        OR_ID = orid;" + _NewLine;
        //    code += "//        output = true;" + _NewLine;
        //    code += "//    }" + _NewLine;
        //    code += "//    catch (System.Exception ex)" + _NewLine;
        //    code += "//    {" + _NewLine;
        //    code += "//        Db.RollBackTransaction();" + _NewLine;
        //    code += "//        ErrorLogging.LogErrorToLogFile(ex, \"\");" + _NewLine;
        //    code += "//        throw ex;" + _NewLine;
        //    code += "//    }" + _NewLine;

        //    code += "//    return output;" + _NewLine;

        //    return code;
        //}

        private string GenUpdateColumn()
        {
            var code = "";
            //id, column, value
            code += "   public Boolean UpdateColumn(string id, string column,string value) " + NewLine;
            code += "        { " + NewLine;
            code += "            var prset = new List<IDataParameter>(); " + NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@" + Ds.Tables[0].PrimaryKey[0].ColumnName +
                    "\", id)); " + NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@Data\", value)); " + NewLine;
            code += "             var sql = @\"UPDATE   " + TableName + " SET \"+column+ \"=@Data where " +
                    Ds.Tables[0].PrimaryKey[0].ColumnName + " = @" + Ds.Tables[0].PrimaryKey[0].ColumnName + "\"; " +
                    NewLine;
            code += " " + NewLine;
            code += "            int output = Db.FbExecuteNonQuery(sql, prset); " + NewLine;
            code += "            if (output == 1) " + NewLine;
            code += "            { " + NewLine;
            code += "                return true; " + NewLine;
            code += "            } " + NewLine;
            code += " " + NewLine;
            code += "            return false;   " + NewLine;
            code += "        } " + NewLine;

            return code;
        }

        private string GenGetKeyWordsAllColumn()
        {
            var code = "";

            code += " public List<string> GetKeyWordsAllColumn(string Keyword) " + NewLine;
            code += "    { " + NewLine;
            code += "        " + NewLine;
            //[Sp_Getfxrates_family_Autocomplete]
            code += "        string sql = \"Sp_Get" + TableName + "_Autocomplete\"; " + NewLine;
            code += "        var prset = new List<IDataParameter>(); " + NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@Key_word\", Keyword)); " + NewLine;
            code += " " + NewLine;
            code += "        List<string> dataArray = new List<string>(); " + NewLine;
            code += " " + NewLine;
            code += "        DataSet ds = Db.GetDataSet(sql, prset,CommandType.StoredProcedure); " + NewLine;
            code += "        foreach (DataRow row in ds.Tables[0].Rows) " + NewLine;
            code += "        { " + NewLine;
            code += "            dataArray.Add(row[0].ToString()); " + NewLine;
            code += "        } " + NewLine;
            code += " " + NewLine;
            code += "        return dataArray; " + NewLine;
            code += "    }" + NewLine;

            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            var code = "";
            code += "  public List<string> GetKeyWordsOneColumn(string column, string keyword) " + NewLine;
            code += "  { " + NewLine;
            code += "          " + NewLine;
            code += " " + NewLine;
            code += "  string sql = \"SELECT  \" + column + \" FROM " + TableName +
                    " where lower(\" + column + \") like '\" + keyword.ToLower() + \"%'   group by \" + column + \" order by count(*) desc;\"; " +
                    NewLine;

            code += "         " + NewLine;
            code += "         " + NewLine;
            code += "  List<string> dataArray = new List<string>(); " + NewLine;
            code += " " + NewLine;
            code += " " + NewLine;
            code += "  DataSet ds = Db.GetDataSet(sql); " + NewLine;
            code += "  foreach (DataRow row in ds.Tables[0].Rows) " + NewLine;
            code += "        { " + NewLine;
            code += "            dataArray.Add(row[0].ToString()); " + NewLine;
            code += "        } " + NewLine;
            code += " " + NewLine;
            code += "        return dataArray; " + NewLine;
            code += "    } " + NewLine;
            return code;
        }

        private string GenWhereformProperties()
        {
            var code = "";

            code += "public string GenWhereformProperties() " + NewLine;
            code += "{" + NewLine;
            code += "  String sql=\"\";" + NewLine;
            code += "   sql += \"WHERE (1=1) \"; " + NewLine;
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                code += "            if (( _" + TableName + "." + dataColumn.ColumnName + "!= null) )" + NewLine;
                code += "            { " + NewLine;
                if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += "                sql += string.Format(\" AND ((''='{0}') or (" + dataColumn.ColumnName +
                            "='{0}') )\", StkDate.ConvertDateEnForDb(Convert.ToDateTime( _" + TableName + "." +
                            dataColumn.ColumnName + "))); " + NewLine;
                }
                else
                {
                    code += "                sql += string.Format(\" AND ((''='{0}') or (" + dataColumn.ColumnName +
                            "='{0}') )\", _" + TableName + "." + dataColumn.ColumnName + "); " + NewLine;
                }
                code += "            } " + NewLine;
            }

            code += "return sql;" + NewLine;
            code += "}" + NewLine;
            return code;
        }

        public string GenSql()
        {
            var code = "";
            code += "  public string GenSql() " + NewLine;
            code += "        { " + NewLine;
            code += "            string sql = \"\"; " + NewLine;
            code += " " + NewLine;
            code +=
                "            //Set @Command = 'insert into  #Results   SELECT ROW_NUMBER() OVER (ORDER BY [EM_ID] desc )AS RowNumber ,*  FROM [" +
                TableName + "]' + @CommandFilter; " + NewLine;
            code += "            string ColumnSort = \"\"; " + NewLine;
            code += "            if (_SortExpression == null) " + NewLine;
            code += "            { " + NewLine;
            code += "                ColumnSort = DataKey; " + NewLine;
            code += "            } " + NewLine;
            code += "            else " + NewLine;
            code += "            { " + NewLine;
            code += "                ColumnSort = _SortExpression; " + NewLine;
            code += "            } " + NewLine;
            code += "            string sortCommnad = GenSort(_SortDirection, ColumnSort); " + NewLine;
            code +=
                "            sql = string.Format(\"insert into  #Results   SELECT ROW_NUMBER() OVER (  {0} )AS RowNumber ,*  FROM [" +
                TableName + "] \", sortCommnad); " + NewLine;
            code += " " + NewLine;
            code += "            sql += GenWhereformProperties(); " + NewLine;
            code += "            return sql; " + NewLine;
            code += "        }" + NewLine;
            return code;
        }

        private string GenProperties()
        {
            var code = "";
            //code += " public SortDirection _SortDirection { get; set; }" + _NewLine;
            //code += " public string _SortExpression { get; set; }" + _NewLine;
            code += "public string _SortDirection { get; set; }" + NewLine;
            code += "public string _SortExpression { get; set; }" + NewLine;
            return code;
        }

        private string Get_row_number_command()
        {
            var code = "";
            code += "    public string Get_row_number_command() " + NewLine;
            code += "    { " + NewLine;
            code +=
                "        return \"rdb$get_context('USER_TRANSACTION', 'row#') as row_number,rdb$set_context('USER_TRANSACTION', 'row#', coalesce(cast(rdb$get_context('USER_TRANSACTION', 'row#') as integer), 0) + 1)\"; " +
                NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public override void Gen()

        {
            var code = GenUsign();

            code += GenBeginNameSpaceAndClass();
            code += GenProperties();
            code += GenConStance();

            code += GenGetAll();
            code += GenSelectOne();
            code += GetWithFilter();

            code += GenGetPageWise();
            code += GenInsert();
            code += GenUpdate();
            code += GenDelete();
            code += GenConvertDataList();
            code += GenUpdateColumn();

            code += GenGetKeyWordsAllColumn();
            code += GenGetKeyWordsOneColumn();
            code += GenSql();
            code += GenWhereformProperties();
            code += Get_row_number_command();

            code += GenEndNameSpaceAndClass();

            //code += Comment();

            //FileName name = new FileName();
            //name._TableName = _TableName;
            //name._ds = _ds;

            FileCode.WriteFile(FileName(TableName), code);
        }
    }
}