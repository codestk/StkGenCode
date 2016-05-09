namespace StkGenCode.Code.Template
{
    public class JsCode : CodeBase
    {
        private string GenJquerySaveData()
        {
            string code = "";
            FileName name = new FileName();

            code += "var " + _TableName + "Service = {}; " + _NewLine;
            code += "(function () { " + _NewLine;
            code += "    var url = \"" + _FileName.PageServiceName() + "/\"; " + _NewLine;

            code += " " + _NewLine;
            code += "    this.SaveColumn =  function (id, column, value) { " + _NewLine;
            code += "            var result; " + _NewLine;
            code += "            ////data \"{ssss:1,ddddd:1}\" " + _NewLine;
            code += "            var tag = '{id:\"' + id + '\",column:\"' + column + '\",value:\"' + value + '\"}'; " + _NewLine;
            code += "            var F = CallServices(url + \"SaveColumn\", tag, false, function (msg) { " + _NewLine;
            code += "                result = msg.d; " + _NewLine;
            code += "            }); " + _NewLine;
            code += "            return result; " + _NewLine;
            code += "        };//SaveColumn " + _NewLine;
            code += "   " + _NewLine;

            //--------------------------------------------------------------------------------------
            code += "   " + _NewLine;
            code += " this.GetKeyWordsAllColumn = function (keyword) { " + _NewLine;
            code += "        var result; " + _NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + _NewLine;
            code += "        var tag = '{keyword:\"' + keyword + '\"}'; " + _NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsAllColumn\", tag, false, function (msg) { " + _NewLine;
            code += "            result = msg.d; " + _NewLine;
            code += "        }); " + _NewLine;
            code += "        return result; " + _NewLine;
            code += "    };//GetKeyWordsAllColumn  " + _NewLine;
            code += "   " + _NewLine;

            code += "   " + _NewLine;
            code += " this.GetKeyWordsOneColumn = function (column,keyword) { " + _NewLine;
            code += "        var result; " + _NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + _NewLine;

            code +=    " var tag = '{column:\"' + column + '\",keyword:\"' + keyword + '\"}';" + _NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsOneColumn\", tag, false, function (msg) { " + _NewLine;
            code += "            result = msg.d; " + _NewLine;
            code += "        }); " + _NewLine;
            code += "        return result; " + _NewLine;
            code += "    };//GetKeyWordsOneColumn  " + _NewLine;
            code += "   " + _NewLine;

            code += "}).apply(" + _TableName + "Service); " + _NewLine;

            return code;
        }

        public override void Gen()
        {
            _FileName = new FileName();
            _FileName._TableName = _TableName;
            _FileName._ds = _ds;

            string _code = "";
            _code += GenJquerySaveData();
            _FileCode.writeFile(_FileName.JsCodeName(), _code);
            //_FileCode.writeFile(FileName, _code, _fileType);
        }
    }
}