using System.Data;

namespace StkGenCode.Code.Template
{
    public class PropertiesCode : CodeBase
    {
        //public FileCode _FileCode;
        //public DataSet _ds;
        //public string _TableName;

        //private string _NewLine = " \r\n";

        private string GenUsign()
        {
            var code = "";
            code += "using System;" + _NewLine;
            //_code += "using System.Collections.Generic;" + _NewLine;
            //_code += "using System.Data;" + _NewLine;
            //_code += "using System.Linq;" + _NewLine;
            //_code += "using System.Web;" + _NewLine;

            return code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            var code = "";
            //_code = "namespace XXXXX.Code.Bu" + _NewLine;
            //_code += "{" + _NewLine;
            code += "public class  " + _TableName + _NewLine + " : BaseProperties";
            code += "{" + _NewLine;

            return code;
        }

        private string GenEndNameSpaceAndClass()
        {
            var code = "}" + _NewLine;
            //_code += " }"; //End Name Space

            return code;
        }

        public string GenProperties()
        {
            var code = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                var nullType = dataColumn.DataType.ToString() == "System.String" ? "" : "?";
                code += dataColumn.DataType.Name + nullType + " _" + dataColumn.ColumnName + "; \r\n";
                code += "public " + dataColumn.DataType.Name + nullType + " " + dataColumn.ColumnName +
                         " { get { return _" + dataColumn.ColumnName + "; } set { _" + dataColumn.ColumnName +
                         " = value; } } \r\n \r\n";
            }

            return code;
        }

        public override void Gen()
        {
            var code = "";
            code += GenUsign();
            code += GenBeginNameSpaceAndClass();
            code += GenProperties();
            code += GenEndNameSpaceAndClass();

            //var name = new FileName();
            //name._TableName = _TableName;
            //name._ds = _ds;
            InnitProperties();
            _FileCode.writeFile(_FileName.PropertiesCodeName(), code);
            // _FileCode.writeFile(_TableName, _code, _fileType);
        }
    }
}