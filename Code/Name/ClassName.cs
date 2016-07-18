using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Name
{
   public class ClassName
    {
        public static string ImageHandlerName(string tableName)
        {
         
            return $"{tableName}ImageHandler";
        }
        public static string ImageControllerName(string tableName)
        {
            return $"{tableName}ImageController";
        }
        public static string  ImageDbName(string tableName)
        {
            return $"{tableName}ImageDb";
        }

    }
}
