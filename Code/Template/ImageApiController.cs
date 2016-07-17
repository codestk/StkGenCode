using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
    public class ImageApiController : CodeBase
    {

        string Using()
           {
            string code = "";
            code += "using System.Linq;";
            code += "using System.Web;";
            code += "using System.Web.Http;";

            return code;
        }
        private string BeginClass()
        {
            //CatImageController : ApiController
            var code = "  ";
            code += "public partial class " + TableName + "ImageController:ApiController" + NewLine;
            code += "{" + NewLine;

            return code;
        }
        private string EndClass()
        {
            return "}";
        }

        public override void Gen()
        {
            InnitProperties();
            var code = "";
            code = Using();
            code = BeginClass();



            code = EndClass();
                
            FileCode.WriteFile(FileName.ControllerName(), code);
        }
    }
}
