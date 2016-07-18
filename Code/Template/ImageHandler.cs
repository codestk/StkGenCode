using StkGenCode.Code.Name;

namespace StkGenCode.Code.Template
{
    public class ImageHandler : CodeBase
    {
        private string HeadFile()
        {
            string code = "";
            string ImageHandler = ClassName.ImageHandlerName(TableName);
            code += $"<%@ WebHandler Language =\"C#\" Class=\"{ImageHandler}\" %>" + NewLine;
            return code;
        }

        private string Using()
        {
            string code = "";
            //code += "using System.Linq;" + NewLine;
            code += "using System.Web;" + NewLine;
            code += "  using System.Data;" + NewLine;
          
            //code += "using System.Web.Http;" + NewLine;

            return code;
        }

        private string BeginClass()
        {
            string code = "";
            string ImageHandler = ClassName.ImageHandlerName(TableName);
            code += $"public class {ImageHandler} : IHttpHandler";
            code += "{" + NewLine;
            return code;
        }

        private string EndClass()
        {
            string code = "}" + NewLine;
            return code;
        }

        private string ProcessRequest()
        {
            string code = "";
            string ImageDbName = ClassName.ImageDbName(TableName);
            string pictureColumn = GetColumnPicture();
            code += "  public void ProcessRequest(HttpContext context)" + NewLine;
            code += "    {" + NewLine;
            code += "        byte[] buffer = null;" + NewLine;
            code += "" + NewLine;
            code += "        string id = \"\";" + NewLine;
            code += "        if (context.Request.QueryString[\"Q\"] != null)" + NewLine;
            code += "        {";
            code += "            id = context.Request.QueryString[\"Q\"];" + NewLine;
            code += "        }" + NewLine;
            code += "        else" + NewLine;
            code += "        {" + NewLine;
            code += "            return;" + NewLine;
            code += "        }" + NewLine;
            code += "" + NewLine;
            code += "        IDataReader reader = null;" + NewLine;
            code += $"        {ImageDbName} ImageDb = new {ImageDbName}();" + NewLine;
            code += "        try" + NewLine;
            code += "        {" + NewLine;
            code += "            using (reader = ImageDb.GetPicture(id))" + NewLine;
            code += "            {" + NewLine;
            code += "" + NewLine;
            code += "                //get the extension name of image" + NewLine;
            code += "                while (reader.Read())" + NewLine;
            code += "                {" + NewLine;
            code += "" + NewLine;
            code += "                    string extensionName = \"jpg\";" + NewLine;
            code += "                    if (reader[\"Picture\"] != System.DBNull.Value)" + NewLine;
            code += "                    {" + NewLine;
            code += $"                        buffer = (byte[])reader[\"{pictureColumn}\"];" + NewLine;
            code += "" + NewLine;
            code += "                        context.Response.Clear();" + NewLine;
            code += "                        context.Response.ContentType = \"image/\" + extensionName;" + NewLine;
            code += "" + NewLine;
            code += "                        //context.Response.OutputStream.Write(buffer, 78, buffer.Length - 78);" + NewLine;
            code += "                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);" + NewLine;
            code += "                    }";
            code += "                    context.Response.Flush();" + NewLine;
            code += "                    context.Response.Close();" + NewLine;
            code += "" + NewLine;
            code += "                    //context.Response.OutputStream.Write(buffer, 78, buffer.Length - 78);" + NewLine;
            code += "                    //ctx.Response.ContentType = \"image/bmp\";" + NewLine;
            code += "                    //ctx.Response.OutputStream.Write(pict, 78, pict.Length - 78);" + NewLine;
            code += "" + NewLine;
            code += "                }" + NewLine;
            code += "                reader.Close();" + NewLine;
            code += "            }" + NewLine;
            code += "" + NewLine;
            code += "        }";
            code += "        finally";
            code += "        {";
            code += "" + NewLine;
            code += "            ImageDb.Db.CloseFbData();" + NewLine;
            code += "        }" + NewLine;
            code += "    }" + NewLine;

            return code;
        }

        private string IsReusable()
        {
            string code = "";
            code += "  public bool IsReusable" + NewLine;
            code += "    {" + NewLine;
            code += "        get";
            code += "        {" + NewLine;
            code += "            return false;" + NewLine;
            code += "        }" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public override void Gen()
        {
            string code = "";
            code += HeadFile();
            code += Using();

            code += BeginClass();
            code += ProcessRequest();
            code += IsReusable();

            code += EndClass();

            InnitProperties();

            FileCode.WriteFile(FileName.ImageHandlerName(), code);
        }
    }
}