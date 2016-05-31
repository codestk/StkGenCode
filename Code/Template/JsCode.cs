using StkGenCode.Code.Column;

namespace StkGenCode.Code.Template
{
    public class JsCode : CodeBase
    {
        private string GenJquerySaveData()
        {
            var code = "";

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
            code += SelectAll();

            code += Select();

            code += "}).apply(" + _TableName + "Service); " + _NewLine;

            return code;
        }

        private string GenSaveColumn()
        {
            var code = "";
            code += " " + _NewLine;
            code += "    this.SaveColumn =  function (id, column, value) { " + _NewLine;
            code += "            var result; " + _NewLine;
            code += "            ////data \"{ssss:1,ddddd:1}\" " + _NewLine;
            code += "            var tag = '{id:\"' + id + '\",column:\"' + column + '\",value:\"' + value + '\"}'; " +
                    _NewLine;
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
            var code = "";
            code += "   " + _NewLine;
            code += " this.GetKeyWordsAllColumn = function (keyword) { " + _NewLine;
            code += "        var result; " + _NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + _NewLine;
            code += "        var tag = '{keyword:\"' + keyword + '\"}'; " + _NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsAllColumn\", tag, false, function (msg) { " +
                    _NewLine;
            code += "            result = msg.d; " + _NewLine;
            code += "        }); " + _NewLine;
            code += "        return result; " + _NewLine;
            code += "    };//GetKeyWordsAllColumn  " + _NewLine;
            code += "   " + _NewLine;
            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            var code = "";
            code += "   " + _NewLine;
            code += " this.GetKeyWordsOneColumn = function (column,keyword) { " + _NewLine;
            code += "        var result; " + _NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + _NewLine;

            code += " var tag = '{column:\"' + column + '\",keyword:\"' + keyword + '\"}';" + _NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsOneColumn\", tag, false, function (msg) { " +
                    _NewLine;
            code += "            result = msg.d; " + _NewLine;
            code += "        }); " + _NewLine;
            code += "        return result; " + _NewLine;
            code += "    };//GetKeyWordsOneColumn  " + _NewLine;
            code += "   " + _NewLine;

            return code;
        }

        public override void Gen()
        {
            //_FileName = new FileName();
            //_FileName._TableName = _TableName;
            //_FileName._ds = _ds;
            InnitProperties();

            var code = "";
            code += GenJquerySaveData();
            _FileCode.WriteFile(_FileName.JsCodeName(), code);
            //_FileCode.writeFile(FileName, _code, _fileType);
        }

        #region Search

        private string GenSearch()
        {
            var columnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            columnsParameterFunction = "PageIndex,PageSize,SortExpression,SortDirection," +
                                       columnsParameterFunction.TrimEnd(',') + ",RederTable_Pagger";

            var columnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson =
                "PageIndex:\"'+PageIndex+'\",PageSize:\"'+PageSize+'\",SortExpression:\"'+SortExpression+'\",SortDirection:\"'+SortDirection+'\"," +
                columnsParameterJson.TrimEnd(',');
            var code = "";

            code += "  this.Search = function (" + columnsParameterFunction + ") {" + _NewLine;
            code += "        var result;" + _NewLine;
            code += "" + _NewLine;
            code += "        var tag = '{" + columnsParameterJson + "}';" + _NewLine;
            code += "        var F = CallServices(url + \"Search\", tag, true, function (msg) {" + _NewLine;
            code += "            result = msg.d;" + _NewLine;
            code += "" + _NewLine;
            code += "            RederTable_Pagger(result);" + _NewLine;
            code += "        });" + _NewLine;
            code += "        return result;" + _NewLine;
            code += "    };//Save" + _NewLine;

            return code;
        }

        private string SelectAll()
        {
            var code = "";

            code += "" + _NewLine;
            code += "    this.SelectAll = function () {" + _NewLine;
            code += "        var result;" + _NewLine;
            code += "" + _NewLine;
            code += "        var tag = '{}';" + _NewLine;
            code += "        var F = CallServices(url + \"SelectAll\", tag, false, function (msg) {" + _NewLine;
            code += "            result = msg.d;" + _NewLine;
            code += "        });" + _NewLine;
            code += "        return result;" + _NewLine;
            code += "    };//SelectAll" + _NewLine;
            return code;
        }

        private string Select()
        {
            var code = "";

            code += "" + _NewLine;
            code += "    this.Select = function (" + _ds.Tables[0].PrimaryKey[0] + ") {" + _NewLine;
            code += "        var result;" + _NewLine;
            code += "" + _NewLine;
            code += " var tag = '{" + _ds.Tables[0].PrimaryKey[0] + ":\"'+" + _ds.Tables[0].PrimaryKey[0] + "+'\"}';" +
                    _NewLine;

            code += "        var F = CallServices(url + \"Select\", tag, false, function (msg) {" + _NewLine;
            code += "            result = msg.d;" + _NewLine;
            code += "        });" + _NewLine;
            code += "        return result;" + _NewLine;
            code += "    };//SelectAll" + _NewLine;
            return code;
        }

        #endregion Search

        #region EditData

        private string GenSave()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Save = function({columnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Save\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Save" + _NewLine;
            return code;
        }

        private string GenUpdate()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Update = function({columnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Update\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Update" + _NewLine;
            return code;
        }

        private string GenDelete()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(_ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(_ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Delete = function({columnsParameterFunction})" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "            var result;" + _NewLine;
            code += "" + _NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + _NewLine;
            code += "            var F = CallServices(url + \"Delete\", tag, false, function(msg) {" + _NewLine;
            code += "                result = msg.d;" + _NewLine;
            code += "            });" + _NewLine;
            code += "            return result;" + _NewLine;
            code += "        };//Delete" + _NewLine;
            return code;
        }

        #endregion EditData
    }
}