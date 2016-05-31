using System.IO;

namespace StkGenCode.Code
{
    public class FileCode
    {
        public string Path;

        public void WriteFile(string name, string content)
        {
            // string path = textBox1.Text;
            // check if directory exists
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            // _pathFull = path + Table + "_" + DateTime.Today.ToString("dd-MM-yy") + FileType;
            var pathFull = Path + name;

            // check if file exist
            //File.Delete(path);
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            // log the error now
            using (var writer = File.AppendText(pathFull))
            {
                var error = content;
                writer.WriteLine(error);
                //writer.WriteLine("==========================================");
                writer.Flush();
                writer.Close();
            }
            //  return userFriendlyError;
        }

        public void ClearAllFile()
        {
            var di = new DirectoryInfo(Path);

            if (di.Exists)
            {
                //foreach (FileInfo file in di.GetFiles())
                //{
                //    file.Delete();
                //}
                foreach (var file in di.GetFiles()) file.Delete();
                foreach (var subDirectory in di.GetDirectories()) subDirectory.Delete(true);
            }
            // File.Delete(path);
        }

        //    string space = "\r\n";
        //{

        //public void AddSpace(string Table, string FileType)
        //    writeFile(Table, space);
        //}
    }
}