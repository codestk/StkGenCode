using StkGenCode.Code.Column;
using StkGenCode.Code.Name;
using System.Collections.Generic;
using System.Data;
using StkGenCode.Code.Label;

namespace StkGenCode.Code.Template
{
    public abstract class CodeBase
    {
        public DataSet Ds;
        public FileCode FileCode;
        protected FileName FileName;

        public string NewLine = "\r\n";

        public string NotImplement = "throw new Exception(\"Not implement\");";
        public string TableName;

        public static List<string> ExceptionType = new List<string>(
new[] { "System.Byte[]", "System.Guid" });

         

        public TextTemplate LabelTemplate { get; set; }

        /// <summary>
        /// ใช้ไม่ให้แสดงบาง Column
        /// </summary>
        ///
        ///  int _number;
        private List<string> _exceptionColumn;

        public List<string> ExceptionColumn
        {
            get
            {
                if (_exceptionColumn == null)
                {
                    _exceptionColumn = new List<string>();
                    _exceptionColumn.Add("");
                }

                return _exceptionColumn;
            }
            set
            {
                _exceptionColumn = value;
            }
        }

        /// <summary>
        ///     ใช้ สำหรับ Gen Code Dropdown list
        ///     ColumnName:Table
        /// </summary>
        public List<MappingColumn> DropColumns { get; set; }

        public abstract void Gen();

        protected void InnitProperties()
        {
            FileName = new FileName
            {
                TableName = TableName,
                Ds = Ds
            };
        }

        protected string GetColumnParameter(string format = "{0},")
        {
            var parameter = ColumnString.GenLineString(Ds, format);
            parameter = parameter.Trim(',');
            return parameter;
        }

        #region Controls

        /// <summary>
        ///     Refer CSS col s12  เป็น Base
        /// </summary>
        /// <param name="columnSize"></param>
        /// <param name="chekPrimarykey"></param>
        /// <param name="exceptionColumne"></param>
        /// <returns></returns>
        public string GenControls(int columnSize, bool chekPrimarykey, bool exceptionColumne)
        {
            var code = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                //ยอมให้ Data Type Byte[] ผ่าน
                //ไม่ต้อง AddControl ที่เป็น column ยกเว้น

                //ไม่ใช้ Exception Tableเพราะใช้ในการ ค้นหา
                // if ((ExceptionType.Contains(dataColumn.DataType.ToString())) && (dataColumn.DataType.ToString() != "System.Byte[]"))
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()) || ExceptionColumn.Contains(dataColumn.ColumnName))

                //{
                //   continue;
                //}

                var label =  dataColumn.ColumnName;
                if (LabelTemplate != null)
                {
                    label = LabelTemplate.GetLabel(dataColumn.ColumnName);

                }

                if (IsExceptionColumn(dataColumn, exceptionColumne))
                {
                    continue;
                }

                var disabled = "";

                var controlTextBoxName = string.Format(ControlName.FormatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(ControlName.FormatChekBoxName, dataColumn.ColumnName);

                string currentControl = "";
                if (DropColumns != null)
                {
                    var codedrp = GenDropDown(dataColumn.ColumnName, columnSize);

                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                var isPrimayKey = false;
                if (chekPrimarykey)
                {
                    //Check แบบ KEy เดียว
                    if ((dataColumn.ColumnName == Ds.Tables[0].PrimaryKey[0].ToString()) &&
                        Ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    {
                        isPrimayKey = true;
                    }
                }

                //ถ้าเป็น Byte[] เป็นรูปไม่ต้องใส่ Column
                if (dataColumn.DataType.ToString() != "System.Byte[]")
                {
                    if (isPrimayKey)
                    {
                        disabled = "ReadOnly=\"true\" ";

                        code += $"<div class=\"  col s{columnSize}\"> " + NewLine;
                        code += "<label>" + label + " </label> " + NewLine;
                        //code += "<label>" +   LabelTemplate.GetLabel( dataColumn.ColumnName) + " </label> " + NewLine;
                         

                    }
                    else
                    {
                        code += $"<div class=\"input-field col s{columnSize}\"> " + NewLine;
                    }
                }

                //CssClass = "datepicker" type = "date"
                if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    //CssClass = "datepicker" type = "date"
                    currentControl = controlTextBoxName;
                    //code += "<asp:TextBox " + disabled + " ID=\"" + controlTextBoxName +
                    //        "\" CssClass=\"datepicker\" type =\"date\" runat=\"server\"></asp:TextBox>" + NewLine;

                    code += "<input " + disabled + " id=\"" + controlTextBoxName +
                          "\"  class=\"datepicker\" type =\"date\"   />" + NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    currentControl = controlChekBoxName;
                    //code += "<asp:CheckBox " + disabled + " ID=\"" + controlChekBoxName +
                    //        "\"    runat=\"server\"></asp:CheckBox>" + NewLine;
                    code += "<input " + disabled + " id=\"" + controlChekBoxName +
                          "\"    type=\"checkbox\" />" + NewLine;
                    // <input type="checkbox" id="test5" />
                }
                else if (dataColumn.DataType.ToString() == "System.String")
                {
                    currentControl = controlTextBoxName;

                    code += "<input " + disabled + " id=\"" + controlTextBoxName + "\" type=\"text\" data-column-id=\"" +
                           dataColumn.ColumnName + "\"  class=\"validate " + dataColumn.ColumnName +
                           "\"   length=\"" + dataColumn.MaxLength +
                           "\"   maxlength=\"" + dataColumn.MaxLength + "\"                /> " + NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    var max = 9;
                    currentControl = controlTextBoxName;

                    code += "<input " + disabled + " id=\"" + controlTextBoxName + "\" type=\"text\" data-column-id=\"" +
                            dataColumn.ColumnName + "\"  Class=\"validate " + dataColumn.ColumnName +
                            "\" length=\"" + max + "\"        maxlength=\"" + max + "\"     />" +
                            NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Byte[]")
                {
                    //กรณีที่เป็นรูป
                    code += "" + NewLine;
                    code += "            <div id=\"drop-area\"  style=\"display:none\">" + NewLine;
                    code += "                <div id=\"drop-area-detail\">" + NewLine;
                    code += "" + NewLine;
                    code += "                    <h3 class=\"drop-text\">Drag and Drop Images Here</h3>" + NewLine;
                    code += "" + NewLine;
                    code += "                    <div class=\"progressUpload\">" + NewLine;
                    code += "                        <div class=\"bar\"></div>" + NewLine;
                    code += "                        <div class=\"percent\">0%</div>" + NewLine;
                    code += "                    </div>" + NewLine;
                    code += "                </div>" + NewLine;
                    code += "                <div id=\"drop-area-preview\" style=\"display: none\">" + NewLine;
                    code += "                    <img id=\"imgPreview\"   height=\"131\" width=\"174\" alt=\"Image preview...\">" + NewLine;
                    code += "                    <img id=\"imgRemove\" src=\"Images/Close.png\" />" + NewLine;
                    code += "                </div>" + NewLine;
                    code += "                <div id=\"status\"></div>" + NewLine;
                    code += "            </div>" + NewLine;
                }
                else
                {
                    currentControl = controlTextBoxName;

                    code += "<input  id =\"" + controlTextBoxName + "\" type=\"text\" class=\"validate\" " + disabled + "  >" + NewLine;
                }

                //เป็นรูปไม่ต้องใส่ Label
                if (dataColumn.DataType.ToString() != "System.Byte[]")
                {
                    if (isPrimayKey == false)
                    {
                        
                        code += "<label for=\"" + currentControl + "\">" + label +
                                " </label> " + NewLine;
                        //code += "<label for=\"" + currentControl + "\">" + dataColumn.ColumnName +
                        //     " </label> " + NewLine;
                    }

                    code += " </div> " + NewLine;
                }
            }

            return code;
        }

        #endregion Controls

        #region Validate

        public string ForceNumberTextBox(bool checkprimary)
        {
            var code = "";
            code += "function ForceNumberTextBox() " + NewLine;
            code += "{" + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                //ถ้าเป็น DropDown ไม่ต้อง Force ตัวเลข
                if (IsDropDown(dataColumn))
                    continue;

                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                var controlTextBoxName = string.Format(ControlName.FormatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);

                if (checkprimary)
                {
                    if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName) &&
                        Ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    {
                        continue;
                    }
                }

                if ((dataColumn.DataType.ToString() == "System.Guid") ||
                    (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    continue;
                }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += "$(\"#" + controlTextBoxName + "\").ForceNumericOnly();" + NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += "$(\"#" + controlTextBoxName + "\").ForceNumericOnly2Digit();" + NewLine;
                }
            }

            code += "}" + NewLine;

            return code;
        }

        #endregion Validate

        #region Map

        protected string MapProPertiesToControl(DataSet ds, bool commentKey = false)
        {
            var code = "";

            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}
                if (IsExceptionColumn(dataColumn, true))
                {
                    continue;
                }

                var propertieName = string.Format(ControlName.FormatpropertieName, TableName, dataColumn.ColumnName);
                var controlTextBoxName = string.Format(ControlName.FormatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(ControlName.FormatChekBoxName, dataColumn.ColumnName);
                var controlDropDownName = string.Format(ControlName.FormatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"$('#{controlDropDownName}').val({propertieName});" + NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok
                    code += $"$('#{controlChekBoxName}').prop('checked', {propertieName}); " +
                            NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += $"var $input = $('#{controlTextBoxName}').pickadate() " +
                            NewLine;

                    code += $"// Use the picker object directly. " +
                            NewLine;
                    code += $"var picker = $input.pickadate('picker'); " +
                            NewLine;
                    code += $"picker.set('select',{propertieName}) " +
                            NewLine;
                }
                else
                {
                    //input
                    code += $"$('#{controlTextBoxName}').val({propertieName});" + NewLine;
                }
            }
            return code;
        }

        protected string MapControlHtmlToValiable(DataSet ds, bool useExcptionColumn)
        {
            var code = "";

            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                //ไม่ต้อง Add Mapp ถ้าเป็น Type ต้องห้ามกับ Column ที่เป็น Auto
                if (ExceptionType.Contains(dataColumn.DataType.ToString()) || (ExceptionColumn.Contains(dataColumn.ColumnName)) && (useExcptionColumn))
                {
                    continue;
                }

                var columnName = dataColumn.ColumnName;
                var controlTextBoxName = string.Format(ControlName.FormatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(ControlName.FormatChekBoxName, dataColumn.ColumnName);
                var controlDropDownName = string.Format(ControlName.FormatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"var  {columnName} =$('#{controlDropDownName}').val();" + NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok
                    code += $"var  {columnName} =$('#{controlChekBoxName}').prop('checked');" +
                            NewLine;
                }
                else
                {
                    //input
                    code += $"var  {columnName} =$('#{controlTextBoxName}').val();" + NewLine;
                }
            }
            return code;
        }

        protected string MapJsonToProPerties(DataSet ds, bool commentKey, bool ExceptionColumn)
        {
            var code = "";

            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                var propertieName = string.Format(ControlName.FormatpropertieName, TableName, dataColumn.ColumnName);

                var columName = dataColumn.ColumnName;
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()) || ExceptionColumn.Contains(dataColumn.ColumnName))
                //{
                //    continue;
                //}
                if (IsExceptionColumn(dataColumn, ExceptionColumn))
                {
                    continue;
                }
                if (commentKey)
                {
                    var primary = (dataColumn.ColumnName == ds.Tables[0].PrimaryKey[0].ToString()) &&
                                  ds.Tables[0].PrimaryKey[0].AutoIncrement;

                    if (primary)
                    {
                        // continue;
                        code += "// ";
                    }
                }
                var formatChekEmtyp = "if (" + columName + "!= \"\") {0}" + NewLine;

                string convertPattern;
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    convertPattern = propertieName + " = Convert.ToInt32(" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    convertPattern = propertieName + " =  Convert.ToDecimal (" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    convertPattern = propertieName + " =StkGlobalDate.TextEnToDate(" + columName + ");";
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }

                //Json ไม่มี Bool ใน Json
                //else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                //         (dataColumn.DataType.ToString() == "System.Int16"))
                //{
                //    convertPattern = "{string bit = (" + columName + " == \"true\" ? \"1\" : \"0\");" + NewLine;
                //    convertPattern += propertieName + " =Convert.ToInt16(bit);}" + NewLine;
                //    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                //}
                else if ((dataColumn.DataType.ToString() == "System.Boolean")
                       )
                {
                    //convertPattern = "{string bit = (" + columName + " == \"1\" ? \"1\" : \"0\");" + NewLine;

                    convertPattern = "{int bit = (" + columName + " == \"true\" ? 1 : 0);" + NewLine;
                    convertPattern += propertieName + " =Convert.ToBoolean(bit);}" + NewLine;
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                       (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    convertPattern = "{int bit = (" + columName + " == \"true\" ? 1 : 0);" + NewLine;
                    convertPattern += propertieName + " =Convert.ToInt16(bit);}" + NewLine;
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }
                else
                {
                    convertPattern = propertieName + " =  " + columName + "; " + NewLine;
                    code += string.Format(formatChekEmtyp, convertPattern) + NewLine;
                }
            }

            return code;
        }

        #endregion Map

        #region DropDown

        public string BindSelectOption()
        {
            var code = "";
            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
                {
                    foreach (var map in DropColumns)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != TableName))
                        {
                            //SetSelectCategory('#drpTermId');
                            code += $"SetSelect{dataColumn.ColumnName}('#drp{dataColumn.ColumnName}');" +
                                    NewLine;
                        }
                    }
                }
                //code += "$('select').material_select(); " + NewLine;
            }

            return code;
        }

        public string SetSelect()
        {
            var code = "";
            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
                {
                    foreach (var map in DropColumns)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != TableName))
                        {
                            code += " function SetSelect" + dataColumn.ColumnName + "(control) {" + NewLine;
                            code += "" + NewLine;
                            code += "var innitOption = '<option value=\"\">Please Select</option>';" + NewLine;
                            code += $"var result{map.TableName} = {map.TableName}Service.SelectAll();" + NewLine;
                            code += "$(control).append(innitOption);" + NewLine;
                            code += "$.each(result" + map.TableName + ", function (index, value) {" + NewLine;
                            code += "//Appending the json items to the dropdown (select tag)" + NewLine;
                            code += "//item is the id of your select tag" + NewLine;
                            code += "" + NewLine;
                            code += "var text = value.text;" + NewLine;
                            code += "var value = value.value;" + NewLine;
                            code += "" + NewLine;
                            code += "$(control).append('<option value=\"' + value + '\">' + text + '</option>');" +
                                    NewLine;
                            code += "" + NewLine;
                            code += "});" + NewLine;
                            code += "" + NewLine;
                            code += "   " + NewLine;
                            code += "" + NewLine;
                            code += "}" + NewLine;
                        }
                    }
                }
            }
            else
            {
                code += "//No drop/." + NewLine;
            }
            return code;
        }


        private string  GetLabel(string columnName)
        {

            string label = columnName;
            if (LabelTemplate != null)
            { label = LabelTemplate.GetLabel(columnName);}

            return label;

        }

        public string GenDropDown(string columnname, int columnSize)
        {
            var controlDropDownName = string.Format(ControlName.FormatDropDownName, columnname);

            var code = "";
            foreach (var map in DropColumns)
            {
                if ((map.ColumnName == columnname) && (map.TableName != TableName))
                {
                    code += "<div class=\"input-field col s" + columnSize + "\"> " + NewLine;
                    code += "<select id=\"" + controlDropDownName + "\" > " + NewLine;
                    code += " " + NewLine;
                    code += "</select>" + NewLine;
                    code += "<label for=\"" + controlDropDownName + "\">" + GetLabel(columnname) + "</label>" +
                            NewLine;

                    //code += "<label for=\"" + controlDropDownName + "\">" + columnname + "</label>" +
                    //        NewLine;
                    code += "</div>" + NewLine;

                    break;
                }
            }

            return code;
        }

        public bool HaveDropDown()
        {
            if (DropColumns == null)
                return false;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                var haveDropDown = IsDropDown(dataColumn);
                if (haveDropDown)
                    return true;
            }

            return false;
        }

        public bool IsDropDown(DataColumn column)
        {
            var isDropDown = false;

            if (DropColumns == null)
                return false;

            foreach (var map in DropColumns)
            {
                if ((map.ColumnName == column.ColumnName) && (map.TableName != TableName))
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
        //protected string DropDownFindByValue(string columnName)
        //{
        //    var code = "";
        //    foreach (var map in DropColumns)
        //    {
        //        if ((map.ColumnName == columnName) && (map.TableName != TableName))
        //        {
        //            var controlDropDownName = string.Format(ControlName.FormatDropDownName, columnName);
        //            var propertieName = string.Format(ControlName.FormatpropertieName, TableName, columnName);
        //            //code = string.Format("{0}.Items.FindByValue({1}).Selected = true; ", controlDropDownName, propertieName);
        //            code +=
        //                string.Format("ListItem {0}ListItem = {0}.Items.FindByValue({1}.ToString()); ",
        //                    controlDropDownName, propertieName) + NewLine;
        //            code += " " + NewLine;
        //            code += $"if ({controlDropDownName}ListItem != null) " + NewLine;
        //            code += "{" + NewLine;
        //            code += $"{controlDropDownName}ListItem.Selected = true; " + NewLine;
        //            code += "};" + NewLine;
        //            code += " " + NewLine;
        //            break;
        //        }
        //    }

        //    return code;
        //}

        //public string MapDropDownToProPerties(DataColumn column)
        //{
        //    var code = "";
        //    foreach (var map in DropColumns)
        //    {
        //        if ((map.ColumnName == column.ColumnName) && (map.TableName != TableName))
        //        {
        //            var controlDropDownName = string.Format(ControlName.FormatDropDownName, column.ColumnName);
        //            var propertieName = string.Format(ControlName.FormatpropertieName, TableName, column.ColumnName);
        //            //mpoOrder.CUS_ID = drpCustomer.SelectedValue;
        //            code += $"if ({controlDropDownName}.SelectedIndex > 0)" + NewLine;

        //            code += "{" + NewLine;
        //            if (column.DataType.ToString() == "System.Decimal")
        //            {
        //                code += $"{propertieName} = Convert.ToDecimal({controlDropDownName}.SelectedValue); " + NewLine;
        //            }
        //            else if (column.DataType.ToString() == "System.Int32")
        //            {
        //                //Convert.ToInt32
        //                code += $"{propertieName} = Convert.ToInt32({controlDropDownName}.SelectedValue); " + NewLine;
        //            }
        //            else if (column.DataType.ToString() == "System.Int16")
        //            {
        //                //Convert.ToInt32
        //                code += $"{propertieName} = Convert.ToInt16({controlDropDownName}.SelectedValue); " + NewLine;
        //            }
        //            else
        //            {
        //                code += $"{propertieName} = {controlDropDownName}.SelectedValue; " + NewLine;
        //            }

        //            code += "}" + NewLine;

        //            //code = string.Format("{0} = {1}.SelectedValue; ", propertieName, controlDropDownName) + _NewLine;

        //            break;
        //        }
        //    }

        //    return code;
        //}

        //protected string GenInnitDropDown()
        //{
        //    var code = "";

        //    if (HaveDropDown())
        //    {
        //        code += "private void BindDropDown() " + NewLine;
        //        code += "{ " + NewLine;

        //        foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
        //        {
        //            foreach (var map in DropColumns)
        //            {
        //                if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != TableName))
        //                {
        //                    //Table MAster Of Drop DownList
        //                    var dropDownTableName = map.TableName;
        //                    var controlDropDownName = string.Format(ControlName.FormatDropDownName, dataColumn.ColumnName);
        //                    code += string.Format("{0}Db _{0}Db = new {0}Db(); ", dropDownTableName) + NewLine;
        //                    code += $"{controlDropDownName}.DataSource =  _{dropDownTableName}Db.Select(); " + NewLine;
        //                    code += $"{controlDropDownName}.DataTextField = {dropDownTableName}Db.DataText;" + NewLine;
        //                    code += $"{controlDropDownName}.DataValueField = {dropDownTableName}Db.DataValue;" +
        //                            NewLine;

        //                    code += $"{controlDropDownName}.DataBind();" + NewLine;

        //                    code += $"var {controlDropDownName}lt = new ListItem(\"please select\", \"0\"); " + NewLine;
        //                    code += string.Format("{0}.Items.Insert(0, {0}lt);", controlDropDownName) + NewLine;

        //                    code += " " + NewLine;
        //                }
        //            }
        //        }
        //        code += "}" + NewLine;
        //    }

        //    return code;
        //}

        #endregion DropDown

        #region Image

        public bool HavePicture()
        {
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (dataColumn.DataType.ToString() == "System.Byte[]")
                    return true;
            }

            return false;
        }

        public string GetColumnPicture()
        {
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (dataColumn.DataType.ToString() == "System.Byte[]")
                    return dataColumn.ColumnName.ToString();
            }
            return "";
        }

        #endregion Image

        public bool IsExceptionColumn(DataColumn dataColumn, bool exColumnDisplay)
        {
            if (ExceptionType.Contains(dataColumn.DataType.ToString()) || (ExceptionColumn.Contains(dataColumn.ColumnName)) && exColumnDisplay)
            {
                return true;
            }

            return false;
        }
    }
}