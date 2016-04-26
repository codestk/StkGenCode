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

        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" + _TableName + "List.aspx.cs\" Inherits=\"" + _TableName + "List\" %>" + _NewLine;

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
            //string code = "  ";
            //code += "  <ul class=\"pagination\"> " + _NewLine;
            //code += "   <asp:Repeater ID = \"rpt" + _TableName + "Pager\" runat=\"server\"> " + _NewLine;
            //code += "   <ItemTemplate> " + _NewLine;
            //code += "    <li class=\"<%# PaggerClass(Eval(\"Enabled\"), Eval(\"Text\")) %>\"> " + _NewLine;
            //code += "   <asp:LinkButton ID = \"lnkPage\" runat=\"server\" Text='<%#Eval(\"Text\") %>' CommandArgument='<%# Eval(\"Value\") %>' " + _NewLine;
            //code += "     OnClick=\"Page_Changed\" OnClientClick='<%# !Convert.ToBoolean(Eval(\"Enabled\")) ? \"javascript:return false;\" : \"\" %>'></asp:LinkButton></li>" + _NewLine;
            //code += "      </ItemTemplate>" + _NewLine;
            //code += "   </asp:Repeater> " + _NewLine;
            //code += " </ul> " + _NewLine;
            //code += "  " + _NewLine;
            //code += "  " + _NewLine;

            string code = "";
            code += "  <ul id=\"ulpage\" class=\"pagination\"> " + _NewLine;
            code += "            <asp:Repeater ID=\"rpt" + _TableName + "Pagger\" runat=\"server\"> " + _NewLine;
            code += "                <ItemTemplate> " + _NewLine;
            code += "                    <li class=\"<%# PaggerClass(Eval(\"Enabled\"), Eval(\"Text\")) %>\"> " + _NewLine;
            code += "                        <asp:LinkButton ID=\"lnkPage\" runat=\"server\" Text='<%#Eval(\"Text\") %>' CommandArgument='<%# Eval(\"Value\") %>' " + _NewLine;
            code += "                            OnClick=\"Page_Changed\" OnClientClick='<%# !Convert.ToBoolean(Eval(\"Enabled\")) ? \"javascript:return false;\" : \"\" %>'></asp:LinkButton></li> " + _NewLine;
            code += "                </ItemTemplate> " + _NewLine;
            code += "            </asp:Repeater> " + _NewLine;
            code += "        </ul>";

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

        private string GenReferJavaScript()
        {
            string code = "";
            code += "<script src=\"Bu/LocationManageCallServices.js\"></script>" + _NewLine;
            code += "<script src=\"Bu/AutoCompleteService.js\"></script>" + _NewLine;
            code += "<script src=\"Module/Pagger/jquery.simplePagination.js\"></script>" + _NewLine;
            return code;
        }

        private string GenReferCSS()

        {
            string code = "";
            return code = " <link href=\"Module/Search/SearchControl.css\" rel=\"stylesheet\" />";
            return code;
        }

        private string GenJavaScript()
        {
            string code = "";
            code += "  $(document).ready(function () {" + _NewLine;

            //===Pager---------------------------------------------------------------------------
            code += "$('#ulpage').pagination({" + _NewLine;
            code += "items: '<%=  ViewState[\"recordCount\"].ToString()%>'," + _NewLine;
            code += "itemsOnPage: '<%= PageSize%>'," + _NewLine;

            code += " prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />', " + _NewLine;
            code += " nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />'," + _NewLine;

            code += "  currentPage: '<%=  ViewState[\"CurrentPage\"].ToString()%>'," + _NewLine;
            //cssStyle: 'light-theme',
            code += " onPageClick: function(event, page) {" + _NewLine;
            code += "   var pagenum = '';" + _NewLine;
            code += "  if (event < 10) {" + _NewLine;
            code += "   pagenum = '0' + event;" + _NewLine;
            code += " }" + _NewLine;
            code += "            else {" + _NewLine;
            code += "               pagenum = event;" + _NewLine;
            code += " }" + _NewLine;
            code += " var code = 'ctl00$ContentPlaceHolder1$rptlocation2Pager$ctl' + pagenum + '$lnkPage';" + _NewLine;

            code += "         __doPostBack(code, '');" + _NewLine;
            code += "}" + _NewLine;

            code += "});" + _NewLine;
            //=================================================================================================

            code += "   });//End Document Ready" + _NewLine;
            return code;
        }

        private string GenSearch()
        {
            string code = "";

            code += "  <div class=\"sw  card   hoverable\">" + _NewLine;

            code += "  <asp:TextBox ID=\"txtSearch\" CssClass=\"search\" projectID=\"99\" placeholder=\"Search the web...\" AutoCompleteType=\"Disabled\" runat=\"server\"></asp:TextBox>" + _NewLine;

            code += "  <asp:ImageButton ID=\"ImageButton1\" CssClass=\"go\" ImageUrl=\"Images/search73.svg\" OnClick=\"btnSearch_Click\" OnClientClick=\"return Search();\" runat=\"server\" />" + _NewLine;

            code += "  <a href=\"#\" class=\"logo\" title=\"Storm\">Search</a>" + _NewLine;

            code += "  </div>" + _NewLine;

            return code;
        }

        private string GenNoResult()
        {
            string code = "";

            code += " <div id=\"DivNoresults\" class=\"container\" visible=\"false\" runat=\"server\"> " + _NewLine;
            code += "        <p class=\"flow-text\">No results found. Try the following:</p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Make sure all words are spelled correctly. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Try differrent keywords. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Try more general keywords. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "    </div>";

            return code;
        }

        private string GenBeginResult()
        {
            string code = "";
            code = "<div id=\"divResult\" runat=\"server\">";

            return code;
        }

        private string GenEndResult()
        {
            string code = "";
            code = "</div>";
            return code;
        }

        //string GenBeginResult()
        //{
        //    string code = "";
        //    return code;
        //}

        private string GenDocumentReady()
        {
            string code = " <script>";
            code += "     var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.'; " + _NewLine;
            code += " " + _NewLine;
            code += "        $(document).ready(function () { " + _NewLine;
            code += "            ForceNumberTextBox(); " + _NewLine;
            code += "            //For Change Lange " + _NewLine;
            code += "            $('.dropdown-button').dropdown({ " + _NewLine;
            code += "                inDuration: 300, " + _NewLine;
            code += "                outDuration: 225, " + _NewLine;
            code += "                constrain_width: true, // Does not change width of dropdown to that of the activator " + _NewLine;
            code += "                hover: false, // Activate on hover " + _NewLine;
            code += "                gutter: 0, // Spacing from edge " + _NewLine;
            code += "                belowOrigin: true, // Displays dropdown below the button " + _NewLine;
            code += "                alignment: 'left' // Displays dropdown with edge aligned to the left of button " + _NewLine;
            code += "            }); " + _NewLine;
            code += "            $('.dropdown-button').hover(function () { " + _NewLine;
            code += "                GotoTop(); " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $('select').material_select(); " + _NewLine;
            code += " " + _NewLine;
            code += "            $('#ulpage').pagination({ " + _NewLine;
            code += "                items: '<%=  ViewState[\"recordCount\"].ToString()%>', " + _NewLine;
            code += "                itemsOnPage: '<%= PageSize%>', " + _NewLine;
            code += " " + _NewLine;
            code += "                prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />', " + _NewLine;
            code += "                nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />', " + _NewLine;
            code += " " + _NewLine;
            code += "                currentPage: '<%=  ViewState[\"CurrentPage\"].ToString()%>', " + _NewLine;
            code += "                //cssStyle: 'light-theme', " + _NewLine;
            code += "                onPageClick: function (event, page) { " + _NewLine;
            code += "                    var pagenum = ''; " + _NewLine;
            code += "                    if (event < 10) { " + _NewLine;
            code += "                        pagenum = '0' + event; " + _NewLine;
            code += "                    } " + _NewLine;
            code += "                    else { " + _NewLine;
            code += "                        pagenum = event; " + _NewLine;
            code += "                    } " + _NewLine;
            code += "                    var code = 'ctl00$ContentPlaceHolder1$rptlocation2Pager$ctl' + pagenum + '$lnkPage'; " + _NewLine;
            code += " " + _NewLine;
            code += "                    __doPostBack(code, ''); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $('.modal-trigger').leanModal({ " + _NewLine;
            code += "                dismissible: false, // Modal can be dismissed by clicking outside of the modal " + _NewLine;
            code += "                opacity: .5, // Opacity of modal background " + _NewLine;
            code += "                in_duration: 300, // Transition in duration " + _NewLine;
            code += "                out_duration: 200, // Transition out duration " + _NewLine;
            code += "                starting_top: '50%' " + _NewLine;
            code += " " + _NewLine;
            code += "                //  ready: function () { alert('Ready'); }, // Callback for Modal open " + _NewLine;
            code += "                //  complete: function () { alert('Closed'); } // Callback for Modal close " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            //Edit table-------------------------------------------------------------------------------------------- " + _NewLine;
            code += "            $('.tdThaiName,.tdThaiAddress1,.tdThaiAddress2,.tdThaiAddress3,.tdThaiProvince,.tdPostcode,.tdEnglishName,.tdEnglishAddress1,.tdEnglishAddress2,.tdEnglishAddress3,.tdEnglishProvince').dblclick(function () { " + _NewLine;
            code += "                var ClassEvent = trim(this.className, ' '); " + _NewLine;
            code += " " + _NewLine;
            code += "                if ((ClassEvent == 'tdThaiName') || (ClassEvent == 'tdThaiAddress1') || (ClassEvent == 'tdEnglishName') || (ClassEvent == 'tdEnglishAddress1')) { " + _NewLine;
            code += "                    //Add Space for edit " + _NewLine;
            code += "                    $(this).addClass(\"widthEditBig\"); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    $(this).addClass(\"widthEditSmall\"); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "                var sp = $(this).find(\"span\"); " + _NewLine;
            code += "                var di = $(this).find(\"div\"); " + _NewLine;
            code += "                sp.hide(); " + _NewLine;
            code += "                di.show(); " + _NewLine;
            code += " " + _NewLine;
            code += "                di.focus(); " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $(\".lblCancel\").click(function (event) { " + _NewLine;
            code += "                $(this).parent().parent().find(\"span\").show(); " + _NewLine;
            code += "                $(this).parent().parent().find(\"div\").hide(); " + _NewLine;
            code += "                $(this).parent().parent().removeClass(\"widthEditBig\"); " + _NewLine;
            code += "                $(this).parent().parent().removeClass(\"widthEditSmall\"); " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $(\".lblSave\").click(function (event) { " + _NewLine;
            code += "                var tdContent = $(this).parent().parent(); " + _NewLine;
            code += "                var inputBox = tdContent.find(\"input:text\")[0]; //Get Data inputbox " + _NewLine;
            code += "                var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " + _NewLine;
            code += " " + _NewLine;
            code += "                var column = inputBox.attributes['data-column-id'].value; " + _NewLine;
            code += "                var data = inputBox.value; " + _NewLine;
            code += " " + _NewLine;
            code += "                //Save Data To CodeBehide " + _NewLine;
            code += "                var result = LocationManageService.SaveColumn(id, column, data); " + _NewLine;
            code += " " + _NewLine;
            code += "                if (result == true) { " + _NewLine;
            code += "                    tdContent.find(\"span\").show(); " + _NewLine;
            code += "                    tdContent.find(\"div\").hide(); " + _NewLine;
            code += "                    tdContent.removeClass(\"widthEditBig\"); " + _NewLine;
            code += "                    tdContent.removeClass(\"widthEditSmall\"); " + _NewLine;
            code += " " + _NewLine;
            code += "                    //var txtVal = tdContent.find(\"input:text\")[0].value; " + _NewLine;
            code += " " + _NewLine;
            code += "                    tdContent.find(\"span\")[0].innerText = data; //Swap Value " + _NewLine;
            code += "                    Materialize.toast('Your data has been saved.', 3000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    Materialize.toast(MsgError, 5000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
            code += "                // <p><input name = '' type = 'radio' id = 'test1'  checked= 'true' /><label for= 'test1'></label></p> " + _NewLine;
            code += "                var chk = $(this).find('input:radio')[0]; " + _NewLine;
            code += " " + _NewLine;
            code += "                //  var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " + _NewLine;
            code += "                var id = chk.attributes['data-column-key'].value; " + _NewLine;
            code += "                debugger " + _NewLine;
            code += "                var column = chk.attributes['data-column-id'].value; " + _NewLine;
            code += "                var Data = \"\"; " + _NewLine;
            code += " " + _NewLine;
            code += "                var status = false; " + _NewLine;
            code += "                if (chk.checked) { " + _NewLine;
            code += "                    status = false; " + _NewLine;
            code += "                    Data = \"\"; " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    status = true; " + _NewLine;
            code += "                    Data = \"x\"; " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "                var result = LocationManageService.SaveColumn(id, column, Data); " + _NewLine;
            code += " " + _NewLine;
            code += "                if (result == true) { " + _NewLine;
            code += "                    //Display Message Display Checkbox " + _NewLine;
            code += "                    chk.checked = status; " + _NewLine;
            code += "                    Materialize.toast('Your data has been saved.', 3000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    Materialize.toast(MsgError, 5000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += "            //Edit table===================================================================================================================== " + _NewLine;
            code += " " + _NewLine;
            code += "            //Fix Stlyte " + _NewLine;
            code += "            var UA = navigator.userAgent; " + _NewLine;
            code += "            var html = document.documentElement; " + _NewLine;
            code += "            if (UA.indexOf(\"IEMobile\") === -1) { " + _NewLine;
            code += "                if ((UA.indexOf(\"rv:11.\") !== -1) && (!html.classList.contains('ie11')) && window.navigator.msPointerEnabled) { " + _NewLine;
            code += "                    html.classList.add(\"ie11\"); " + _NewLine;
            code += "                } else if ((UA.indexOf(\"MSIE 10.\") !== -1) && (!html.classList.contains('ie10')) && window.navigator.msPointerEnabled) { " + _NewLine;
            code += "                    html.classList.add(\"ie10\"); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else if ((UA.indexOf(\"MSIE 8.\") !== -1) && (!html.classList.contains('ie8')) && window.navigator.msPointerEnabled) { " + _NewLine;
            code += "                    html.classList.add(\"ie8\"); " + _NewLine;
            code += "                } " + _NewLine;
            code += "            } " + _NewLine;
            code += " " + _NewLine;
            code += "            //For Autocomplete  ----------------------------------------------------------------------------------- " + _NewLine;
            code += "            $(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;
            code += " " + _NewLine;
            code += "                source: function (request, response) { " + _NewLine;
            code += " " + _NewLine;
            code += "                    var postCode = this.element[0].parentNode.parentNode.parentNode.childNodes[13].innerText; " + _NewLine;
            code += "                    var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += " " + _NewLine;
            code += "                    var data = AutoCompleteService.GetAutoComplete(column, request.term, postCode); " + _NewLine;
            code += " " + _NewLine;
            code += "                    response(data); " + _NewLine;
            code += "                    //$.ajax({ " + _NewLine;
            code += "                    //    url: \"/WebTemplate/LocationManage.aspx/GetAutoComplete\", " + _NewLine;
            code += "                    //    dataType: \"json\", " + _NewLine;
            code += "                    //    data: { " + _NewLine;
            code += "                    //        Column: 'EnglishAddress3', " + _NewLine;
            code += "                    //        value: request.term " + _NewLine;
            code += "                    //    }, " + _NewLine;
            code += "                    //    contentType: \"application/json; charset=utf-8\", " + _NewLine;
            code += "                    //    success: function (data) { " + _NewLine;
            code += " " + _NewLine;
            code += "                    //        response(data); " + _NewLine;
            code += "                    //    }, " + _NewLine;
            code += "                    //    error: function (msg) { " + _NewLine;
            code += "                    //        debugger " + _NewLine;
            code += "                    //        alert(msg); " + _NewLine;
            code += " " + _NewLine;
            code += "                    //    } " + _NewLine;
            code += "                    //}); " + _NewLine;
            code += "                }, " + _NewLine;
            code += "                minLength: 3, " + _NewLine;
            code += "                select: function (event, ui) { " + _NewLine;
            code += "                    //log(ui.item ?\"Selected: \" + ui.item.label :\"Nothing selected, input was \" + this.value); " + _NewLine;
            code += "                }, " + _NewLine;
            code += "                open: function () { " + _NewLine;
            code += "                    $(this).removeClass(\"ui-corner-all\").addClass(\"ui-corner-top\"); " + _NewLine;
            code += "                }, " + _NewLine;
            code += "                close: function () { " + _NewLine;
            code += "                    $(this).removeClass(\"ui-corner-top\").addClass(\"ui-corner-all\"); " + _NewLine;
            code += "                } " + _NewLine;
            code += "            }); " + _NewLine;
            code += "            //For Autocomplete================================================================================== " + _NewLine;
            code += " " + _NewLine;
            code += "        });//End " + _NewLine;
            code += " " + _NewLine;
            code += "        function Search() { " + _NewLine;
            code += " " + _NewLine;
            code += "            if ($('#<%=txtSearch.ClientID%>').val() == '') { " + _NewLine;
            code += "                Materialize.toast('Please specify the filter.', 3000, 'toastCss'); " + _NewLine;
            code += "                return false; " + _NewLine;
            code += "            } " + _NewLine;
            code += "            $('#modal1').openModal(); " + _NewLine;
            code += " " + _NewLine;
            code += "            return true; " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += "        $('select').material_select(); " + _NewLine;
            code += " " + _NewLine;
            code += "        function GotoTop() { " + _NewLine;
            code += "            $(\"html, body\").animate({ scrollTop: 0 }, \"verry fast\"); " + _NewLine;
            code += "            //    $(\"html, body\").scrollTop(); " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += "        //For Validate Type " + _NewLine;
            code += "        function ForceNumberTextBox() { " + _NewLine;
            code += "            $(\".ForceNumber\").ForceNumericOnly(); " + _NewLine;
            code += "        } " + _NewLine;

            code += " </script>";
            return code;
        }



        private string GenModalProgress()
        {
            string code = "";
            code += " " + _NewLine;
            code += "       <!-- Modal Structure --> " + _NewLine;
            code += "    <div id=\"modal1\" class=\"modal\"> " + _NewLine;
            code += "        <div class=\"modal-content\"> " + _NewLine;
            code += "            <p>Loading</p> " + _NewLine;
            code += "            <div class=\"progress\"> " + _NewLine;
            code += "                <div class=\"indeterminate\"></div> " + _NewLine;
            code += "            </div> " + _NewLine;
            code += "        </div> " + _NewLine;
            code += "      " + _NewLine;
            code += "    </div>" + _NewLine;
            code += "    </div>" + _NewLine;
            return code;
        }
        public void Gen()
        {
            string _code = "";
            _code += GenHeadeFile();
            _code += GenContentHeadBegin();

            _code += GenReferJavaScript();

            _code += GenDocumentReady();

            _code += GenReferCSS();
            _code += GenContentHeadEnd();

            _code += GenContentBodyBegin();

            _code += GenSearch();

            _code += GenBeginResult();

            _code += GenTableRepeater();
            _code += GenPagger();

            _code += GenEndResult();

            _code += GenNoResult();


            _code += GenModalProgress();
            _code += GenContentBodyEnd();
            _FileCode.writeFile(_TableName + "List", _code, _fileType);
            //_FileCode.writeFile(FileName, _code, _fileType);
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