using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!File.Exists(_pathFull))
            {
                File.Create(_pathFull).Dispose();
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

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
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