using System.IO;

namespace StkGenCode.Code
{
    public class FileCode
    {
        public string path;

        public void writeFile(string Table, string content, string FileType)
        {
            // string path = textBox1.Text;
            // check if directory exists
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            string _pathFull;
            // _pathFull = path + Table + "_" + DateTime.Today.ToString("dd-MM-yy") + FileType;
            _pathFull = path + Table + FileType;

            // check if file exist
            //File.Delete(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // log the error now
            using (StreamWriter writer = File.AppendText(_pathFull))
            {
                string error = content;
                writer.WriteLine(error);
                //writer.WriteLine("==========================================");
                writer.Flush();
                writer.Close();
            }
            //  return userFriendlyError;
        }

        public void ClearAllFile()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                //foreach (FileInfo file in di.GetFiles())
                //{
                //    file.Delete();
                //}
                foreach (System.IO.FileInfo file in di.GetFiles()) file.Delete();
                foreach (System.IO.DirectoryInfo subDirectory in di.GetDirectories()) subDirectory.Delete(true);
            }
            // File.Delete(path);
        }

        public void AddSpace(string Table, string FileType)
        {
            string space = "\r\n";
            writeFile(Table, space, FileType);
        }
    }
}