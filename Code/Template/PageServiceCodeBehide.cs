using System.Data;

namespace StkGenCode.Code.Template
{
    public class PageServiceCodeBehide : CodeBase
    {
        private string GenUsign()
        {
            var code = "";
            code += "using System; " + NewLine;
            code += "using System.Collections.Generic; " + NewLine;
            code += "using System.Linq; " + NewLine;
            code += "using System.Web; " + NewLine;
            code += "using System.Web.Services; " + NewLine;
            code += "using WebApp.Business;" + NewLine;
            code += "using StkLib.Common;" + NewLine;

            code += "using WebApp.Code.Utility.Properties.Controls;" + NewLine;
            return code;
        }

        public string GenHeadFile()
        {
            var code = "";
            code += "    /// <summary> " + NewLine;
            code += "    /// Summary description for AutoCompleteService " + NewLine;
            code += "    /// </summary> " + NewLine;
            code += "    [WebService(Namespace = \"http://tempuri.org/\")] " + NewLine;
            code += "    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)] " + NewLine;
            code += "    [System.ComponentModel.ToolboxItem(false)] " + NewLine;
            code +=
                "    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.  " +
                NewLine;
            code += "     [System.Web.Script.Services.ScriptService] " + NewLine;
            code += " " + NewLine;
            return code;
        }

        //

        private string GenBeginNameSpace()
        {
            var code = "";
            code += "namespace WebApp.Services  " + NewLine;
            code += "{" + NewLine;
            return code;
        }

        private string GenBeginClass()
        {
            var code = "";
            code += "public class " + TableName + "Service : System.Web.Services.WebService " + NewLine;
            code += "{" + NewLine;
            return code;
        }

        private string GenEndClass()
        {
            var code = "";
            code += "}"; //Class

            return code;
        }

        private string GenEndNameSpace()
        {
            var code = "";
            code += "}"; //Class

            return code;
        }

        private string GenVersionMethod()
        {
            var code = "";
            code += " " + NewLine;
            code += "        [WebMethod] " + NewLine;
            code += "        public string Service()" + NewLine;
            code += "        { " + NewLine;
            code += "            return \"1.0.0.1\";" + NewLine;
            code += "        } " + NewLine;
            return code;
        }

        private string GenSaveColumn()
        {
            var code = "";

            code +=
                " //For Jquery  ---------------------------------------------------------------------------------------------- " +
                NewLine;
            code += "        [WebMethod] " + NewLine;
            code += "        public   Boolean SaveColumn(string id, string column, string value) " + NewLine;
            code += "        { " + NewLine;
            code += "            " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += " " + NewLine;

            code += " " + NewLine;
            code += "            bool isUpdate = _" + TableName + "Db.UpdateColumn(id, column, value); " + NewLine;
            code += "            return isUpdate; " + NewLine;
            code += "        }" + NewLine;

            return code;
        }

        private string GenGetKeyWordsAllColumn()
        {
            var code = "";
            code += "[WebMethod] " + NewLine;
            code += "       public List<string> GetKeyWordsAllColumn(string keyword) " + NewLine;
            code += "       { " + NewLine;
            code += "           " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += "           List<string> keywords = _" + TableName + "Db.GetKeyWordsAllColumn(keyword); " + NewLine;
            code += "           return keywords; " + NewLine;
            code += "       }" + NewLine;
            code += "" + NewLine;

            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            var code = "";
            code += "[WebMethod] " + NewLine;
            code += "       public List<string> GetKeyWordsOneColumn(string column, string keyword) " + NewLine;
            code += "       { " + NewLine;
            code += "           " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += "           List<string> keywords = _" + TableName + "Db.GetKeyWordsOneColumn(column,keyword); " +
                    NewLine;
            code += "           return keywords; " + NewLine;
            code += "       }" + NewLine;
            code += "" + NewLine;

            return code;
        }

        public string GenSearch()
        {
            var pararmeter = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = "string PageIndex,string PageSize,string SortExpression,string SortDirection," +
                         pararmeter.Trim(',');
            var code = "";
            code += "" + NewLine;
            code += "    [WebMethod]" + NewLine;

            code += $"public List<{TableName}> Search({pararmeter})" + NewLine;

            code += "    {" + NewLine;

            code += " " + TableName + " _" + TableName + " = new " + TableName + "(); " + NewLine;
            code += "  " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += MapJsonToProPerties(Ds,false,false);

            code += "  _" + TableName + "Db._" + TableName + " = _" + TableName + ";" + NewLine;
            code += "int _PageIndex = Convert.ToInt32(PageIndex);" + NewLine;
            code += "int _PageSize = Convert.ToInt32(PageSize);" + NewLine;
            code += "" + NewLine;

            code += " if (SortExpression.Trim() != \"\")" + NewLine;
            code += "        {" + NewLine;
            code += $"            _{TableName}Db._SortDirection = SortDirection;" + NewLine;
            code += "" + NewLine;
            code += $"            _{TableName}Db._SortExpression = SortExpression;" + NewLine;
            code += "        }" + NewLine;

            code += "return _" + TableName + "Db.GetPageWise(_PageIndex, _PageSize);" + NewLine;

            code += "   }" + NewLine;

            return code;
        }

        public string GenSave()
        {
            var pararmeter = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)

            {
                if (IsExceptionColumn(dataColumn,true))
                {
                    continue;
                }
                //{  if (ExceptionType.Contains(dataColumn.DataType.ToString()) || ExceptionColumn.Contains(dataColumn.ColumnName))
                //    {
                //        continue;
                //    }
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            var code = "";
            code += "" + NewLine;
            code += "    [WebMethod]" + NewLine;

            code += $"public string Save({pararmeter})" + NewLine;

            code += "    {" + NewLine;

            code += " " + TableName + " _" + TableName + " = new " + TableName + "(); " + NewLine;
            code += "  " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += MapJsonToProPerties(Ds, true,true);

            code += "  _" + TableName + "Db._" + TableName + " = _" + TableName + ";" + NewLine;
            code += "  object result= _" + TableName + "Db.Insert(); " + NewLine;
            code += "   return result.ToString();" + NewLine;

            code += "   }" + NewLine;

            return code;
        }

        //code += "" + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
        //    code += "" + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
        //    code += MapControlToProPerties(_ds);

        //code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
        //    code += "  _" + _TableName + "Db.Update(); " + _NewLine;
        public string GenUpdate()
        {
            var pararmeter = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}

                if (IsExceptionColumn(dataColumn,true))
                {
                    continue;
                }

                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            var code = "";
            code += "" + NewLine;
            code += "    [WebMethod]" + NewLine;

            code += $"public string Update({pararmeter})" + NewLine;

            code += "    {" + NewLine;

            code += " " + TableName + " _" + TableName + " = new " + TableName + "(); " + NewLine;
            code += "  " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += MapJsonToProPerties(Ds,false,true);

            code += "  _" + TableName + "Db._" + TableName + " = _" + TableName + ";" + NewLine;
            code += "    _" + TableName + "Db.Update(); " + NewLine;
            code += "   return \"\";" + NewLine;

            code += "   }" + NewLine;

            return code;
        }

        private string GenDelete()
        {
            var pararmeter = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }
                //Parameter เป็น String หมด
                pararmeter += $"string {dataColumn.ColumnName},";
            }
            pararmeter = pararmeter.Trim(',');
            var code = "";
            code += "" + NewLine;
            code += "    [WebMethod]" + NewLine;

            code += $"public string Delete({pararmeter})" + NewLine;

            code += "    {" + NewLine;

            code += " " + TableName + " _" + TableName + " = new " + TableName + "(); " + NewLine;
            code += "  " + TableName + "Db _" + TableName + "Db = new " + TableName + "Db(); " + NewLine;

            code += MapJsonToProPerties(Ds,false,true);

            code += "  _" + TableName + "Db._" + TableName + " = _" + TableName + ";" + NewLine;
            code += "    _" + TableName + "Db.Delete(); " + NewLine;
            code += "   return \"\";" + NewLine;

            code += "   }" + NewLine;

            return code;
        }

        public string SelectAll()
        {
            var code = "";

            code += "  [WebMethod]" + NewLine;
            code += "    public List<SelectInputProperties> SelectAll()" + NewLine;
            code += "    {" + NewLine;
            code += $"        {TableName}Db _{TableName}Db = new {TableName}Db();" + NewLine;
            code += $"        return _{TableName}Db.Select();" + NewLine;
            code += "    }" + NewLine;

            return code;
        }

        public string Select()
        {
            var code = " [WebMethod] " + NewLine;
            code += $"   public {TableName} Select(string {Ds.Tables[0].PrimaryKey[0]})" + NewLine;
            code += "    {" + NewLine;
            code += $"        {TableName}Db _{TableName}Db = new {TableName}Db();" + NewLine;
            code += $"        return _{TableName}Db.Select({Ds.Tables[0].PrimaryKey[0]});" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public override void Gen()
        {
            //_FileName = new FileName();
            //_FileName._TableName = _TableName;
            //_FileName._ds = _ds;
            InnitProperties();

            var code = "";
            code += GenUsign();

            code += GenBeginNameSpace();
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
            code += GenEndNameSpace(); //Close tag
            FileCode.WriteFile(FileName.PageServiceCodeBehideName(), code);
        }
    }
}