using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
    public class AspxCode
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;

        private string _fileType = ".aspx";
        private string _NewLine = " \r\n";

        private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/Site1.Master\" AutoEventWireup=\"true\" CodeBehind=\"" + _TableName + ".aspx.cs\" Inherits=\"WebTemplate." + _TableName + "\" %>" + _NewLine;

            return code;
        }

        private string GenContentHead()
        {
            string code = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\"> </ asp:Content > " + _NewLine;
            return code;
        }

        private string GenContentBodyBegin()
        {
            string code = "<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"ContentPlaceHolder1\" runat=\"server\" > " + _NewLine;
            return code;
        }

        private string GenContentBodyEnd()
        {
            string code = "</asp:Content>" + _NewLine;
            return code;
        }

        private string GenDivFormBegin()
        {
            string code = "<div class=\"col s12\">" + _NewLine;
            return code;
        }

        private string GenDivFormEnd()
        {
            string code = "  </div>" + _NewLine;
            return code;
        }

        // <div class="row">
        //    <div class="input-field col s12">
        //        <%--   <input placeholder = "Placeholder" id="first_name" type="text" class="validate">--%>
        //        <asp:TextBox ID = "TextBox1" CssClass="validate" runat="server"></asp:TextBox>
        //        <label for="first_name">First Name</label>
        //    </div>
        //    <%-- <div class="input-field col s6">
        //        <input id = "last_name" type="text" class="validate">
        //        <label for="last_name">Last Name</label>
        //    </div>--%>
        //</div>

        // <div class=\"row\">
        //    <div class=\"input-field col s12\">
        //        <%--   <input placeholder = \"Placeholder\" id=\"first_name\" type=\"text\" class=\"validate\">--%>
        //        <asp:TextBox ID = \"TextBox1\" CssClass=\"validate\" runat=\"server\"></asp:TextBox>
        //        <label for=\"first_name\">First Name</label>
        //    </div>
        //    <%-- <div class=\"input-field col s6\">
        //        <input id = \"last_name\" type=\"text\" class=\"validate\">
        //        <label for=\"last_name\">Last Name</label>
        //    </div>--%>
        //</div>
        private string GenTextBox()
        {
            string code = "  ";
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                code += "  <div class=\"row\"> " + _NewLine;
                code += " <div class=\"input-field col s12\"> " + _NewLine;

                code += "   <asp:TextBox ID = \"txt" + _DataColumn.ColumnName + "\" CssClass=\"validate\" runat=\"server\"></asp:TextBox>" + _NewLine;

                code += "    <label for=\" <%= txt" + _DataColumn.ColumnName + ".ClientID %> \">" + _DataColumn.ColumnName + " </label> " + _NewLine;

                code += " </div> " + _NewLine;
                code += " </div> " + _NewLine;
            }
            return code;
        }

        //     <div class="row">
        //    <div class="input-field col s12">

        //        <asp:LinkButton ID = "btnOk" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnOk_Click"><i class="material-icons left">cloud</i> button</asp:LinkButton>
        //    </div>
        //</div>

        //     <div class=\"row\">
        //    <div class=\"input-field col s12\">

        //        <asp:LinkButton ID = \"btnOk\" CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClick=\"btnOk_Click\"><i class=\"material-icons left\">cloud</i> button</asp:LinkButton>
        //    </div>
        //</div>
        private String GenButton()
        {
            string code = "  ";
            code += " <div class=\"row\">  " + _NewLine;
            code += "    <div class=\"input-field col s12\">" + _NewLine;
            code += "  <asp:LinkButton ID = \"btnOk\" CssClass=\"waves-effect waves-light btn\" runat=\"server\" OnClick=\"btnOk_Click\"><i class=\"material-icons left\">cloud</i> Save</asp:LinkButton> " + _NewLine;
            code += "   </div> " + _NewLine;
            code += " </div> " + _NewLine;
            code += "  " + _NewLine;

            return code;
        }

        private string T()
        {
            string code = "  ";
            code += "  " + _NewLine;

            return code;
        }

        public void Gen()
        {
            string _code = "";
            _code += GenHeadeFile();
            _code += GenContentHead();
            _code += GenContentBodyBegin();

            _code += GenDivFormBegin();

            _code += GenTextBox() + _NewLine;

            _code += GenButton();
            _code += GenDivFormEnd();
            _code += GenContentBodyEnd();

            _FileCode.writeFile(_TableName + "Web", _code, _fileType);
        }
    }
}