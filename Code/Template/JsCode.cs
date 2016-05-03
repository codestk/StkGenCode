﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
   public class JsCode : CodeBase
    {
        
        string GenJquerySaveData()
        {
            string code = "";
            FileName name = new FileName();


            code += "var "+_TableName+"Service = {}; " + _NewLine;
            code += "(function () { " + _NewLine;
            code += "    var url = \""+_FileName.AspxTableCodeName()+"/\"; " + _NewLine;
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
            code += "}).apply("+ _TableName + "Service); " + _NewLine;


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