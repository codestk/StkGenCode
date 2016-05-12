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
            string code = "";
            code = "$('.modal-trigger').leanModal();";
            return code;
        }

        //Method JavaScript ====================================================
        public string ForceNumberTextBox()
        {
            string code = "";
            code += "function ForceNumberTextBox() " + _NewLine;
            code += "{" + _NewLine;

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                if ((_DataColumn.Table.PrimaryKey[0].ToString() == _DataColumn.ColumnName.ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                {
                    continue;
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                if ((_DataColumn.DataType.ToString() == "System.Int32") || (_DataColumn.DataType.ToString() == "System.Int16"))
                {
                    code += "$(\"#<%= txt" + _DataColumn.ColumnName + ".ClientID %>\").ForceNumericOnly();" + _NewLine; ;
                }
                else if (_DataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += "$(\"#<%= txt" + _DataColumn.ColumnName + ".ClientID %>\").ForceNumericOnly2Digit();" + _NewLine; ;
                }
            }

            code += "}" + _NewLine;

            return code;
        }

        //
        public string Validate()
        {
            string code = "";
            code += "function Validate() {" + _NewLine;
            code += "var output=true;" + _NewLine;

            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                if (_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString())
                {
                    if (_ds.Tables[0].PrimaryKey[0].AutoIncrement == true)
                    { continue; }
                }

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }
                //code += "CheckEmtyp($(\"#<%= txt" + _DataColumn.ColumnName + ".ClientID %>\")) && ";
                code += "if (CheckEmtyp($(\"#<%= txt" + _DataColumn.ColumnName + ".ClientID %>\"))) output = false;" + _NewLine;
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
            code += "    $('#modalConfirm').openModal(); " + _NewLine;
            code += "    return false; " + _NewLine;
            code += "}" + _NewLine;

            return code;
        }

        protected string GenJavaScriptDocumentReady()
        {
            string code = "<script type=\"text/javascript\"> " + _NewLine;
            code += " $(document).ready(function () " + _NewLine;
            code += "{ " + _NewLine;
            code += "$('.modal-trigger').leanModal();" + _NewLine;

            code += "  ForceNumberTextBox(); " + _NewLine;

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
            code += "<div class=\"col s12\">" + _NewLine;
            return code;
        }

        protected string GenDivFormEnd()
        {
            string code = "";
            code += "</div>" + _NewLine;
            code += "</div>" + _NewLine;  // containerFor
            return code;
        }

        /// <summary>
        /// Refer CSS col s12  เป็น Base
        /// </summary>
        /// <param name="columnSize"></param>
        /// <returns></returns>
        public string GenTextBox(int columnSize)
        {
            string code = "  ";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                string disabled = "";

                //SetDisable For PrimaryKEY
                //foreach (var item in _ds.Tables[0].PrimaryKey)
                //{
                //Check แบบ KEy เดียว
                if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                {
                    disabled = "ReadOnly=\"true\" ";
                }
                //}

                if ((_DataColumn.DataType.ToString() == "System.Guid"))
                { continue; }

                //code += "<div class=\"row\"> " + _NewLine;
                code += string.Format("<div class=\"input-field col s{0}\"> ", columnSize.ToString()) + _NewLine;

                //CssClass = "datepicker" type = "date"
                if ((_DataColumn.DataType.ToString() == "System.DateTime"))
                {
                    //CssClass = "datepicker" type = "date"
                    code += "<asp:TextBox " + disabled + " ID=\"txt" + _DataColumn.ColumnName + "\" CssClass=\"datepicker\" type =\"date\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                {
                    //< asp:CheckBox ID = "chkRemittanceService" runat = "server" />
                    //< label for= "<%= chkRemittanceService.ClientID %>" > RemittanceService </ label >
                    code += "<asp:CheckBox " + disabled + " ID=\"txt" + _DataColumn.ColumnName + "\"    runat=\"server\"></asp:CheckBox>" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.String"))
                {
                    code += "<asp:TextBox " + disabled + " ID=\"txt" + _DataColumn.ColumnName + "\" data-column-id=\"" + _DataColumn.ColumnName + "\"  CssClass=\"validate " + _DataColumn.ColumnName + "\" MaxLength=\"" + _DataColumn.MaxLength + "\" length=\"" + _DataColumn.MaxLength + "\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    // int max = System.Int32.MaxValue.ToString().Length;
                    int max = 9;
                    code += "<asp:TextBox " + disabled + " ID=\"txt" + _DataColumn.ColumnName + "\" data-column-id=\"" + _DataColumn.ColumnName + "\"  CssClass=\"validate " + _DataColumn.ColumnName + "\" MaxLength=\"" + max.ToString() + "\" length=\"" + max.ToString() + "\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }
                else
                {
                    code += "<asp:TextBox " + disabled + " ID=\"txt" + _DataColumn.ColumnName + "\" CssClass=\"validate\" runat=\"server\"></asp:TextBox>" + _NewLine;
                }

                code += "<label for=\"<%= txt" + _DataColumn.ColumnName + ".ClientID %>\">" + _DataColumn.ColumnName + " </label> " + _NewLine;
                //code += " </div> " + _NewLine;
                code += " </div> " + _NewLine;
            }
            return code;
        }

        protected String GenButton()
        {
            string code = "  ";
            code += "<div class=\"row\">  " + _NewLine;
            code += "<div class=\"input-field col s12\">" + _NewLine;
            code += "<asp:LinkButton ID = \"btnSave\"   CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClientClick=\"return Validate();\" OnClick=\"btnSave_Click\">  Save</asp:LinkButton> " + _NewLine;
            code += "<asp:LinkButton ID = \"btnUpdate\" CssClass=\"waves-effect waves-light btn\" runat= \"server\" OnClientClick=\"return Validate();\" OnClick= \"btnUpdate_Click\" >  Update</asp:LinkButton> " + _NewLine;
            //code += "<asp:LinkButton ID = \"btnDelete\" CssClass=\"waves-effect waves-light btn\" runat= \"server\" OnClick= \"btnDelete_Click\" >  Delete</asp:LinkButton>" + _NewLine;
            code += "<asp:LinkButton ID=\"btnConfirm\" CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClientClick=\"javascript:return Delete();\">  Delete</asp:LinkButton>" + _NewLine;

            code += "</div>" + _NewLine;
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
            code += "        <div class=\"modal-content\"> " + _NewLine;
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
            string _code = "";

            _code += GenHeadeFile();

            _code += GenContentHeadBegin();

            _code += GenJavaScriptDocumentReady();

            _code += GenContentHeadEnd();

            _code += GenContentBodyBegin();

            _code += GenDivFormBegin();

            _code += GenTextBox(12);

            _code += GenButton();

            _code += GenDivFormEnd();

            _code += GenModal();

            _code += GenModalConfirm();

            _code += GenContentBodyEnd();

            FileName name = new FileName();

            name._TableName = _TableName;

            name._ds = _ds;

            _FileCode.writeFile(name.AspxFromCodeName(), _code);
        }
    }
}