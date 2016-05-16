namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumn : AspxTableCode
    {
        public AspxFromCode AspxFromCodeaspx;

        private string GenHeadeFile()
        {
            string code = "<%@ Page Title=\"" + _TableName + "\" Language=\"C#\" MasterPageFile=\"~/MasterPage.master\" AutoEventWireup=\"true\" CodeFile=\"" + _FileName.AspxTableCodeFilterColumnBehineName() + "\" Inherits=\"" + _TableName + "Filter\" %>" + _NewLine;

            return code;
        }

        private string GenSearch()
        {
            string code = "";
            code += " <div class=\"container\"> " + _NewLine;
            //code += "        <div class=\" col s12\"> " + _NewLine;

            //Usign Gen Text Box
            code += "        <div class=\"row\"> " + _NewLine;
            string txtBoxSet = AspxFromCodeaspx.GenControls(6);
            //txtBoxSet = txtBoxSet.Replace("s12", "s3");
            //txtBoxSet = "";
            code += txtBoxSet;
            code += "                <div class=\"input-field col s12\"> " + _NewLine;
            code += " " + _NewLine;
            // code += "                    <a class=\"waves-effect waves-light btn center\">Search</a> " + _NewLine;
            code += "<asp:Button ID =\"btnSearch\" CssClass=\"waves-effect waves-light btn center\" OnClick=\"btnSearch_Click\"   runat=\"server\" Text=\"Search\" />" + _NewLine;

            code += "                </div> " + _NewLine;
            code += "            </div> " + _NewLine;
            code += "        " + _NewLine;
            code += "        </div> " + _NewLine;
            //code += "        </div> " + _NewLine;

            return code;
        }

        //javascript Method
        private string GenDocumentReady()
        {
            string code = " <script> " + _NewLine;
            code += "     var MsgError = 'UPDATE: An unexpected error has occurred. Please contact your system Administrator.'; " + _NewLine;
            code += " " + _NewLine;
            code += "        $(document).ready(function () { " + _NewLine;
            code += "            ForceNumberTextBox(); " + _NewLine;

            code += "//For dropdown" + _NewLine;
            code += "$('select').material_select(); " + _NewLine;

            code += "            //For Change Lange " + _NewLine;
            //code += "            $('.dropdown-button').dropdown({ " + _NewLine;
            //code += "                inDuration: 300, " + _NewLine;
            //code += "                outDuration: 225, " + _NewLine;
            //code += "                constrain_width: true, // Does not change width of dropdown to that of the activator " + _NewLine;
            //code += "                hover: false, // Activate on hover " + _NewLine;
            //code += "                gutter: 0, // Spacing from edge " + _NewLine;
            //code += "                belowOrigin: true, // Displays dropdown below the button " + _NewLine;
            //code += "                alignment: 'left' // Displays dropdown with edge aligned to the left of button " + _NewLine;
            //code += "            }); " + _NewLine;

            code += "            $('.datepicker').pickadate({ " + _NewLine;
            code += "                selectMonths: true, // Creates a dropdown to control month  " + _NewLine;
            code += "                selectYears: 15,// Creates a dropdown of 15 years to control year,  " + _NewLine;
            code += "                format: 'd mmmm yyyy', " + _NewLine;
            code += "            });" + _NewLine;

            code += " " + _NewLine;

            code += " " + _NewLine;

            code += PaginationJavaScript();

            code += " " + _NewLine;

            code += LeanModalJavaScript();

            code += EditTableJavaScript();

            //code += AutocompleteJavaScript();
            code += AutocompleteMutilColumnJavaScript();
            code += "        });//End " + _NewLine;
            code += " " + _NewLine;
            //code += SearchJavaScript();
            //code += _AspxFromCodeaspx.Validate();
            //code += SearchJavaScript();

            code += " " + _NewLine;

            code += ForceNumberTextBoxJavaScript();
            code += "</script>";
            return code;
        }

        public override void Gen()
        {
            InnitProperties();

            //_AspxFromCodeaspx = new AspxFromCode();
            //_AspxFromCodeaspx._ds = _ds;
            //_AspxFromCodeaspx._TableName = _TableName;

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

            _FileCode.WriteFile(_FileName.AspxTableCodeFilterColumnName(), code);
        }
    }
}