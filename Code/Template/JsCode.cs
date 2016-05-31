using StkGenCode.Code.Column;

namespace StkGenCode.Code.Template
{
    public class JsCode : CodeBase
    {
        private string GenJquerySaveData()
        {
            var code = "";

            code += "var " + TableName + "Service = {}; " + NewLine;
            code += "(function () { " + NewLine;
            code += "    var url = \"" + FileName.PageServiceName() + "/\"; " + NewLine;

            code += GenSaveColumn();

            code += GetKeyWordsAllColumn();

            code += GenGetKeyWordsOneColumn();

            code += GenSearch();

            code += GenSave();

            code += GenUpdate();

            code += GenDelete();
            code += SelectAll();

            code += Select();

            code += "}).apply(" + TableName + "Service); " + NewLine;

            return code;
        }

        private string GenSaveColumn()
        {
            var code = "";
            code += " " + NewLine;
            code += "    this.SaveColumn =  function (id, column, value) { " + NewLine;
            code += "            var result; " + NewLine;
            code += "            ////data \"{ssss:1,ddddd:1}\" " + NewLine;
            code += "            var tag = '{id:\"' + id + '\",column:\"' + column + '\",value:\"' + value + '\"}'; " +
                    NewLine;
            code += "            var F = CallServices(url + \"SaveColumn\", tag, false, function (msg) { " + NewLine;
            code += "                result = msg.d; " + NewLine;
            code += "            }); " + NewLine;
            code += "            return result; " + NewLine;
            code += "        };//SaveColumn " + NewLine;
            code += "   " + NewLine;
            return code;
        }

        private string GetKeyWordsAllColumn()
        {
            var code = "";
            code += "   " + NewLine;
            code += " this.GetKeyWordsAllColumn = function (keyword) { " + NewLine;
            code += "        var result; " + NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + NewLine;
            code += "        var tag = '{keyword:\"' + keyword + '\"}'; " + NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsAllColumn\", tag, false, function (msg) { " +
                    NewLine;
            code += "            result = msg.d; " + NewLine;
            code += "        }); " + NewLine;
            code += "        return result; " + NewLine;
            code += "    };//GetKeyWordsAllColumn  " + NewLine;
            code += "   " + NewLine;
            return code;
        }

        private string GenGetKeyWordsOneColumn()
        {
            var code = "";
            code += "   " + NewLine;
            code += " this.GetKeyWordsOneColumn = function (column,keyword) { " + NewLine;
            code += "        var result; " + NewLine;
            code += "        ////data \"{ssss:1,ddddd:1}\"   " + NewLine;

            code += " var tag = '{column:\"' + column + '\",keyword:\"' + keyword + '\"}';" + NewLine;
            code += "        var F = CallServices(url + \"GetKeyWordsOneColumn\", tag, false, function (msg) { " +
                    NewLine;
            code += "            result = msg.d; " + NewLine;
            code += "        }); " + NewLine;
            code += "        return result; " + NewLine;
            code += "    };//GetKeyWordsOneColumn  " + NewLine;
            code += "   " + NewLine;

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
            FileCode.WriteFile(FileName.JsCodeName(), code);
            //_FileCode.writeFile(FileName, _code, _fileType);
        }

        #region Search

        private string GenSearch()
        {
            var columnsParameterFunction = ColumnString.GenLineString(Ds, "{0},");
            columnsParameterFunction = "PageIndex,PageSize,SortExpression,SortDirection," +
                                       columnsParameterFunction.TrimEnd(',') + ",RederTable_Pagger";

            var columnsParameterJson = ColumnString.GenLineString(Ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson =
                "PageIndex:\"'+PageIndex+'\",PageSize:\"'+PageSize+'\",SortExpression:\"'+SortExpression+'\",SortDirection:\"'+SortDirection+'\"," +
                columnsParameterJson.TrimEnd(',');
            var code = "";

            code += "  this.Search = function (" + columnsParameterFunction + ") {" + NewLine;
            code += "        var result;" + NewLine;
            code += "" + NewLine;
            code += "        var tag = '{" + columnsParameterJson + "}';" + NewLine;
            code += "        var F = CallServices(url + \"Search\", tag, true, function (msg) {" + NewLine;
            code += "            result = msg.d;" + NewLine;
            code += "" + NewLine;
            code += "            RederTable_Pagger(result);" + NewLine;
            code += "        });" + NewLine;
            code += "        return result;" + NewLine;
            code += "    };//Save" + NewLine;

            return code;
        }

        private string SelectAll()
        {
            var code = "";

            code += "" + NewLine;
            code += "    this.SelectAll = function () {" + NewLine;
            code += "        var result;" + NewLine;
            code += "" + NewLine;
            code += "        var tag = '{}';" + NewLine;
            code += "        var F = CallServices(url + \"SelectAll\", tag, false, function (msg) {" + NewLine;
            code += "            result = msg.d;" + NewLine;
            code += "        });" + NewLine;
            code += "        return result;" + NewLine;
            code += "    };//SelectAll" + NewLine;
            return code;
        }

        private string Select()
        {
            var code = "";

            code += "" + NewLine;
            code += "    this.Select = function (" + Ds.Tables[0].PrimaryKey[0] + ") {" + NewLine;
            code += "        var result;" + NewLine;
            code += "" + NewLine;
            code += " var tag = '{" + Ds.Tables[0].PrimaryKey[0] + ":\"'+" + Ds.Tables[0].PrimaryKey[0] + "+'\"}';" +
                    NewLine;

            code += "        var F = CallServices(url + \"Select\", tag, false, function (msg) {" + NewLine;
            code += "            result = msg.d;" + NewLine;
            code += "        });" + NewLine;
            code += "        return result;" + NewLine;
            code += "    };//SelectAll" + NewLine;
            return code;
        }

        #endregion Search

        #region EditData

        private string GenSave()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(Ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(Ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Save = function({columnsParameterFunction})" + NewLine;
            code += "{" + NewLine;
            code += "" + NewLine;
            code += "            var result;" + NewLine;
            code += "" + NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + NewLine;
            code += "            var F = CallServices(url + \"Save\", tag, false, function(msg) {" + NewLine;
            code += "                result = msg.d;" + NewLine;
            code += "            });" + NewLine;
            code += "            return result;" + NewLine;
            code += "        };//Save" + NewLine;
            return code;
        }

        private string GenUpdate()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(Ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(Ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Update = function({columnsParameterFunction})" + NewLine;
            code += "{" + NewLine;
            code += "" + NewLine;
            code += "            var result;" + NewLine;
            code += "" + NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + NewLine;
            code += "            var F = CallServices(url + \"Update\", tag, false, function(msg) {" + NewLine;
            code += "                result = msg.d;" + NewLine;
            code += "            });" + NewLine;
            code += "            return result;" + NewLine;
            code += "        };//Update" + NewLine;
            return code;
        }

        private string GenDelete()
        {
            var code = "";
            var columnsParameterFunction = ColumnString.GenLineString(Ds, "{0},");
            columnsParameterFunction = columnsParameterFunction.TrimEnd(',');

            var columnsParameterJson = ColumnString.GenLineString(Ds, "{0}:\"' + {0} + '\",");
            columnsParameterJson = columnsParameterJson.TrimEnd(',');

            code += $"this.Delete = function({columnsParameterFunction})" + NewLine;
            code += "{" + NewLine;
            code += "" + NewLine;
            code += "            var result;" + NewLine;
            code += "" + NewLine;
            code += "            var tag = '{" + columnsParameterJson + "}';" + NewLine;
            code += "            var F = CallServices(url + \"Delete\", tag, false, function(msg) {" + NewLine;
            code += "                result = msg.d;" + NewLine;
            code += "            });" + NewLine;
            code += "            return result;" + NewLine;
            code += "        };//Delete" + NewLine;
            return code;
        }

        #endregion EditData
    }
}