using System;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxFromCodeBehide : CodeBase
    {
        //public FileCode _FileCode;
        //public DataSet _ds;
        //public string _TableName;

        //private string _NewLine = " \r\n";

        //private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string GenPageLoad()
        {
            string code = "  ";
            code += "  protected void Page_Load(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " if (!Page.IsPostBack) " + _NewLine;
            code += " {  " + _NewLine;
            code += " BindForm(); " + _NewLine;
            code += " } " + _NewLine;
            code += "}  " + _NewLine;

            return code;
        }

        private string GenBtnSave()
        {
            string code = "  ";
            code += "protected void btnSave_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapControlToProPerties(_ds, true);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            //code += "  _" + _TableName + "Db.Insert(); " + _NewLine;

            //  txtid.Text = _modulesDb.Insert().ToString();
            code += "txt" + _ds.Tables[0].PrimaryKey[0] + ".Text= _" + _TableName + "Db.Insert().ToString();; " + _NewLine;

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
            code += MapControlToProPerties(_ds, false);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "  _" + _TableName + "Db.Update(); " + _NewLine;
            code += "MsgBox.Alert(\"Updated!!!\"); " + _NewLine;

            code += "btnUpdate.Visible = false; " + _NewLine;

            code += "} " + _NewLine;
            return code;
        }

        private string GenBtnDelete()
        {
            string code = "  ";
            code += "protected void btnDelete_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += " " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapControlToProPerties(_ds, false);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "  _" + _TableName + "Db.Delete(); " + _NewLine;
            code += " MsgBox.Alert(\"Deleted!!!\"); " + _NewLine;

            code += " btnDelete.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        private String MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        {
            string code = "";

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                if (CommentKey)
                {
                    bool primary = false;

                    //ต้อง เป็น Auto ถึงจะ Comment
                    if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                    {
                        primary = true;
                    }

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                if ((_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                //else if (_DataColumn.DataType.ToString() == "System.Int16")
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt16(txt" + _DataColumn.ColumnName + ".Checked);" + _NewLine;
                //}
                else if (_DataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =  Convert.ToDecimal (txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =StkGlobalDate.TextEnToDate(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                {
                    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToInt16(txt" + _DataColumn.ColumnName + ".Checked);" + _NewLine;
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
            string code = "";
            code += " private void BindForm() " + _NewLine;
            code += " { " + _NewLine;
            code += " if (Request.QueryString[\"Q\"] == null)" + _NewLine;
            code += " { return; }" + _NewLine;
            code += "  String Q = Stk_QueryString.DecryptQuery(\"Q\");" + _NewLine;
            code += " // " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db();" + _NewLine;
            code += "  " + _TableName + "Db  _" + _TableName + "Db = new " + _TableName + "Db();" + _NewLine;

            code += "  " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += " _" + _TableName + " = _" + _TableName + "Db.Select(Q); " + _NewLine;
            code += "  " + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                string propertieName = string.Format("_{0}.{1}", _TableName, _DataColumn.ColumnName);
                string controlName = string.Format("txt{0}" , _DataColumn.ColumnName);

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                if (_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ColumnName)
                {
                    code += controlName + ".Enabled = false;" + _NewLine;
                }

                if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                { code += controlName + ".Checked = Convert.ToBoolean(" + propertieName + ");" + _NewLine; }
                else if ((_DataColumn.DataType.ToString() == "System.DateTime") || (_DataColumn.DataType.ToString() == "System.Decimal") || (_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    code += " if (" + propertieName + ".HasValue)" + _NewLine;
                    code += "{" + _NewLine;
                    code += controlName + ".Text = StkGlobalDate.DateToTextEngFormat(" + propertieName + "); " + _NewLine;
                    code += "}" + _NewLine;
                }
                else
                { code += controlName + ".Text = Stk_TextNull.StringTotext(" + propertieName + ".ToString()); " + _NewLine; }
            }
            code += " } " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
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

        public override void Gen()
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
            FileName name = new FileName();
            name._TableName = _TableName;
            name._ds = _ds;
            _FileCode.writeFile(name.AspxFromCodeBehideName(), _code);
            //  _FileCode.writeFile(FileName, _code, _fileType);
        }
    }
}