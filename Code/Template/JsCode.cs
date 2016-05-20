using StkGenCode.Code.Column;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class JsCode : CodeBase
    {
        private string GenJquerySaveData()
        {
            string code = "";

            code += "var " + _TableName + "Service = {}; " + _NewLine;
            code += "(function () { " + _NewLine;
            code += "    var url = \"" + _FileName.PageServiceName() + "/\"; " + _NewLine;

            code += GenSaveColumn();

            code += GetKeyWordsAllColumn();

            code += GenGetKeyWordsOneColumn();

            code += GenSearch();

            code += GenSave();

            code += GenUpdate();

            code += GenDelete();

            code += "}).apply(" + _TableName + "Service); " + _NewLine;

            return code;
        }

        private string GenSaveColumn()
        {
            string code = "";
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
            return code;
        }

        private string GetKeyWordsAllColumn()
        {
            string code = "";
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
            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            string code = "";
            code += "   " + _NewLine;
            code += " this.GetKeyWordsOneColumn = function (column,keyword) { " + _NewLine;
            code += "        var result; " + _NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + _NewLine;

            code += " var tag = '{column:\"' + column + '\",keyword:\"' + keyword + '\"}';" + _NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsOneColumn\", tag, false, function (msg) { " + _NewLine;
            code += "            result = msg.d; " + _NewLine;
            code += "        }); " + _NewLine;
            code += "        return result; " + _NewLine;
            code += "    };//GetKeyWordsOneColumn  " + _NewLine;
            code += "   " + _NewLine;

            return code;
        }

        #region Search

        private string GenSearch()
        {
            string ColumnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            ColumnsParameterFunction = "PageIndex,PageSize,SortExpression,SortDirection," + ColumnsParameterFunction.TrimEnd(',') + ",RederTable_Pagger";

            string ColumnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            ColumnsParameterJson = "PageIndex:\"'+PageIndex+'\",PageSize:\"'+PageSize+'\",SortExpression:\"'+SortExpression+'\",SortDirection:\"'+SortDirection+'\"," + ColumnsParameterJson.TrimEnd(',');
            string code = "";

            code += "  this.Search = function (" + ColumnsParameterFunction + ") {" + _NewLine;
            code += "        var result;" + _NewLine;
            code += "" + _NewLine;
            code += "        var tag = '{" + ColumnsParameterJson + "}';" + _NewLine;
            code += "        var F = CallServices(url + \"Search\", tag, true, function (msg) {" + _NewLine;
            code += "            result = msg.d;" + _NewLine;
            code += "" + _NewLine;
            code += "            RederTable_Pagger(result);" + _NewLine;
            code += "        });" + _NewLine;
            code += "        return result;" + _NewLine;
            code += "    };//Save" + _NewLine;

            return code;
        }

        #endregion Search

        #region EditData

        private string GenSave()
        {
            string code = "";
            string ColumnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            ColumnsParameterFunction = ColumnsParameterFunction.TrimEnd(',');

            string ColumnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            ColumnsParameterJson = ColumnsParameterJson.TrimEnd(',');

            code += $"this.Save = function({ColumnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + ColumnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Save\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Save" + _NewLine;
            return code;
        }

        private string GenUpdate()
        {
            string code = "";
            string ColumnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            ColumnsParameterFunction = ColumnsParameterFunction.TrimEnd(',');

            string ColumnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            ColumnsParameterJson = ColumnsParameterJson.TrimEnd(',');

            code += $"this.Update = function({ColumnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + ColumnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Update\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Update" + _NewLine;
            return code;
        }

        private string GenDelete()
        {
            string code = "";
            string ColumnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            ColumnsParameterFunction = ColumnsParameterFunction.TrimEnd(',');

            string ColumnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            ColumnsParameterJson = ColumnsParameterJson.TrimEnd(',');

            code += $"this.Delete = function({ColumnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + ColumnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Delete\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Delete" + _NewLine;
            return code;
        }

        #endregion EditData

        public override void Gen()
        {
            //_FileName = new FileName();
            //_FileName._TableName = _TableName;
            //_FileName._ds = _ds;
            InnitProperties();

            string code = "";
            code += GenJquerySaveData();
            _FileCode.WriteFile(_FileName.JsCodeName(), code);
            //_FileCode.writeFile(FileName, _code, _fileType);
        }
    }
}