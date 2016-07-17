using StkGenCode.Code.Name;

namespace StkGenCode.Code.Template
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

        string UploadFile()
        {
            string code = "";
            code += "   [HttpGet]";
            code += "    [HttpPost]";
            code += "    [Route(\"api/"+ ClassName.ControllerName(TableName) + "/UploadFile/{id}\")]";
            code += "    public string UploadFile(string id)";
            code += "    {";
            code += "        if (HttpContext.Current.Request.Files.AllKeys.Any())";
            code += "        {";
            code += "            // Get the uploaded image from the Files collection";
            code += "            var httpPostedFile = HttpContext.Current.Request.Files[\"UploadedImage\"];";
            code += "";
            code += "            if (httpPostedFile != null)";
            code += "            {";
            code += "                // Validate the uploaded image(optional)";
            code += "                int lengths = httpPostedFile.ContentLength;";
            code += "                byte[] imgbytes = new byte[lengths];";
            code += "                httpPostedFile.InputStream.Read(imgbytes, 0, lengths);";
            code += "                // Get the complete file path";
            code += "                //var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath(\"~/UploadedFiles\"), httpPostedFile.FileName);";
            code += "";
            code += "                // Save the uploaded file to \"UploadedFiles\" folder";
            code += "                // httpPostedFile.SaveAs(fileSavePath);";
            code += "";
            code += "                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(\"Data Source=NODE-PC;Initial Catalog=Northwind;User ID=sa;Password=P@ssw0rd\");";
            code += "                con.Open();";
            code += "                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(\"UPDATE [dbo].[Categories] SET  [Picture] =@Picture  WHERE [CategoryID]=@CategoryID\", con);";
            code += "";
            code += "                cmd.Parameters.AddWithValue(\"@Picture\", imgbytes);";
            code += "                cmd.Parameters.AddWithValue(\"@CategoryID\", id);";
            code += "";
            code += "                int count = cmd.ExecuteNonQuery();";
            code += "            }";
            code += "        }";
            code += "";
            code += "        return \"FFFF\";";
            code += "    }";
            return code;

        }

        public override void Gen()
        {
            var code = "";
            code += GenUsing();
            code += BeginClass();
            code += UploadFile();
            code += EndClass();

            InnitProperties();

            FileCode.WriteFile(FileName.AspxFromCodeBehideName(), code);
        }
    }
}