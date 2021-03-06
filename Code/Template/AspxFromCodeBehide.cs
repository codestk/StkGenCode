﻿namespace StkGenCode.Code.Template
{
    public class AspxFromCodeBehide : CodeBase
    {
        private string GenUsing()
        {
            var code = "  ";
            code += "using System;" + NewLine;
            code += "using System.Collections.Generic;" + NewLine;
            code += "using System.Linq;" + NewLine;
            code += "using System.Web;" + NewLine;
            code += "using System.Web.UI;" + NewLine;
            code += "using System.Web.UI.WebControls;" + NewLine;
            return code;
        }

        private string BeginClass()
        {
            var code = "  ";
            code += "public partial class " + TableName + "Web: System.Web.UI.Page" + NewLine;
            code += "{" + NewLine;

            return code;
        }

        private string EndClass()
        {
            return "}";
        }

        private string BeginNameSpace()
        {
            var code = "  ";
            code += "namespace WebApp" + NewLine;
            code += "{" + NewLine;

            return code;
        }

        private string EndNameSpace()
        {
            return "}";
        }

        public override void Gen()
        {
            var code = "";
            code += GenUsing();


            code += BeginNameSpace();
            code += BeginClass();

            code += EndClass();
            code += EndNameSpace();

            InnitProperties();

            FileCode.WriteFile(FileName.AspxFromCodeBehideName(), code);
        }
    }
}