namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumnCodeBehide : CodeBase
    {
        //public AspxFromCodeBehide AspxFromCodeBehide;

        private string BeginClass()
        {
            var code = "   namespace WebApp ";
                       code += " { ";
            code += "public partial class " + TableName + "Filter: System.Web.UI.Page" + NewLine;
            code += "{" + NewLine;
            return code;
        }

        private string EndClass()
        {
            return "} }"; // Name Space and Class
        }

        public override void Gen()
        {
            var code = "";

            //code += GenUsign();
            code += BeginClass();
            //code += GenConstance();

            //code += GenPageLoad();
            //if (AspxFromCodeBehide.HaveDropDown())
            //if (HaveDropDown())
            //{
            //    code += GenInnitDropDown();
            //}

            code += EndClass();

            InnitProperties();

            FileCode.WriteFile(FileName.AspxTableCodeFilterColumnBehineName(), code);
        }
    }
}