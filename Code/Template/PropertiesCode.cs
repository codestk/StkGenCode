using System.Data;

namespace StkGenCode.Code.Template
{
    public class PropertiesCode : CodeBase
    {
       

        private string GenUsign()
        {
            var code = "";
            code += "using System;" + NewLine;
            code += "using WebApp.Code.Utility;" + NewLine;
            //_code += "using System.Collections.Generic;" + _NewLine;
            //_code += "using System.Data;" + _NewLine;
            //_code += "using System.Linq;" + _NewLine;
            //_code += "using System.Web;" + _NewLine;
            code += "using FluentValidation;" + NewLine;
            code += "using WebApp.Business;" + NewLine;
            return code;
        }

        private string GenBeginNameSpaceAndClass()
        {
            
   


        var code = "";
            code = "namespace WebApp.Business" + NewLine;
            code += "{" + NewLine;
            code += "public class  " + TableName + NewLine + " : BaseProperties";
            code += "{" + NewLine;

            return code;
        }

        private string GenEndNameSpaceAndClass()
        {
            var code = "} }" + NewLine;
            //_code += " }"; //End Name Space

            return code;
        }

        public string GenProperties()
        {
            var code = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                var nullType = dataColumn.DataType.ToString() == "System.String" ? "" : "?";

                if (dataColumn.DataType.ToString() == "System.Byte[]")
                {
                    nullType = "";
                }

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
            FileCode.WriteFile(FileName.PropertiesCodeName(), code);
            // _FileCode.writeFile(_TableName, _code, _fileType);
        }
    }
}