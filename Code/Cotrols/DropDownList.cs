using StkGenCode.Code.Column;
using StkGenCode.Code.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Cotrols
{
    public class DropDownList  
    {
 
        private String MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        {
            string code = "";

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, _DataColumn.ColumnName);
                string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);

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

                //For Drop Down List
                if (_MappingColumn != null)
                {
                    string codedrp = GenMapDropDownToProPerties (_DataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                if ((_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    code += propertieName + " = Convert.ToInt32(" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += propertieName + " =  Convert.ToDecimal (" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += propertieName + " =StkGlobalDate.TextEnToDate(" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                {
                    // code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToInt16(" + controlChekBoxName  + ".Checked);" + _NewLine;
                    code += propertieName + " =Convert.ToInt16(" + controlChekBoxName + ".Checked);" + _NewLine;
                }
                else
                {
                    code += propertieName + " =  " + controlTextBoxName + ".Text;" + _NewLine;
                }
            }

            return code;
        }

        public Boolean HaveDropDown()
        {
            bool _HaveDropDown = false;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                foreach (MappingColumn _Map in _MappingColumn)
                {
                    if ((_Map.ColumnName == _DataColumn.ColumnName) && (_Map.TableName != _TableName))
                    {
                        _HaveDropDown = true;
                        break;
                    }
                }
            }

            return _HaveDropDown;
        }

        /// <summary>
        /// For Bind Data
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private string GenProtiesToDropDown(string columnName)
        {
            string code = "";
            foreach (MappingColumn _Map in _MappingColumn)
            {
                if ((_Map.ColumnName == columnName) && (_Map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, columnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
                    code += string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}); ", controlDropDownName, propertieName) + _NewLine;
                    code += " " + _NewLine;
                    code += string.Format("if ({0}ListItem != null) ", controlDropDownName) + _NewLine;
                    code += "{" + _NewLine;
                    code += string.Format("{0}ListItem.Selected = true; ", controlDropDownName) + _NewLine;
                    code += "};" + _NewLine;
                    code +=  " "  + _NewLine;
                    break;
                }
            }

            return code;
        }

        public  string GenMapDropDownToProPerties(string columnName)
        {
            string code = "";
            foreach (MappingColumn _Map in _MappingColumn)
            {
                if ((_Map.ColumnName == columnName) && (_Map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, columnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
                    code = string.Format("{0} = {1}.SelectedValue; ", propertieName, controlDropDownName) + _NewLine;

                    break;
                }
            }

            return code;
        }

        private string GenInnitDropDown()
        {
            string code = "";

            if (HaveDropDown())
            {
                code += "private void BindDropDown() " + _NewLine;
                code += "{ " + _NewLine;

                foreach(DataColumn _DataColumn in _ds.Tables[0].Columns)
                {

                    foreach (MappingColumn _Map in _MappingColumn)
                    {
                        if ((_Map.ColumnName == _DataColumn.ColumnName) && (_Map.TableName != _TableName))
                        {
                            string controlDropDownName = string.Format(_formatDropDownName, _DataColumn.ColumnName);
                            code += string.Format("{0}Db _{0}Db = new {0}Db(); ",_TableName ) + _NewLine;
                            code += string.Format("{0}.DataSource =  _{1}Db.Select(); ", controlDropDownName,_TableName) + _NewLine;
                            code += string.Format("{0}.DataTextField = {1}Db.DataText;", controlDropDownName, _TableName) + _NewLine;
                            code += string.Format("{0}.DataValueField = {1}Db.DataValue;", controlDropDownName, _TableName) + _NewLine;

                            code += string.Format("{0}.DataBind();", controlDropDownName) + _NewLine;

                            code += string.Format("var {0}lt = new ListItem(\"please select\", \"0\"); ", controlDropDownName) + _NewLine;
                            code += string.Format("{0}.Items.Insert(0, {0}lt);", controlDropDownName) + _NewLine;

                            code += " " + _NewLine;
                        }
                    }





                }
                code += "}" + _NewLine;
            }


            return code;
        }


    }
}
