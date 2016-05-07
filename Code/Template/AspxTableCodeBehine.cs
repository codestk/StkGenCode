using System.Data;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeBehine:CodeBase
    {
        //public FileCode _FileCode;
        //public DataSet _ds;
        //public string _TableName;

        //protected string _fileType = ".aspx.cs";
        //protected string _NewLine = " \r\n";

        //protected string _NotImplement = "throw new Exception(\"Not implement\");";

        protected string GenUsign()
        {
            string _code = "";
            _code += "using System;" + _NewLine;
            _code += "using System.Collections.Generic;" + _NewLine;
            _code += "using System.Data;" + _NewLine;
            _code += "using System.Linq;" + _NewLine;
            _code += "using System.Web;" + _NewLine;
            _code += "using System.Web.UI.WebControls;" + _NewLine;
            _code += "using System.Web.Services;" + _NewLine;
            
            //_code += "using ExchangeService.Code;" + _NewLine;

            return _code;
        }

        protected string GenConstance()
        {
            string code = "  ";
            //code += " public int PageSize = 10; " + _NewLine;
            //code += "public int PageSize = 20;" + _NewLine;
            return code;
        }

        protected string GenPageLoad()
        {
            string code = "  ";
            //code += " protected void Page_Load(object sender, EventArgs e) " + _NewLine;
            //code += "{" + _NewLine;
            //code += "if (!IsPostBack) " + _NewLine;
            //code += "{ " + _NewLine;
            //code += "this.GetPageWise(1); " + _NewLine;
            //code += "} " + _NewLine;
            //code += "} " + _NewLine;
            code += "  protected void Page_Load(object sender, EventArgs e) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            if (!IsPostBack) " + _NewLine;
            code += "            { " + _NewLine;
            //code += "                ViewState[\"CurrentPage\"] = 1; " + _NewLine;
            //code += "                ViewState[\"recordCount\"] = 0; " + _NewLine;
            code += "   CurrentPage = 1; " + _NewLine;
            code += "            RecordCount = 0;" + _NewLine;
            code += "             " + _NewLine;
            code += "                hideTool(); " + _NewLine;
            code += "               " + _NewLine;
            code += "            } " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        private string GenBind()
        {
            string code = "";
            code += "   protected override void  Bind()" + _NewLine;
            code += "   { " + _NewLine;

            code += "int pageIndex = Convert.ToInt32(CurrentPage);" + _NewLine;
            code += " " + _TableName + " _" + _TableName + " = new " + _TableName + "(); " + _NewLine;
            code += "  " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;

           


            code += "    _" + _TableName + "Db._" + _TableName + " = _" + _TableName + "; " + _NewLine;
            code += " " + _NewLine;
            code += "        if (SortExpression != null) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            _" + _TableName + "Db._SortDirection = SortDirection; " + _NewLine;
            code += "            _" + _TableName + "Db._SortExpression = SortExpression; " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += " " + _NewLine;
            code += "        _" + _TableName + "Db._" + _TableName + " = _" + _TableName + "; " + _NewLine;
            //code += "        string wherefilter = _" + _TableName + "Db.GenSql(); " + _NewLine;
            code += "      string filterCommnad = SearchUtility.SqlContain(txtSearch.Text);";
            code += "        var result = _" + _TableName + "Db.GetPageWise(pageIndex, PageSize, filterCommnad); " + _NewLine;
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
            code += "        this.PopulatePager(RecordCount, pageIndex); " + _NewLine;
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

        protected string GenPopulatePager()
        {
            string code = "  ";
            //code += " protected void PopulatePager(int recordCount, int currentPage) " + _NewLine;
            //code += "  {" + _NewLine;
            //code += "  double dblPageCount = (double)((decimal)recordCount / (PageSize)); " + _NewLine;
            //code += "  int pageCount = (int)Math.Ceiling(dblPageCount);";
            //code += "  List<ListItem> pages = new List<ListItem>(); " + _NewLine;
            //code += " if (pageCount > 0) " + _NewLine;
            //code += " { " + _NewLine;
            //code += "  ListItem First = new ListItem(\"<i class='material-icons'>chevron_left</i>\", \"1\", currentPage > 1); " + _NewLine;
            //code += "   pages.Add(First); " + _NewLine;
            //code += " for (int i = 1; i <= pageCount; i++)  " + _NewLine;
            //code += " { " + _NewLine;
            //code += "  pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); " + _NewLine;
            //code += "  } " + _NewLine;
            //code += "    pages.Add(new ListItem(\"<i class='material-icons'>chevron_right</i>\", pageCount.ToString(), currentPage < pageCount));" + _NewLine;
            //code += "   } " + _NewLine;
            //ID =\"rpt" + _TableName + "Pagger\
            //code += " rpt" + _TableName + "Pagger.DataSource = pages; " + _NewLine;
            //code += " rpt" + _TableName + "Pagger.DataBind();  " + _NewLine;
            //code += "  }" + _NewLine;

            code += "  protected void PopulatePager(int recordCount, int currentPage) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            double dblPageCount = (double)((decimal)recordCount / (PageSize)); " + _NewLine;
            code += "            int pageCount = (int)Math.Ceiling(dblPageCount); List<ListItem> pages = new List<ListItem>(); " + _NewLine;
            code += "            if (pageCount > 0) " + _NewLine;
            code += "            { " + _NewLine;
            code += "                //ListItem First = new ListItem(\"<i class='material-icons'>chevron_left</i>\", \"1\", currentPage > 1); " + _NewLine;
            code += "                ListItem First = new ListItem(\"<i class=''><</i>\", \"1\", currentPage > 1); " + _NewLine;
            code += "                pages.Add(First); " + _NewLine;
            code += "                for (int i = 1; i <= pageCount; i++) " + _NewLine;
            code += "                { " + _NewLine;
            code += "                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); " + _NewLine;
            code += "                } " + _NewLine;
            code += "                //pages.Add(new ListItem(\"<i class='material-icons'>chevron_right</i>\", pageCount.ToString(), currentPage < pageCount)); " + _NewLine;
            code += "                pages.Add(new ListItem(\"<i class=''>></i>\", pageCount.ToString(), currentPage < pageCount)); " + _NewLine;
            code += "            } " + _NewLine;
            code += "            rpt" + _TableName + "Pagger.DataSource = pages; " + _NewLine;
            code += "            rpt" + _TableName + "Pagger.DataBind(); " + _NewLine;
            code += "        }" + _NewLine;

            return code;
        }

        private string GenPageChange()
        {
            string code = "  ";
            code += " protected void Page_Changed(object sender, EventArgs e) " + _NewLine;
            code += " { " + _NewLine;
            code += " int pageIndex = int.Parse((sender as LinkButton).CommandArgument);" + _NewLine;
            code += " this.GetPageWise(pageIndex); " + _NewLine;

            code += " ViewState[\"CurrentPage\"] = pageIndex;";
            code += " }" + _NewLine;
            code += "  " + _NewLine;
 

            return code;
        }

        protected string GenPaggerClass()
        {
            string code = "  ";
            code += "public string PaggerClass(object Enabled, object Value) " + _NewLine;
            code += "{ " + _NewLine;
            code += " string output = \"\"; " + _NewLine;
            code += " Boolean isfirst = Value.ToString().ToLower().Contains(\"chevron_left\"); " + _NewLine;
            code += " Boolean isend = Value.ToString().ToLower().Contains(\"chevron_right\"); " + _NewLine;
            code += " if ((isfirst == true) && (Enabled.ToString().ToLower() == \"false\")) " + _NewLine;
            code += " { " + _NewLine;
            code += " return \"disabled\"; " + _NewLine;
            code += " } " + _NewLine;

            code += " if ((isend == true) && (Enabled.ToString().ToLower() == \"false\")) " + _NewLine;
            code += " { " + _NewLine;
            code += " return \"disabled\"; " + _NewLine;
            code += " } " + _NewLine;

            code += "  " + _NewLine;
            code += " string classLidisabled = \"active\"; " + _NewLine;
            code += " string classpageitem = \"waves-effect\";  " + _NewLine;
            code += " if (Enabled.ToString().ToLower() == \"false\")" + _NewLine;
            code += " { " + _NewLine;
            code += " output = classLidisabled; " + _NewLine;
            code += " } " + _NewLine;
            code += " else " + _NewLine;
            code += " { output = classpageitem; } " + _NewLine;
            code += " return output; " + _NewLine;
            code += "  }" + _NewLine;

            return code;
        }

        protected string GenHideResult()
        {
            string code = "  ";
            code += "  protected void hideTool() " + _NewLine;
            code += "        { " + _NewLine;
            code += "             " + _NewLine;
            code += "            divResult.Visible = false; " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        protected string GenShowResult()
        {
            string code = "  ";
            code += "   protected void ShowTool() " + _NewLine;
            code += "        { " + _NewLine;
            code += "            // divfilter.Visible = true; " + _NewLine;
            code += "            divResult.Visible = true; " + _NewLine;
            code += "        } " + _NewLine;

            return code;
        }

        protected string T()
        {
            string code = "  ";
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            code += "  " + _NewLine;
            return code;
        }

        protected string BeginClass()
        {
            string code = "  ";
            code += "public partial class " + _TableName + "List: StkRepeaterExten" + _NewLine;
            code += "{" + _NewLine;
            return code;
        }

        protected string EndClass()
        {
            return "}";
        }

        private string GenSearchEvent()
        {
            string code = "";

            code += " protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)" + _NewLine;
            code += " {" + _NewLine;
            code += "   if (txtSearch.Text.Trim() == \"\")" + _NewLine;
            code += " {" + _NewLine;
            //divResult.Visible = false;
            //divResult.Visible = false;
            code += "  return;" + _NewLine;
            code += " }" + _NewLine;
            code += "  SortExpression = null;// ClearSort " + _NewLine;
            code += "        CurrentPage = 1; " + _NewLine;
            code += "        Bind();" + _NewLine;
            code += "}" + _NewLine;

            return code;
        }

        protected string GedTagCheck()
        {
            string code = "";

            code += "   public string TagCheck(Object val) " + _NewLine;
            code += "    { " + _NewLine;
            code += "        string status = \"\"; " + _NewLine;
            code += "        if (val == null) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            return \"\"; " + _NewLine;
            code += "        } " + _NewLine;
            code += " " + _NewLine;
            code += "        if (Convert.ToBoolean(val) ==true) " + _NewLine;
            code += "        { " + _NewLine;
            code += "            // inputbox = \" <p><input name = '' type = 'radio' id = 'test1'  checked= 'true'/><label for= 'test1'></label></p>\"; " + _NewLine;
            code += "            status = \"checked\"; " + _NewLine;
            code += "        } " + _NewLine;
            code += "        else " + _NewLine;
            code += "        { " + _NewLine;
            code += "            status = \"\"; " + _NewLine;
            code += "        } " + _NewLine;
            code += "        return status; " + _NewLine;
            code += "    }" + _NewLine;

            return code;
        }

        //protected string GenSaveColumn()
        //{
        //    string code = "";

        //    code += " //For Jquery  ---------------------------------------------------------------------------------------------- " + _NewLine;
        //    code += "        [WebMethod] " + _NewLine;
        //    code += "        public static Boolean SaveColumn(string id, string column, string value) " + _NewLine;
        //    code += "        { " + _NewLine;
        //    code += "            " + _TableName + "Db _" + _TableName + "Db = new " + _TableName + "Db(); " + _NewLine;
         
        //    code += " " + _NewLine;
    
        //    code += " " + _NewLine;
        //    code += "            bool isUpdate = _" + _TableName + "Db.UpdateColumn(id, column, value); " + _NewLine;
        //    code += "            return isUpdate; " + _NewLine;
        //    code += "        }" + _NewLine;

        //    return code;
        //}





        public override void Gen()
        {
            string _code = "";

            _code += GenUsign();
            _code += BeginClass();
            _code += GenConstance();

            _code += GenPageLoad();
            _code += GenSearchEvent();
            //_code += GenPageChange();
            _code += GenBind();
            //_code += GenPopulatePager();

            //_code += GenPaggerClass();

            _code += GenHideResult();
            _code += GenShowResult();
           // _code += GedTagCheck();
            //_code += GenSaveColumn();


            _code += EndClass();


            FileName name = new FileName();
            name._TableName = _TableName;
            name._ds = _ds;
            _FileCode.writeFile(name.AspxTableCodeBehineName(), _code);
           // _FileCode.writeFile(_TableName + "List", _code);
        }
    }
}