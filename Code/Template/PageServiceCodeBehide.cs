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

            code += GenEndClass();

            _FileCode.writeFile(_FileName.PageServiceCodeBehideName(), code);
        }
    }
}