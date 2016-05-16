using StkGenCode.Code.Column;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCode : CodeBase
    {
        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" + _FileName.AspxTableCodeBehineName() + "\" Inherits=\"" + _TableName + "List\" %>" + _NewLine;

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

            code += " </asp:Content> " + _NewLine;
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

        protected string GenTableRepeater()
        {
            string code = "  ";
            code += "  <asp:Repeater ID = \"rpt" + _TableName + "Data\" runat= \"server\"  OnItemCommand=\"Sort_Click\"  OnItemCreated=\"rptSTK_USERData_ItemCreated\"    > " + _NewLine;

            code += "    <HeaderTemplate>  <table class=\"  bordered  striped \"> " + _NewLine;
            code += "  <thead> " + _NewLine;
            code += "  <tr> " + _NewLine;
            //code += " <th>Currency</th>  " + _NewLine;
            //code += "  <th>Description</th> " + _NewLine;
            //code += "   <th>Bank Note</th>" + _NewLine;
            //code += "  <th>Buying Rates	</th> " + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //code += " <th style=\"width: 300px; \">" + _DataColumn.ColumnName.ToUpper() + "</th> " + _NewLine;

                code += "<th> <asp:LinkButton ID =\"lnk" + dataColumn.ColumnName + "\"  Width =\"300px\"  runat =\"server\" CommandName=\"" + dataColumn.ColumnName + "\" CssClass=\"hrefclass\"  >" + dataColumn.ColumnName + "</asp:LinkButton></th> " + _NewLine;
            }

            code += "  </tr> " + _NewLine;
            code += "   </thead>" + _NewLine;
            code += "   <tbody> " + _NewLine;
            code += "  </HeaderTemplate> " + _NewLine;

            code += "   <ItemTemplate>" + _NewLine;
            code += " <tr> " + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //code += " <td>" + _DataColumn.ColumnName.ToUpper() + "</td> " + _NewLine;
                //code += " <td><%# Eval(\"" + _DataColumn.ColumnName + "\") %></td>";

                if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    code += "    <td class=\"borderRight chekBox" + dataColumn.ColumnName + "\"> " + _NewLine;
                    code += "                                    <p>" + _NewLine;
                    code += "                                        <input name='' type='radio' data-column-id=\"" + dataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0] + "\") %>\" <%# TagCheck(Eval(\"" + dataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    code += "                                    </p> " + _NewLine;
                    code += "                                </td> " + _NewLine;
                }
                else
                {
                    code += " <td class=\"td" + dataColumn.ColumnName + "\"> " + _NewLine;
                    if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                    {
                        code += "     <a id=\"btnShowPopup0\" target=\"_blank\" href=\"" + _TableName + "Web.aspx?Q=<%# Stk_QueryString.EncryptQuery( (Eval(\"" + dataColumn.ColumnName + "\")))%>\"" + _NewLine;
                        code += "                        title=\"\"> " + _NewLine;
                        code += "                        <span><%# Eval(\"" + dataColumn.ColumnName + "\") %> </span>  " + _NewLine;
                        code += "                    </a>" + _NewLine;
                    }
                    else
                    if ((dataColumn.DataType.ToString() == "System.DateTime"))
                    {
                        code += "       <span><%# StkGlobalDate.DateToTextEngFormat(Eval(\"" + dataColumn.ColumnName + "\")) %> </span> " + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.String"))
                    {
                        code += "       <span class=\"truncate\"><%# Eval(\"" + dataColumn.ColumnName + "\") %> </span> " + _NewLine;
                    }
                    else
                    {
                        code += "<span><%# Eval(\"" + dataColumn.ColumnName + "\") %> </span> " + _NewLine;
                    }

                    code += "                                    <div style=\"display: none\"> " + _NewLine;

                    if ((dataColumn.DataType.ToString() == "System.String"))
                    {
                        code += "<input data-column-id=\"" + dataColumn.ColumnName + "\" type=\"text\" MaxLength=\"" + dataColumn.MaxLength + "\" length=\"" + dataColumn.MaxLength + "\" class=\"validate truncate" + dataColumn.ColumnName + "\" value=\"<%# Eval(\"" + dataColumn.ColumnName + "\") %>\" style=\"height: unset; margin: 0px;\"> " + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.Int32"))

                    {
                        code += "<input data-column-id=\"" + dataColumn.ColumnName + "\" type=\"text\" class=\"validate ForceNumber \" value=\"<%# Eval(\"" + dataColumn.ColumnName + "\") %>\" style=\"height: unset; margin: 0px;\"> " + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.Decimal"))

                    {
                        code += "                                        <input data-column-id=\"" + dataColumn.ColumnName + "\" type=\"text\" class=\"validate ForceNumber2Digit \" value=\"<%# Eval(\"" + dataColumn.ColumnName + "\") %>\" style=\"height: unset; margin: 0px;\"> " + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.DateTime"))

                    {
                        code += "                                        <input data-column-id=\"" + dataColumn.ColumnName + "\"    class=\"datepicker\" type=\"date\" value=\"<%# StkGlobalDate.DateToTextEngFormat(Eval(\"" + dataColumn.ColumnName + "\")) %>\" style=\"height: unset; margin: 0px;\"> " + _NewLine;
                    }

                    code += "                                        <label class=\"lblSave\">Save</label> " + _NewLine;
                    code += "                                        <label class=\"lblCancel\">" + _NewLine;
                    code += "                                            Cancel</label> " + _NewLine;
                    code += "                                    </div> " + _NewLine;
                    code += "  </td>" + _NewLine;
                }

                // < input name = '' data - column - id = "BualuangExclusive" data - column - key = "<%# Eval("ID1") %>" type = 'radio' <%# ServiceSet(Eval("BualuangExclusive")) %> /><label for='test1'></label>
            }
            code += "   </tr> " + _NewLine;
            code += "   </ItemTemplate > " + _NewLine;
            code += "   </asp:Repeater> " + _NewLine;
            code += "  </tbody> " + _NewLine;
            code += "  </table> " + _NewLine;

            return code;
        }

        protected string GenPagger()
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

        protected string GenReferJavaScript()
        {
            string code = "";
            // code += "<script src=\"Bu/LocationManageCallServices.js\"></script>" + _NewLine;
            //code += "<script src=\"Bu/AutoCompleteService.js\"></script>" + _NewLine;
            code += "<script src=\"Module/Pagger/jquery.simplePagination.js\"></script>" + _NewLine;

            code += "<script src=\"Js_U/" + _FileName.JsCodeName() + "\"></script>" + _NewLine;
            return code;
        }

        protected string GenReferCss()

        {
            return " <link href=\"Module/Search/SearchControl.css\" rel=\"stylesheet\" />" + _NewLine;
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

        protected string GenNoResult()
        {
            string code = "";

            code += " <div id=\"DivNoresults\" class=\"container\" visible=\"false\" runat=\"server\"> " + _NewLine;
            code += "        <p class=\"flow-text\">No results found. Try the following:</p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Make sure all words are spelled correctly. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Try different keywords. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "        <p> " + _NewLine;
            code += "            Try more general keywords. " + _NewLine;
            code += "        </p> " + _NewLine;
            code += "    </div>";

            return code;
        }

        protected string GenBeginResult()
        {
            var code = "<div id=\"divResult\" runat=\"server\">";

            return code;
        }

        protected string GenEndResult()
        {
            var code = "</div>";
            return code;
        }

        //JavaScript Method ==================================================================================
        public string PaginationJavaScript()
        {
            string code = "";
            code += "            $('#ulpage').pagination({ " + _NewLine;
            //code += "                items: '<%=  ViewState[\"recordCount\"].ToString()%>', " + _NewLine;

            code += "items: '<%= RecordCount.ToString()%>',   " + _NewLine;
            code += "                itemsOnPage: '<%= PageSize%>', " + _NewLine;
            code += " " + _NewLine;
            code += "                prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />', " + _NewLine;
            code += "                nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />', " + _NewLine;
            code += " " + _NewLine;
            //code += "                currentPage: '<%=  ViewState[\"CurrentPage\"].ToString()%>', " + _NewLine;
            code += " currentPage: '<%=   CurrentPage.ToString()%>',";
            code += "                //cssStyle: 'light-theme', " + _NewLine;
            code += "                onPageClick: function (event, page) { " + _NewLine;
            code += "                    var pagenum = ''; " + _NewLine;
            code += "                    if (event < 10) { " + _NewLine;
            code += "                        pagenum = '0' + event; " + _NewLine;
            code += "                    } " + _NewLine;
            code += "                    else { " + _NewLine;
            code += "                        pagenum = event; " + _NewLine;
            code += "                    } " + _NewLine;
            code += "                    var code = 'ctl00$ContentPlaceHolder1$rpt" + _TableName + "Pagger$ctl' + pagenum + '$lnkPage'; " + _NewLine;
            code += " " + _NewLine;

            code += "   $('#modal1').openModal();  " + _NewLine;
            code += "                    __doPostBack(code, ''); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            return code;
        }

        public string LeanModalJavaScript()
        {
            string code = "";
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
            return code;
        }

        public string EditTableJavaScript()
        {
            string code = "";

            code += "//Edit table-------------------------------------------------------------------------------------------- " + _NewLine;

            //ClassSet = ColumnString.GenLineString(_ds, _TableName, ".td{0},");
            //ClassSet = ClassSet.TrimEnd(',');

            var classTextBox = GenClassTextBoxList();
            code += "            $('" + classTextBox + "').dblclick(function () { " + _NewLine;

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
            //code += "                $(this).parent().parent().removeClass(\"widthEditBig\"); " + _NewLine;
            //code += "                $(this).parent().parent().removeClass(\"widthEditSmall\"); " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "  $(\".lblSave\").click(function (event) { " + _NewLine;
            code += "                var tdContent = $(this).parent().parent(); " + _NewLine;
            code += "                var inputBox = tdContent.find(\"input:text\")[0]; //Get Data inputbox " + _NewLine;
            code += "                var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " + _NewLine;
            code += "                var column = inputBox.attributes['data-column-id'].value; " + _NewLine;
            code += "                var data = inputBox.value; " + _NewLine;
            code += " " + _NewLine;
            code += "                var txtspan = tdContent.find(\"span\")[0]; " + _NewLine;
            code += "                var oldData = trim(txtspan.innerText,\" \"); " + _NewLine;
            code += " " + _NewLine;
            code += "              " + _NewLine;
            code += "                if (data == oldData) " + _NewLine;
            code += "                { " + _NewLine;
            code += "                    //CalCell " + _NewLine;
            code += "                    tdContent.find(\"span\").show(); " + _NewLine;
            code += "                    tdContent.find(\"div\").hide(); " + _NewLine;
            code += "                    return; " + _NewLine;
            code += "                    // " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "                //Save Data To CodeBehide " + _NewLine;
            code += "                var result = " + _TableName + "Service.SaveColumn(id, column, data); " + _NewLine;
            code += " " + _NewLine;
            code += "                if (result == true) { " + _NewLine;
            code += "                    tdContent.find(\"span\").show(); " + _NewLine;
            code += "                    tdContent.find(\"div\").hide(); " + _NewLine;
            code += "                    tdContent.removeClass(\"widthEditBig\"); " + _NewLine;
            code += "                    tdContent.removeClass(\"widthEditSmall\"); " + _NewLine;
            code += "                    //tdContent.addClass(\"saved\"); " + _NewLine;
            code += "                   " + _NewLine;
            code += "                    //tdContent.find(\"span\")[0].innerText = data; //Swap Value " + _NewLine;
            code += "                    //tdContent.find(\"span\")[0].className += \"saved\"; " + _NewLine;
            code += "                      " + _NewLine;
            code += "                    txtspan.innerText = data; " + _NewLine;
            code += "                    txtspan.className = \"saved\"; " + _NewLine;
            code += "                    Materialize.toast('Your data has been saved.', 3000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    Materialize.toast(MsgError, 5000, 'toastCss'); " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "            });" + _NewLine;

            string classCheck = GenClassCheckBoxList();
            //code += "            $('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
            code += "            $('" + classCheck + "').dblclick(function () { " + _NewLine;

            code += "                // <p><input name = '' type = 'radio' id = 'test1'  checked= 'true' /><label for= 'test1'></label></p> " + _NewLine;
            code += "                var chk = $(this).find('input:radio')[0]; " + _NewLine;
            code += " " + _NewLine;
            code += "                //  var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " + _NewLine;
            code += "                var id = chk.attributes['data-column-key'].value; " + _NewLine;

            code += "                var column = chk.attributes['data-column-id'].value; " + _NewLine;
            code += "                var Data = \"\"; " + _NewLine;
            code += " " + _NewLine;
            code += "                var status = false; " + _NewLine;
            code += "                if (chk.checked) { " + _NewLine;
            code += "                    status = false; " + _NewLine;
            code += "                    Data = \"0\"; " + _NewLine;
            code += "                } " + _NewLine;
            code += "                else { " + _NewLine;
            code += "                    status = true; " + _NewLine;
            code += "                    Data = \"1\"; " + _NewLine;
            code += "                } " + _NewLine;
            code += " " + _NewLine;
            code += "                var result = " + _TableName + "Service.SaveColumn(id, column, Data); " + _NewLine;
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

            return code;
        }

        public string AutocompleteOneJavaScript()
        {
            string code = "";
            code += "//For Autocomplete  ----------------------------------------------------------------------------------- " + _NewLine;
            //code += "            $(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;
            //code += "              $('" + ClassTextBox + "').autocomplete({ " + _NewLine;
            code += " $('#<%= txtSearch.ClientID%>').autocomplete({ " + _NewLine;
            code += " " + _NewLine;
            code += "  source: function (request, response) { " + _NewLine;
            code += " " + _NewLine;
            //code += "                    var postCode = this.element[0].parentNode.parentNode.parentNode.childNodes[13].innerText; " + _NewLine;
            //code += "                    var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += "var keyword = this.element[0].value " + _NewLine;
            code += " " + _NewLine;
            code += "                    var data = " + _TableName + "Service.GetKeyWordsAllColumn(keyword); " + _NewLine;
            code += " " + _NewLine;
            code += "                    response(data); " + _NewLine;
            code += "                    //$.ajax({ " + _NewLine;
            code += "                    //    url: \"/WebTemplate/LocationManage.aspx/GenGetKeyWordsAllColumn\", " + _NewLine;
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
            return code;
        }

        protected string AutocompleteMutilColumnJavaScript()
        {
            string code = "";
            string classIds = ColumnString.GenLineString(_ds, ".{0},");
            classIds = classIds.TrimEnd(',');
            //code += "    $(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;
            code += "    $(\"" + classIds + "\").autocomplete({ " + _NewLine;

            code += " " + _NewLine;
            code += "                source: function (request, response) { " + _NewLine;
            code += " " + _NewLine;

            code += "                    var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += " " + _NewLine;
            code += "                    var data = " + _TableName + "Service.GetKeyWordsOneColumn(column, request.term); " + _NewLine;
            code += " " + _NewLine;
            code += "                    response(data); " + _NewLine;
            code += "                   " + _NewLine;
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
            code += "            });" + _NewLine;
            return code;
        }

        public string ForceNumberTextBoxJavaScript()
        {
            string code = "";

            code += "        //For Validate Type " + _NewLine;
            code += "        function ForceNumberTextBox() { " + _NewLine;
            // code += "            $(\".ForceNumber\").ForceNumericOnly(); " + _NewLine;

            code += "$(\".ForceNumber\").ForceNumericOnly();  " + _NewLine;
            code += "$(\".ForceNumber2Digit\").ForceNumericOnly2Digit(); " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        public string SearchJavaScript()
        {
            string code = "";
            code += "        function Search() { " + _NewLine;
            code += " " + _NewLine;
            code += "//Uncoment for chek empty" + _NewLine;
            code += "if ($('#<%=txtSearch.ClientID %>').val() == '') { " + _NewLine;
            code += "Materialize.toast('Please specify the filter.', 3000, 'toastCss'); " + _NewLine;
            code += "return false; " + _NewLine;
            code += "} " + _NewLine;
            code += " $('#modal1').openModal(); " + _NewLine;
            code += " " + _NewLine;
            code += "            return true; " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            return code;
        }

        private string GenDocumentReady()
        {
            string code = " <script> " + _NewLine;
            code += "     var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.'; " + _NewLine;
            code += " " + _NewLine;
            code += "        $(document).ready(function () { " + _NewLine;
            code += "            ForceNumberTextBox(); " + _NewLine;
            code += "            //For Change Lange " + _NewLine;

            code += "            $('.datepicker').pickadate({ " + _NewLine;
            code += "                selectMonths: true, // Creates a dropdown to control month  " + _NewLine;
            code += "                selectYears: 15,// Creates a dropdown of 15 years to control year,  " + _NewLine;
            code += "                format: 'd mmmm yyyy', " + _NewLine;
            code += "            });" + _NewLine;

            //code += "            $('.dropdown-button').hover(function () { " + _NewLine;
            //code += "                GotoTop(); " + _NewLine;
            //code += "            }); " + _NewLine;
            code += " " + _NewLine;
            //code += "            $('select').material_select(); " + _NewLine;
            code += " " + _NewLine;
            //code += "            $('#ulpage').pagination({ " + _NewLine;
            //code += "                items: '<%=  ViewState[\"recordCount\"].ToString()%>', " + _NewLine;
            //code += "                itemsOnPage: '<%= PageSize%>', " + _NewLine;
            //code += " " + _NewLine;
            //code += "                prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />', " + _NewLine;
            //code += "                nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />', " + _NewLine;
            //code += " " + _NewLine;
            //code += "                currentPage: '<%=  ViewState[\"CurrentPage\"].ToString()%>', " + _NewLine;
            //code += "                //cssStyle: 'light-theme', " + _NewLine;
            //code += "                onPageClick: function (event, page) { " + _NewLine;
            //code += "                    var pagenum = ''; " + _NewLine;
            //code += "                    if (event < 10) { " + _NewLine;
            //code += "                        pagenum = '0' + event; " + _NewLine;
            //code += "                    } " + _NewLine;
            //code += "                    else { " + _NewLine;
            //code += "                        pagenum = event; " + _NewLine;
            //code += "                    } " + _NewLine;
            //code += "                    var code = 'ctl00$ContentPlaceHolder1$rpt" + _TableName + "Pagger$ctl' + pagenum + '$lnkPage'; " + _NewLine;
            //code += " " + _NewLine;
            //code += "                    __doPostBack(code, ''); " + _NewLine;
            //code += "                } " + _NewLine;
            //code += " " + _NewLine;
            //code += "            }); " + _NewLine;
            code += PaginationJavaScript();

            code += " " + _NewLine;

            code += LeanModalJavaScript();

            code += EditTableJavaScript();

            code += AutocompleteOneJavaScript();

            //code += "            //Fix Stlyte " + _NewLine;
            //code += "            var UA = navigator.userAgent; " + _NewLine;
            //code += "            var html = document.documentElement; " + _NewLine;
            //code += "            if (UA.indexOf(\"IEMobile\") === -1) { " + _NewLine;
            //code += "                if ((UA.indexOf(\"rv:11.\") !== -1) && (!html.classList.contains('ie11')) && window.navigator.msPointerEnabled) { " + _NewLine;
            //code += "                    html.classList.add(\"ie11\"); " + _NewLine;
            //code += "                } else if ((UA.indexOf(\"MSIE 10.\") !== -1) && (!html.classList.contains('ie10')) && window.navigator.msPointerEnabled) { " + _NewLine;
            //code += "                    html.classList.add(\"ie10\"); " + _NewLine;
            //code += "                } " + _NewLine;
            //code += "                else if ((UA.indexOf(\"MSIE 8.\") !== -1) && (!html.classList.contains('ie8')) && window.navigator.msPointerEnabled) { " + _NewLine;
            //code += "                    html.classList.add(\"ie8\"); " + _NewLine;
            //code += "                } " + _NewLine;
            //code += "            } " + _NewLine;
            //code += " " + _NewLine;

            code += "        });//End " + _NewLine;
            code += " " + _NewLine;

            code += SearchJavaScript();

            code += "        $('select').material_select(); " + _NewLine;
            code += " " + _NewLine;
            //code += "        function GotoTop() { " + _NewLine;
            //code += "            $(\"html, body\").animate({ scrollTop: 0 }, \"verry fast\"); " + _NewLine;
            //code += "            //    $(\"html, body\").scrollTop(); " + _NewLine;
            //code += "        } " + _NewLine;
            //code += " " + _NewLine;

            //code += "        //For Validate Type " + _NewLine;
            //code += "        function ForceNumberTextBox() { " + _NewLine;
            //// code += "            $(\".ForceNumber\").ForceNumericOnly(); " + _NewLine;

            //code += "$(\".ForceNumber\").ForceNumericOnly();  " + _NewLine;
            //code += "$(\".ForceNumber2Digit\").ForceNumericOnly2Digit(); " + _NewLine;
            //code += "        } " + _NewLine;
            code += ForceNumberTextBoxJavaScript();
            code += "</script>";
            return code;
        }

        protected string GenClassCheckBoxList()
        {
            string code = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //code += " <td>" + _DataColumn.ColumnName.ToUpper() + "</td> " + _NewLine;
                //code += " <td><%# Eval(\"" + _DataColumn.ColumnName + "\") %></td>";
                if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    code += $".chekBox{dataColumn.ColumnName},";
                    //code += "    <td class=\"borderRight chekBox" + _DataColumn.ColumnName + "\"> " + _NewLine;
                    //code += "                                    <p> " + _NewLine;
                    //code += "                                        <input name='' type='radio' data-column-id=\"" + _DataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0].ToString() + "\") %>\" <%# TagCheck(Eval(\"" + _DataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    //code += "                                    </p> " + _NewLine;
                    //code += "                                </td> " + _NewLine;
                }
                //else
                //{
                //    code += " <td class=\"td"+ _DataColumn.ColumnName + "\"> " + _NewLine;
                //    code += "       <span><%# Eval(\"" + _DataColumn.ColumnName + "\") %> </span> " + _NewLine;
                //    code += "                                    <div style=\"display: none\"> " + _NewLine;
                //    code += "                                        <input data-column-id=\"" + _DataColumn.ColumnName + "\" type=\"text\" class=\"validate " + _DataColumn.ColumnName + "\" value=\"<%# Eval(\"" + _DataColumn.ColumnName + "\") %>\"> " + _NewLine;
                //    code += "                                        <label class=\"lblSave\">Save</label> " + _NewLine;
                //    code += "                                        <label class=\"lblCancel\">" + _NewLine;
                //    code += "                                            Cancel</label> " + _NewLine;
                //    code += "                                    </div> " + _NewLine;
                //    code += "  </td>" + _NewLine;
                //}

                // < input name = '' data - column - id = "BualuangExclusive" data - column - key = "<%# Eval("ID1") %>" type = 'radio' <%# ServiceSet(Eval("BualuangExclusive")) %> /><label for='test1'></label>
            }

            code = code.TrimEnd(',');
            return code;
        }

        protected string GenClassTextBoxList()
        {
            string code = "";
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName))
                {
                    continue;
                }
                //code += " <td>" + _DataColumn.ColumnName.ToUpper() + "</td> " + _NewLine;
                //code += " <td><%# Eval(\"" + _DataColumn.ColumnName + "\") %></td>";
                if ((dataColumn.DataType.ToString() != "System.Boolean") || (dataColumn.DataType.ToString() != "System.Int16"))
                {
                    code += $".td{dataColumn.ColumnName},";
                    //code += "    <td class=\"borderRight chekBox" + _DataColumn.ColumnName + "\"> " + _NewLine;
                    //code += "                                    <p> " + _NewLine;
                    //code += "                                        <input name='' type='radio' data-column-id=\"" + _DataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0].ToString() + "\") %>\" <%# TagCheck(Eval(\"" + _DataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    //code += "                                    </p> " + _NewLine;
                    //code += "                                </td> " + _NewLine;
                }
                //else
                //{
                //    code += " <td class=\"td"+ _DataColumn.ColumnName + "\"> " + _NewLine;
                //    code += "       <span><%# Eval(\"" + _DataColumn.ColumnName + "\") %> </span> " + _NewLine;
                //    code += "                                    <div style=\"display: none\"> " + _NewLine;
                //    code += "                                        <input data-column-id=\"" + _DataColumn.ColumnName + "\" type=\"text\" class=\"validate " + _DataColumn.ColumnName + "\" value=\"<%# Eval(\"" + _DataColumn.ColumnName + "\") %>\"> " + _NewLine;
                //    code += "                                        <label class=\"lblSave\">Save</label> " + _NewLine;
                //    code += "                                        <label class=\"lblCancel\">" + _NewLine;
                //    code += "                                            Cancel</label> " + _NewLine;
                //    code += "                                    </div> " + _NewLine;
                //    code += "  </td>" + _NewLine;
                //}

                // < input name = '' data - column - id = "BualuangExclusive" data - column - key = "<%# Eval("ID1") %>" type = 'radio' <%# ServiceSet(Eval("BualuangExclusive")) %> /><label for='test1'></label>
            }

            code = code.TrimEnd(',');
            return code;
        }

        protected string GenModalProgress()
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
            //  code += "    </div>" + _NewLine;
            return code;
        }

        protected string GenSectionHeader()
        {
            string code = "";
            code += " <div class=\"container\">";
            code += "<h5>" + _TableName + "</h5> " + _NewLine;
            code += "<div class=\"divider\"></div>" + _NewLine;
            code += "</div>" + _NewLine;
            return code;
        }

        public override void Gen()
        {
            InnitProperties();
            string code = "";

            code += GenHeadeFile();

            code += GenContentHeadBegin();

            code += GenReferJavaScript();

            code += GenDocumentReady();

            code += GenReferCss();
            code += GenContentHeadEnd();

            code += GenContentBodyBegin();
            //_code += GenSectionHeader();
            code += GenSearch();

            code += GenBeginResult();

            code += GenTableRepeater();

            code += GenPagger();

            code += GenEndResult();

            code += GenNoResult();

            code += GenModalProgress();

            code += GenContentBodyEnd();

            _FileCode.writeFile(_FileName.AspxTableCodeName(), code);
        }
    }
}