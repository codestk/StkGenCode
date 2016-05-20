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

        protected string GetColumnParameter(string format = "{0},")
        {
            string parameter = "";
            parameter = ColumnString.GenLineString(_ds, format);
            parameter = parameter.Trim(',');
            return parameter;
        }

        #region Map

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
                    string codedrp = MapDropDownToProPerties(dataColumn);
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

        protected string MapControlHtmlToValiable(DataSet _ds)
        {
            string columnParameter = ColumnString.GenLineString(_ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            string code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                string columnName = dataColumn.ColumnName;
                string propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                string controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                string controlDropDownName = string.Format(_formatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"var  {columnName} =$('#<%={controlDropDownName}.ClientID %>').val();" + _NewLine;
                }
                else
                if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {//chek bok
                    code += $"var  {columnName} =$('#<%={controlChekBoxName}.ClientID %>').val();" + _NewLine;
                }
                else
                {//input
                    code += $"var  {columnName} =$('#<%={controlTextBoxName}.ClientID %>').val();" + _NewLine;
                }
            }
            return code;
        }

        protected string MapJsonToProPerties(DataSet _ds, bool CommentKey = false)
        {
            string code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                string propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                //string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                string columName = dataColumn.ColumnName;

                if (CommentKey)
                {
                    bool primary = (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && _ds.Tables[0].PrimaryKey[0].AutoIncrement;

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }
                string formatChekEmtyp = "if (" + columName + "!= \"\") {0}" + _NewLine;

                string ConvertPattern = "";
                if (dataColumn.DataType.ToString() == "System.Guid")
                { continue; }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    ConvertPattern = propertieName + " = Convert.ToInt32(" + columName + ");";
                    code += string.Format(formatChekEmtyp, ConvertPattern) + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    ConvertPattern = propertieName + " =  Convert.ToDecimal (" + columName + ");";
                    code += string.Format(formatChekEmtyp, ConvertPattern) + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    ConvertPattern = propertieName + " =StkGlobalDate.TextEnToDate(" + columName + ");";
                    code += string.Format(formatChekEmtyp, ConvertPattern) + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    ConvertPattern = propertieName + " =Convert.ToInt16(" + columName + ");";
                    code += string.Format(formatChekEmtyp, ConvertPattern) + _NewLine;
                }
                else
                {
                    code += propertieName + " =  " + columName + "; " + _NewLine;
                }
            }

            return code;
        }

        #endregion Map

        #region DropDown

        public Boolean HaveDropDown()
        {
            bool haveDropDown = false;

            if (_MappingColumn == null)
                return haveDropDown;

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

        public Boolean IsDropDown(DataColumn Column)
        {
            bool isDropDown = false;

            if (_MappingColumn == null)
                return isDropDown;

            //foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            //{
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == Column.ColumnName) && (map.TableName != _TableName))
                {
                    isDropDown = true;
                    break;
                }
            }
            //}

            return isDropDown;
        }

        /// <summary>
        /// For Bind Data
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected string DropDownFindByValue(string columnName)
        {
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == columnName) && (map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, columnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
                    code += string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}.ToString()); ", controlDropDownName, propertieName) + _NewLine;
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

        public string MapDropDownToProPerties(DataColumn column)
        {
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == column.ColumnName) && (map.TableName != _TableName))
                {
                    string controlDropDownName = string.Format(_formatDropDownName, column.ColumnName);
                    string propertieName = string.Format(_formatpropertieName, _TableName, column.ColumnName);
                    //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
                    code += $"if ({controlDropDownName}.SelectedIndex > 0)" + _NewLine;

                    code += "{" + _NewLine;
                    if (column.DataType.ToString() == "System.Decimal")
                    {
                        code += $"{propertieName} = Convert.ToDecimal({controlDropDownName}.SelectedValue); " + _NewLine;
                    }
                    else if (column.DataType.ToString() == "System.Int32")
                    {//Convert.ToInt32
                        code += $"{propertieName} = Convert.ToInt32({controlDropDownName}.SelectedValue); " + _NewLine;
                    }
                    else if (column.DataType.ToString() == "System.Int16")
                    {//Convert.ToInt32
                        code += $"{propertieName} = Convert.ToInt16({controlDropDownName}.SelectedValue); " + _NewLine;
                    }
                    else
                    {
                        code += $"{propertieName} = {controlDropDownName}.SelectedValue; " + _NewLine;
                    }

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