using StkGenCode.Code.Column;
using StkGenCode.Code.Name;
using System;
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

        public string _formatpropertieName = "_{0}.{1}";
        public string _formatTextBoxName = "txt{0}";
        public string _formatChekBoxName = "chk{0}";
        public string _formatDropDownName = "drp{0}";

        #endregion Constant

        /// <summary>
        /// ใช้ สำหรับ Gen Code Dropdown list
        /// ColumnName:Table
        /// </summary>

        public List<MappingColumn> _MappingColumn { get; set; }

        public abstract void Gen();

        protected void InnitProperties()
        {
            _FileName = new FileName();
            _FileName._TableName = _TableName;
            _FileName._ds = _ds;
        }

        #region DropDown

        protected string MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        {
            string code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                string propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                string controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);

                if (CommentKey)
                {
                    bool primary = (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && _ds.Tables[0].PrimaryKey[0].AutoIncrement;

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }

                //For Drop Down List
                if (_MappingColumn != null)
                {
                    string codedrp = GenMapDropDownToProPerties(dataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                if (dataColumn.DataType.ToString() == "System.Guid")
                { continue; }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += propertieName + " = Convert.ToInt32(" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += propertieName + " =  Convert.ToDecimal (" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += propertieName + " =StkGlobalDate.TextEnToDate(" + controlTextBoxName + ".Text);" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
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
            bool haveDropDown = false;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                foreach (MappingColumn map in _MappingColumn)
                {
                    if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                    {
                        haveDropDown = true;
                        break;
                    }
                }
            }

            return haveDropDown;
        }

        /// <summary>
        /// For Bind Data
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected string GenProtiesToDropDown(string columnName)
        {
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == columnName) && (map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, columnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
                    code += string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}); ", controlDropDownName, propertieName) + _NewLine;
                    code += " " + _NewLine;
                    code += $"if ({controlDropDownName}ListItem != null) " + _NewLine;
                    code += "{" + _NewLine;
                    code += $"{controlDropDownName}ListItem.Selected = true; " + _NewLine;
                    code += "};" + _NewLine;
                    code += " " + _NewLine;
                    break;
                }
            }

            return code;
        }

        public string GenMapDropDownToProPerties(string columnName)
        {
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == columnName) && (map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, columnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
                    code += $"if ({controlDropDownName}.SelectedIndex > 0)" + _NewLine;

                    code += "{" + _NewLine;
                    code += $"{propertieName} = {controlDropDownName}.SelectedValue; " + _NewLine;
                    code += "}" + _NewLine;

                    //code = string.Format("{0} = {1}.SelectedValue; ", propertieName, controlDropDownName) + _NewLine;

                    break;
                }
            }

            return code;
        }

        protected string GenInnitDropDown()
        {
            string code = "";

            if (HaveDropDown())
            {
                code += "private void BindDropDown() " + _NewLine;
                code += "{ " + _NewLine;

                foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
                {
                    foreach (MappingColumn map in _MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                        {
                            //Table MAster Of Drop DownList
                            string dropDownTableName = map.TableName;
                            string controlDropDownName = string.Format(_formatDropDownName, dataColumn.ColumnName);
                            code += string.Format("{0}Db _{0}Db = new {0}Db(); ", dropDownTableName) + _NewLine;
                            code += $"{controlDropDownName}.DataSource =  _{dropDownTableName}Db.Select(); " + _NewLine;
                            code += $"{controlDropDownName}.DataTextField = {dropDownTableName}Db.DataText;" + _NewLine;
                            code += $"{controlDropDownName}.DataValueField = {dropDownTableName}Db.DataValue;" + _NewLine;

                            code += $"{controlDropDownName}.DataBind();" + _NewLine;

                            code += $"var {controlDropDownName}lt = new ListItem(\"please select\", \"0\"); " + _NewLine;
                            code += string.Format("{0}.Items.Insert(0, {0}lt);", controlDropDownName) + _NewLine;

                            code += " " + _NewLine;
                        }
                    }
                }
                code += "}" + _NewLine;
            }

            return code;
        }

        #endregion DropDown
    }
}