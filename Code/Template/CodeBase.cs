using StkGenCode.Code.Column;
using StkGenCode.Code.Name;
using System.Collections.Generic;
using System.Data;

namespace StkGenCode.Code.Template
{
    public abstract class CodeBase
    {
        public DataSet _ds;
        public FileCode _FileCode;
        protected FileName _FileName;

        public string _NewLine = " \r\n";

        public string _NotImplement = "throw new Exception(\"Not implement\");";
        public string _TableName;

        /// <summary>
        ///     ใช้ สำหรับ Gen Code Dropdown list
        ///     ColumnName:Table
        /// </summary>
        public List<MappingColumn> _MappingColumn { get; set; }

        public abstract void Gen();

        protected void InnitProperties()
        {
            _FileName = new FileName
            {
                _TableName = _TableName,
                _ds = _ds
            };
        }

        protected string GetColumnParameter(string format = "{0},")
        {
            var parameter = ColumnString.GenLineString(_ds, format);
            parameter = parameter.Trim(',');
            return parameter;
        }

        #region Constant

        public string _formatpropertieName = "_{0}.{1}";
        public string _formatTextBoxName = "txt{0}";
        public string _formatChekBoxName = "chk{0}";
        public string _formatDropDownName = "drp{0}";

        #endregion Constant

        #region Map

        protected string MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        {
            var code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                var propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                var controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);

                if (CommentKey)
                {
                    var primary = (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) &&
                                  _ds.Tables[0].PrimaryKey[0].AutoIncrement;

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }

                //For Drop Down List
                if (_MappingColumn != null)
                {
                    var codedrp = MapDropDownToProPerties(dataColumn);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                if (dataColumn.DataType.ToString() == "System.Guid")
                {
                    continue;
                }

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
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
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

        protected string MapProPertiesToControl(DataSet _ds, bool CommentKey = false)
        {
            var columnParameter = ColumnString.GenLineString(_ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            var code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                var propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                var controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                var controlDropDownName = string.Format(_formatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"$('#<%={controlDropDownName}.ClientID %>').val({propertieName});" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok
                    code += $"$('#<%={controlChekBoxName}.ClientID %>').prop('checked', {propertieName}); " +
                            _NewLine;
                }
                else
                {
                    //input
                    code += $"$('#<%={controlTextBoxName}.ClientID %>').val({propertieName});" + _NewLine;
                }
            }
            return code;
        }

        protected string MapControlHtmlToValiable(DataSet _ds)
        {
            var columnParameter = ColumnString.GenLineString(_ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            var code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                var columnName = dataColumn.ColumnName;
                var controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                var controlDropDownName = string.Format(_formatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"var  {columnName} =$('#<%={controlDropDownName}.ClientID %>').val();" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok
                    code += $"var  {columnName} =$('#<%={controlChekBoxName}.ClientID %>').prop('checked');" +
                            _NewLine;
                }
                else
                {
                    //input
                    code += $"var  {columnName} =$('#<%={controlTextBoxName}.ClientID %>').val();" + _NewLine;
                }
            }
            return code;
        }

        protected string MapJsonToProPerties(DataSet _ds, bool CommentKey = false)
        {
            var code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                var propertieName = string.Format(_formatpropertieName, _TableName, dataColumn.ColumnName);
                //string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                var columName = dataColumn.ColumnName;

                if (CommentKey)
                {
                    var primary = (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) &&
                                  _ds.Tables[0].PrimaryKey[0].AutoIncrement;

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }
                var formatChekEmtyp = "if (" + columName + "!= \"\") {0}" + _NewLine;

                string convertPattern;
                if (dataColumn.DataType.ToString() == "System.Guid")
                {
                    continue;
                }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    convertPattern = propertieName + " = Convert.ToInt32(" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    convertPattern = propertieName + " =  Convert.ToDecimal (" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    convertPattern = propertieName + " =StkGlobalDate.TextEnToDate(" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + _NewLine;
                }

                //Json ไม่มี Bool ใน Json
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    convertPattern = "{string bit = (" + columName + " == \"true\" ? \"1\" : \"0\");" + _NewLine;
                    convertPattern += propertieName + " =Convert.ToInt16(bit);}" + _NewLine;
                    code += string.Format(formatChekEmtyp, convertPattern) + _NewLine;
                }
                else
                {
                    convertPattern = propertieName + " =  " + columName + "; " + _NewLine;
                    code += string.Format(formatChekEmtyp, convertPattern) + _NewLine;
                }
            }

            return code;
        }

        #endregion Map

        #region DropDown

        public bool HaveDropDown()
        {
            if (_MappingColumn == null)
                return false;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //foreach (MappingColumn map in _MappingColumn)
                //{
                //    if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                //    {
                //        haveDropDown = true;
                //        break;
                //    }
                //}
                var haveDropDown = IsDropDown(dataColumn);
                if (haveDropDown)
                    return true;
            }

            return false;
        }

        public bool IsDropDown(DataColumn column)
        {
            var isDropDown = false;

            if (_MappingColumn == null)
                return false;

            foreach (var map in _MappingColumn)
            {
                if ((map.ColumnName == column.ColumnName) && (map.TableName != _TableName))
                {
                    isDropDown = true;
                    break;
                }
            }

            return isDropDown;
        }

        /// <summary>
        ///     For Bind Data
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected string DropDownFindByValue(string columnName)
        {
            var code = "";
            foreach (var map in _MappingColumn)
            {
                if ((map.ColumnName == columnName) && (map.TableName != _TableName))
                {
                    var controlDropDownName = string.Format(_formatDropDownName, columnName);
                    var propertieName = string.Format(_formatpropertieName, _TableName, columnName);
                    //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
                    code +=
                        string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}.ToString()); ",
                            controlDropDownName, propertieName) + _NewLine;
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
            var code = "";
            foreach (var map in _MappingColumn)
            {
                if ((map.ColumnName == column.ColumnName) && (map.TableName != _TableName))
                {
                    var controlDropDownName = string.Format(_formatDropDownName, column.ColumnName);
                    var propertieName = string.Format(_formatpropertieName, _TableName, column.ColumnName);
                    //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
                    code += $"if ({controlDropDownName}.SelectedIndex > 0)" + _NewLine;

                    code += "{" + _NewLine;
                    if (column.DataType.ToString() == "System.Decimal")
                    {
                        code += $"{propertieName} = Convert.ToDecimal({controlDropDownName}.SelectedValue); " + _NewLine;
                    }
                    else if (column.DataType.ToString() == "System.Int32")
                    {
                        //Convert.ToInt32
                        code += $"{propertieName} = Convert.ToInt32({controlDropDownName}.SelectedValue); " + _NewLine;
                    }
                    else if (column.DataType.ToString() == "System.Int16")
                    {
                        //Convert.ToInt32
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
            var code = "";

            if (HaveDropDown())
            {
                code += "private void BindDropDown() " + _NewLine;
                code += "{ " + _NewLine;

                foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
                {
                    foreach (var map in _MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                        {
                            //Table MAster Of Drop DownList
                            var dropDownTableName = map.TableName;
                            var controlDropDownName = string.Format(_formatDropDownName, dataColumn.ColumnName);
                            code += string.Format("{0}Db _{0}Db = new {0}Db(); ", dropDownTableName) + _NewLine;
                            code += $"{controlDropDownName}.DataSource =  _{dropDownTableName}Db.Select(); " + _NewLine;
                            code += $"{controlDropDownName}.DataTextField = {dropDownTableName}Db.DataText;" + _NewLine;
                            code += $"{controlDropDownName}.DataValueField = {dropDownTableName}Db.DataValue;" +
                                    _NewLine;

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