using System;
using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeFilterColumnCodeBehide : AspxTableCodeBehine
    {
        private string BeginClass()
        {
            string code = "  ";
            code += "public partial class " + _TableName + "Filter: StkRepeaterExten" + _NewLine;
            code += "{" + _NewLine;
            return code;
        }

        private String MapControlToProPerties(DataSet _ds, bool CommentKey = false)
        {
            string code = "";
            //code += "bool sortAscending = SortDirection == SortDirection.Ascending;" + _NewLine;
            //code += string.Format("var _{0} = new {0}();", _TableName) + _NewLine;
            foreach (DataColumn _DataColumn in _ds.Tables[0].Columns)
            {
                //if (CommentKey)
                //{
                //    bool primary = false;

                //    //ต้อง เป็น Auto ถึงจะ Comment
                //    if ((_DataColumn.ColumnName == _ds.Tables[0].PrimaryKey[0].ToString()) && (_ds.Tables[0].PrimaryKey[0].AutoIncrement))
                //    {
                //        primary = true;
                //    }

                //    if (primary)
                //    {
                //        // continue;
                //        code += "// ";
                //    }
                //}

                //if ((_DataColumn.DataType.ToString() == "System.Guid"))
                //{ continue; }

                //if ((_DataColumn.DataType.ToString() == "System.Int32"))
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt32(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                //}
                //else if (_DataColumn.DataType.ToString() == "System.Int16")
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = Convert.ToInt16(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                //}
                //else if (_DataColumn.DataType.ToString() == "System.Decimal")
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =  Convert.ToDecimal (txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                //}
                //else if (_DataColumn.DataType.ToString() == "System.DateTime")
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToDateTime(txt" + _DataColumn.ColumnName + ".Text);" + _NewLine;
                //}
                //else if (_DataColumn.DataType.ToString() == "System.Boolean")
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " =Convert.ToBoolean(txt" + _DataColumn.ColumnName + ".Checked);" + _NewLine;
                //}
                //else
                //{
                //    code += "_" + _TableName + "." + _DataColumn.ColumnName + " = txt" + _DataColumn.ColumnName + ".Text;" + _NewLine;
                //}

                if ((_DataColumn.DataType.ToString() == "System.Int32"))
                {
                    code += string.Format("if (txt{0}.Text != \"\") ", _DataColumn.ColumnName) + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = Convert.ToInt32(txt{1}.Text);", _TableName, _DataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.Decimal")
                {
                    code += string.Format("  if (txt{0}.Text != \"\")", _DataColumn.ColumnName) + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = Convert.ToDecimal(txt{1}.Text);", _TableName, _DataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
                else if ((_DataColumn.DataType.ToString() == "System.Boolean") || (_DataColumn.DataType.ToString() == "System.Int16"))
                {
                    //code += string.Format("  if (txt{0} != \"\")", _DataColumn.ColumnName) + _NewLine;
                    //code += " {" + _NewLine;
                    //code += string.Format("_{0}.{1} = Convert.ToDecimal(txt{1}.Text;)", _TableName, _DataColumn.ColumnName) + _NewLine;
                }
                else if (_DataColumn.DataType.ToString() == "System.DateTime")
                {
                    code += string.Format("  if (txt{0}.Text != \"\")", _DataColumn.ColumnName) + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = Convert.ToDateTime(txt{1}.Text);", _TableName, _DataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
                else
                {
                    code += string.Format("  if (txt{0}.Text != \"\")", _DataColumn.ColumnName) + _NewLine;
                    code += " {" + _NewLine;
                    code += string.Format("_{0}.{1} = txt{1}.Text;", _TableName, _DataColumn.ColumnName) + _NewLine;
                    code += "}" + _NewLine;
                }
            }

            return code;
        }

        protected new string GenGetPageWise()
        {
            string code = "  ";

            code += "     protected void GetPageWise(int pageIndex,string wherefilter)  " + _NewLine;
            code += "{ " + _NewLine;
            code += "\\ " + _TableName + "Db _" + _TableName + " = new " + _TableName + "Db();" + _NewLine;

            code += "\\ " + DbCodeFireBird.ClassName(_TableName) + "D _" + _TableName + " = new " + DbCodeFireBird.ClassName(_TableName) + "();" + _NewLine;
            //code += " string wherefilter = SearchUtility.SqlContain(txtSearch.Text);" + _NewLine;
            code += "var result = _" + _TableName + ".GetPageWise(pageIndex, PageSize, wherefilter); " + _NewLine;
            //code += "string services = GetSelectedService(); " + _NewLine;
            //code += "var result = _" + _TableName + ".GetPageWise(pageIndex, PageSize, txtSearch.Text); " + _NewLine;
            code += "           // var result = _" + _TableName + ".GetWithFilter(false, \"\"); " + _NewLine;

            code += "if (result.Count == 0) " + _NewLine;
            code += "{ " + _NewLine;
            code += "//No data " + _NewLine;
            code += "hideTool(); " + _NewLine;
            //rptauth_dtData
            code += "rpt" + _TableName + "Data.DataBind(); " + _NewLine;
            code += "DivNoresults.Visible = true; " + _NewLine;
            code += "   return; " + _NewLine;
            code += "} " + _NewLine;
            code += "//Hide MEssage " + _NewLine;
            code += "DivNoresults.Visible = false; " + _NewLine;
            //code += "ShowTool(); " + _NewLine;
            code += "int recordCount = result[0].RecordCount; " + _NewLine;
            code += "ViewState[\"recordCount\"] = recordCount; " + _NewLine;

            //code += "divfilter.Visible = true; " + _NewLine;
            code += "divResult.Visible = true; " + _NewLine;
            code += "this.PopulatePager(recordCount, pageIndex); " + _NewLine;
            code += "rpt" + _TableName + "Data.DataSource = result; " + _NewLine;
            code += "rpt" + _TableName + "Data.DataBind(); " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        private string GenPageChange()
        {
            string code = "  ";
            code += " protected void Page_Changed(object sender, EventArgs e) " + _NewLine;
            code += " { " + _NewLine;
            code += " int pageIndex = int.Parse((sender as LinkButton).CommandArgument);" + _NewLine;

            code += " ViewState[\"CurrentPage\"] = pageIndex;" + _NewLine;
            code += " Bind();" + _NewLine;

            code += " }" + _NewLine;
            code += "  " + _NewLine;

            return code;
        }

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
            code += MapControlToProPerties(_ds, false);

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

        public override void Gen()
        {
            string _code = "";

            _code += GenUsign();
            _code += BeginClass();
            _code += GenConstance();

            _code += GenPageLoad();
            _code += GenSearchEvent();
            _code += GenBind();
            // _code += GenGetPageWise();
            //_code += GenPageChange();
            //_code += GenGetPageWise();
            // _code += GenPopulatePager();

            // _code += GenPaggerClass();

            _code += GenHideResult();
            _code += GenShowResult();
            // _code += GedTagCheck();
            //_code += GenSaveColumn();

            _code += EndClass();

            FileName name = new FileName();
            name._TableName = _TableName;
            name._ds = _ds;
            _FileCode.writeFile(name.AspxTableCodeFilterColumnBehineName(), _code);
            // _FileCode.writeFile(_TableName + "List", _code);
        }
    }
}