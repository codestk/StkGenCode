using System.Data;
using StkGenCode.Code.Name;

namespace StkGenCode.Code.Template
{
    public class DbCodeSqlServer : CodeBase
    {
        private string GenUsign()
        {
            var code = "";
            code += "using System;" + NewLine;
            code += "using System.Collections.Generic;" + NewLine;
            code += "using System.Data;" + NewLine;
            code += "using System.Linq;" + NewLine;
            code += "using System.Web;" + NewLine;
            code += "using System.Web.UI.WebControls;" + NewLine;

            code += "using WebApp.Business;" + NewLine;
            code += "using WebApp.Code.Utility;" + NewLine;
            code += "using WebApp.Code.Utility.Properties.Controls;" + NewLine;


            return code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            var code = "";
            //_code = "namespace XXXXX.Code.Bu" + _NewLine;
            //_code += "{" + _NewLine;
            code = "namespace WebApp.Business" + NewLine;
            code += "{" + NewLine;


            code += "public class  " + TableName + "Db: DataAccess" + NewLine;
            code += "{" + NewLine;

            return code;
        }

        private string GenEndNameSpaceAndClass()
        {
            var code = "} " + NewLine;   //Clos Class
            code += " }";  // Close Name

            return code;
        }

        private string GenGetAll()
        {
            //string code = "";
            //code += "public List<" + _TableName + "> Select()\r\n {";
            //code += "string _sql1 = \"SELECT * FROM " + _TableName + "\";\r\n";
            //code += " DataSet ds = Db.GetDataSet(_sql1);  \r\n ";
            //code += " return DataSetToList(ds);   \r\n";
            //code += "} \r\n";
            //return code;
            var code = "";

            //code += "public List<" + _TableName + "> Select()\r\n {";
            ////_code += "string _sql1 = \"SELECT * FROM " + _TableName + "\";\r\n";
            //code += " string sql = \"SELECT " + sqlColumnList + "0 AS RecordCount FROM " + _TableName + "\";" + _NewLine;
            //code += " DataSet ds = Db.GetDataSet(sql);  \r\n ";
            //code += " return DataSetToList(ds);   \r\n";
            //code += "} \r\n";

            code += " public List<SelectInputProperties> Select()" + NewLine;
            code += "    {" + NewLine;
            code += $" string sql = \"SELECT * FROM {TableName}\";" + NewLine;
            code += "        DataSet ds = Db.GetDataSet(sql);" + NewLine;
            code += "" + NewLine;
            code += "        return SelectInputProperties.DataSetToList(ds);" + NewLine;
            code += "		" + NewLine;
            code += "    }" + NewLine;
            code += "	" + NewLine;

            return code;
        }

        private string GenSelectOne()
        {
            var code = "";
            var column = Ds.Tables[0].PrimaryKey[0].ToString();

            code += "public " + TableName + " Select(string " + column + ") " + NewLine;
            code += "{ " + NewLine;
            code += " string _sql1 = \"SELECT *,0 AS RecordCount FROM " + TableName + " where " + column + " = @" +
                    column + "; \"; " + NewLine;
            code += "   var prset = new List<IDataParameter>();" + NewLine;
            code += "  prset.Add(Db.CreateParameterDb(\"@" + column + "\", " + column + "));" + NewLine;
            code += "  DataSet ds = Db.GetDataSet(_sql1,prset);" + NewLine;
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

            sql += "if (sortExpression == null)";
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

            code += "public List<" + TableName +
                    "> GetPageWise(int pageIndex, int PageSize, string  wordFullText=\"\") " + NewLine;
            code += "{ " + NewLine;
            code += "string store = \"Sp_Get" + TableName + "PageWise\"; " + NewLine;

            //code += "            string sql = \"\"; " + _NewLine;
            code += " " + NewLine;

            code += " " + NewLine;

            code += "var dbParameter = GetParameter(pageIndex,PageSize);" + NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@PageIndex\", pageIndex)); " + _NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@PageSize\", PageSize)); " + _NewLine;
            code += "" + NewLine;

            code += " " + NewLine;
            //code += "prset.Add(Db.CreateParameterDb(\"@CommandFilter\", sql)); " + _NewLine;
            code += " " + NewLine;
            code += "DataSet ds = Db.GetDataSet(store, dbParameter, CommandType.StoredProcedure); " + NewLine;
            code += "return DataSetToList(ds); " + NewLine;
            code += "}" + NewLine;

            return code;
        }

        private string GenInsert()
        {
            var insercolumn = "";
            var inservalue = "";
            var insertparameter = "";

            var returnPrimary = "SELECT SCOPE_IDENTITY();";
            // string updateCommand = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (IsExceptionColumn( dataColumn))
                {
                    continue;
                }

                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}
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
                    returnPrimary = "Select @" + dataColumn.Table.PrimaryKey[0];
                }

                insercolumn += dataColumn.ColumnName + ",";
                inservalue += "@" + dataColumn.ColumnName + ",";
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + dataColumn.ColumnName + "\",_" + TableName +
                                   "." + dataColumn.ColumnName + "));";
            }

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            var code = "";
            code += "public object Insert() {";
            code += NewLine + "var prset = new List<IDataParameter>();";
            code += "var sql = \"INSERT INTO " + TableName + "(" + insercolumn + ")";
            code += " VALUES (" + inservalue + ") ;" + returnPrimary + "\";";
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

                if (IsExceptionColumn( dataColumn))
                {
                    continue;
                }


                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}

                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + dataColumn.ColumnName + "\",_" + TableName +
                                   "." + dataColumn.ColumnName + "));";
                if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                {
                    continue;
                }

                updateCommand += dataColumn.ColumnName + "=@" + dataColumn.ColumnName + ",";
            }
            //Key

            var code = "";
            code += "public void Update() {";
            code += NewLine + "var prset = new List<IDataParameter>();";

            code += NewLine + insertparameter;

            code += NewLine;

            code += "var sql = @\"UPDATE   " + TableName + " SET  " + updateCommand.Trim(',') + " where " +
                    Ds.Tables[0].PrimaryKey[0] + " = @" + Ds.Tables[0].PrimaryKey[0] + "\";" + NewLine;

            code += NewLine;

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

            code += "private List<" + TableName + "> DataSetToList(DataSet ds) \r\n";
            code += "{\r\n";
            code += " EnumerableRowCollection<" + TableName + "> q = (from temp in ds.Tables[0].AsEnumerable()\r\n";
            code += " select new " + TableName + "\r\n";
            code += "{\r\n";

            //Inherrit
            code += "RecordCount = temp.Field<Int32>(\"RecordCount\"),";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += dataColumn.ColumnName + "= Convert.ToDecimal (temp.Field<Object>(\"" + dataColumn.ColumnName +
                            "\")), \r\n ";
                }
                else if (dataColumn.DataType.ToString() == "System.String")
                {
                    //  TermUpdateDate = temp.Field<DateTime?>("TermUpdateDate"),
                    code += dataColumn.ColumnName + "= temp.Field<" + dataColumn.DataType.Name + ">(\"" +
                          dataColumn.ColumnName + "\"), \r\n ";
                }
                else
                {
                    code += dataColumn.ColumnName + "= temp.Field<" + dataColumn.DataType.Name + "?>(\"" +
                            dataColumn.ColumnName + "\"), \r\n ";
                }

                //if (dataColumn.DataType.ToString() == "System.Decimal")
                //{
                //    code += dataColumn.ColumnName + "= Convert.ToDecimal (temp.Field<Object>(\"" + dataColumn.ColumnName +
                //            "\")), \r\n ";
                //}
                //else if (dataColumn.DataType.ToString() == "System.DateTime")
                //{
                //    //  TermUpdateDate = temp.Field<DateTime?>("TermUpdateDate"),
                //    code += dataColumn.ColumnName + "= temp.Field<DateTime?>(\"" + dataColumn.ColumnName + "\"), \r\n ";
                //}
                //else
                //{
                //    code += dataColumn.ColumnName + "= temp.Field<" + dataColumn.DataType.Name + ">(\"" +
                //            dataColumn.ColumnName + "\"), \r\n ";
                //}
            }

            code += " });\r\n";

            code += "  return q.ToList();\r\n";
            code += "}\r\n";

            return code;
        }

        //public const string DataText = "COMPANY";
        //public const string DataValue = "CUSTNO";

        private string GenConStance()
        {
            var code = "";
            code += "  public " + TableName + " _" + TableName + ";" + NewLine;

            code += "public const string DataKey = \"" + Ds.Tables[0].PrimaryKey[0].ColumnName + "\";" + NewLine;
            code += "public const string DataText = \"" + Ds.Tables[0].Columns[1].ColumnName + "\";" + NewLine;
            code += "public const string DataValue = \"" + Ds.Tables[0].PrimaryKey[0].ColumnName + "\";" + NewLine;
            return code;
        }

        private string Comment()
        {
            var code = NewLine + "//Trasaction User" + NewLine;
            code += "//bool output = false;" + NewLine;
            code += "//    try" + NewLine;
            code += "//    {" + NewLine;
            code += "//        Db.OpenFbData();" + NewLine;
            code += "//        Db.BeginTransaction();" + NewLine;

            code += "//        MPO_ORDERS o1 = new MPO_ORDERS();" + NewLine;
            code += "//o1 = _MPO_ORDERS;" + NewLine;
            code += "//        int orid = o1.Save();" + NewLine;

            code += "//MPO_ODERDETAILS o2 = new MPO_ODERDETAILS();" + NewLine;
            code += "//o2.Save(orid, ODERDETAILS);" + NewLine;

            code += "//        Db.CommitTransaction();" + NewLine;
            code += "//        OR_ID = orid;" + NewLine;
            code += "//        output = true;" + NewLine;
            code += "//    }" + NewLine;
            code += "//    catch (System.Exception ex)" + NewLine;
            code += "//    {" + NewLine;
            code += "//        Db.RollBackTransaction();" + NewLine;
            code += "//        ErrorLogging.LogErrorToLogFile(ex, \"\");" + NewLine;
            code += "//        throw ex;" + NewLine;
            code += "//    }" + NewLine;

            code += "//    return output;" + NewLine;

            return code;
        }

        private string GenUpdateColumn()
        {
            var code = "";
            //id, column, value
            code += "   public Boolean UpdateColumn(string id, string column,string value) " + NewLine;
            code += "        { " + NewLine;
            code += "            var prset = new List<IDataParameter>(); " + NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@" + Ds.Tables[0].PrimaryKey[0].ColumnName +
                    "\", id)); " + NewLine;
            code += "prset.Add(Db.CreateParameterDb(\"@Column\", column));" + NewLine;
            code += "            prset.Add(Db.CreateParameterDb(\"@Data\", value)); " + NewLine;
            // code += "             var sql = @\"UPDATE   " + _TableName + " SET  [\"+column+ \"]=@Data where " + _ds.Tables[0].PrimaryKey[0].ColumnName + " = @" + _ds.Tables[0].PrimaryKey[0].ColumnName + "\"; " + _NewLine;
            code += $"   var sql = @\"Sp_Get{TableName}_UpdateColumn\";" + NewLine;
            code += "            int output = Db.FbExecuteNonQuery(sql, prset, CommandType.StoredProcedure); " + NewLine;
            code += "            if (output == 1) " + NewLine;
            code += "            { " + NewLine;
            code += "                return true; " + NewLine;
            code += "            } " + NewLine;
            code += " " + NewLine;
            code += "            return false;   " + NewLine;
            code += "        } " + NewLine;

            return code;
        }

        private string GenAutoCompleteMethod()
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
                //if (isfirst == true)
                //{
                //    code += "sql += string.Format(\" where ((''='{0}')or(" + _DataColumn.ColumnName + "='{0}'))\", _" + _TableName + "." + _DataColumn.ColumnName + ");";
                //    code += _NewLine;
                //    isfirst = false;
                //}
                //else
                //{
                //    code += "sql += string.Format(\"  and ((''='{0}')or(" + _DataColumn.ColumnName + "='{0}'))\", _" + _TableName + "." + _DataColumn.ColumnName + ");";
                //    code += _NewLine;
                //}

                code += "            if ( _" + TableName + "." + dataColumn.ColumnName + "!= null) " + NewLine;
                code += "            { " + NewLine;
                code += "                sql += string.Format(\" AND ((''='{0}') or (" + dataColumn.ColumnName +
                        "='{0}') )\", _" + TableName + "." + dataColumn.ColumnName + "); " + NewLine;
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
            code += " public string _SortDirection { get; set; }" + NewLine;
            code += " public string _SortExpression { get; set; }" + NewLine;
            return code;
        }

        private string GenGetParameters()
        {
            var code = "";

            code += "      public List<IDataParameter> GetParameter(int pageIndex, int PageSize)" + NewLine;
            code += "        {" + NewLine;
            code += "            var sqlStorePamameters = new List<IDataParameter>();" + NewLine;
            code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@PageIndex\", pageIndex));" + NewLine;
            code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@PageSize\", PageSize));" + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                var propertieName = string.Format(ControlName.FormatpropertieName, TableName, dataColumn.ColumnName);
                code += $"if ({propertieName} != null)" + NewLine;
                code += "            {" + NewLine;
                code +=
                    $"sqlStorePamameters.Add(Db.CreateParameterDb(\"@{dataColumn.ColumnName}\", {propertieName}));" +
                    NewLine;
                code += "" + NewLine;
                code += "            }" + NewLine;
            }
            code += "/*Sort Order*/" + NewLine;

            code += "  if (_SortExpression != null)" + NewLine;
            code += "        {" + NewLine;
            code += "           " + NewLine;
            code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@SortColumn\", _SortExpression));" +
                    NewLine;
            code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@SortOrder\", _SortDirection));" +
                    NewLine;
            code += "        }" + NewLine;

            //code += "        bool sortAscending = _SortDirection == SortDirection.Ascending;" + _NewLine;
            //code += "        if (_SortExpression != null)" + _NewLine;
            //code += "        {" + _NewLine;
            //code += "            string SortOrder = (sortAscending ? \"ASC\" : \"DESC\");" + _NewLine;
            //code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@SortColumn\", _SortExpression));" + _NewLine;
            //code += "            sqlStorePamameters.Add(Db.CreateParameterDb(\"@SortOrder\", SortOrder));" + _NewLine;
            //code += "        }" + _NewLine;

            code += "" + NewLine;
            code += "" + NewLine;
            code += "            return sqlStorePamameters;" + NewLine;
            code += "        }" + NewLine;

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

            code += GenAutoCompleteMethod();
            code += GenGetKeyWordsOneColumn();
            //code += GenSql();
            code += GenWhereformProperties();
            code += GenGetParameters();
            code += GenEndNameSpaceAndClass();

            code += Comment();

            //FileName name = new FileName();
            //name._TableName = _TableName;
            //name._ds = _ds;
            InnitProperties();

            FileCode.WriteFile(FileName.DbCodeName(), code);
            //_FileCode.writeFile(_TableName + "Db", _code, _fileType);
        }
    }
}