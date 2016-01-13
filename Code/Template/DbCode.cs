using System.Data;

namespace StkGenCode.Code.Template
{
    public class DbCode
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;

        private string _fileType = ".cs";
        private string _NewLine = " \r\n";

        private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string GenUsign()
        {
            string _code = "";
            _code = "using System;" + _NewLine;
            _code += "using System.Collections.Generic;" + _NewLine;
            _code += "using System.Data;" + _NewLine;
            _code += "using System.Linq;" + _NewLine;
            _code += "using System.Web;" + _NewLine;
            _code += "using ExchangeService.Code;" + _NewLine;
            // _FileCode.writeFile(_ds.Tables[0].TableName, _code, _fileType);

            return _code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            string _code = "";
            _code = "namespace XXXXX.Code.Bu" + _NewLine;
            _code += "{" + _NewLine;
            _code += " public class  " + _TableName + "Db: DataAccess" + _NewLine;
            _code += "{" + _NewLine;

            return _code;
        }

        private string GenEndNameSpaceAndClass()
        {
            string _code = "" + _NewLine;
            _code = "}" + _NewLine;
            _code += " }";

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
            sql += "Db.SetSort(sortAscending, sortExpression);";
            sql += "}" + _NewLine;

            sql += _NewLine;
            sql += "DataSet ds = Db.GetDataSet(sql);";
            sql += "return DataSetToList(ds);";
            sql += "}" + _NewLine;
            return sql;
        }

        //public List<AmpBangkok> GetPageWise(int pageIndex, int PageSize)
        //{
        //    string _sql1 = \"Sp_GetCustomersPageWise\";
        //    var prset = new List<IDataParameter>();
        //    prset.Add(Db.CreateParameterDb(\"@PageIndex\", pageIndex));
        //    prset.Add(Db.CreateParameterDb(\"@PageSize\", PageSize));
        //    DataSet ds = Db.GetDataSet(_sql1, prset, CommandType.StoredProcedure);
        //    return DataSetToList(ds);
        //}

        private string GenGetPageWise()
        {
            string command = "";
            command += "public List<" + _TableName + "> GetPageWise(int pageIndex, int PageSize)" + _NewLine;
            command += "{" + _NewLine;
            command += " string _sql1 = \"Sp_Get" + _TableName + "PageWise\";" + _NewLine;
            command += "  var prset = new List<IDataParameter>();" + _NewLine;
            command += " prset.Add(Db.CreateParameterDb(\"@PageIndex\", pageIndex));" + _NewLine;
            command += " prset.Add(Db.CreateParameterDb(\"@PageSize\", PageSize));" + _NewLine;
            command += " DataSet ds = Db.GetDataSet(_sql1, prset, CommandType.StoredProcedure);" + _NewLine;
            command += "  return DataSetToList(ds);" + _NewLine;
            command += "}" + _NewLine;
            return command;
        }

        private string GenInsert()
        {
            string insercolumn = "";
            string inservalue = "";
            string insertparameter = "";

            // string updateCommand = "";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //              insercolumn += _DataColumn.ColumnName + "," ;
                //              inservalue += "?,";
                //insertparameter += "new FbParameter(\":"+ _DataColumn.ColumnName+"\", _obj."+ _DataColumn.ColumnName+"),";

                // updateCommand += _DataColumn.ColumnName + "=?,";
                //=====================================================================
                // New Version
                //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
                insercolumn += _DataColumn.ColumnName + ",";
                inservalue += "@" + _DataColumn.ColumnName + ",";
                insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
            }

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            string _code = "";
            _code += " void Insert() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            _code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            _code += _NewLine + insertparameter;

            _code += _NewLine;

            // _code += "var sql = \"UPDATE   " + _TableName + " SET  " + updateCommand.Trim(',') + " where " + _NewLine; ;
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
            _code += " throw new System.Exception(\"Insert\" + this.ToString());}   }" + _NewLine;

            _code += _NewLine;
            return _code;
        }

        private string GenUpdate()
        {
            //string insercolumn = "";
            //string inservalue = "";
            //string insertparameter = "";

            string updateCommand = "";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //              insercolumn += _DataColumn.ColumnName + "," ;
                //              inservalue += "?,";
                //insertparameter += "new FbParameter(\":"+ _DataColumn.ColumnName+"\", _obj."+ _DataColumn.ColumnName+"),";

                updateCommand += _DataColumn.ColumnName + "=@" + _DataColumn.ColumnName + ",";
                //=====================================================================
                // New Version
                //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
                // insercolumn += _DataColumn.ColumnName + ",";
                //inservalue += "@" + _DataColumn.ColumnName + ",";
                //insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
            }

            //insercolumn = insercolumn.TrimEnd(',');
            //inservalue = inservalue.TrimEnd(',');
            //insertparameter = insertparameter.TrimEnd(',');

            string _code = "";
            _code += " void Update() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            //_code += _NewLine + insertparameter;

            _code += _NewLine;

            _code += "var sql = @\"UPDATE   " + _TableName + " SET  " + updateCommand.Trim(',') + " where " + _ds.Tables[0].Columns[0].ColumnName + " = @" + _ds.Tables[0].Columns[0].ColumnName + "\";" + _NewLine; ;
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

        //void Delete()
        //{
        //    var prset = new List<IDataParameter>();
        //    prset.Add(Db.CreateParameterDb("@AmpBangkokIndex", _AmpBangkok.AmpBangkokIndex)); prset.Add(Db.CreateParameterDb("@AmpText", _AmpBangkok.AmpText)); prset.Add(Db.CreateParameterDb("@ENGAmpText", _AmpBangkok.ENGAmpText)); prset.Add(Db.CreateParameterDb("@ZoneID", _AmpBangkok.ZoneID)); prset.Add(Db.CreateParameterDb("@rowguid", _AmpBangkok.rowguid)); prset.Add(Db.CreateParameterDb("@msrepl_tran_version", _AmpBangkok.msrepl_tran_version));
        //    var sql = @"DELETE FROM AmpBangkok where ";

        //    int output = Db.FbExecuteNonQuery(sql, prset);
        //    if (output != 1)
        //    {
        //        throw new System.Exception("Save" + this.ToString());
        //    }
        //}
        private string GenDelete()
        {
            string insercolumn = "";
            string inservalue = "";
            string insertparameter = "";

            //string updateCommand = "";
            //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            //{
            //    //              insercolumn += _DataColumn.ColumnName + "," ;
            //    //              inservalue += "?,";
            //    //insertparameter += "new FbParameter(\":"+ _DataColumn.ColumnName+"\", _obj."+ _DataColumn.ColumnName+"),";

            //    updateCommand += _DataColumn.ColumnName + "=@" + _DataColumn.ColumnName + ",";
            //    //=====================================================================
            //    // New Version
            //    //   Db.CreateParameterDb(":V1_GOOD_INOUT_ID", _obj.V1_GOOD_INOUT_ID.ToString().Trim())
            //    insercolumn += _DataColumn.ColumnName + ",";
            //    inservalue += "@" + _DataColumn.ColumnName + ",";
            //    insertparameter += " prset.Add(Db.CreateParameterDb(\"@" + _DataColumn.ColumnName + "\",_" + _TableName + "." + _DataColumn.ColumnName + "));";
            //}

            insertparameter = " prset.Add(Db.CreateParameterDb(\"@" + _ds.Tables[0].Columns[0].ColumnName + "\",_" + _TableName + "." + _ds.Tables[0].Columns[0].ColumnName + "));";

            insercolumn = insercolumn.TrimEnd(',');
            inservalue = inservalue.TrimEnd(',');
            insertparameter = insertparameter.TrimEnd(',');

            string _code = "";
            _code += " void Delete() {";
            _code += _NewLine + "var prset = new List<IDataParameter>();";
            // _code += "var sql = \"INSERT INTO " + _TableName + "(" + insercolumn + ")";
            //_code += " VALUES (" + inservalue + ")returning  " + _ds.Tables[0].Columns[0].ColumnName + ";\";";
            //textBox4.Text +=_NewLine + "var prset = new List<FbParameter> { " + insertparameter + "};";
            _code += _NewLine + insertparameter;

            _code += _NewLine;

            _code += "var sql =@\"DELETE FROM " + _TableName + " where " + _ds.Tables[0].Columns[0].ColumnName + "=@" + _ds.Tables[0].Columns[0].ColumnName + "\";" + _NewLine; ;
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
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                _code += _DataColumn.ColumnName + "= temp.Field<" + _DataColumn.DataType.Name + ">(\"" + _DataColumn.ColumnName + "\"), \r\n ";

                // _code += "_obj." + _DataColumn.ColumnName + "= " + _DataColumn.ColumnName + "; \r\n ";
                //  _code += "" + _DataColumn.ColumnName + "= _obj." + _DataColumn.ColumnName + "; \r\n ";
            }

            _code += " });\r\n";

            _code += "  return q.ToList();\r\n";
            _code += "}\r\n";

            return _code;
        }

        //public const string DataText = "COMPANY";
        //public const string DataValue = "CUSTNO";

        private string GenConStance()
        {
            string _code = "";
            _code += "  public " + _TableName + " _" + _TableName + ";";
            _code += "public const string DataText = \"" + _ds.Tables[0].Columns[0].ColumnName + "\";" + _NewLine;
            _code += "public const string DataValue = \"" + _ds.Tables[0].Columns[1].ColumnName + "\";" + _NewLine;
            return _code;
        }

        //bool output = false;
        //    try
        //    {
        //        Db.OpenFbData();
        //        Db.BeginTransaction();

        //        MPO_ORDERS o1 = new MPO_ORDERS();
        //o1 = _MPO_ORDERS;
        //        int orid = o1.Save();

        //MPO_ODERDETAILS o2 = new MPO_ODERDETAILS();
        //o2.Save(orid, ODERDETAILS);

        //        Db.CommitTransaction();
        //        OR_ID = orid;
        //        output = true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Db.RollBackTransaction();
        //        ErrorLogging.LogErrorToLogFile(ex, "");
        //        throw ex;
        //    }

        //    return output;
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

        public void Gen()
        {
            string _code = "";
            _code = GenUsign();

            _code += GenBeginNameSpaceAndClass();
            // _code += GenProperties();
            _code += GenConStance();

            _code += GenGetAll();
            _code += GetWithFilter();

            _code += GenGetPageWise();
            _code += GenInsert();
            _code += GenUpdate();
            _code += GenDelete();
            _code += GenConvertDataList();
            _code += GenEndNameSpaceAndClass();
            _code += Comment();
            _FileCode.writeFile(_TableName + "Db", _code, _fileType);
        }
    }
}