using System;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumnCodeBehide : AspxTableCodeBehine
    {
        public AspxFromCodeBehide AspxFromCodeBehide;

        private string BeginClass()
        {
            string code = "  ";
            code += "public partial class " + _TableName + "Filter: StkRepeaterExten" + _NewLine;
            code += "{" + _NewLine;
            return code;
        }

        private new String MapControlToProPerties(DataSet _ds, bool commentKey = false)
        {
            string code = "";
            //code += "bool sortAscending = SortDirection == SortDirection.Ascending;" + _NewLine;
            //code += string.Format("var _{0} = new {0}();", _TableName) + _NewLine;
            foreach (DataColumn dataColumn in _ds.Tables[0].Columns)
            {
                if (_MappingColumn != null)
                {
                    string codedrp = AspxFromCodeBehide.GenMapDropDownToProPerties(dataColumn.ColumnName);
                    code += codedrp;
                    if (codedrp != "")
                    {
                        continue;
                    }
                }

                if (dataColumn.DataType.ToString() == "System.Int32")
                {
                    code += $"if (txt{dataColumn.ColumnName}.Text != \"\") " + _NewLine;
                    code += " {" + _NewLine;
                    code +=
                        $"_{_TableName}.{dataColumn.ColumnName} = Convert.ToInt32(txt{dataColumn.ColumnName}.Text);" + _NewLine;
                    code += "}" + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += $"  if (txt{dataColumn.ColumnName}.Text != \"\")" + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = Convert.ToDecimal(txt{1}.Text);", _TableName, dataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
                else if ((dataColumn.DataType.ToString() == "System.Boolean") || (dataColumn.DataType.ToString() == "System.Int16"))
                {
                    //code += string.Format("  if (txt{0} != \"\")", _DataColumn.ColumnName) + _NewLine;
                    //code += " {" + _NewLine;
                    //code += string.Format("_{0}.{1} = Convert.ToDecimal(txt{1}.Text;)", _TableName, _DataColumn.ColumnName) + _NewLine;
                }
                else if (dataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += $"  if (txt{dataColumn.ColumnName}.Text != \"\")" + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = Convert.ToDateTime(txt{1}.Text);", _TableName, dataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
                else
                {
                    code += $"  if (txt{dataColumn.ColumnName}.Text != \"\")" + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = txt{1}.Text;", _TableName, dataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
            }

            return code;
        }

        //protected new string GenGetPageWise()
        //{
        //    string code = "  ";

        //    code += "     protected void GetPageWise(int pageIndex,string wherefilter)  " + _NewLine;
        //    code += "{ " + _NewLine;
        //    code += "\\ " + _TableName + "Db _" + _TableName + " = new " + _TableName + "Db();" + _NewLine;

        //    code += "\\ " + DbCodeFireBird.ClassName(_TableName) + "D _" + _TableName + " = new " + DbCodeFireBird.ClassName(_TableName) + "();" + _NewLine;
        //    //code += " string wherefilter = SearchUtility.SqlContain(txtSearch.Text);" + _NewLine;
        //    code += "var result = _" + _TableName + ".GetPageWise(pageIndex, PageSize, wherefilter); " + _NewLine;
        //    //code += "string services = GetSelectedService(); " + _NewLine;
        //    //code += "var result = _" + _TableName + ".GetPageWise(pageIndex, PageSize, txtSearch.Text); " + _NewLine;
        //    code += "           // var result = _" + _TableName + ".GetWithFilter(false, \"\"); " + _NewLine;

        //    code += "if (result.Count == 0) " + _NewLine;
        //    code += "{ " + _NewLine;
        //    code += "//No data " + _NewLine;
        //    code += "hideTool(); " + _NewLine;
        //    //rptauth_dtData
        //    code += "rpt" + _TableName + "Data.DataBind(); " + _NewLine;
        //    code += "DivNoresults.Visible = true; " + _NewLine;
        //    code += "   return; " + _NewLine;
        //    code += "} " + _NewLine;
        //    code += "//Hide MEssage " + _NewLine;
        //    code += "DivNoresults.Visible = false; " + _NewLine;
        //    //code += "ShowTool(); " + _NewLine;
        //    code += "int recordCount = result[0].RecordCount; " + _NewLine;
        //    code += "ViewState[\"recordCount\"] = recordCount; " + _NewLine;

        //    //code += "divfilter.Visible = true; " + _NewLine;
        //    code += "divResult.Visible = true; " + _NewLine;
        //    code += "this.PopulatePager(recordCount, pageIndex); " + _NewLine;
        //    code += "rpt" + _TableName + "Data.DataSource = result; " + _NewLine;
        //    code += "rpt" + _TableName + "Data.DataBind(); " + _NewLine;
        //    code += "        } " + _NewLine;

        //    return code;
        //}

        //private string GenPageChange()
        //{
        //    string code = "  ";
        //    code += " protected void Page_Changed(object sender, EventArgs e) " + _NewLine;
        //    code += " { " + _NewLine;
        //    code += " int pageIndex = int.Parse((sender as LinkButton).CommandArgument);" + _NewLine;

        //    code += " ViewState[\"CurrentPage\"] = pageIndex;" + _NewLine;
        //    code += " Bind();" + _NewLine;

        //    code += " }" + _NewLine;
        //    code += "  " + _NewLine;

        //    return code;
        //}

        private string GenSearchEvent()
        {
            string code = "";
            code += " protected void btnSearch_Click(object sender, EventArgs e) " + _NewLine;
            code += "   { " + _NewLine;
            code += "   _SortExpression = null;// ClearSort " + _NewLine;
            code += "   CurrentPage = 1; " + _NewLine;
            code += "   Bind();" + _NewLine;
            code += "   }" + _NewLine;
            return code;
        }

        private string GenBind()
        {
            string code = "";
            code += "   protected override void Bind()" + _NewLine;
            code += "   { " + _NewLine;

            code += "int pageIndex = Convert.ToInt32(CurrentPage);" + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  //" + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
            code += " " + DbCodeFireBird.ClassName(_TableName) + " _" + _TableName + "Db = new " + DbCodeFireBird.ClassName(_TableName) + "(); " + _NewLine;
            code += MapControlToProPerties(_ds);

            code += "    _" + _TableName + "Db._" + _TableName + " = _" + _TableName + "; " + _NewLine;
            code += " " + _NewLine;
            code += "        if (_SortExpression != null) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            _" + _TableName + "Db._SortDirection = _SortDirection; " + _NewLine;
            code += "            _" + _TableName + "Db._SortExpression = _SortExpression; " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "        _" + _TableName + "Db._" + _TableName + " = _" + _TableName + "; " + _NewLine;
            code += "//         string wherefilter = _STK_USERDb.GenWhereformProperties();" + _NewLine;
            code += "        var result = _" + _TableName + "Db.GetPageWise(pageIndex, PageSize); " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "  " + _NewLine;
            code += "        if (result.Count == 0) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            //No data " + _NewLine;
            code += "            hideTool(); " + _NewLine;
            code += "            rpt" + _TableName + "Data.DataBind(); " + _NewLine;
            code += "            DivNoresults.Visible = true; " + _NewLine;
            code += "            return; " + _NewLine;
            code += "        } " + _NewLine;
            code += "        //Hide MEssage " + _NewLine;
            code += "        DivNoresults.Visible = false; " + _NewLine;
            code += "        int rowCount = result[0].RecordCount; " + _NewLine;
            code += "        RecordCount = rowCount; " + _NewLine;
            code += "        divResult.Visible = true; " + _NewLine;
            //code += "        this.PopulatePager(RecordCount, pageIndex); " + _NewLine;

            code += " " + _NewLine;
            code += "             rpt" + _TableName + "Pagger.DataSource = this.PopulatePager(RecordCount, pageIndex);  " + _NewLine;
            code += "             rpt" + _TableName + "Pagger.DataBind();" + _NewLine;
            code += "        rpt" + _TableName + "Data.DataSource = result; " + _NewLine;
            code += "        rpt" + _TableName + "Data.DataBind(); " + _NewLine;

            //code += "_" + _TableName + "Db._" + _TableName + " = _" + _TableName + "; " + _NewLine;
            //code += " " + _NewLine;
            //code += "        string wherefilter = _" + _TableName + "Db.GetWhereformProperties();" + _NewLine;
            //code += " int pageInt = Convert.ToInt32(ViewState[\"CurrentPage\"]);" + _NewLine;
            //code += " GetPageWise(pageInt, wherefilter);" + _NewLine;
            code += "    }" + _NewLine;
            return code;
        }

        private new string GenPageLoad()
        {
            string code = "  ";
            code += "protected void Page_Load(object sender, EventArgs e)" + _NewLine;
            code += "{ " + _NewLine;
            code += "if (!Page.IsPostBack) " + _NewLine;
            code += "{  " + _NewLine;

            if (AspxFromCodeBehide.HaveDropDown())
            {
                code += "BindDropDown();" + _NewLine;
            }

            code += "} " + _NewLine;
            code += "}  " + _NewLine;

            return code;
        }

        public override void Gen()
        {
            string code = "";

            code += GenUsign();
            code += BeginClass();
            code += GenConstance();

            code += GenPageLoad();
            if (AspxFromCodeBehide.HaveDropDown())
            {
                code += GenInnitDropDown();
            }
            code += GenSearchEvent();
            code += GenBind();
            // _code += GenGetPageWise();
            //_code += GenPageChange();
            //_code += GenGetPageWise();
            // _code += GenPopulatePager();

            // _code += GenPaggerClass();

            code += GenHideResult();
            code += GenShowResult();
            // _code += GedTagCheck();
            //_code += GenSaveColumn();

            code += EndClass();

            //FileName name = new FileName();
            //name._TableName = _TableName;
            //name._ds = _ds;
            InnitProperties();

            _FileCode.writeFile(_FileName.AspxTableCodeFilterColumnBehineName(), code);
            // _FileCode.writeFile(_TableName + "List", _code);
        }
    }
}