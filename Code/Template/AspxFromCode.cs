﻿using StkGenCode.Code.Column;
using System;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxFromCode : CodeBase
    {
        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" + _TableName + "Web.aspx.cs\" Inherits=\"" + _TableName + "Web\" %>" + _NewLine;

            return code;
        }

        protected string GenContentHeadBegin()
        {
            string code = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">  " + _NewLine;

            return code;
        }

        protected string GenContentHeadEnd()
        {
            string code = "";
            code += "</asp:Content> " + _NewLine;
            return code;
        }

        protected string GenSrciptModal()
        {
            var code = "$('.modal-trigger').leanModal();";
            return code;
        }

        //Method JavaScript ====================================================
        public string ForceNumberTextBox()
        {
            string code = "";
            code += "function ForceNumberTextBox() " + _NewLine;
            code += "{" + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);

                if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                {
                    continue;
                }

                if ((dataColumn.DataType.ToString() == "System.Guid") || (dataColumn.DataType.ToString() == "System.Int16"))
                { continue; }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += "$(\"#<%=" + controlTextBoxName + ".ClientID %>\").ForceNumericOnly();" + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += "$(\"#<%=" + controlTextBoxName + ".ClientID %>\").ForceNumericOnly2Digit();" + _NewLine;
                }
            }

            code += "}" + _NewLine;

            return code;
        }

        private string GenValidateDropDown(string columnname)
        {
            string controlDropDownName = string.Format(_formatDropDownName, columnname);
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == columnname) && (map.TableName != _TableName))
                {
                    code += "if(($(\"#<%=" + controlDropDownName + ".ClientID%>\").prop('selectedIndex')==0)||($(\"#<%=" + controlDropDownName + ".ClientID%>\").prop('selectedIndex')==-1)){" + _NewLine;
                    code += "output=false;" + _NewLine;
                    code += "" + _NewLine;
                    code += "$(\"#<%=" + controlDropDownName + ".ClientID%>\").prev().prev().addClass('CustomInvalid');" + _NewLine;
                    code += "" + _NewLine;
                    code += "}" + _NewLine;
                    code += "else { " + _NewLine;
                    code += "$(\"#<%=" + controlDropDownName + ".ClientID%>\").prev().prev().removeClass('CustomInvalid');" + _NewLine;
                    code += "$(\"#<%=" + controlDropDownName + ".ClientID%>\").prev().prev().addClass('CustomValid');" + _NewLine;
                    code += " " + _NewLine;
                    code += " " + _NewLine;
                    code += "}" + _NewLine;
                    break;
                }
            }

            return code;
        }

        //

        public string Validate()
        {
            string code = "";
            code += "function Validate() {" + _NewLine;
            code += "var output=true;" + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(_formatDropDownName, _DataColumn.ColumnName);

                if (dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString())
                {
                    if (_ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    { continue; }
                }

                if ((dataColumn.DataType.ToString() == "System.Guid") || (dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                { continue; }

                //For Dropw
                if (_MappingColumn != null)
                {
                    string codedrp = GenValidateDropDown(dataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                //ForText Box
                code += "if (CheckEmtyp($(\"#<%=" + controlTextBoxName + ".ClientID %>\"))) output = false;" + _NewLine;
            }

            code += "if (output == false)" + _NewLine;
            code += "Materialize.toast('please validate your input.', 3000, 'toastCss');" + _NewLine;
            code += "return output;" + _NewLine;
            code += "}" + _NewLine;
            return code;
        }

        public string Delete()
        {
            string code = "";

            code += "function Delete() { " + _NewLine;
            code += "$('#modalConfirm').openModal(); " + _NewLine;
            code += "return false; " + _NewLine;
            code += "}" + _NewLine;

            return code;
        }

        protected string GenJavaScriptDocumentReady()
        {
            string code = "<script type=\"text/javascript\"> " + _NewLine;
            code += " $(document).ready(function () " + _NewLine;
            code += "{" + _NewLine;
            code += "$('.modal-trigger').leanModal();" + _NewLine;

            code += "  ForceNumberTextBox(); " + _NewLine;
            code += "//For dropdown" + _NewLine;
            code += "$('select').material_select(); " + _NewLine;
            code += "$('.datepicker').pickadate({" + _NewLine;
            code += "    selectMonths: true, // Creates a dropdown to control month" + _NewLine;
            code += "    selectYears: 15 ,// Creates a dropdown of 15 years to control year," + _NewLine;
            code += "     format: 'd mmmm yyyy'," + _NewLine;
            code += "});" + _NewLine;

            code += " }); " + _NewLine;

            //ForceNumberTextBox==============================================================================================
            code += ForceNumberTextBox();

            //function Validate ===============================================================================================
            code += Validate();
            //=============================================================================================
            code += Delete();

            code += "</script>" + _NewLine;

            return code;
        }

        protected string GenContentBodyBegin()
        {
            string code = "<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"ContentPlaceHolder1\" runat=\"server\" > " + _NewLine;
            return code;
        }

        protected string GenContentBodyEnd()
        {
            string code = "</asp:Content>" + _NewLine;
            return code;
        }

        protected string GenDivFormBegin()
        {
            string code = "";
            code += "<div class=\"container\">" + _NewLine;
            //code += "<div class=\"col s12\">" + _NewLine;
            code += "<div class=\"row\">";

            return code;
        }

        protected string GenDivFormEnd()
        {
            string code = "";
            code += "</div>" + _NewLine;
            code += "</div>" + _NewLine;  // containerFor
            return code;
        }

        private string GenDropDown(string columnname, int columnSize)
        {
            string controlDropDownName = string.Format(_formatDropDownName, columnname);
            string code = "";
            foreach (MappingColumn map in _MappingColumn)
            {
                if ((map.ColumnName == columnname) && (map.TableName != _TableName))
                {
                    code += "<div class=\"input-field col s" + columnSize.ToString() + "\"> " + _NewLine;
                    code += "<asp:DropDownList ID =\"" + controlDropDownName + "\" runat=\"server\"> " + _NewLine;
                    code += " " + _NewLine;
                    code += "</asp:DropDownList>" + _NewLine;
                    code += "<label for=\"<%=" + controlDropDownName + ".ClientID %>\">drp</label>" + _NewLine;
                    code += "</div>" + _NewLine;

                    break;
                }
            }

            return code;
        }

        /// <summary>
        /// Refer CSS col s12  เป็น Base
        /// </summary>
        /// <param name="columnSize"></param>
        /// <returns></returns>
        public string GenControls(int columnSize)
        {
            string code = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                string disabled = "";
                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                string controlChekBoxName = string.Format(_formatChekBoxName, dataColumn.ColumnName);
                //string controlDropDownName = string.Format(_formatDropDownName, _DataColumn.ColumnName);

                string currentControl;
                if (_MappingColumn != null)
                {
                    string codedrp = GenDropDown(dataColumn.ColumnName, columnSize);

                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                //SetDisable For PrimaryKEY
                //foreach (var item in _ds.Tables[0].PrimaryKey)
                //{
                //Check แบบ KEy เดียว
                if ((dataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                {
                    disabled = "ReadOnly=\"true\" ";
                }
                //}

                if ((dataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                //code += "<div class=\"row\"> " + _NewLine;
                code += $"<div class=\"input-field col s{columnSize.ToString()}\"> " + _NewLine;

                //CssClass = "datepicker" type = "date"
                if ((dataColumn.DataType.ToString() == "System.DateTime"))
                {
                    //CssClass = "datepicker" type = "date"
                    currentControl = controlTextBoxName;
                    code += "<asp:TextBox " + disabled + " ID=\"" + controlTextBoxName + "\" CssClass=\"datepicker\" type =\"date\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //< asp:CheckBox ID = "chkRemittanceService" runat = "server" />
                    //< label for= "<%= chkRemittanceService.ClientID %>" > RemittanceService </ label >
                    currentControl = controlChekBoxName;
                    code += "<asp:CheckBox " + disabled + " ID=\"" + controlChekBoxName + "\"    runat=\"server\"></asp:CheckBox>" + _NewLine;
                    //code += "<label for=\"<%=" + controlChekBoxName + ".ClientID %>\">" + _DataColumn.ColumnName + " </label> " + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.String"))
                {
                    currentControl = controlTextBoxName;
                    code += "<asp:TextBox " + disabled + " ID=\"" + controlTextBoxName + "\" data-column-id=\"" + dataColumn.ColumnName + "\"  CssClass=\"validate " + dataColumn.ColumnName + "\" MaxLength=\"" + dataColumn.MaxLength + "\" length=\"" + dataColumn.MaxLength + "\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Int32"))
                {
                    // int max = System.Int32.MaxValue.ToString().Length;
                    int max = 9;
                    currentControl = controlTextBoxName;
                    code += "<asp:TextBox " + disabled + " ID=\"" + controlTextBoxName + "\" data-column-id=\"" + dataColumn.ColumnName + "\"  CssClass=\"validate " + dataColumn.ColumnName + "\" MaxLength=\"" + max.ToString() + "\" length=\"" + max.ToString() + "\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else
                {
                    currentControl = controlTextBoxName;
                    code += "<asp:TextBox " + disabled + " ID=\"" + controlTextBoxName + "\" CssClass=\"validate\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }

                code += "<label for=\"<%=" + currentControl + ".ClientID %>\">" + dataColumn.ColumnName + " </label> " + _NewLine;

                code += " </div> " + _NewLine;
            }
            return code;
        }

        protected String GenButton()
        {
            string code = "  ";

            code += "<div class=\"input-field col s12\">" + _NewLine;
            code += "<asp:LinkButton ID =\"btnSave\" CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClientClick=\"return Validate();\" OnClick=\"btnSave_Click\">Save</asp:LinkButton> " + _NewLine;
            code += "<asp:LinkButton ID =\"btnUpdate\" CssClass=\"waves-effect waves-light btn\" runat= \"server\" OnClientClick=\"return Validate();\" OnClick=\"btnUpdate_Click\" >Update</asp:LinkButton> " + _NewLine;
            code += "<asp:LinkButton ID=\"btnConfirm\" CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClientClick=\"javascript:return Delete();\">Delete</asp:LinkButton>" + _NewLine;
            code += "</div>" + _NewLine;
            code += "  " + _NewLine;

            return code;
        }

        protected string GenModal()
        {
            string code = "  ";
            code += " <!-- Modal Trigger --> " + _NewLine;
            code += " <%--  <a class=\"waves-effect waves-light btn modal-trigger\" href=\"#modal1\">Modal</a>--%> " + _NewLine;
            code += " <!-- Modal Structure --> " + _NewLine;
            code += "  <div id = \"modal1\" class=\"modal\">" + _NewLine;
            code += "   <div class=\"modal-content\">" + _NewLine;
            code += "  <h4>Message</h4>" + _NewLine;
            code += "   <p id=\"PmsageAlert\">Saved!!!</p>" + _NewLine;
            code += "  </div> " + _NewLine;
            code += "   <div class=\"modal-footer\">" + _NewLine;
            code += "    <a href = \"#!\" class=\" modal-action modal-close waves-effect waves-green btn-flat\">Agree</a>" + _NewLine;
            code += "   </div>" + _NewLine;
            code += "   </div>" + _NewLine;

            return code;
        }

        protected string GenModalConfirm()
        {
            string code = "";
            code += " <div id=\"modalConfirm\" class=\"modal\"> " + _NewLine;
            code += " <div class=\"modal-content\"> " + _NewLine;
            code += "            <h4>Message</h4> " + _NewLine;
            code += "            <p id=\"\">Are you sure?!!!</p> " + _NewLine;
            code += "        </div> " + _NewLine;
            code += "        <div class=\"modal-footer\"> " + _NewLine;
            code += "             " + _NewLine;
            code += "            <asp:LinkButton ID=\"btnDelete\" CssClass=\"waves-effect waves-light btn left\" runat=\"server\" OnClick=\"btnDelete_Click\">Delete</asp:LinkButton> " + _NewLine;
            code += "             " + _NewLine;
            code += "            <asp:LinkButton ID=\"btnCancel\" CssClass=\"modal-action modal-close waves-effect waves-light btn right\" runat=\"server\" OnClientClick=\"javascript:return false;\">  Cancel</asp:LinkButton> " + _NewLine;
            code += "        </div> " + _NewLine;
            code += "    </div>" + _NewLine;

            return code;
        }

        public override void Gen()
        {
            string code = "";

            code += GenHeadeFile();

            code += GenContentHeadBegin();

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

            //FileName name = new FileName();

            //name._TableName = _TableName;

            //name._ds = _ds;
            InnitProperties();

            _FileCode.writeFile(_FileName.AspxFromCodeName(), code);
        }
    }
}