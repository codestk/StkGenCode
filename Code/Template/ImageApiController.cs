using StkGenCode.Code.Name;

namespace StkGenCode.Code.Template
{
    public class ImageApiController : CodeBase
    {
        private string Using()
        {
            string code = "";
            code += "using System.Linq;" + NewLine;
            code += "using System.Web;" + NewLine;
            code += "using System.Web.Http;" + NewLine;

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

        public string UploadFile()
        {
            var code = "  ";
            string ControllerName = ClassName.ImageControllerName(TableName);
            string  ImageDbName = ClassName.ImageDbName(TableName);
            code += " [HttpGet]" + NewLine;
            code += "    [HttpPost]" + NewLine;
            code += "    [Route(\"api/"+ ControllerName + "/UploadFile/{id}\")]" + NewLine;
            code += "    public bool UploadFile(string id)" + NewLine;
            code += "    {";
            code += "        bool result = false;";
            code += "        if (HttpContext.Current.Request.Files.AllKeys.Any())" + NewLine;
            code += "        {";
            code += "// Get the uploaded image from the Files collection" + NewLine;
            code += "var httpPostedFile = HttpContext.Current.Request.Files[\"UploadedImage\"];" + NewLine;
            code += "";
            code += "if (httpPostedFile != null)" + NewLine;
            code += "{";
            code += "    // Validate the uploaded image(optional)" + NewLine;
            code += "    int lengths = httpPostedFile.ContentLength;" + NewLine;
            code += "    byte[] imgbytes = new byte[lengths];" + NewLine;
            code += "    httpPostedFile.InputStream.Read(imgbytes, 0, lengths);" + NewLine;

            code += "" + NewLine;
            code += "" + NewLine;
            code += $"    {ImageDbName}  ImageDb = new  {ImageDbName}();" + NewLine;
            code += "" + NewLine;
            code += "" + NewLine;
            code += "    result = ImageDb.SavePicture(id, imgbytes);" + NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;
            code += "        }" + NewLine;
            code += "" + NewLine;
            code += "        return result;" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public string Delete()
        {
            var code = "";
            string ControllerName = ClassName.ImageControllerName(TableName);
            string ImageDbName = ClassName.ImageDbName(TableName);
            code += " [HttpGet]" + NewLine;
            code += "    [HttpPost]" + NewLine;
            code += $"    [Route(\"api/"+ ControllerName + "/Delete/{id}\")]" + NewLine;
            code += "    public bool Delete(string id)" + NewLine;
            code += "    {" + NewLine;
            code += $"        {ImageDbName} ImageDb = new {ImageDbName}();" + NewLine;
            code += "" + NewLine;
            code += "" + NewLine;
            code += "        var result = ImageDb.DeletePicture(id);" + NewLine;
            code += "        return result;" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public override void Gen()
        {
            InnitProperties();
            var code = "";
            code += Using();
            code += BeginClass();

            code += UploadFile();
            code += Delete();
            code += EndClass();

            FileCode.WriteFile(FileName.ControllerName(), code);
        }
    }
}