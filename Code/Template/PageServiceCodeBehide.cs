using System.Data;

namespace StkGenCode.Code.Template
{
    public class PageServiceCodeBehide : CodeBase
    {
        private string GenUsign()
        {
            string code = "";
            code += "using System; " + _NewLine;
            code += "using System.Collections.Generic; " + _NewLine;
            code += "using System.Linq; " + _NewLine;
            code += "using System.Web; " + _NewLine;
            code += "using System.Web.Services; " + _NewLine;

            return code;
        }

        public string GenHeadFile()
        {
            string code = "";
            code += "    /// <summary> " + _NewLine;
            code += "    /// Summary description for AutoCompleteService " + _NewLine;
            code += "    /// </summary> " + _NewLine;
            code += "    [WebService(Namespace = \"http://tempuri.org/\")] " + _NewLine;
            code += "    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)] " + _NewLine;
            code += "    [System.ComponentModel.ToolboxItem(false)] " + _NewLine;
            code += "    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.  " + _NewLine;
            code += "     [System.Web.Script.Services.ScriptService] " + _NewLine;
            code += " " + _NewLine;
            return code;
        }

        private string GenBeginClass()
        {
            string code = "";
            code += "public class " + _TableName + "Service : System.Web.Services.WebService " + _NewLine;
            code += "{" + _NewLine;
            return code;
        }

        private string GenEndClass()
        {
            string code = "";
            code += "}";
            return code;
        }

        private string GenVersionMethod()
        {
            string code = "";
            code += " " + _NewLine;
            code += "        [WebMethod] " + _NewLine;
            code += "        public string Service()" + _NewLine;
            code += "        { " + _NewLine;
            code += "            return \"1.0.0.1\";" + _NewLine;
            code += "        } " + _NewLine;
            return code;
        }

        private string GenSaveColumn()
        {
            string code = "";

            code += " //For Jquery  ---------------------------------------------------------------------------------------------- " + _NewLine;
            code += "        [WebMethod] " + _NewLine;
            code += "        public   Boolean SaveColumn(string id, string column, string value) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += " " + _NewLine;

            code += " " + _NewLine;
            code += "            bool isUpdate = _" + _TableName + "Db.UpdateColumn(id, column, value); " + _NewLine;
            code += "            return isUpdate; " + _NewLine;
            code += "        }" + _NewLine;

            return code;
        }

        private string GenGetKeyWordsAllColumn()
        {
            string code = "";
            code += "[WebMethod] " + _NewLine;
            code += "       public List<string> GetKeyWordsAllColumn(string keyword) " + _NewLine;
            code += "       { " + _NewLine;
            code += "           " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += "           List<string> keywords = _" + _TableName + "Db.GetKeyWordsAllColumn(keyword); " + _NewLine;
            code += "           return keywords; " + _NewLine;
            code += "       }" + _NewLine;
            code += "" + _NewLine;

            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            string code = "";
            code += "[WebMethod] " + _NewLine;
            code += "       public List<string> GetKeyWordsOneColumn(string column, string keyword) " + _NewLine;
            code += "       { " + _NewLine;
            code += "           " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += "           List<string> keywords = _" + _TableName + "Db.GetKeyWordsOneColumn(column,keyword); " + _NewLine;
            code += "           return keywords; " + _NewLine;
            code += "       }" + _NewLine;
            code += "" + _NewLine;

            return code;
        }

        public string GenSearch()
        {
            string pararmeter = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = "string PageIndex,string PageSize,string SortExpression,string SortDirection," + pararmeter.Trim(',');
            string code = "";
            code += "" + _NewLine;
            code += "    [WebMethod]" + _NewLine;

            code += $"public List<{_TableName}> Search({pararmeter})" + _NewLine;

            code += "    {" + _NewLine;

            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapJsonToProPerties(_ds);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "int _PageIndex = Convert.ToInt32(PageIndex);" + _NewLine;
            code += "int _PageSize = Convert.ToInt32(PageSize);" + _NewLine;
            code += "" + _NewLine;

            code += " if (SortExpression.Trim() != \"\")" + _NewLine;
            code += "        {" + _NewLine;
            code += $"            _{_TableName}Db._SortDirection = SortDirection;" + _NewLine;
            code += "" + _NewLine;
            code += $"            _{_TableName}Db._SortExpression = SortExpression;" + _NewLine;
            code += "        }" + _NewLine;

            code += "return _" + _TableName + "Db.GetPageWise(_PageIndex, _PageSize);" + _NewLine;

            code += "   }" + _NewLine;

            return code;
        }

        public string GenSave()
        {
            string pararmeter = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            string code = "";
            code += "" + _NewLine;
            code += "    [WebMethod]" + _NewLine;

            code += $"public string Save({pararmeter})" + _NewLine;

            code += "    {" + _NewLine;

            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapJsonToProPerties(_ds, true);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "  object result= _" + _TableName + "Db.Insert(); " + _NewLine;
            code += "   return result.ToString();" + _NewLine;
            //  txtid.Text = _modulesDb.Insert().ToString();

            //code += "txt" + _ds.Tables[0].PrimaryKey[0] + ".Text= _" + _TableName + "Db.Insert().ToString();; " + _NewLine;
            //code += controlTextBoxName + ".Text= _" + _TableName + "Db.Insert().ToString(); " + _NewLine;

            //code += "        TradeFromTerm _TradeFromTerm = new TradeFromTerm();" + _NewLine;
            //code += "        TradeFromTermDb _TradeFromTermDb = new TradeFromTermDb();" + _NewLine;
            //code += "        _TradeFromTerm.TermId = Convert.ToDecimal(txtTermId.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermName = txtTermName.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermContent = txtTermContent.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermCreateDate = StkGlobalDate.TextEnToDate(txtTermCreateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateDate = StkGlobalDate.TextEnToDate(txtTermUpdateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateCount = Convert.ToDecimal(txtTermUpdateCount.Text);" + _NewLine;
            //code += "       _TradeFromTermDb._TradeFromTerm = _TradeFromTerm;" + _NewLine;
            //code += "       _TradeFromTermDb.Update();" + _NewLine;

            code += "   }" + _NewLine;

            return code;
        }

        //code += "" + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
        //    code += "" + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
        //    code += MapControlToProPerties(_ds);

        //code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
        //    code += "  _" + _TableName + "Db.Update(); " + _NewLine;
        public string GenUpdate()
        {
            string pararmeter = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            string code = "";
            code += "" + _NewLine;
            code += "    [WebMethod]" + _NewLine;

            code += $"public string Update({pararmeter})" + _NewLine;

            code += "    {" + _NewLine;

            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapJsonToProPerties(_ds);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "    _" + _TableName + "Db.Update(); " + _NewLine;
            code += "   return \"\";" + _NewLine;
            //  txtid.Text = _modulesDb.Insert().ToString();

            //code += "txt" + _ds.Tables[0].PrimaryKey[0] + ".Text= _" + _TableName + "Db.Insert().ToString();; " + _NewLine;
            //code += controlTextBoxName + ".Text= _" + _TableName + "Db.Insert().ToString(); " + _NewLine;

            //code += "        TradeFromTerm _TradeFromTerm = new TradeFromTerm();" + _NewLine;
            //code += "        TradeFromTermDb _TradeFromTermDb = new TradeFromTermDb();" + _NewLine;
            //code += "        _TradeFromTerm.TermId = Convert.ToDecimal(txtTermId.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermName = txtTermName.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermContent = txtTermContent.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermCreateDate = StkGlobalDate.TextEnToDate(txtTermCreateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateDate = StkGlobalDate.TextEnToDate(txtTermUpdateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateCount = Convert.ToDecimal(txtTermUpdateCount.Text);" + _NewLine;
            //code += "       _TradeFromTermDb._TradeFromTerm = _TradeFromTerm;" + _NewLine;
            //code += "       _TradeFromTermDb.Update();" + _NewLine;

            code += "   }" + _NewLine;

            return code;
        }

        private string GenDelete()
        {
            string pararmeter = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            string code = "";
            code += "" + _NewLine;
            code += "    [WebMethod]" + _NewLine;

            code += $"public string Delete({pararmeter})" + _NewLine;

            code += "    {" + _NewLine;

            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapJsonToProPerties(_ds);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "    _" + _TableName + "Db.Delete(); " + _NewLine;
            code += "   return \"\";" + _NewLine;
            //  txtid.Text = _modulesDb.Insert().ToString();

            //code += "txt" + _ds.Tables[0].PrimaryKey[0] + ".Text= _" + _TableName + "Db.Insert().ToString();; " + _NewLine;
            //code += controlTextBoxName + ".Text= _" + _TableName + "Db.Insert().ToString(); " + _NewLine;

            //code += "        TradeFromTerm _TradeFromTerm = new TradeFromTerm();" + _NewLine;
            //code += "        TradeFromTermDb _TradeFromTermDb = new TradeFromTermDb();" + _NewLine;
            //code += "        _TradeFromTerm.TermId = Convert.ToDecimal(txtTermId.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermName = txtTermName.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermContent = txtTermContent.Text;" + _NewLine;
            //code += "        _TradeFromTerm.TermCreateDate = StkGlobalDate.TextEnToDate(txtTermCreateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateDate = StkGlobalDate.TextEnToDate(txtTermUpdateDate.Text);" + _NewLine;
            //code += "        _TradeFromTerm.TermUpdateCount = Convert.ToDecimal(txtTermUpdateCount.Text);" + _NewLine;
            //code += "       _TradeFromTermDb._TradeFromTerm = _TradeFromTerm;" + _NewLine;
            //code += "       _TradeFromTermDb.Update();" + _NewLine;

            code += "   }" + _NewLine;

            return code;
        }

        public string SelectAll()
        {
            string code = "";

            code += "  [WebMethod]" + _NewLine;
            code += "    public List<SelectInputProperties> SelectAll()" + _NewLine;
            code += "    {" + _NewLine;
            code += $"        {_TableName}Db _{_TableName}Db = new {_TableName}Db();" + _NewLine;
            code += $"        return _{_TableName}Db.Select();" + _NewLine;
            code += "    }" + _NewLine;

            return code;
        }

        public string Select()
        {
            string code = " [WebMethod] " + _NewLine;
            code += $"   public {_TableName} Select(string {_ds.Tables[0].PrimaryKey[0]})" + _NewLine;
            code += "    {" + _NewLine;
            code += $"        {_TableName}Db _{_TableName}Db = new {_TableName}Db();" + _NewLine;
            code += $"        return _{_TableName}Db.Select({_ds.Tables[0].PrimaryKey[0]});" + _NewLine;
            code += "    }" + _NewLine;
            return code;
        }

        public override void Gen()
        {
            //_FileName = new FileName();
            //_FileName._TableName = _TableName;
            //_FileName._ds = _ds;
            InnitProperties();

            string code = "";
            code += GenUsign();
            code += GenHeadFile();
            code += GenBeginClass();
            code += GenVersionMethod();

            code += GenSaveColumn();
            code += GenGetKeyWordsAllColumn();
            code += GenGetKeyWordsOneColumn();
            code += GenSearch();
            code += GenSave();
            code += GenUpdate();
            code += GenDelete();

            code += SelectAll();
            code += Select();
            code += GenEndClass(); //Close tag

            _FileCode.WriteFile(_FileName.PageServiceCodeBehideName(), code);
        }
    }
}