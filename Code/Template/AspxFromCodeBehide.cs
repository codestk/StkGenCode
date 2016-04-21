using System;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxFromCodeBehide
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;

        private string _fileType = ".aspx.cs";
        private string _NewLine = " \r\n";

        private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string FileName;

        private string GenBtnSave()
        {
            string code = "  ";
            code += "protected void btnSave_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
            code += MapControlToProPErties(_ds);
            //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            //{
            //    if (_DataColumn.DataType.ToString() == "System.Int32")
            //    {
            //        code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    }
            //    else
            //    {
            //        code += "_" + _TableName + "." + _DataColumn.ColumnName + " = txt" + _DataColumn.ColumnName + ".Text;" + _NewLine;
            //    }
            //    // _AmpBangkok.AmpBangkokIndex = Convert.ToInt32(txtAmpBangkokIndex.Text);
            //    //  _AmpBangkok.AmpText = txtAmpText.Text;
            //}
            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "    _" + _TableName + "Db.Insert(); " + _NewLine;
            code += "  MsgBox.Alert(\"Saved!!!\"); " + _NewLine;

            code += "  btnSave.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        private string GenBtnUpdate()
        {
            string code = "  ";
            code += "protected void btnUpdate_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
            code += MapControlToProPErties(_ds);
            //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            //{
            //    if (_DataColumn.DataType.ToString() == "System.Int32")
            //    {
            //        code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    }
            //    else
            //    {
            //        code += "_" + _TableName + "." + _DataColumn.ColumnName + " = txt" + _DataColumn.ColumnName + ".Text;" + _NewLine;
            //    }
            //    // _AmpBangkok.AmpBangkokIndex = Convert.ToInt32(txtAmpBangkokIndex.Text);
            //    //  _AmpBangkok.AmpText = txtAmpText.Text;
            //}
            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "    _" + _TableName + "Db.Update(); " + _NewLine;
            code += "   MsgBox.Alert(\"Updated!!!\"); " + _NewLine;

            code += "  btnUpdate.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        private string GenBtnDelete()
        {
            string code = "  ";
            code += "protected void btnDelete_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            //foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            //{
            //    //System.Console.Write(_DataColumn.DataType.ToString()+ _NewLine);
            //    //if (_DataColumn.DataType.ToString() == "System.Int32")
            //    //{
            //    //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    //}
            //    //else if (_DataColumn.DataType.ToString() == "System.Decimal")
            //    //{
            //    //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =  Convert.ToDecimal (txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    //}
            //    //else if (_DataColumn.DataType.ToString() == "System.DateTime")
            //    //{
            //    //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToDateTime(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    //}
            //    //else if (_DataColumn.DataType.ToString() == "System.Boolean")
            //    //{
            //    //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToBoolean(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
            //    //}
            //    //else
            //    //{
            //    //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = txt" + _DataColumn.ColumnName + ".Text;" + _NewLine;
            //    //}

            //}
            code += MapControlToProPErties(_ds);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "    _" + _TableName + "Db.Delete(); " + _NewLine;
            code += "   MsgBox.Alert(\"Deleted!!!\"); " + _NewLine;

            code += "  btnDelete.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        private String MapControlToProPErties(DataSet _ds)
        {
            string code = "";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                System.Console.Write(_DataColumn.DataType.ToString() + _NewLine);
                if (_DataColumn.DataType.ToString() == "System.Int32")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =  Convert.ToDecimal (txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToDateTime(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.Boolean")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToBoolean(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = txt" + _DataColumn.ColumnName + ".Text;" + _NewLine;
                }
            }

            return code;
        }

        private string GenBindForm()
        {
            string code = "  ";
            code += " private void BindForm() " + _NewLine;
            code += " { " + _NewLine;
            code += " if (Request.QueryString[\"Q\"] == null)" + _NewLine;
            code += " { return; }" + _NewLine;
            code += "  String Q = Stk_QueryString.DecryptQuery(\"Q\");" + _NewLine;
            code += "    " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db();" + _NewLine;
            code += "   " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  _" + _TableName + " = _" + _TableName + "Db.Select(Q); " + _NewLine;
            code += "  " + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                code += "  txt" + _DataColumn.ColumnName + ".Text = Stk_TextNull.StringTotext(_" + _TableName + "." + _DataColumn.ColumnName + ".ToString()); " + _NewLine;
            }
            code += "   } " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            return code;
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        BindForm();
        //    }
        //}
        private string GenPageLoad()
        {
            string code = "  ";
            code += "  protected void Page_Load(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += "  if (!Page.IsPostBack) " + _NewLine;
            code += " {  " + _NewLine;
            code += "    BindForm(); " + _NewLine;
            code += "  } " + _NewLine;
            code += "}  " + _NewLine;
            return code;
        }

        private string T()
        {
            string code = "  ";
            code += "  " + _NewLine;
            code += "  " + _NewLine; code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            return code;
        }

        private string GenUsing()
        {
            string code = "  ";
            code += "using System;" + _NewLine;
            code += "using System.Collections.Generic;" + _NewLine;
            code += "using System.Linq;" + _NewLine;
            code += "using System.Web;" + _NewLine;
            code += "using System.Web.UI;" + _NewLine;
            code += "using System.Web.UI.WebControls;" + _NewLine;
            return code;
        }

        private string BeginClass()
        {
            string code = "  ";
            code += "public partial class " + _TableName + "Web: System.Web.UI.Page" + _NewLine;
            code += "{" + _NewLine;
            return code;
        }

        private string EndClass()
        {
            return "}";
        }

        public void Gen()
        {
            string _code = "";
            _code += GenUsing();
            _code += BeginClass();
            _code += GenPageLoad();
            _code += GenBindForm();
            _code += GenBtnSave();
            _code += GenBtnUpdate();

            _code += GenBtnDelete();

            _code += EndClass();
            //_FileCode.writeFile(FileName, _code, _fileType);
            string FileName = _TableName + "Web";
            _FileCode.writeFile(FileName, _code, _fileType);
        }
    }
}