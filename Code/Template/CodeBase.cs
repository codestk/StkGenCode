using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
   public  abstract class  CodeBase
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;
        protected FileName _FileName;

        public string _NewLine = " \r\n";

        public string _NotImplement = "throw new Exception(\"Not implement\");";

        public abstract void Gen();

        protected void  innitProperties()
        {
            _FileName = new FileName();
            _FileName._TableName = _TableName;
            _FileName._ds = _ds;


           
        }
    }
}
