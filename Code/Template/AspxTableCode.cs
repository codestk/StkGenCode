using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCode
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;

        private string _fileType = ".aspx";
        private string _NewLine = " \r\n";

        private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string FileName;
        public AspxTableCode()
        {
            FileName = _TableName + "AspxTableCode";
        }

        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeBehind=\"" + FileName + ".aspx.cs\" Inherits=\"_" + _TableName + "\" %>" + _NewLine;

            return code;
        }

        private string GenContentHeadBegin()
        {
            string code = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">  " + _NewLine;

            return code;
        }

        private string GenContentHeadEnd()
        {
            string code = "";

            code += " </asp:Content> " + _NewLine;
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

        //    <table class=\"striped\">
        //        <thead>
        //            <tr>
        //                <th>Currency</th>
        //                <th>Description</th>
        //                <th>Bank Note</th>
        //                <th>Buying Rates	</th>
        //            </tr>
        //        </thead>
        //        <tbody>
        //            <asp:Repeater ID = \"rptCustomers\" runat= \"server\" >
        //                < ItemTemplate >

        //                    < tr >
        //                        < td ><%# Eval(\"ENGAmpText\") %></td>
        //                        < td ><%# Eval(\"AmpText\") %></td>
        //                        < td ><%# Eval(\"RecordCount\") %></td>
        //                        < td ><%# Eval(\"RecordCount\") %></td>
        //                    </ tr >
        //                </ ItemTemplate >
        //            </ asp:Repeater>
        //        </tbody>
        //    </table>

        private string GenTableRepeater()
        {
            string code = "  ";
            code += "   <table class=\"striped\"> " + _NewLine;
            code += "  <thead> " + _NewLine;
            code += "  <tr> " + _NewLine;
            //code += " <th>Currency</th>  " + _NewLine;
            //code += "  <th>Description</th> " + _NewLine;
            //code += "   <th>Bank Note</th>" + _NewLine;
            //code += "  <th>Buying Rates	</th> " + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                code += " <th>" + _DataColumn.ColumnName.ToUpper() + "</th> " + _NewLine;
            }

            code += "  </tr> " + _NewLine;
            code += "   </thead>" + _NewLine;
            code += "   <tbody> " + _NewLine;
            code += "  <asp:Repeater ID = \"rpt" + _TableName + "Data\" runat= \"server\" > " + _NewLine;
            code += "   <ItemTemplate>" + _NewLine;
            code += " <tr> " + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //code += " <td>" + _DataColumn.ColumnName.ToUpper() + "</td> " + _NewLine;
                code += " <td><%# Eval(\"" + _DataColumn.ColumnName + "\") %></td>";
            }
            code += "   </tr> " + _NewLine;
            code += "   </ItemTemplate > " + _NewLine;
            code += "   </asp:Repeater> " + _NewLine;
            code += "  </tbody> " + _NewLine;
            code += "  </table> " + _NewLine;

            return code;
        }

        //    <ul class=\"pagination\">

        //        <asp:Repeater ID = \"rptPager\" runat=\"server\">
        //            <ItemTemplate>
        //                <li class=\"<%# LiPaggerClass(Eval(\"Enabled\"), Eval(\"Text\")) %>\">
        //                    <asp:LinkButton ID = \"lnkPage\" runat=\"server\" Text='<%#Eval(\"Text\") %>' CommandArgument='<%# Eval(\"Value\") %>'
        //                        OnClick=\"Page_Changed\" OnClientClick='<%# !Convert.ToBoolean(Eval(\"Enabled\")) ? \"javascript:return false;\" : \"\" %>'></asp:LinkButton></li>
        //            </ItemTemplate>
        //        </asp:Repeater>
        //    </ul>
        private string GenPagger()
        {
            string code = "  ";
            code += "  <ul class=\"pagination\"> " + _NewLine;
            code += "   <asp:Repeater ID = \"rpt" + _TableName + "Pager\" runat=\"server\"> " + _NewLine;
            code += "   <ItemTemplate> " + _NewLine;
            code += "    <li class=\"<%# PaggerClass(Eval(\"Enabled\"), Eval(\"Text\")) %>\"> " + _NewLine;
            code += "   <asp:LinkButton ID = \"lnkPage\" runat=\"server\" Text='<%#Eval(\"Text\") %>' CommandArgument='<%# Eval(\"Value\") %>' " + _NewLine;
            code += "     OnClick=\"Page_Changed\" OnClientClick='<%# !Convert.ToBoolean(Eval(\"Enabled\")) ? \"javascript:return false;\" : \"\" %>'></asp:LinkButton></li>" + _NewLine;
            code += "      </ItemTemplate>" + _NewLine;
            code += "   </asp:Repeater> " + _NewLine;
            code += " </ul> " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            return code;
        }

        private string T()
        {
            string code = "  ";
            code += "  " + _NewLine;
            code += "  " + _NewLine; code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            return code;
        }

        public void Gen()
        {
            string _code = "";
            _code += GenHeadeFile();
            _code += GenContentHeadBegin();

            _code += GenContentHeadEnd();

            _code += GenContentBodyBegin();
            _code += GenTableRepeater();
            _code += GenPagger();
            _code += GenContentBodyEnd();

            _FileCode.writeFile(FileName, _code, _fileType);
        }

        //        <asp:Content ID = "Content1" ContentPlaceHolderID="head" runat="server">
        //</asp:Content>
        //<asp:Content ID = "Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        //    <table class="striped">
        //        <thead>
        //            <tr>
        //                <th>Currency</th>
        //                <th>Description</th>
        //                <th>Bank Note</th>
        //                <th>Buying Rates	</th>
        //            </tr>
        //        </thead>
        //        <tbody>
        //            <asp:Repeater ID = "rptCustomers" runat= "server" >
        //                < ItemTemplate >

        //                    < tr >
        //                        < td ><%# Eval("ENGAmpText") %></td>
        //                        < td ><%# Eval("AmpText") %></td>
        //                        < td ><%# Eval("RecordCount") %></td>
        //                        < td ><%# Eval("RecordCount") %></td>
        //                    </ tr >
        //                </ ItemTemplate >
        //            </ asp:Repeater>
        //        </tbody>
        //    </table>

        //    <ul class="pagination">

        //        <asp:Repeater ID = "rptPager" runat="server">
        //            <ItemTemplate>
        //                <li class="<%# LiPaggerClass(Eval("Enabled"), Eval("Text")) %>">
        //                    <asp:LinkButton ID = "lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
        //                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "javascript:return false;" : "" %>'></asp:LinkButton></li>
        //            </ItemTemplate>
        //        </asp:Repeater>
        //    </ul>
        //</asp:Content>
    }
}