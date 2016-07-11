using StkGenCode.Code.Column;
using StkGenCode.Code.Name;
using StkGenCode.Code.Template.Format;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxFromCode : CodeBase
    {
        private string GenHeadeFile()
        {
            var code = "<%@ Page Title=\"" + TableName +
                       "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" +
                       TableName + "Web.aspx.cs\" Inherits=\"" + TableName + "Web\" %>" + NewLine;

            return code;
        }

        protected string GenContentHeadBegin()
        {
            var code = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">  " + NewLine;

            return code;
        }

        protected string GenContentHeadEnd()
        {
            var code = "";
            code += "</asp:Content> " + NewLine;
            return code;
        }

        protected string GenSrciptModal()
        {
            var code = "$('.modal-trigger').leanModal();";
            return code;
        }

        //Method JavaScript ====================================================
        //public string ForceNumberTextBox()
        //{
        //    string code = "";
        //    code += "function ForceNumberTextBox() " + _NewLine;
        //    code += "{" + _NewLine;

        //    foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
        //    {
        //        //ถ้าเป็น DropDown ไม่ต้อง Force ตัวเลข
        //        if (IsDropDown(dataColumn))
        //            continue;

        //        //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
        //        string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
        //        //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
        //        //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);

        //        if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
        //        {
        //            continue;
        //        }

        //        if ((dataColumn.DataType.ToString() == "System.Guid") || (dataColumn.DataType.ToString() == "System.Int16"))
        //        { continue; }

        //        if (dataColumn.DataType.ToString() == "System.Int32")
        //        {
        //            code += "$(\"#" + controlTextBoxName + "\").ForceNumericOnly();" + _NewLine;
        //        }
        //        else if (dataColumn.DataType.ToString() == "System.Decimal")
        //        {
        //            code += "$(\"#" + controlTextBoxName + "\").ForceNumericOnly2Digit();" + _NewLine;
        //        }
        //    }

        //    code += "}" + _NewLine;

        //    return code;
        //}

        private string GenValidateDropDown(string columnname)
        {
            var controlDropDownName = string.Format(NameMing.FormatDropDownName, columnname);
            var code = "";
            foreach (var map in MappingColumn)
            {
                if ((map.ColumnName == columnname) && (map.TableName != TableName))
                {
                    code += "if(($(\"#" + controlDropDownName + "\").prop('selectedIndex')==0)||($(\"#" +
                            controlDropDownName + "\").prop('selectedIndex')==-1)){" + NewLine;
                    code += "output=false;" + NewLine;
                    code += "" + NewLine;
                    code += "$(\"#" + controlDropDownName + "\").prev().prev().addClass('CustomInvalid');" +
                            NewLine;
                    code += "" + NewLine;
                    code += "}" + NewLine;
                    code += "else { " + NewLine;
                    code += "$(\"#" + controlDropDownName +
                            "\").prev().prev().removeClass('CustomInvalid');" + NewLine;
                    code += "$(\"#" + controlDropDownName + "\").prev().prev().addClass('CustomValid');" +
                            NewLine;
                    code += " " + NewLine;
                    code += " " + NewLine;
                    code += "}" + NewLine;
                    break;
                }
            }

            return code;
        }

        //

        public string Validate()
        {
            var code = "";
            code += "function Validate() {" + NewLine;
            code += "var output=true;" + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                var controlTextBoxName = string.Format(NameMing.FormatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(_formatDropDownName, _DataColumn.ColumnName);

                if (dataColumn.ColumnName == Ds.Tables[0].PrimaryKey[0].ToString())
                {
                    if (Ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    {
                        continue;
                    }
                }

                if ((dataColumn.DataType.ToString() == "System.Guid") ||
                    (dataColumn.DataType.ToString() == "System.Boolean") ||
                    (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    continue;
                }

                //For Dropw
                if (MappingColumn != null)
                {
                    var codedrp = GenValidateDropDown(dataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                //ForText Box
                code += "if (CheckEmtyp($(\"#" + controlTextBoxName + "\"))) output = false;" + NewLine;
            }

            code += "if (output == false)" + NewLine;
            code += "Materialize.toast('please validate your input.', 3000, 'toastCss');" + NewLine;
            code += "return output;" + NewLine;
            code += "}" + NewLine;
            return code;
        }

        public string GenJavaScriptConfirm()
        {
            var code = "";

            code += "function Confirm() { " + NewLine;
            code += "$('#modalConfirm').openModal(); " + NewLine;
            code += "return false; " + NewLine;
            code += "}" + NewLine;

            return code;
        }

        private string GenJavaScriptSave()
        {
            var columnParameter = ColumnString.GenLineString(Ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            var code = "";
            code += "function Save() {" + NewLine;

            code += " if (Validate() == false) { return false; }" + NewLine;

            code += MapControlHtmlToValiable(Ds);

            var controlTextBoxPrimay = string.Format(NameMing.FormatTextBoxName, Ds.Tables[0].PrimaryKey[0]);
            code += $"var result = {TableName}Service.Save({columnParameter});" + NewLine;
            code += "" + NewLine;

            code += "  if (result != null) {" + NewLine;
            code += "" + NewLine;
            code += "Materialize.toast('Your data has been saved.', 3000, 'toastCss');" + NewLine;
            code += $" $('#{controlTextBoxPrimay}').val(result);" + NewLine;

            code += "$('#btnSave').hide();" + NewLine;

            code += "$('#btnUpdate').show();" + NewLine;
            code += "$('#btnDelete').show();" + NewLine;
            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "Materialize.toast(MsgError, 5000, 'toastCss');" + NewLine;
            code += "}" + NewLine;

            code += "} " + NewLine;
            return code;
        }

        private string GenJavaScriptUpdate()
        {
            var columnParameter = ColumnString.GenLineString(Ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            var code = "";
            code += "function Update() {" + NewLine;

            code += " if (Validate() == false) { return false; }" + NewLine;
            code += MapControlHtmlToValiable(Ds);

            code += $"var result = {TableName}Service.Update({columnParameter});" + NewLine;
            code += "" + NewLine;

            code += "  if (result != null) {" + NewLine;
            code += "" + NewLine;
            code += "Materialize.toast('Your data has been saved.', 3000, 'toastCss');" + NewLine;

            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "Materialize.toast(MsgError, 5000, 'toastCss');" + NewLine;
            code += "}" + NewLine;

            code += "} " + NewLine;
            return code;
        }

        private string GenJavaScriptDelete()
        {
            var columnParameter = ColumnString.GenLineString(Ds, "{0},");
            columnParameter = columnParameter.TrimEnd(',');
            var code = "";
            code += "function Delete() {" + NewLine;

            code += " if (Validate() == false) { return false; }" + NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    continue;
                }

                var columnName = dataColumn.ColumnName;
                var controlTextBoxName = string.Format(NameMing.FormatTextBoxName, dataColumn.ColumnName);
                var controlChekBoxName = string.Format(NameMing.FormatChekBoxName, dataColumn.ColumnName);
                var controlDropDownName = string.Format(NameMing.FormatDropDownName, dataColumn.ColumnName);

                if (IsDropDown(dataColumn))
                {
                    code += $"var  {columnName} =$('#{controlDropDownName}').val();" + NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                         (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok
                    code += $"var  {columnName} =$('#{controlChekBoxName}').val();" + NewLine;
                }
                else
                {
                    //input
                    code += $"var  {columnName} =$('#{controlTextBoxName}').val();" + NewLine;
                }
            }
            code += $"var result = {TableName}Service.Delete({columnParameter});" + NewLine;
            code += "" + NewLine;

            code += "  if (result != null) {" + NewLine;
            code += "" + NewLine;
            code += "Materialize.toast('Your data has been saved.', 3000, 'toastCss');" + NewLine;
            //code += $" $('#{controlTextBoxPrimay}').val(result);" + _NewLine;
            code += "$('#modalConfirm').closeModal();" + NewLine;

            code += "   setInterval(function () { this.close() }, 2000);" + NewLine;
            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "Materialize.toast(MsgError, 5000, 'toastCss');" + NewLine;
            code += "}" + NewLine;

            code += "} " + NewLine;
            return code;
        }

        private string BindQueryString()
        {
            var code = "";
            code += "function BindQueryString() {" + NewLine;
            code += "" + NewLine;
            code += $"var {Ds.Tables[0].PrimaryKey[0]} = GetQueryString('Q');" + NewLine;
            code += "if (" + Ds.Tables[0].PrimaryKey[0] + " != '') {" + NewLine;
            code += $"var _{TableName} = {TableName}Service.Select({Ds.Tables[0].PrimaryKey[0]});" + NewLine;
            code += "" + NewLine;
            code += "$('#txt" + Ds.Tables[0].PrimaryKey[0] + "').prop('disabled', true );" + NewLine;
            code += MapProPertiesToControl(Ds);

            code += "$('#btnSave').hide();" + NewLine;
            code += "}" + NewLine;
            code += "else{" + NewLine;
            code += "$('#btnSave').show();" + NewLine;
            code += "$('#btnUpdate').hide();" + NewLine;
            code += "$('#btnDelete').hide();" + NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;

            code += "}" + NewLine;
            return code;
        }

        protected string GenJavaScriptDocumentReady()
        {
            var code = "<script type=\"text/javascript\"> " + NewLine;

            code +=
                "var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.';" +
                NewLine;
            code += " $(document).ready(function () " + NewLine;
            code += "{" + NewLine;

            code += "$('.modal-trigger').leanModal();" + NewLine;

            code += "ForceNumberTextBox(); " + NewLine;

            //Set Drop

            code += "//For dropdown" + NewLine;
            code += BindSelectOption();

            code += "BindQueryString();" + NewLine;
            if (HaveDropDown())
            {
                code += "$('select').material_select(); " + NewLine;
            }

            code += "$('.datepicker').pickadate({" + NewLine;
            code += "selectMonths: true, // Creates a dropdown to control month" + NewLine;
            code += "selectYears: 15 ,// Creates a dropdown of 15 years to control year," + NewLine;
            code += "format: 'd mmmm yyyy'," + NewLine;
            code += "});" + NewLine;

            code += " }); " + NewLine;

            //ForceNumberTextBox==============================================================================================
            code += ForceNumberTextBox(true);

            //function Validate ===============================================================================================
            code += Validate();
            //=============================================================================================
            code += GenJavaScriptConfirm();

            code += GenJavaScriptSave();

            code += GenJavaScriptUpdate();

            code += GenJavaScriptDelete();

            code += SetSelect();

            code += BindQueryString();
            code += "</script>" + NewLine;

            return code;
        }

        protected string GenContentBodyBegin()
        {
            var code = "<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"ContentPlaceHolder1\" runat=\"server\" > " +
                       NewLine;
            return code;
        }

        protected string GenContentBodyEnd()
        {
            var code = "</asp:Content>" + NewLine;
            return code;
        }

        protected string GenDivFormBegin()
        {
            var code = "";
            code += "<div class=\"container\">" + NewLine;
            //code += "<div class=\"col s12\">" + _NewLine;
            code += "<div class=\"row\">";

            return code;
        }

        protected string GenDivFormEnd()
        {
            var code = "";
            code += "</div>" + NewLine;
            code += "</div>" + NewLine; // containerFor
            return code;
        }

        protected string GenButton()
        {
            var code = "  ";

            code += "<div class=\"input-field col s12\">" + NewLine;

            code += "<input id=\"btnSave\" type=\"button\" value=\"Save\" class=\" btn\" onclick=\"Save();\" />";
            code += "<input id=\"btnUpdate\" type=\"button\" value=\"Update\" class=\" btn\" onclick=\"Update();\" />";
            code +=
                "<input id=\"btnDelete\" type=\"button\" value=\"Delete\" class=\" btn\" onclick=\"javascript:return Confirm();\" />";

            code += "</div>" + NewLine;
            code += "  " + NewLine;

            return code;
        }

        protected string GenModal()
        {
            var code = "  ";
            code += " <!-- Modal Trigger --> " + NewLine;
            code += " <%--  <a class=\"waves-effect waves-light btn modal-trigger\" href=\"#modal1\">Modal</a>--%> " +
                    NewLine;
            code += " <!-- Modal Structure --> " + NewLine;
            code += "  <div id = \"modal1\" class=\"modal\">" + NewLine;
            code += "   <div class=\"modal-content\">" + NewLine;
            code += "  <h4>Message</h4>" + NewLine;
            code += "   <p id=\"PmsageAlert\">Saved!!!</p>" + NewLine;
            code += "  </div> " + NewLine;
            code += "   <div class=\"modal-footer\">" + NewLine;
            code += "<a href = \"#!\" class=\" modal-action modal-close waves-effect waves-green btn-flat\">Agree</a>" +
                    NewLine;
            code += "   </div>" + NewLine;
            code += "   </div>" + NewLine;

            return code;
        }

        protected string GenModalConfirm()
        {
            var code = "";
            code += " <div id=\"modalConfirm\" class=\"modal\"> " + NewLine;
            code += " <div class=\"modal-content\"> " + NewLine;
            code += "<h5>Message</h5> " + NewLine;
            code += "<h6>Are you sure?!!!</h6> " + NewLine;
            code += "</div> " + NewLine;
            code += "<div class=\"modal-footer\"> " + NewLine;
            code += " " + NewLine;
            code +=
                "<input id=\"btnConfirm\" type=\"button\" value=\"Confirm\" class=\"modal-action modal-close waves-effect waves-light btn\" onclick=\"javascript:return Delete();\" />";
            code +=
                "<input id=\"btnCancel\" type=\"button\" value=\"Cancel\" class=\"modal-action modal-close waves-effect waves-light btn\"  />";

            // code += "<asp:LinkButton ID=\"btnDelete\" CssClass=\"waves-effect waves-light btn left\" runat=\"server\" OnClick=\"btnDelete_Click\">Delete</asp:LinkButton> " + _NewLine;
            code += " " + NewLine;
            // code += "<asp:LinkButton ID=\"btnCancel\" CssClass=\"modal-action modal-close waves-effect waves-light btn right\" runat=\"server\" OnClientClick=\"javascript:return false;\">  Cancel</asp:LinkButton> " + _NewLine;
            code += "</div> " + NewLine;
            code += "</div>" + NewLine;

            return code;
        }

        private string GenReferJavaScript()
        {
            var code = "";

            code += "<script src=\"Js_U/" + FileName.JsCodeName() + "\"></script>" + NewLine;
            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
                {
                    foreach (var map in MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != TableName))
                        {
                            code += "<script src=\"Js_U/" + FileName.JsCodeName(map.TableName) + "\"></script>" +
                                    NewLine;
                        }
                    }
                }
            }

            return code;
        }

        public override void Gen()
        {
            InnitProperties();
            var code = "";

            code += GenHeadeFile();

            code += GenContentHeadBegin();

            code += GenReferJavaScript();

            code += GenJavaScriptDocumentReady();

            code += GenContentHeadEnd();

            code += GenContentBodyBegin();

            code += GenDivFormBegin();

            code += GenControls(12);

            code += GenButton();

            code += GenDivFormEnd();

            code += GenModal();

            code += GenModalConfirm();

            code += GenContentBodyEnd();

            FileCode.WriteFile(FileName.AspxFromCodeName(), code);
        }
    }
}