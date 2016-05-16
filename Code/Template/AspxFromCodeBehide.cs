using StkGenCode.Code.Name;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxFromCodeBehide : CodeBase
    {
        private string GenPageLoad()
        {
            string code = "  ";
            code += "protected void Page_Load(object sender, EventArgs e)" + _NewLine;
            code += "{ " + _NewLine;
            code += "if (!Page.IsPostBack) " + _NewLine;
            code += "{  " + _NewLine;

            if (HaveDropDown())
            {
                code += "BindDropDown();" + _NewLine;
            }

            code += "BindForm(); " + _NewLine;

            code += "} " + _NewLine;
            code += "}  " + _NewLine;

            return code;
        }

        private string GenBtnSave()
        {
            string controlTextBoxName = string.Format(_formatTextBoxName, _ds.Tables[0].PrimaryKey[0]);

            string code = "  ";
            code += "protected void btnSave_Click(object sender, EventArgs e)" + _NewLine;
            code += " { " + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

            code += MapControlToProPerties(_ds, true);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            //code += "  _" + _TableName + "Db.Insert(); " + _NewLine;

            //  txtid.Text = _modulesDb.Insert().ToString();

            //code += "txt" + _ds.Tables[0].PrimaryKey[0] + ".Text= _" + _TableName + "Db.Insert().ToString();; " + _NewLine;
            code += controlTextBoxName + ".Text= _" + _TableName + "Db.Insert().ToString(); " + _NewLine;

            code += "  MsgBox.Alert(\"Saved!!!\"); " + _NewLine;

            code += "  btnSave.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        private string GenBtnUpdate()
        {
            string code = "  ";
            code += "protected void btnUpdate_Click(object sender, EventArgs e)" + _NewLine;
            code += "{ " + _NewLine;
            code += "" + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "" + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
            code += MapControlToProPerties(_ds);

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

            code += MapControlToProPerties(_ds);

            code += "  _" + _TableName + "Db._" + _TableName + " = _" + _TableName + ";" + _NewLine;
            code += "  _" + _TableName + "Db.Delete(); " + _NewLine;
            code += " MsgBox.Alert(\"Deleted!!!\"); " + _NewLine;

            code += " btnDelete.Visible = false; " + _NewLine;

            code += " } " + _NewLine;
            return code;
        }

        //private String MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        //{
        //    string code = "";

        //    foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
        //    {
        //        string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
        //        string controlTextBoxName = string.Format(_formatTextBoxName, _DataColumn.ColumnName);
        //        string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);

        //        if (CommentKey)
        //        {
        //            bool primary = false;

        //            //ต้อง เป็น Auto ถึงจะ Comment
        //            if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
        //            {
        //                primary = true;
        //            }

        //            if (primary)
        //            {
        //                // continue;
        //                code += "// ";
        //            }
        //        }

        //        //For Drop Down List
        //        if (_MappingColumn != null)
        //        {
        //            string codedrp = GenMapDropDownToProPerties (_DataColumn.ColumnName);
        //            code += codedrp;
        //            if (codedrp != "")
        //            {
        //                continue;
        //            }
        //        }

        //        if ((_DataColumn.DataType.ToString() == "System.Guid"))
        //        { continue; }

        //        if ((_DataColumn.DataType.ToString() == "System.Int32"))
        //        {
        //            code += propertieName + " = Convert.ToInt32(" + controlTextBoxName + ".Text);" + _NewLine;
        //        }
        //        else if (_DataColumn.DataType.ToString() == "System.Decimal")
        //        {
        //            code += propertieName + " =  Convert.ToDecimal (" + controlTextBoxName + ".Text);" + _NewLine;
        //        }
        //        else if (_DataColumn.DataType.ToString() == "System.DateTime")
        //        {
        //            code += propertieName + " =StkGlobalDate.TextEnToDate(" + controlTextBoxName + ".Text);" + _NewLine;
        //        }
        //        else if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
        //        {
        //            // code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToInt16(" + controlChekBoxName  + ".Checked);" + _NewLine;
        //            code += propertieName + " =Convert.ToInt16(" + controlChekBoxName + ".Checked);" + _NewLine;
        //        }
        //        else
        //        {
        //            code += propertieName + " =  " + controlTextBoxName + ".Text;" + _NewLine;
        //        }
        //    }

        //    return code;
        //}

        //public Boolean HaveDropDown()
        //{
        //    bool _HaveDropDown = false;
        //    foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
        //    {
        //        foreach (MappingColumn _Map in _MappingColumn)
        //        {
        //            if ((_Map.ColumnName == _DataColumn.ColumnName) && (_Map.TableName != _TableName))
        //            {
        //                _HaveDropDown = true;
        //                break;
        //            }
        //        }
        //    }

        //    return _HaveDropDown;
        //}

        ///// <summary>
        ///// For Bind Data
        ///// </summary>
        ///// <param name="columnName"></param>
        ///// <returns></returns>
        //private string GenProtiesToDropDown(string columnName)
        //{
        //    string code = "";
        //    foreach (MappingColumn _Map in _MappingColumn)
        //    {
        //        if ((_Map.ColumnName == columnName) && (_Map.TableName != _TableName))
        //        {
        //            string controlDropDownName = string.Format(_formatDropDownName, columnName);
        //            string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
        //            //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
        //            code += string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}); ", controlDropDownName, propertieName) + _NewLine;
        //            code += " " + _NewLine;
        //            code += string.Format("if ({0}ListItem != null) ", controlDropDownName) + _NewLine;
        //            code += "{" + _NewLine;
        //            code += string.Format("{0}ListItem.Selected = true; ", controlDropDownName) + _NewLine;
        //            code += "};" + _NewLine;
        //            code +=  " "  + _NewLine;
        //            break;
        //        }
        //    }

        //    return code;
        //}

        //public  string GenMapDropDownToProPerties(string columnName)
        //{
        //    string code = "";
        //    foreach (MappingColumn _Map in _MappingColumn)
        //    {
        //        if ((_Map.ColumnName == columnName) && (_Map.TableName != _TableName))
        //        {
        //            string controlDropDownName = string.Format(_formatDropDownName, columnName);
        //            string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
        //            //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
        //            code = string.Format("{0} = {1}.SelectedValue; ", propertieName, controlDropDownName) + _NewLine;

        //            break;
        //        }
        //    }

        //    return code;
        //}

        //private string GenInnitDropDown()
        //{
        //    string code = "";

        //    if (HaveDropDown())
        //    {
        //        code += "private void BindDropDown() " + _NewLine;
        //        code += "{ " + _NewLine;

        //        foreach(DataColumn _DataColumn in _ds.Tables[0].Columns)
        //        {
        //            foreach (MappingColumn _Map in _MappingColumn)
        //            {
        //                if ((_Map.ColumnName == _DataColumn.ColumnName) && (_Map.TableName != _TableName))
        //                {
        //                    string controlDropDownName = string.Format(_formatDropDownName, _DataColumn.ColumnName);
        //                    code += string.Format("{0}Db _{0}Db = new {0}Db(); ",_TableName ) + _NewLine;
        //                    code += string.Format("{0}.DataSource =  _{1}Db.Select(); ", controlDropDownName,_TableName) + _NewLine;
        //                    code += string.Format("{0}.DataTextField = {1}Db.DataText;", controlDropDownName, _TableName) + _NewLine;
        //                    code += string.Format("{0}.DataValueField = {1}Db.DataValue;", controlDropDownName, _TableName) + _NewLine;

        //                    code += string.Format("{0}.DataBind();", controlDropDownName) + _NewLine;

        //                    code += string.Format("var {0}lt = new ListItem(\"please select\", \"0\"); ", controlDropDownName) + _NewLine;
        //                    code += string.Format("{0}.Items.Insert(0, {0}lt);", controlDropDownName) + _NewLine;

        //                    code += " " + _NewLine;
        //                }
        //            }

        //        }
        //        code += "}" + _NewLine;
        //    }

        //    return code;
        //}

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
                string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, _DataColumn.ColumnName);
                string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);
                //For Drop Down List
                if (_MappingColumn != null)
                {
                    string codedrp = GenProtiesToDropDown(_DataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                if (_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ColumnName)
                {
                    code += controlTextBoxName + ".Enabled = false;" + _NewLine;
                }

                if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                { code += controlChekBoxName + ".Checked = Convert.ToBoolean(" + propertieName + ");" + _NewLine; }
                else if ((_DataColumn.DataType.ToString() == "System.DateTime") || (_DataColumn.DataType.ToString() == "System.Decimal") || (_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    code += " if (" + propertieName + ".HasValue)" + _NewLine;
                    code += "{" + _NewLine;
                    code += controlTextBoxName + ".Text = StkGlobalDate.DateToTextEngFormat(" + propertieName + "); " + _NewLine;
                    code += "}" + _NewLine;
                }
                else
                { code += controlTextBoxName + ".Text = Stk_TextNull.StringTotext(" + propertieName + ".ToString()); " + _NewLine; }
            }

            //ซ่อนปุ่ม Save ตอนรับ Parameter มาจากหน้าอื่นๆ
            code += " btnSave.Visible = false;" + _NewLine;

            code += " } " + _NewLine;
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

            _code += GenInnitDropDown();
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