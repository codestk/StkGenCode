using StkGenCode.Code.Column;
using System.Collections.Generic;
using System.Data;

namespace StkGenCode.Code.Template
{
    public abstract class CodeBase
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;
        protected FileName _FileName;

        public string _NewLine = " \r\n";

        public string _NotImplement = "throw new Exception(\"Not implement\");";

        #region Constant

        protected string _formatpropertieName = "_{0}.{1}";
        protected string _formatTextBoxName = "txt{0}";
        protected string _formatChekBoxName = "chk{0}";
        protected string _formatDropDownName = "drp{0}";

        #endregion Constant

        /// <summary>
        /// ใช้ สำหรับ Gen Code Dropdown list
        /// ColumnName:Table
        /// </summary>

        public List<MappingColumn> _MappingColumn { get; set; }

        public abstract void Gen();

        protected void innitProperties()
        {
            _FileName = new FileName();
            _FileName._TableName = _TableName;
            _FileName._ds = _ds;
        }
    }
}