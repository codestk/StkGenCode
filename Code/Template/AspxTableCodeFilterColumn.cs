using StkGenCode.Code.Name;
using System.Data;
using System.Drawing;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumn : CodeBase
    {
        //javascript Method
        private string GenJavaScript()
        {
            var code = "<script>" + NewLine;

            code += GlobalValiable();

            code += GenDocumentReady();

            code += ForceNumberTableEditJs();

            code += Search();

            code += ClearValueJs();

            code += SortJs();

            code += ClearSortJs();

            code += SetTableJs();

            code += RederTable_Pagger();

            code += AutocompleteMutilColumnJs();

            code += BindEditTableJs();

            code += SetPaggerJs();

            code += SetDatepicker();

            code += SetSelect();

            code += ForceNumberTextBox(false);
            code += "</script>";
            return code;
        }

        public override void Gen()

        {
            InnitProperties();

            var code = "";

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

            FileCode.WriteFile(FileName.AspxTableCodeFilterColumnName(), code);
        }

        #region Properties

        //public AspxFromCode AspxFromCodeaspx;

        #endregion Properties

        #region Head

        private string GenHeadeFile()
        {
            var code = "<%@ Page Title=\"" + TableName +
              "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" +
              FileName.AspxTableCodeFilterColumnBehineName() + "\" Inherits=\"WebApp." + TableName + "Filter\" %>" +
              NewLine;

            return code;
        }

        protected string GenReferJavaScript()
        {
            var code = "";
            // code += "<script src=\"Bu/LocationManageCallServices.js\"></script>" + _NewLine;
            //code += "<script src=\"Bu/AutoCompleteService.js\"></script>" + _NewLine;
            code += "<script src=\"Module/Pagger/jquery.simplePagination.js\"></script>" + NewLine;

            code += "<script src=\"Js_U/" + FileName.JsCodeName() + "\"></script>" + NewLine;

            //Refer JsDropDownlist

            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
                {
                    foreach (var map in DropColumns)
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

        protected string GenReferCss()

        {
            return " <link href=\"Module/Search/SearchControl.css\" rel=\"stylesheet\" />" + NewLine;
        }

        #endregion Head

        #region JavaScript

        private string SetDatepicker()
        {
            var code = "";
            code += "function SetDatepicker()" + NewLine;
            code += "{" + NewLine;
            code += "$('.datepicker').pickadate({ " + NewLine;
            code += "selectMonths: true, // Creates a dropdown to control month  " + NewLine;
            code += "selectYears: 15,// Creates a dropdown of 15 years to control year,  " + NewLine;
            code += "format: 'd mmm yyyy' " + NewLine;
            code += "});" + NewLine;
            code += "}" + NewLine;
            return code;
        }

        private string GenDocumentReady()
        {
            var code = "";

            code += "$(document).ready(function () { " + NewLine;

            code += "ForceNumberTextBox(); " + NewLine;

            code += "SetDatepicker();" + NewLine;

            code += "//For search dropdown" + NewLine;

            code += BindSelectOption();

            if (HaveDropDown())
            {
                code += "$('select').material_select(); " + NewLine;
            }

            code += LeanModalJs(); //

            //code += AutocompleteMutilColumnJs();

            code += "});//End ready " + NewLine;

            return code;
        }

        private string SetTableJs()
        {
            var code = "";

            code += "  function SetTable() {" + NewLine;

            //ไม่ใช่้ EXcepcomunl เพราะใช้ในการ Search
            code += MapControlHtmlToValiable(Ds, false);

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                 (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //chek bok false  == ''

                    code += $" if ({dataColumn.ColumnName}== false)" + NewLine;
                    code += " {" + NewLine;
                    code += $"{dataColumn.ColumnName} = ''";

                    code += " }" + NewLine;
                    //code += $"{dataColumn.ColumnName} ='';" + NewLine;
                }
            }

            code += "" + NewLine;
            code += "$('#modal1').openModal();" + NewLine;
            code +=
             $"var result = {TableName}Service.Search(PageIndex, PageSize, SortExpression, SortDirection, {GetColumnParameter()},RederTable_Pagger);" +
             NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;

            return code;
        }

        private string RederTable_Pagger()
        {
            var code = "";
            code += "function RederTable_Pagger(result) {" + NewLine;
            code += "var totalRecord = 0;" + NewLine;
            code += "" + NewLine;
            code += "if (result.length > 0)" + NewLine;
            code += "{" + NewLine;
            code += "$('#tbResult').show();" + NewLine;
            code += "$('#ulpage').show();" + NewLine;
            code += "$('#DivNoresults').hide();" + NewLine;
            code += "$(\"#tbResult > tbody:last\").children().remove();" + NewLine;
            code += "for (var key in result)" + NewLine;
            code += "{" + NewLine;
            code += "if (result.hasOwnProperty(key))" + NewLine;
            code += "{" + NewLine;
            code += "totalRecord = result[key].RecordCount;" + NewLine;

            code += GenTrTemplate(); //********

            code += "$('#tbResult> tbody').append(TrTempplate);" + NewLine;
            code += "}" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;
            code += "else" + NewLine;
            code += "{" + NewLine;
            code += "" + NewLine;
            code += "$('#tbResult').hide();" + NewLine;
            code += "$('#ulpage').hide();" + NewLine;
            code += "$('#DivNoresults').show();" + NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "$('#modal1').closeModal();" + NewLine;

            code += "BindEditTable();" + NewLine;

            code += "SetPagger(totalRecord);" + NewLine;

            code += "AutoCompleteEditMode();";
            //code += "$('select').material_select();" + _NewLine;

            if (HaveDropDown())
            {
                foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
                {
                    foreach (var map in DropColumns)
                    {
                        if ((map.ColumnName == dataColumn.ColumnName) && (map.TableName != TableName))
                        {
                            //SetSelectCategory('#<%=drpTermId');
                            //SetSelectCategoryID('.selectCategoryID');
                            code += "//Set Selectin table" + NewLine;
                            code += $"SetSelect{dataColumn.ColumnName}('.select{dataColumn.ColumnName}');" + NewLine;
                            code += "  $(\".select" + dataColumn.ColumnName + "\").each(function () {" + NewLine;
                            code += "var selectInput = $(this);" + NewLine;
                            code += "var DefaultSelected = selectInput.attr('data-column-value');" + NewLine;
                            code += "selectInput.val(DefaultSelected);" + NewLine;
                            code += "" + NewLine;
                            code += "var selectedText = selectInput.find('option:selected').text();" + NewLine;
                            code += "" + NewLine;
                            code += "$(this).parent().prev()[0].innerText = selectedText" + NewLine;
                            code += "" + NewLine;
                            code += "});" + NewLine;
                            code += "" + NewLine;
                        }
                    }
                }
            }

            code += "}" + NewLine;
            code += "" + NewLine;
            code += "" + NewLine;

            return code;
        }

        private string SetPaggerJs()
        {
            var code = "";

            code += "  function SetPagger(RecordCount) {" + NewLine;
            code += "$('#ulpage').pagination({" + NewLine;
            code += "items: RecordCount," + NewLine;
            code += "itemsOnPage: PageSize," + NewLine;
            code += "" + NewLine;
            code += "prevText: '<img class=\"iconDirection\" src=\"Images/1457020750_arrow-left-01.svg\" />'," + NewLine;
            code += "nextText: '<img class=\"iconDirection\" src=\"Images/1457020740_arrow-right-01.svg\" />'," +
              NewLine;
            code += "" + NewLine;
            code += "currentPage: PageIndex,//cssStyle: 'light-theme'," + NewLine;
            code += "onPageClick: function (event) {" + NewLine;
            code += "" + NewLine;
            code += "if (event < 10) {" + NewLine;
            code += "PageIndex = '0' + event;" + NewLine;
            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "PageIndex = event;" + NewLine;
            code += "}" + NewLine;
            code += "SetTable();" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "});" + NewLine;
            code += "" + NewLine;
            code += "}" + NewLine;

            return code;
        }

        private string ClearValueJs()
        {
            var code = "";

            code += "function ClearValue() {" + NewLine;
            code +=
             "ClearInputValue(\".input-field input[type=text],.input-field  input[type=password],.input-field  input[type=checkbox],.input-field  select,.input-field  input[type=radio]\");" +
             NewLine;
            code += "return false;" + NewLine;
            code += "}" + NewLine;

            return code;
        }

        private string Search()
        {
            var code = "";
            code += "function Search() {" + NewLine;
            code += "PageIndex = 1;" + NewLine;
            code += "SortExpression = '';" + NewLine;
            code += "SortDirection = '';" + NewLine;
            code += "ClearSort();" + NewLine;
            code += "SetTable();" + NewLine;

            code += "}";
            return code;
        }

        private string GlobalValiable()
        {
            var code = "";
            code +=
             "var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.'; " +
             NewLine;
            code += "var PageIndex = '1';" + NewLine;
            code += "var PageSize = '10';" + NewLine;
            code += "var SortExpression = '';" + NewLine;
            code += "var SortDirection = '';" + NewLine;
            return code;
        }

        private string BindEditTableJs()
        {
            var code = "";

            code +=
             "//Edit table-------------------------------------------------------------------------------------------- " +
             NewLine;

            //ClassSet = ColumnString.GenLineString(_ds, _TableName, ".td{0},");
            //ClassSet = ClassSet.TrimEnd(',');

            var classTextBox = GenClassTextBoxList();

            code += "function BindEditTable() {" + NewLine;
            code += "$('" + classTextBox + "').dblclick(function () { " + NewLine;

            code += " " + NewLine;
            code += "var sp = $(this).find(\"span\"); " + NewLine;
            code += "var di = $(this).find(\"div\"); " + NewLine;

            //สลับ Datepiker ให้ขึ้นถูกอัน
            code += " var $input =$(this).find(\".datepicker\").pickadate();" + NewLine;
            code += "if ($input.length != 0) {" + NewLine;
            code += "    // Use the picker object directly." + NewLine;
            code += "   var picker = $input.pickadate('picker');" + NewLine;
            code += "" + NewLine;
            code += "    picker.set('select', sp[\"0\"].innerText);" + NewLine;
            code += " }" + NewLine;
            //===========================================================================

            code += "sp.hide(); " + NewLine;
            code += "di.show(); " + NewLine;
            code += " " + NewLine;
            code += "di.focus(); " + NewLine;
            code += "}); " + NewLine;
            code += " " + NewLine;
            code += "$(\".lblCancel\").click(function (event) { " + NewLine;
            code += "$(this).parent().parent().find(\"span\").show(); " + NewLine;
            code += "$(this).parent().parent().find(\"div\").hide(); " + NewLine;
            //code += "$(this).parent().parent().removeClass(\"widthEditBig\"); " + _NewLine;
            //code += "$(this).parent().parent().removeClass(\"widthEditSmall\"); " + _NewLine;
            code += " " + NewLine;
            code += "}); " + NewLine;
            code += " " + NewLine;

            code += "" + NewLine;
            code += "$(\".lblSave\").click(function (event) {" + NewLine;
            code += "var tdContent = $(this).parent().parent();" + NewLine;
            code += "var inputBox = tdContent.find(\"input,select\")[0]; //Get Data inputbox" + NewLine;
            code += "//if (inputBox == undefined) {" + NewLine;
            code += "" + NewLine;
            code += "//inputBox = tdContent.find(\"select\")[0];" + NewLine;
            code += "//}" + NewLine;
            code += "" + NewLine;
            code += "var id = $(this).parent().parent().parent().find(\"span\")[0].outerText; //Get first Td (ID1)" +
              NewLine;
            code += "var column = inputBox.attributes['data-column-id'].value;" + NewLine;
            code += "var data = inputBox.value;" + NewLine;
            code += "" + NewLine;

            //validate

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (IsExceptionColumn(dataColumn, false))
                {
                    continue;
                }
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}

                if (IsDropDown(dataColumn))
                {
                    code += "" + NewLine;
                    code += "if (column == \"" + dataColumn.ColumnName + "\")" + NewLine;
                    code += "{" + NewLine;
                    code += "if ($(inputBox).prop('selectedIndex') == 0) {" + NewLine;
                    code += "$(inputBox).addClass(\"invalid\");" + NewLine;
                    code += " Materialize.toast('please validate your input.', 3000, 'toastCss');  " + NewLine;
                    code += "return;" + NewLine;
                    code += "}" + NewLine;
                    code += "}" + NewLine;
                    code += "" + NewLine;
                }
                else
                {
                    code += "if (column == \"" + dataColumn.ColumnName + "\")" + NewLine;
                    code += "{" + NewLine;
                    code += "if ($(inputBox).val().trim() == '') {" + NewLine;
                    code += "$(inputBox).addClass(\"invalid\");" + NewLine;
                    code += " Materialize.toast('please validate your input.', 3000, 'toastCss');  " + NewLine;

                    code += "return;" + NewLine;
                    code += "}" + NewLine;
                    code += "}" + NewLine;
                    code += "" + NewLine;
                }
            }

            //======Valide

            code += "" + NewLine;
            code += "var txtspan = tdContent.find(\"span\")[0];" + NewLine;
            code += "var oldData = trim(txtspan.innerText, \" \");" + NewLine;
            code += "" + NewLine;
            code += "if (data == oldData) {" + NewLine;
            code += "//CalCell" + NewLine;
            code += "tdContent.find(\"span\").show();" + NewLine;
            code += "tdContent.find(\"div\").hide();" + NewLine;
            code += "return;" + NewLine;
            code += "//" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "//Save Data To CodeBehide" + NewLine;
            code += $"var result = {TableName}Service.SaveColumn(id, column, data);" + NewLine;
            code += "" + NewLine;
            code += "//Convert Select" + NewLine;
            code += "if (inputBox.tagName == 'SELECT') {" + NewLine;
            code += "data = $(inputBox).find('option:selected').text();" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "if (result == true) {" + NewLine;
            code += "tdContent.find(\"span\").show();" + NewLine;
            code += "tdContent.find(\"div\").hide();" + NewLine;
            code += "tdContent.removeClass(\"widthEditBig\");" + NewLine;
            code += "tdContent.removeClass(\"widthEditSmall\");" + NewLine;
            code += "//tdContent.addClass(\"saved\");" + NewLine;
            code += "" + NewLine;
            code += "//tdContent.find(\"span\")[0].innerText = data; //Swap Value" + NewLine;
            code += "//tdContent.find(\"span\")[0].className += \"saved\";" + NewLine;
            code += "" + NewLine;
            code += "txtspan.innerText = data;" + NewLine;
            code += "txtspan.className = \"saved\";" + NewLine;
            code += "Materialize.toast('Your data has been saved.', 3000, 'toastCss');" + NewLine;
            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "Materialize.toast(MsgError, 5000, 'toastCss');" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;

            code += "});" + NewLine;

            var classCheck = GenClassCheckBoxList();
            //code += "$('.chekBoxAtm,.chekBoxBranch,.chekBoxSubBranch,.chekBoxMicroBranch,.chekBoxInternationalBranch,.chekBoxBusinessCenter,.chekBoxFXBooth,.chekBoxBualuangExclusive,.chekBoxFCDService,.chekBoxRemittanceService ,.chekBoxWesternUnionService').dblclick(function () { " + _NewLine;
            code += "$('" + classCheck + "').dblclick(function () { " + NewLine;

            code +=
             "// <p><input name = '' type = 'radio' id = 'test1'  checked= 'true' /><label for= 'test1'></label></p> " +
             NewLine;
            code += "var chk = $(this).find('input:radio')[0]; " + NewLine;
            code += " " + NewLine;
            code +=
             "//  var id = $(this).parent().parent().parent().find(\"td:first\")[0].outerText; //Get first Td (ID1) " +
             NewLine;
            code += "var id = chk.attributes['data-column-key'].value; " + NewLine;

            code += "var column = chk.attributes['data-column-id'].value; " + NewLine;
            code += "var Data = \"\"; " + NewLine;
            code += " " + NewLine;
            code += "var status = false; " + NewLine;
            code += "if (chk.checked) { " + NewLine;
            code += "status = false; " + NewLine;
            code += "Data = \"0\"; " + NewLine;
            code += "} " + NewLine;
            code += "else { " + NewLine;
            code += "status = true; " + NewLine;
            code += "Data = \"1\"; " + NewLine;
            code += "} " + NewLine;
            code += " " + NewLine;
            code += "var result = " + TableName + "Service.SaveColumn(id, column, Data); " + NewLine;
            code += " " + NewLine;
            code += "if (result == true) { " + NewLine;
            code += "//Display Message Display Checkbox " + NewLine;
            code += "chk.checked = status; " + NewLine;
            code += "Materialize.toast('Your data has been saved.', 3000, 'toastCss'); " + NewLine;
            code += "} " + NewLine;
            code += "else { " + NewLine;
            code += "Materialize.toast(MsgError, 5000, 'toastCss'); " + NewLine;
            code += "} " + NewLine;
            code += " " + NewLine;
            code += "}); " + NewLine;

            code += "ForceNumberTextBoxEditMode();" + NewLine;
            code += "SetDatepicker();//" + NewLine;
            code += " }" + NewLine;
            code +=
             "//Edit table===================================================================================================================== " +
             NewLine;

            return code;
        }

        private string ClearSortJs()
        {
            var code = "";
            code += "function ClearSort() {" + NewLine;
            code += "$('#tbResult').find('th').each(function() {" + NewLine;
            code += "var columnName = $(this).attr(\"data-column-id\");" + NewLine;
            code += "$(this).html(columnName);" + NewLine;
            code += "});" + NewLine;
            code += "}" + NewLine;
            return code;
        }

        private string SortJs()
        {
            var code = "";
            code += "function Sort(th) {" + NewLine;
            code += "" + NewLine;
            code += "var ColumnSortName = th.attributes['data-column-id'].value;" + NewLine;
            code += "ClearSort();" + NewLine;
            code += "SortExpression = ColumnSortName;" + NewLine;
            code += "if (SortDirection == 'DESC') {" + NewLine;
            code += "SortDirection = 'ASC';" + NewLine;
            code += "" + NewLine;
            code += "$(th).html(ColumnSortName + ' <i class=\"Small material-icons\">arrow_drop_up</i>');" + NewLine;
            code += "}" + NewLine;
            code += "else {" + NewLine;
            code += "" + NewLine;
            code += "SortDirection = 'DESC';" + NewLine;
            code += "$(th).html(ColumnSortName + ' <i class=\"Small material-icons\">arrow_drop_down</i>');" + NewLine;
            code += "}" + NewLine;
            code += "" + NewLine;
            code += "SetTable();" + NewLine;
            code += "}" + NewLine;
            return code;
        }

        /// <summary>
        ///  InnitModal
        /// </summary>
        /// <returns></returns>
        public string LeanModalJs()
        {
            var code = "";
            code += "$('.modal-trigger').leanModal({ " + NewLine;
            code += "dismissible: false, // Modal can be dismissed by clicking outside of the modal " + NewLine;
            code += "opacity: .5, // Opacity of modal background " + NewLine;
            code += "in_duration: 300, // Transition in duration " + NewLine;
            code += "out_duration: 200, // Transition out duration " + NewLine;
            code += "starting_top: '50%' " + NewLine;
            code += "" + NewLine;
            code += "//ready: function () { alert('Ready'); }, // Callback for Modal open " + NewLine;
            code += "//complete: function () { alert('Closed'); } // Callback for Modal close " + NewLine;
            code += "}); " + NewLine;
            return code;
        }

        public string AutocompleteOneJavaScript()
        {
            var code = "";
            code +=
             "//For Autocomplete  ----------------------------------------------------------------------------------- " +
             NewLine;
            //code += "$(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;
            //code += "  $('" + ClassTextBox + "').autocomplete({ " + _NewLine;
            code += " $('#<%= txtSearch').autocomplete({ " + NewLine;
            code += " " + NewLine;
            code += "  source: function (request, response) { " + NewLine;
            code += " " + NewLine;
            //code += "var postCode = this.element[0].parentNode.parentNode.parentNode.childNodes[13].innerText; " + _NewLine;
            //code += "var column = this.element[0].attributes[\"data-column-id\"].value; " + _NewLine;
            code += "var keyword = this.element[0].value " + NewLine;
            code += " " + NewLine;
            code += "var data = " + TableName + "Service.GetKeyWordsAllColumn(keyword); " + NewLine;
            code += " " + NewLine;
            code += "response(data); " + NewLine;
            code += "//$.ajax({ " + NewLine;
            code += "//url: \"/WebTemplate/LocationManage.aspx/GenGetKeyWordsAllColumn\", " + NewLine;
            code += "//dataType: \"json\", " + NewLine;
            code += "//data: { " + NewLine;
            code += "//Column: 'EnglishAddress3', " + NewLine;
            code += "//value: request.term " + NewLine;
            code += "//}, " + NewLine;
            code += "//contentType: \"application/json; charset=utf-8\", " + NewLine;
            code += "//success: function (data) { " + NewLine;
            code += " " + NewLine;
            code += "//response(data); " + NewLine;
            code += "//}, " + NewLine;
            code += "//error: function (msg) { " + NewLine;
            code += "//debugger " + NewLine;
            code += "//alert(msg); " + NewLine;
            code += " " + NewLine;
            code += "//} " + NewLine;
            code += "//}); " + NewLine;
            code += "}, " + NewLine;
            code += "minLength: 3, " + NewLine;
            code += "select: function (event, ui) { " + NewLine;
            code += "//log(ui.item ?\"Selected: \" + ui.item.label :\"Nothing selected, input was \" + this.value); " +
              NewLine;
            code += "}, " + NewLine;
            code += "open: function () { " + NewLine;
            code += "$(this).removeClass(\"ui-corner-all\").addClass(\"ui-corner-top\"); " + NewLine;
            code += "}, " + NewLine;
            code += "close: function () { " + NewLine;
            code += "$(this).removeClass(\"ui-corner-top\").addClass(\"ui-corner-all\"); " + NewLine;
            code += "} " + NewLine;
            code += "}); " + NewLine;
            code +=
             "//For Autocomplete================================================================================== " +
             NewLine;
            code += " " + NewLine;
            return code;
        }

        protected string AutocompleteMutilColumnJs()
        {
            var code = "";

            var classIds = GetColumnParameter(".{0},");
            classIds = classIds.TrimEnd(',');
            //code += "$(\".ThaiAddress2,.ThaiAddress3,.ThaiProvince,.EnglishAddress2,.EnglishAddress3,.EnglishProvince\").autocomplete({ " + _NewLine;

            code += "function AutoCompleteEditMode() { " + NewLine;
            code += "$(\"" + classIds + "\").autocomplete({ " + NewLine;

            code += " " + NewLine;
            code += "source: function (request, response) { " + NewLine;
            code += " " + NewLine;

            code += "var column = this.element[0].attributes[\"data-column-id\"].value; " + NewLine;
            code += " " + NewLine;
            code += "var data = " + TableName + "Service.GetKeyWordsOneColumn(column, request.term); " + NewLine;
            code += " " + NewLine;
            code += "response(data); " + NewLine;
            code += "" + NewLine;
            code += "}, " + NewLine;
            code += "minLength: 3, " + NewLine;
            code += "select: function (event, ui) { " + NewLine;
            code += "//log(ui.item ?\"Selected: \" + ui.item.label :\"Nothing selected, input was \" + this.value); " +
              NewLine;
            code += "}, " + NewLine;
            code += "open: function () { " + NewLine;
            code += "$(this).removeClass(\"ui-corner-all\").addClass(\"ui-corner-top\"); " + NewLine;
            code += "}, " + NewLine;
            code += "close: function () { " + NewLine;
            code += "$(this).removeClass(\"ui-corner-top\").addClass(\"ui-corner-all\"); " + NewLine;
            code += "} " + NewLine;
            code += "});" + NewLine;
            code += "} " + NewLine;
            return code;
        }

        /// <summary>
        ///  บังคับให้กรอกได้แต่ตัวเลข
        /// </summary>
        /// <returns></returns>
        public string ForceNumberTableEditJs()
        {
            var code = "";

            code += "//For Validate Type " + NewLine;
            code += "function ForceNumberTextBoxEditMode() { " + NewLine;
            // code += "$(\".ForceNumber\").ForceNumericOnly(); " + _NewLine;

            code += "$(\".ForceNumber\").ForceNumericOnly();  " + NewLine;
            code += "$(\".ForceNumber2Digit\").ForceNumericOnly2Digit(); " + NewLine;
            code += "} " + NewLine;

            return code;
        }

        //public string ForceNumberFilterJs()
        //{
        // string code = "";
        // code += "function ForceNumberTextBox() " + _NewLine;
        // code += "{" + _NewLine;

        // foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
        // {
        //  //ถ้าเป็น DropDown ไม่ต้อง Force ตัวเลข
        //  if (IsDropDown(dataColumn))
        //continue;

        //  //string propertieName = string.Format(_formatpropertieName, _TableName, _DataColumn.ColumnName);
        //  string controlTextBoxName = string.Format(_formatTextBoxName, dataColumn.ColumnName);
        //  //string controlChekBoxName = string.Format(_formatChekBoxName, _DataColumn.ColumnName);
        //  //string controlDropDownName = string.Format(formatDropDownName , _DataColumn.ColumnName);

        //  //if ((dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
        //  //{
        //  //continue;
        //  //}

        //  if ((dataColumn.DataType.ToString() == "System.Guid") || (dataColumn.DataType.ToString() == "System.Int16"))
        //  { continue; }

        //  if (dataColumn.DataType.ToString() == "System.Int32")
        //  {
        //code += "$(\"#<%=" + controlTextBoxName + "\").ForceNumericOnly();" + _NewLine;
        //  }
        //  else if (dataColumn.DataType.ToString() == "System.Decimal")
        //  {
        //code += "$(\"#<%=" + controlTextBoxName + "\").ForceNumericOnly2Digit();" + _NewLine;
        //  }
        // }

        // code += "}" + _NewLine;

        // return code;
        //}

        #endregion JavaScript

        #region Html

        private string SearcFilterHtml()
        {
            var code = "";
            code += " <div class=\"container\"> " + NewLine;
            //code += "<div class=\" col s12\"> " + _NewLine;

            //Usign Gen Text Box
            code += "<div class=\"row\"> " + NewLine;
            var txtBoxSet = GenControls(6, false, false);
            //txtBoxSet = txtBoxSet.Replace("s12", "s3");
            //txtBoxSet = "";
            code += txtBoxSet;
            code += "<div class=\"input-field col s12\"> " + NewLine;
            code += " " + NewLine;
            // code += "<a class=\"waves-effect waves-light btn center\">Search</a> " + _NewLine;
            //code += "<asp:Button ID =\"btnSearch\" CssClass=\"waves-effect waves-light btn center\" OnClick=\"btnSearch_Click\"runat=\"server\" Text=\"Search\" />" + _NewLine;
            //code += "<asp:Button ID=\"btnClear\" CssClass=\"waves-effect waves-light btn center\" OnClientClick=\"javascript:return ClearValue();\" runat=\"server\" Text=\"Clear\" />";
            code +=
             "<input id=\"btnSearch\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"Search\" onclick=\"Search();\" />" +
             NewLine;
            code +=
             "<input id=\"btnClear\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"Clear\" onclick=\"javascript: return ClearValue();\" />" +
             NewLine;
            code +=
             "<input id =\"btnNew\" class=\"waves-effect waves-light btn center\" type=\"button\" value=\"New\" onclick=\"javascript: window.open('" +
             FileName.AspxFromCodeName() + "', '_blank');\" />" + NewLine;
            code += "</div> " + NewLine;
            code += "</div> " + NewLine;
            code += "" + NewLine;
            code += "</div> " + NewLine;
            //code += "</div> " + _NewLine;

            return code;
        }

        protected string GenTableFromJson()
        {
            var code = "  ";
            code += " <div style=\"overflow: auto\">" + NewLine;
            code += "<table id=tbResult class=\"  bordered  striped \"  style=\"display: none;\" > " + NewLine;
            code += "  <thead> " + NewLine;
            code += "  <tr> " + NewLine;
            //code += " <th>Currency</th>  " + _NewLine;
            //code += "  <th>Description</th> " + _NewLine;
            //code += "<th>Bank Note</th>" + _NewLine;
            //code += "  <th>Buying Rates	</th> " + _NewLine;

            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                string sortFunction = "";
                if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                {
                    sortFunction = "";
                }
                else
                {
                    sortFunction = "onclick=\"Sort(this);\"";
                }
                code +=
         $"<th   data-column-id=\"{dataColumn.ColumnName}\"  {sortFunction}>{dataColumn.ColumnName}</th>" +
         NewLine;
                


            }

            code += "</tr>" + NewLine;
            code += "</thead>" + NewLine;
            code += "<tbody>" + NewLine;
            code += "</tbody>" + NewLine;
            code += "</table>" + NewLine;
            code += "</div>" + NewLine;

            return code;
        }

        private string GenTrTemplate()
        {
            var code = "";

            code += "var TrTempplate =\"<tr>\";" + NewLine;
            //style = "text-align: center"
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {

                //if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                //{
                //    //primary key ให้อยู่ตรงกลาง
                //    code += "var TrTempplate =\"<tr style = \"text-align:center\" >" + NewLine;
                //}
                //else
                //{
                //    //ไม่ใช่ primary key อยู่ซ้าย
                //    code += "var TrTempplate =\"<tr>\";" + NewLine;

                //}

            

              



                if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                 (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //check box
                    code += " var status" + dataColumn.ColumnName + " ='' ;" + NewLine;
                    code += "if (result[key]." + dataColumn.ColumnName + " == '1')" + NewLine;
                    code += "{" + NewLine;
                    code += "status" + dataColumn.ColumnName + " = 'checked';" + NewLine;
                    code += "} else" + NewLine;
                    code += "{" + NewLine;
                    code += "status" + dataColumn.ColumnName + " = '';" + NewLine;
                    code += "}" + NewLine;

                    code += "TrTempplate +=\"<td class='borderRight chekBox" + dataColumn.ColumnName + "'>\";" + NewLine;
                    code += "TrTempplate +=\"<p>\";" + NewLine;
                    code += "TrTempplate +=\"<input name='' type='radio' data-column-id='" + dataColumn.ColumnName +
                   "' data-column-key='\"+result[key]." + Ds.Tables[0].PrimaryKey[0] + "+\"' \"+status" +
                   dataColumn.ColumnName + "+\"/><label> </label>\"; " + NewLine;
                    code += "TrTempplate +=\"</p>\"; ";
                    code += "TrTempplate +=\"</td> \";";
                }
                else
                {

                    string stlyeCenter = "";
                    //Set Center ให้ Primary Key
                    if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                    {
                        //primary key ให้อยู่ตรงกลาง
                          stlyeCenter = "style = 'text-align:center'";

                    }
                    else
                    {
                        //ไม่ใช่ primary key อยู่ซ้าย
                        stlyeCenter = "";



                    }



                    code += "TrTempplate +=\"<td class='td" + dataColumn.ColumnName + "'  "+stlyeCenter+"  >\";" + NewLine;
                    //ใช้สำหรับทำ Drop down ตอน Display ก่อน Edit
                    if (IsDropDown(dataColumn))
                    {
                        // code += "TrTempplate +=\"<td class='borderRight chekBox" + dataColumn.ColumnName + "'>\";" + _NewLine;

                        code += "TrTempplate +=\"<span class='truncate'>\"+result[key]." + dataColumn.ColumnName +
                          "+\"</span>\";" + NewLine;
                    }
                    else if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                    {

                   
                        //ImageEdit
                        code += "TrTempplate +=\"<a id='btnShowPopup0' target='_blank' href='" + TableName +
                          "Web.aspx?Q=\"+result[key]." + dataColumn.ColumnName + "+\"'\";" + NewLine;
                        code += "TrTempplate +=\"title=''>\";" + NewLine;

                        //-->เป็นตัวหนังสือ
                        code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" +
                        NewLine;


                        //code += "TrTempplate +=\"<span><img src ='Images/v1/edit.png' /></span>\";" +
                        //NewLine;



                        code += "TrTempplate +=\"</a>\";" + NewLine;
                        //==========================================================================



                        //ID
                        code += "TrTempplate +=\"<a id='btnShowPopup0' target='_blank' href='" + TableName +
                                "Web.aspx?Q=\"+result[key]." + dataColumn.ColumnName + "+\"'\";" + NewLine;
                        code += "TrTempplate +=\"title=''>\";" + NewLine;

                        //-->เป็นตัวหนังสือ
                        code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" +
                        NewLine;


                        //code += "TrTempplate +=\"<span><img src ='Images/v1/edit.png' /></span>\";" +
                        //        NewLine;



                        code += "TrTempplate +=\"</a>\";" + NewLine;















                    }
                    else if (dataColumn.DataType.ToString() == "System.DateTime")
                    {
                        //dateFormat(JsonDateToDate(result[key]." + dataColumn.ColumnName + "), 'd mmm yyyy')
                        code += "TrTempplate +=\"<span>\"+dateFormat(JsonDateToDate(result[key]." +
                          dataColumn.ColumnName + "), 'd mmm yyyy')+\"</span>\";" + NewLine;
                        //code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" + _NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.String")
                    {
                        code += "TrTempplate +=\"<span class=''>\"+result[key]." + dataColumn.ColumnName +
                          "+\"</span>\";" + NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.Byte[]")
                    {
                        code += $"TrTempplate +=\"<img id='imgPreview' src='{TableName}ImageHandler.ashx?Q=\" + result[key].{dataColumn.Table.PrimaryKey[0].ToString()} + \"' height='131' width='174' onerror=this.src='Images/no-image.png' alt='Image preview...'>\"" + NewLine;
                    }

                    // TrTempplate += "<img id='imgPreview' src='ImageHandler.ashx?Q=" + result[key].CategoryID + "' height='131' width='174' onerror=this.src='Images/no-image.png' alt='Image preview...'>";
                    else
                    {
                        code += "TrTempplate +=\"<span>\"+result[key]." + dataColumn.ColumnName + "+\"</span>\";" +
                          NewLine;
                    }

                    //ตัว Edit DataTable ===========================================================================
                    code += "TrTempplate +=\"<div style='display: none'>\";" + NewLine;

                    if (IsDropDown(dataColumn))
                    {
                        //TrTempplate += "<select id='selectCategoryID' class='selectCategoryID selectInputEditMode'></select>";
                        code +=
                         $"TrTempplate += \"<select id='select{dataColumn.ColumnName}' data-column-id='{dataColumn.ColumnName}'  data-column-value='\" + result[key].{dataColumn.ColumnName}+\"' class='select{dataColumn.ColumnName} selectInputEditMode' ></select>\";" +
                         NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.String")
                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName +
                          "' type='text' MaxLength='" + dataColumn.MaxLength + "' length='" + dataColumn.MaxLength +
                          "' class='validate truncate " + dataColumn.ColumnName + "' value='\"+result[key]." +
                          dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.Int32")

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName +
                          "' type='text' class='validate ForceNumber ' value='\"+result[key]." +
                          dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.Decimal")

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName +
                          "' type='text' class='validate ForceNumber2Digit' value='\"+result[key]." +
                          dataColumn.ColumnName + "+\"' style='height: unset; margin: 0px;'>\";" + NewLine;
                    }
                    else if (dataColumn.DataType.ToString() == "System.DateTime")

                    {
                        code += "TrTempplate +=\"<input data-column-id='" + dataColumn.ColumnName +
                          "'class='datepicker' type='date' value='\"+dateFormat(JsonDateToDate(result[key]." +
                          dataColumn.ColumnName + "),'d mmm yyyy')+\"' style='height: unset; margin: 0px;'>\";" +
                          NewLine;
                    }

                    code += "TrTempplate +=\"<label class='lblSave'>Save</label>\";" + NewLine;
                    code += "TrTempplate +=\"<label class='lblCancel'>\";" + NewLine;
                    code += "TrTempplate +=\"Cancel</label>\";" + NewLine;
                    code += "TrTempplate +=\"</div>\";" + NewLine;
                    code += "TrTempplate +=\"</td>\";" + NewLine;
                }
            }
            code += "TrTempplate +=\"</tr>\";" + NewLine;

            return code;
        }

        protected string PaggerHtml()
        {
            var code = "";
            code += "<ul id=\"ulpage\" class=\"pagination\"> " + NewLine;

            code += "</ul>";

            return code;
        }

        protected string ModalProgressHtml()
        {
            var code = "";
            code += " " + NewLine;
            code += "<!-- Modal Structure --> " + NewLine;
            code += "<div id=\"modal1\" class=\"modal\"> " + NewLine;
            code += "<div class=\"modal-content\"> " + NewLine;
            code += "<p>Loading</p> " + NewLine;
            code += "<div class=\"progress\"> " + NewLine;
            code += "<div class=\"indeterminate\"></div> " + NewLine;
            code += "</div> " + NewLine;
            code += "</div> " + NewLine;
            code += "  " + NewLine;
            code += "</div>" + NewLine;
            //  code += "</div>" + _NewLine;
            return code;
        }

        protected string NoResultHtml()
        {
            var code = "";

            code += " <div id=\"DivNoresults\" class=\"container\"  style=\"display: none\" > " + NewLine;
            code += "<p class=\"flow-text\">No results found. Try the following:</p> " + NewLine;
            code += "<p> " + NewLine;
            code += "Make sure all words are spelled correctly. " + NewLine;
            code += "</p> " + NewLine;
            code += "<p> " + NewLine;
            code += "Try different keywords. " + NewLine;
            code += "</p> " + NewLine;
            code += "<p> " + NewLine;
            code += "Try more general keywords. " + NewLine;
            code += "</p> " + NewLine;
            code += "</div>";

            return code;
        }

        #endregion Html

        #region Server Tag

        protected string GenContentHeadBegin()
        {
            var code = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">  " + NewLine;

            return code;
        }

        protected string GenContentHeadEnd()
        {
            var code = "";

            code += " </asp:Content> " + NewLine;
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

        #endregion Server Tag

        #region Utility

        protected string GenClassCheckBoxList()
        {
            var code = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if ((dataColumn.DataType.ToString() == "System.Boolean") ||
                 (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    code += $".chekBox{dataColumn.ColumnName},";
                }
            }

            code = code.TrimEnd(',');
            return code;
        }

        protected string GenClassTextBoxList()
        {
            var code = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                if (IsExceptionColumn(dataColumn, true))
                {
                    continue;
                }
                //if (ExceptionType.Contains(dataColumn.DataType.ToString()))
                //{
                //    continue;
                //}
                if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName)
                {
                    continue;
                }
                //code += " <td>" + _DataColumn.ColumnName.ToUpper() + "</td> " + _NewLine;
                //code += " <td><%# Eval(\"" + _DataColumn.ColumnName + "\") %></td>";
                if ((dataColumn.DataType.ToString() != "System.Boolean") ||
                 (dataColumn.DataType.ToString() != "System.Int16"))
                {
                    code += $".td{dataColumn.ColumnName},";
                    //code += "<td class=\"borderRight chekBox" + _DataColumn.ColumnName + "\"> " + _NewLine;
                    //code += "<p> " + _NewLine;
                    //code += "<input name='' type='radio' data-column-id=\"" + _DataColumn.ColumnName + "\" data-column-key=\"<%# Eval(\"" + _ds.Tables[0].PrimaryKey[0].ToString() + "\") %>\" <%# TagCheck(Eval(\"" + _DataColumn.ColumnName + "\")) %> /><label> </label> " + _NewLine;
                    //code += "</p> " + _NewLine;
                    //code += "</td> " + _NewLine;
                }
                //else
                //{
                //code += " <td class=\"td"+ _DataColumn.ColumnName + "\"> " + _NewLine;
                //code += "<span><%# Eval(\"" + _DataColumn.ColumnName + "\") %> </span> " + _NewLine;
                //code += "<div style=\"display: none\"> " + _NewLine;
                //code += "<input data-column-id=\"" + _DataColumn.ColumnName + "\" type=\"text\" class=\"validate " + _DataColumn.ColumnName + "\" value=\"<%# Eval(\"" + _DataColumn.ColumnName + "\") %>\"> " + _NewLine;
                //code += "<label class=\"lblSave\">Save</label> " + _NewLine;
                //code += "<label class=\"lblCancel\">" + _NewLine;
                //code += "Cancel</label> " + _NewLine;
                //code += "</div> " + _NewLine;
                //code += "  </td>" + _NewLine;
                //}

                // < input name = '' data - column - id = "BualuangExclusive" data - column - key = "<%# Eval("ID1") %>" type = 'radio' <%# ServiceSet(Eval("BualuangExclusive")) %> /><label for='test1'></label>
            }

            code = code.TrimEnd(',');
            return code;
        }

        #endregion Utility
    }
}