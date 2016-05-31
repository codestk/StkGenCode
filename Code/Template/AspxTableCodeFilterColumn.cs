using StkGenCode.Code.Column;
using StkGenCode.Code.Name;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumn : CodeBase
    {
        #region Properties

        public AspxFromCode AspxFromCodeaspx;

        #endregion Properties

        #region Head

        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" + _FileName.AspxTableCodeFilterColumnBehineName() + "\" Inherits=\"" + _TableName + "Filter\" %>" + _NewLine;

            return code;
        }

        protected string GenReferJavaScript()
        {
            string code = "";
            // code += "<script src=\"Bu/LocationManageCallServices.js\"></script>" + _NewLine;
            //code += "<script src=\"Bu/AutoCompleteService.js\"></script>" + _NewLine;
            code += "<script src=\"Module/Pagger/jquery.simplePagination.js\"></script>" + _NewLine;

            code += "<script src=\"Js_U/" + _FileName.JsCodeName() + "\"></script>" + _NewLine;

            //Refer JsDropDownlist

            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
                {
                    foreach (MappingColumn map in _MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                        {
                            code += "<script src=\"Js_U/" + FileName.JsCodeName(map.TableName) + "\"></script>" + _NewLine;
                        }
                    }
                }
            }
            return code;
        }

        protected string GenReferCss()

        {
            return " <link href=\"Module/Search/SearchControl.css\" rel=\"stylesheet\" />" + _NewLine;
        }

        #endregion Head

        #region JavaScript

        private string SetDatepicker()
        {
            string code = "";
            code += "function SetDatepicker()" + _NewLine;
            code += "{" + _NewLine;
            code += "$('.datepicker').pickadate({ " + _NewLine;
            code += "selectMonths: true, // Creates a dropdown to control month  " + _NewLine;
            code += "selectYears: 15,// Creates a dropdown of 15 years to control year,  " + _NewLine;
            code += "format: 'd mmmm yyyy' " + _NewLine;
            code += "});" + _NewLine;
            code += "}" + _NewLine;
            return code;
        }

        private string GenDocumentReady()
        {
            string code = "";
            code += "$(document).ready(function () { " + _NewLine;

            code += "ForceNumberTextBox(); " + _NewLine;
            code += "SetDatepicker();" + _NewLine;

            code += "//For search dropdown" + _NewLine;
            // SetSelectCategory('#<%=drpTermId.ClientID %>');//Set slect box in page
            // $('select').material_select();
            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
                {
                    foreach (MappingColumn map in _MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                        {//SetSelectCategory('#<%=drpTermId.ClientID %>');
                            code += $"SetSelect{dataColumn.ColumnName}('#<%=drp{dataColumn.ColumnName}.ClientID %>');" + _NewLine;
                        }
                    }
                }

                code += "$('select').material_select(); " + _NewLine;
            }

            //code += "$('.datepicker').pickadate({ " + _NewLine;
            //code += "selectMonths: true, // Creates a dropdown to control month  " + _NewLine;
            //code += "selectYears: 15,// Creates a dropdown of 15 years to control year,  " + _NewLine;
            //code += "format: 'd mmmm yyyy' " + _NewLine;
            //code += "});" + _NewLine;

            code += LeanModalJs();//

            code += AutocompleteMutilColumnJs();

            code += "});//End ready " + _NewLine;

            return code;
        }

        private string SetTableJs()
        {
            string code = "";

            code += "  function SetTable() {" + _NewLine;

            code += MapControlHtmlToValiable(_ds);// var CategoryName = $('#<%=txtCategoryName.ClientID %>').val();" + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {//chek bok
                    code += $"   {dataColumn.ColumnName} ='';" + _NewLine;
                }
            }

            code += "" + _NewLine;
            code += "$('#modal1').openModal();" + _NewLine;
            code += $"var result = {_TableName}Service.Search(PageIndex, PageSize, SortExpression, SortDirection, {GetColumnParameter()},RederTable_Pagger);" + _NewLine;
            code += "" + _NewLine;
            code += "        }" + _NewLine;

            return code;
        }

        private string RederTable_Pagger()
        {
            string code = "";
            code += "function RederTable_Pagger(result) {" + _NewLine;
            code += "var totalRecord = 0;" + _NewLine;
            code += "" + _NewLine;
            code += "if (result.length > 0)" + _NewLine;
            code += "{" + _NewLine;
            code += "$('#tbResult').show();" + _NewLine;
            code += "$('#ulpage').show();" + _NewLine;
            code += "$('#DivNoresults').hide();" + _NewLine;
            code += "$(\"#tbResult > tbody:last\").children().remove();" + _NewLine;
            code += "for (var key in result)" + _NewLine;
            code += "{" + _NewLine;
            code += "if (result.hasOwnProperty(key))" + _NewLine;
            code += "{" + _NewLine;
            code += "totalRecord = result[key].RecordCount;" + _NewLine;

            code += GenTrTemplate();//********

            code += "$('#tbResult> tbody').append(TrTempplate);" + _NewLine;
            code += "}" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;
            code += "}" + _NewLine;
            code += "else" + _NewLine;
            code += "{" + _NewLine;
            code += "" + _NewLine;
            code += "$('#tbResult').hide();" + _NewLine;
            code += "$('#ulpage').hide();" + _NewLine;
            code += "$('#DivNoresults').show();" + _NewLine;
            code += "" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;
            code += "$('#modal1').closeModal();" + _NewLine;

            code += "BindEditTable();" + _NewLine;

            code += "SetPagger(totalRecord);" + _NewLine;

            //code += "$('select').material_select();" + _NewLine;

            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
                {
                    foreach (MappingColumn map in _MappingColumn)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                        {//SetSelectCategory('#<%=drpTermId.ClientID %>');
                         //    SetSelectCategoryID('.selectCategoryID');
                            code += "//Set Selectin table" + _NewLine;
                            code += $"SetSelect{dataColumn.ColumnName}('.select{dataColumn.ColumnName}');" + _NewLine;
                            code += "  $(\".select" + dataColumn.ColumnName + "\").each(function () {" + _NewLine;
                            code += "var selectInput = $(this);" + _NewLine;
                            code += "var DefaultSelected = selectInput.attr('data-column-value');" + _NewLine;
                            code += "selectInput.val(DefaultSelected);" + _NewLine;
                            code += "" + _NewLine;
                            code += "var selectedText = selectInput.find('option:selected').text();" + _NewLine;
                            code += "" + _NewLine;
                            code += "$(this).parent().prev()[0].innerText = selectedText" + _NewLine;
                            code += "" + _NewLine;
                            code += "            });" + _NewLine;
                            code += "" + _NewLine;
                        }
                    }
                }
            }

            code += "            }" + _NewLine;
            code += "" + _NewLine;
            code += "" + _NewLine;

            return code;
        }

        private string SetSelectInput()
        {
            string code = "";

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                foreach (MappingColumn map in _MappingColumn)
                {
                    if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != _TableName))
                    {
                        code += " function SetSelect" + dataColumn.ColumnName + "(control) {" + _NewLine;
                        code += "" + _NewLine;
                        code += "            var innitOption = '<option value=\"\">Please Select</option>';" + _NewLine;
                        code += $"            var result{map.TableName} = {map.TableName}Service.SelectAll();" + _NewLine;
                        code += "            $(control).append(innitOption);" + _NewLine;
                        code += "            $.each(result" + map.TableName + ", function (index, value) {" + _NewLine;
                        code += "//Appending the json items to the dropdown (select tag)" + _NewLine;
                        code += "//item is the id of your select tag" + _NewLine;
                        code += "" + _NewLine;
                        code += "var text = value.text;" + _NewLine;
                        code += "var value = value.value;" + _NewLine;
                        code += "" + _NewLine;
                        code += "$(control).append('<option value=\"' + value + '\">' + text + '</option>');" + _NewLine;
                        code += "" + _NewLine;
                        code += "            });" + _NewLine;
                        code += "" + _NewLine;
                        code += "   " + _NewLine;
                        code += "" + _NewLine;
                        code += "        }" + _NewLine;
                    }
                }
            }

            return code;
        }

        private string SetPaggerJs()
        {
            string code = "";

            code += "  function SetPagger(RecordCount) {" + _NewLine;
            code += "            $('#ulpage').pagination({" + _NewLine;
            code += "items: RecordCount," + _NewLine;
            code += "itemsOnPage: PageSize," + _NewLine;
            code += "" + _NewLine;
            code += "prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />'," + _NewLine;
            code += "nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />'," + _NewLine;
            code += "" + _NewLine;
            code += "currentPage: PageIndex,//cssStyle: 'light-theme'," + _NewLine;
            code += "onPageClick: function (event) {" + _NewLine;
            code += "" + _NewLine;
            code += "    if (event < 10) {" + _NewLine;
            code += "        PageIndex = '0' + event;" + _NewLine;
            code += "    }" + _NewLine;
            code += "    else {" + _NewLine;
            code += "        PageIndex = event;" + _NewLine;
            code += "    }" + _NewLine;
            code += "    SetTable();" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;
            code += "            });" + _NewLine;
            code += "" + _NewLine;
            code += "        }" + _NewLine;

            return code;
        }

        private string ClearValueJs()
        {
            string code = "";

            code += "function ClearValue() {" + _NewLine;
            code += "ClearInputValue(\".input-field input[type=text],.input-field  input[type=password],.input-field  input[type=checkbox],.input-field  select,.input-field  input[type=radio]\");" + _NewLine;
            code += "return false;" + _NewLine;
            code += "}" + _NewLine;

            return code;
        }

        private string Search()
        {
            string code = "";
            code += "function Search() {" + _NewLine;
            code += "PageIndex = 1;" + _NewLine;
            code += "SortExpression = '';" + _NewLine;
            code += "SortDirection = '';" + _NewLine;
            code += "ClearSort();" + _NewLine;
            code += "SetTable();" + _NewLine;

            code += "}";
            return code;
        }

        private string GlobalValiable()
        {
            string code = "";
            code += "var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.'; " + _NewLine;
            code += "var PageIndex = '1';" + _NewLine;
            code += "var PageSize = '10';" + _NewLine;
            code += "var SortExpression = '';" + _NewLine;
            code += "var SortDirection = '';" + _NewLine;
            return code;
        }

        private string BindEditTableJs()
        {
            string code = "";

            code += "//Edit table-------------------------------------------------------------------------------------------- " + _NewLine;

            //ClassSet = ColumnString.GenLineString(_ds, _TableName, ".td{0},");
            //ClassSet = ClassSet.TrimEnd(',');

            var classTextBox = GenClassTextBoxList();

            code += "function BindEditTable() {" + _NewLine;
            code += "$('" + classTextBox + "').dblclick(function () { " + _NewLine;

            code += " " + _NewLine;
            code += "var sp = $(this).find(\"span\"); " + _NewLine;
            code += "var di = $(this).find(\"div\"); " + _NewLine;
            code += "sp.hide(); " + _NewLine;
            code += "di.show(); " + _NewLine;
            code += " " + _NewLine;
            code += "di.focus(); " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;
            code += "            $(\".lblCancel\").click(function (event) { " + _NewLine;
            code += "$(this).parent().parent().find(\"span\").show(); " + _NewLine;
            code += "$(this).parent().parent().find(\"div\").hide(); " + _NewLine;
            //code += "$(this).parent().parent().removeClass(\"widthEditBig\"); " + _NewLine;
            //code += "$(this).parent().parent().removeClass(\"widthEditSmall\"); " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;
            code += " " + _NewLine;

            code += "" + _NewLine;
            code += "            $(\".lblSave\").click(function (event) {" + _NewLine;
            code += "var tdContent = $(this).parent().parent();" + _NewLine;
            code += "var inputBox = tdContent.find(\"input,select\")[0]; //Get Data inputbox" + _NewLine;
            code += "//if (inputBox == undefined) {" + _NewLine;
            code += "" + _NewLine;
            code += "//    inputBox = tdContent.find(\"select\")[0];" + _NewLine;
            code += "//}" + _NewLine;
            code += "" + _NewLine;
            code += "var id = $(this).parent().parent().parent().find(\"span\")[0].outerText; //Get first Td (ID1)" + _NewLine;
            code += "var column = inputBox.attributes['data-column-id'].value;" + _NewLine;
            code += "var data = inputBox.value;" + _NewLine;
            code += "" + _NewLine;

            //validate

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if (IsDropDown(dataColumn))
                {
                    code += "" + _NewLine;
                    code += "            if (column == \"" + dataColumn.ColumnName + "\")" + _NewLine;
                    code += "            {" + _NewLine;
                    code += "                if ($(inputBox).prop('selectedIndex') == 0) {" + _NewLine;
                    code += "                        $(inputBox).addClass(\"invalid\");" + _NewLine;
                    code += "                     Materialize.toast('please validate your input.', 3000, 'toastCss');  " + _NewLine;
                    code += "                    return;" + _NewLine;
                    code += "                }" + _NewLine;
                    code += "            }" + _NewLine;
                    code += "" + _NewLine;
                }
                else
                {
                    code += "            if (column == \"" + dataColumn.ColumnName + "\")" + _NewLine;
                    code += "            {" + _NewLine;
                    code += "                if ($(inputBox).val().trim() == '') {" + _NewLine;
                    code += "                        $(inputBox).addClass(\"invalid\");" + _NewLine;
                    code += "                     Materialize.toast('please validate your input.', 3000, 'toastCss');  " + _NewLine;

                    code += "                    return;" + _NewLine;
                    code += "                }" + _NewLine;
                    code += "            }" + _NewLine;
                    code += "" + _NewLine;
                }
            }

            //======Valide

            code += "" + _NewLine;
            code += "var txtspan = tdContent.find(\"span\")[0];" + _NewLine;
            code += "var oldData = trim(txtspan.innerText, \" \");" + _NewLine;
            code += "" + _NewLine;
            code += "if (data == oldData) {" + _NewLine;
            code += "    //CalCell" + _NewLine;
            code += "    tdContent.find(\"span\").show();" + _NewLine;
            code += "    tdContent.find(\"div\").hide();" + _NewLine;
            code += "    return;" + _NewLine;
            code += "    //" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;
            code += "//Save Data To CodeBehide" + _NewLine;
            code += $"var result = {_TableName}Service.SaveColumn(id, column, data);" + _NewLine;
            code += "" + _NewLine;
            code += "//Convert Select" + _NewLine;
            code += "if (inputBox.tagName == 'SELECT') {" + _NewLine;
            code += "    data = $(inputBox).find('option:selected').text();" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;
            code += "if (result == true) {" + _NewLine;
            code += "    tdContent.find(\"span\").show();" + _NewLine;
            code += "    tdContent.find(\"div\").hide();" + _NewLine;
            code += "    tdContent.removeClass(\"widthEditBig\");" + _NewLine;
            code += "    tdContent.removeClass(\"widthEditSmall\");" + _NewLine;
            code += "    //tdContent.addClass(\"saved\");" + _NewLine;
            code += "" + _NewLine;
            code += "    //tdContent.find(\"span\")[0].innerText = data; //Swap Value" + _NewLine;
            code += "    //tdContent.find(\"span\")[0].className += \"saved\";" + _NewLine;
            code += "" + _NewLine;
            code += "    txtspan.innerText = data;" + _NewLine;
            code += "    txtspan.className = \"saved\";" + _NewLine;
            code += "    Materialize.toast('Your data has been saved.', 3000, 'toastCss');" + _NewLine;
            code += "}" + _NewLine;
            code += "else {" + _NewLine;
            code += "    Materialize.toast(MsgError, 5000, 'toastCss');" + _NewLine;
            code += "}" + _NewLine;
            code += "" + _NewLine;

            code += "            });" + _NewLine;

            string classCheck = GenClassCheckBoxList();
            //code += "            $('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
            code += "            $('" + classCheck + "').dblclick(function () { " + _NewLine;

            code += "// <p><input name = '' type = 'radio' id = 'test1'  checked= 'true' /><label for= 'test1'></label></p> " + _NewLine;
            code += "var chk = $(this).find('input:radio')[0]; " + _NewLine;
            code += " " + _NewLine;
            code += "//  var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " + _NewLine;
            code += "var id = chk.attributes['data-column-key'].value; " + _NewLine;

            code += "var column = chk.attributes['data-column-id'].value; " + _NewLine;
            code += "var Data = \"\"; " + _NewLine;
            code += " " + _NewLine;
            code += "var status = false; " + _NewLine;
            code += "if (chk.checked) { " + _NewLine;
            code += "    status = false; " + _NewLine;
            code += "    Data = \"0\"; " + _NewLine;
            code += "} " + _NewLine;
            code += "else { " + _NewLine;
            code += "    status = true; " + _NewLine;
            code += "    Data = \"1\"; " + _NewLine;
            code += "} " + _NewLine;
            code += " " + _NewLine;
            code += "var result = " + _TableName + "Service.SaveColumn(id, column, Data); " + _NewLine;
            code += " " + _NewLine;
            code += "if (result == true) { " + _NewLine;
            code += "    //Display Message Display Checkbox " + _NewLine;
            code += "    chk.checked = status; " + _NewLine;
            code += "    Materialize.toast('Your data has been saved.', 3000, 'toastCss'); " + _NewLine;
            code += "} " + _NewLine;
            code += "else { " + _NewLine;
            code += "    Materialize.toast(MsgError, 5000, 'toastCss'); " + _NewLine;
            code += "} " + _NewLine;
            code += " " + _NewLine;
            code += "            }); " + _NewLine;

            code += "ForceNumberTextBoxEditMode();" + _NewLine;
            code += "SetDatepicker();//" + _NewLine;
            code += " }" + _NewLine;
            code += "//Edit table===================================================================================================================== " + _NewLine;

            return code;
        }

        private string ClearSortJs()
        {
            string code = "";
            code += " function ClearSort() {" + _NewLine;
            code += "            $('#tbResult').find('th').each(function() {" + _NewLine;
            code += "    var columnName = $(this).attr(\"data-column-id\");" + _NewLine;
            code += "$(this).html(columnName);" + _NewLine;
            code += "});" + _NewLine;
            code += "            }" + _NewLine;
            return code;
        }

        private string SortJs()
        {
            string code = "";
            code += "   function Sort(th) {" + _NewLine;
            code += "" + _NewLine;
            code += "            var ColumnSortName = th.attributes['data-column-id'].value;" + _NewLine;
            code += "            ClearSort();" + _NewLine;
            code += "            SortExpression = ColumnSortName;" + _NewLine;
            code += "            if (SortDirection == 'DESC') {" + _NewLine;
            code += "SortDirection = 'ASC';" + _NewLine;
            code += "" + _NewLine;
            code += "$(th).html(ColumnSortName + ' <i class=\"Small material-icons\">arrow_drop_up</i>');" + _NewLine;
            code += "            }" + _NewLine;
            code += "            else {" + _NewLine;
            code += "" + _NewLine;
            code += "SortDirection = 'DESC';" + _NewLine;
            code += "$(th).html(ColumnSortName + ' <i class=\"Small material-icons\">arrow_drop_down</i>');" + _NewLine;
            code += "            }" + _NewLine;
            code += "" + _NewLine;
            code += "            SetTable();" + _NewLine;
            code += "        }" + _NewLine;
            return code;
        }

        /// <summary>
        /// InnitModal
        /// </summary>
        /// <returns></returns>
        public string LeanModalJs()
        {
            string code = "";
            code += "            $('.modal-trigger').leanModal({ " + _NewLine;
            code += "dismissible: false, // Modal can be dismissed by clicking outside of the modal " + _NewLine;
            code += "opacity: .5, // Opacity of modal background " + _NewLine;
            code += "in_duration: 300, // Transition in duration " + _NewLine;
            code += "out_duration: 200, // Transition out duration " + _NewLine;
            code += "starting_top: '50%' " + _NewLine;
            code += " " + _NewLine;
            code += "//  ready: function () { alert('Ready'); }, // Callback for Modal open " + _NewLine;
            code += "//  complete: function () { alert('Closed'); } // Callback for Modal close " + _NewLine;
            code += "            }); " + _NewLine;
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
            //code += "    var postCode = this.element[0].parentNode.parentNode.parentNode.childNodes[13].innerText; " + _NewLine;
            //code += "    var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += "var keyword = this.element[0].value " + _NewLine;
            code += " " + _NewLine;
            code += "    var data = " + _TableName + "Service.GetKeyWordsAllColumn(keyword); " + _NewLine;
            code += " " + _NewLine;
            code += "    response(data); " + _NewLine;
            code += "    //$.ajax({ " + _NewLine;
            code += "    //    url: \"/WebTemplate/LocationManage.aspx/GenGetKeyWordsAllColumn\", " + _NewLine;
            code += "    //    dataType: \"json\", " + _NewLine;
            code += "    //    data: { " + _NewLine;
            code += "    //        Column: 'EnglishAddress3', " + _NewLine;
            code += "    //        value: request.term " + _NewLine;
            code += "    //    }, " + _NewLine;
            code += "    //    contentType: \"application/json; charset=utf-8\", " + _NewLine;
            code += "    //    success: function (data) { " + _NewLine;
            code += " " + _NewLine;
            code += "    //        response(data); " + _NewLine;
            code += "    //    }, " + _NewLine;
            code += "    //    error: function (msg) { " + _NewLine;
            code += "    //        debugger " + _NewLine;
            code += "    //        alert(msg); " + _NewLine;
            code += " " + _NewLine;
            code += "    //    } " + _NewLine;
            code += "    //}); " + _NewLine;
            code += "}, " + _NewLine;
            code += "minLength: 3, " + _NewLine;
            code += "select: function (event, ui) { " + _NewLine;
            code += "    //log(ui.item ?\"Selected: \" + ui.item.label :\"Nothing selected, input was \" + this.value); " + _NewLine;
            code += "}, " + _NewLine;
            code += "open: function () { " + _NewLine;
            code += "    $(this).removeClass(\"ui-corner-all\").addClass(\"ui-corner-top\"); " + _NewLine;
            code += "}, " + _NewLine;
            code += "close: function () { " + _NewLine;
            code += "    $(this).removeClass(\"ui-corner-top\").addClass(\"ui-corner-all\"); " + _NewLine;
            code += "} " + _NewLine;
            code += "            }); " + _NewLine;
            code += "            //For Autocomplete================================================================================== " + _NewLine;
            code += " " + _NewLine;
            return code;
        }

        protected string AutocompleteMutilColumnJs()
        {
            string code = "";
            //string classIds = ColumnString.GenLineString(_ds, ".{0},");

            //classIds = classIds.TrimEnd(',');

            string classIds = GetColumnParameter(".{0},");
            classIds = classIds.TrimEnd(',');
            //code += "    $(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;
            code += "    $(\"" + classIds + "\").autocomplete({ " + _NewLine;

            code += " " + _NewLine;
            code += "source: function (request, response) { " + _NewLine;
            code += " " + _NewLine;

            code += "    var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += " " + _NewLine;
            code += "    var data = " + _TableName + "Service.GetKeyWordsOneColumn(column, request.term); " + _NewLine;
            code += " " + _NewLine;
            code += "    response(data); " + _NewLine;
            code += "   " + _NewLine;
            code += "}, " + _NewLine;
            code += "minLength: 3, " + _NewLine;
            code += "select: function (event, ui) { " + _NewLine;
            code += "    //log(ui.item ?\"Selected: \" + ui.item.label :\"Nothing selected, input was \" + this.value); " + _NewLine;
            code += "}, " + _NewLine;
            code += "open: function () { " + _NewLine;
            code += "    $(this).removeClass(\"ui-corner-all\").addClass(\"ui-corner-top\"); " + _NewLine;
            code += "}, " + _NewLine;
            code += "close: function () { " + _NewLine;
            code += "    $(this).removeClass(\"ui-corner-top\").addClass(\"ui-corner-all\"); " + _NewLine;
            code += "} " + _NewLine;
            code += "            });" + _NewLine;
            return code;
        }

        /// <summary>
        ///บังคับให้กรอกได้แต่ตัวเลข
        /// </summary>
        /// <returns></returns>
        public string ForceNumberTableEditJs()
        {
            string code = "";

            code += "        //For Validate Type " + _NewLine;
            code += "        function ForceNumberTextBoxEditMode() { " + _NewLine;
            // code += "            $(\".ForceNumber\").ForceNumericOnly(); " + _NewLine;

            code += "$(\".ForceNumber\").ForceNumericOnly();  " + _NewLine;
            code += "$(\".ForceNumber2Digit\").ForceNumericOnly2Digit(); " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        public string ForceNumberFilterJs()
        {
            string code = "";
            code += "function ForceNumberTextBox() " + _NewLine;
            code += "{" + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //ถ้าเป็น DropDown ไม่ต้อง Force ตัวเลข
                if (IsDropDown(dataColumn))
                    continue;

                //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
                string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
                //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
                //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);

                //if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                //{
                //    continue;
                //}

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

        #endregion JavaScript

        #region Html

        private string SearcFilterHtml()
        {
            string code = "";
            code += " <div class=\"container\"> " + _NewLine;
            //code += "        <div class=\" col s12\"> " + _NewLine;

            //Usign Gen Text Box
            code += "        <div class=\"row\"> " + _NewLine;
            string txtBoxSet = AspxFromCodeaspx.GenControls(6, false);
            //txtBoxSet = txtBoxSet.Replace("s12", "s3");
            //txtBoxSet = "";
            code += txtBoxSet;
            code += "<div class=\"input-field col s12\"> " + _NewLine;
            code += " " + _NewLine;
            // code += "    <a class=\"waves-effect waves-light btn center\">Search</a> " + _NewLine;
            //code += "<asp:Button ID =\"btnSearch\" CssClass=\"waves-effect waves-light btn center\" OnClick=\"btnSearch_Click\"   runat=\"server\" Text=\"Search\" />" + _NewLine;
            //code += "<asp:Button ID=\"btnClear\" CssClass=\"waves-effect waves-light btn center\" OnClientClick=\"javascript:return ClearValue();\" runat=\"server\" Text=\"Clear\" />";
            code += "<input id=\"btnSearch\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"Search\" onclick=\"Search();\" />" + _NewLine;
            code += "<input id=\"btnClear\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"Clear\" onclick=\"javascript: return ClearValue();\" />" + _NewLine;
            code += "<input id =\"btnNew\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"New\" onclick=\"javascript: window.open('" + _FileName.AspxFromCodeName() + "', '_blank');\" />" + _NewLine;
            code += "</div> " + _NewLine;
            code += "            </div> " + _NewLine;
            code += "        " + _NewLine;
            code += "        </div> " + _NewLine;
            //code += "        </div> " + _NewLine;

            return code;
        }

        protected string GenTableFromJson()
        {
            string code = "  ";

            code += "<table id=tbResult class=\"  bordered  striped \"  style=\"display: none;\" > " + _NewLine;
            code += "  <thead> " + _NewLine;
            code += "  <tr> " + _NewLine;
            //code += " <th>Currency</th>  " + _NewLine;
            //code += "  <th>Description</th> " + _NewLine;
            //code += "   <th>Bank Note</th>" + _NewLine;
            //code += "  <th>Buying Rates	</th> " + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                //code += " <th style=\"width: 300px; \">" + dataColumn.ColumnName.ToUpper() + "</th> " + _NewLine;
                code += $"<th style=\"width:300px;\" data-column-id=\"{dataColumn.ColumnName}\"  onclick=\"Sort(this);\">{dataColumn.ColumnName}</th>" + _NewLine;
                // code += "<th> " + dataColumn.ColumnName + "\" CssClass=\"hrefclass\"  >" + dataColumn.ColumnName + " </th> " + _NewLine;
            }

            code += "</tr>" + _NewLine;
            code += "</thead>" + _NewLine;
            code += "<tbody>" + _NewLine;
            code += "</tbody>" + _NewLine;
            code += "</table>" + _NewLine;

            return code;
        }

        private string GenTrTemplate()
        {
            string code = "";

            code += "var TrTempplate =\"<tr>\";" + _NewLine;

            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {//check box
                    code += " var status" + dataColumn.ColumnName + " ='' ;" + _NewLine;
                    code += "if (result[key]." + dataColumn.ColumnName + " == '1')" + _NewLine;
                    code += "{" + _NewLine;
                    code += "status" + dataColumn.ColumnName + " = 'checked';" + _NewLine;
                    code += "} else" + _NewLine;
                    code += "{" + _NewLine;
                    code += "status" + dataColumn.ColumnName + " = '';" + _NewLine;
                    code += "}" + _NewLine;

                    code += "TrTempplate +=\"<td class='borderRight chekBox" + dataColumn.ColumnName + "'>\";" + _NewLine;
                    code += "TrTempplate +=\"<p>\";" + _NewLine;
                    code += "TrTempplate +=\"<input name='' type='radio' data-column-id='" + dataColumn.ColumnName + "' data-column-key='\"+result[key]." + _ds.Tables[0].PrimaryKey[0] + "+\"' \"+status" + dataColumn.ColumnName + "+\"/><label> </label>\"; " + _NewLine;
                    code += "TrTempplate +=\"</p>\"; ";
                    code += "TrTempplate +=\"</td> \";";
                }
                else
                {
                    code += "TrTempplate +=\"<td class='td" + dataColumn.ColumnName + "'>\";" + _NewLine;
                    //ใช้สำหรับทำ Drop down ตอน Display ก่อน Edit
                    if (IsDropDown(dataColumn))
                    {
                        // code += "TrTempplate +=\"<td class='borderRight chekBox" + dataColumn.ColumnName + "'>\";" + _NewLine;

                        code += "TrTempplate +=\"<span class='truncate'>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                    }
                    else if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                    {
                        code += "TrTempplate +=\"<a id='btnShowPopup0' target='_blank' href='" + _TableName + "Web.aspx?Q=\"+result[key]." + dataColumn.ColumnName + "+\"'\";" + _NewLine;
                        code += "TrTempplate +=\"title=''>\";" + _NewLine;
                        code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                        code += "TrTempplate +=\"</a>\";" + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.DateTime"))
                    {
                        //dateFormat(JsonDateToDate(result[key]." + dataColumn.ColumnName + "), 'd mmm yyyy')
                        code += "TrTempplate +=\"<span>\"+dateFormat(JsonDateToDate(result[key]." + dataColumn.ColumnName + "), 'd mmm yyyy')+\"</span>\";" + _NewLine;
                        //code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.String"))
                    {
                        code += "TrTempplate +=\"<span class=''>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                    }
                    else
                    {
                        code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                    }

                    //ตัว Edit DataTable ===========================================================================
                    code += "TrTempplate +=\"<div style='display: none'>\";" + _NewLine;

                    if (IsDropDown(dataColumn))
                    {
                        //TrTempplate += "<select id='selectCategoryID' class='selectCategoryID selectInputEditMode'></select>";
                        code += $"TrTempplate += \"<select id='select{dataColumn.ColumnName}' data-column-id='{dataColumn.ColumnName}'  data-column-value='\" + result[key].{dataColumn.ColumnName}+\"' class='select{dataColumn.ColumnName} selectInputEditMode' ></select>\";" + _NewLine;
                    }
                    else
                        if ((dataColumn.DataType.ToString() == "System.String"))
                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName + "' type='text' MaxLength='" + dataColumn.MaxLength + "' length='" + dataColumn.MaxLength + "' class='validate truncate" + dataColumn.ColumnName + "' value='\"+result[key]." + dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.Int32"))

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName + "' type='text' class='validate ForceNumber ' value='\"+result[key]." + dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.Decimal"))

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName + "' type='text' class='validate ForceNumber2Digit' value='\"+result[key]." + dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + _NewLine;
                    }
                    else if ((dataColumn.DataType.ToString() == "System.DateTime"))

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName + "'    class='datepicker' type='date' value='\"+dateFormat(JsonDateToDate(result[key]." + dataColumn.ColumnName + "),'d mmm yyyy')+\"' style='height: unset; margin: 0px;'>\";" + _NewLine;
                    }

                    code += "TrTempplate +=\"<label class='lblSave'>Save</label>\";" + _NewLine;
                    code += "TrTempplate +=\"<label class='lblCancel'>\";" + _NewLine;
                    code += "TrTempplate +=\"Cancel</label>\";" + _NewLine;
                    code += "TrTempplate +=\"</div>\";" + _NewLine;
                    code += "TrTempplate +=\"</td>\";" + _NewLine;
                }
            }
            code += "TrTempplate +=\"</tr>\";" + _NewLine;

            return code;
        }

        protected string PaggerHtml()
        {
            string code = "";
            code += "<ul id=\"ulpage\" class=\"pagination\"> " + _NewLine;

            code += "</ul>";

            return code;
        }

        protected string ModalProgressHtml()
        {
            string code = "";
            code += " " + _NewLine;
            code += "       <!-- Modal Structure --> " + _NewLine;
            code += "    <div id=\"modal1\" class=\"modal\"> " + _NewLine;
            code += "        <div class=\"modal-content\"> " + _NewLine;
            code += "            <p>Loading</p> " + _NewLine;
            code += "            <div class=\"progress\"> " + _NewLine;
            code += "<div class=\"indeterminate\"></div> " + _NewLine;
            code += "            </div> " + _NewLine;
            code += "        </div> " + _NewLine;
            code += "      " + _NewLine;
            code += "    </div>" + _NewLine;
            //  code += "    </div>" + _NewLine;
            return code;
        }

        protected string NoResultHtml()
        {
            string code = "";

            code += " <div id=\"DivNoresults\" class=\"container\"  style=\"display: none\" > " + _NewLine;
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

        #endregion Html

        #region Server Control

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

        #endregion Server Control

        //javascript Method
        private string GenJavaScript()
        {
            string code = "<script>" + _NewLine;

            code += GlobalValiable();

            code += GenDocumentReady();

            code += ForceNumberTableEditJs();

            code += Search();

            code += ClearValueJs();

            code += SortJs();

            code += ClearSortJs();

            code += SetTableJs();

            code += RederTable_Pagger();

            code += BindEditTableJs();

            code += SetPaggerJs();

            code += SetDatepicker();
            if (HaveDropDown())
            {
                code += SetSelectInput();
            }
            code += ForceNumberFilterJs();
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
                    //code += "    <p> " + _NewLine;
                    //code += "        <input name='' type='radio' data-column-id=\"" + _DataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0].ToString() + "\") %>\" <%# TagCheck(Eval(\"" + _DataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    //code += "    </p> " + _NewLine;
                    //code += "</td> " + _NewLine;
                }
                //else
                //{
                //    code += " <td class=\"td"+ _DataColumn.ColumnName + "\"> " + _NewLine;
                //    code += "       <span><%# Eval(\"" + _DataColumn.ColumnName + "\") %> </span> " + _NewLine;
                //    code += "    <div style=\"display: none\"> " + _NewLine;
                //    code += "        <input data-column-id=\"" + _DataColumn.ColumnName + "\" type=\"text\" class=\"validate " + _DataColumn.ColumnName + "\" value=\"<%# Eval(\"" + _DataColumn.ColumnName + "\") %>\"> " + _NewLine;
                //    code += "        <label class=\"lblSave\">Save</label> " + _NewLine;
                //    code += "        <label class=\"lblCancel\">" + _NewLine;
                //    code += "            Cancel</label> " + _NewLine;
                //    code += "    </div> " + _NewLine;
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
                    //code += "    <p> " + _NewLine;
                    //code += "        <input name='' type='radio' data-column-id=\"" + _DataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0].ToString() + "\") %>\" <%# TagCheck(Eval(\"" + _DataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    //code += "    </p> " + _NewLine;
                    //code += "</td> " + _NewLine;
                }
                //else
                //{
                //    code += " <td class=\"td"+ _DataColumn.ColumnName + "\"> " + _NewLine;
                //    code += "       <span><%# Eval(\"" + _DataColumn.ColumnName + "\") %> </span> " + _NewLine;
                //    code += "    <div style=\"display: none\"> " + _NewLine;
                //    code += "        <input data-column-id=\"" + _DataColumn.ColumnName + "\" type=\"text\" class=\"validate " + _DataColumn.ColumnName + "\" value=\"<%# Eval(\"" + _DataColumn.ColumnName + "\") %>\"> " + _NewLine;
                //    code += "        <label class=\"lblSave\">Save</label> " + _NewLine;
                //    code += "        <label class=\"lblCancel\">" + _NewLine;
                //    code += "            Cancel</label> " + _NewLine;
                //    code += "    </div> " + _NewLine;
                //    code += "  </td>" + _NewLine;
                //}

                // < input name = '' data - column - id = "BualuangExclusive" data - column - key = "<%# Eval("ID1") %>" type = 'radio' <%# ServiceSet(Eval("BualuangExclusive")) %> /><label for='test1'></label>
            }

            code = code.TrimEnd(',');
            return code;
        }

        public override void Gen()
        {
            InnitProperties();

            string code = "";

            code += GenHeadeFile();

            code += GenContentHeadBegin();

            code += GenReferJavaScript();

            code += GenJavaScript();

            code += GenReferCss();

            code += GenContentHeadEnd();

            code += GenContentBodyBegin();

            code += SearcFilterHtml();

            code += GenTableFromJson();

            code += PaggerHtml();

            code += NoResultHtml();

            code += ModalProgressHtml();

            code += GenContentBodyEnd();

            _FileCode.WriteFile(_FileName.AspxTableCodeFilterColumnName(), code);
        }
    }
}