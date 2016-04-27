using System.Data;

namespace StkGenCode.Code.Template
{
    public class PropertiesCode:CodeBase
    {
        //public FileCode _FileCode;
        //public DataSet _ds;
        //public string _TableName;

      
        //private string _NewLine = " \r\n";

        private string GenUsign()
        {
            string _code = "";
            _code += "using System;" + _NewLine;
            _code += "using System.Collections.Generic;" + _NewLine;
            _code += "using System.Data;" + _NewLine;
            _code += "using System.Linq;" + _NewLine;
            _code += "using System.Web;" + _NewLine;
       

            return _code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            string _code = "";
            //_code = "namespace XXXXX.Code.Bu" + _NewLine;
            //_code += "{" + _NewLine;
            _code += "public class  " + _TableName + _NewLine + " : BaseProperties";
            _code += "{" + _NewLine;

            return _code;
        }

        private string GenEndNameSpaceAndClass()
        {
            string _code = "" + _NewLine;
            _code = "}" + _NewLine;
            //_code += " }"; //End Name Space

            return _code;
        }

        public string GenProperties()
        {
            string _code = "";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                _code += _DataColumn.DataType.Name + " _" + _DataColumn.ColumnName + "; \r\n";
                _code += "public " + _DataColumn.DataType.Name + " " + _DataColumn.ColumnName + " { get { return _" + _DataColumn.ColumnName + "; } set { _" + _DataColumn.ColumnName + " = value; } } \r\n \r\n";
            }

            return _code;
 
        }

        public override void Gen()
        {
            string _code = "";
            _code += GenUsign();
            _code += GenBeginNameSpaceAndClass();
            _code += GenProperties();
            _code += GenEndNameSpaceAndClass();

            FileName name = new FileName();
            name._TableName = _TableName;
            name._ds = _ds;
            _FileCode.writeFile(name.PropertiesCodeName(), _code);
           // _FileCode.writeFile(_TableName, _code, _fileType);
        }
    }
}