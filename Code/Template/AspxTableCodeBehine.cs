using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
    public class AspxTableCodeBehine
    {
        public FileCode _FileCode;
        public DataSet _ds;
        public string _TableName;

        private string _fileType = ".aspx.cs";
        private string _NewLine = " \r\n";

        private string _NotImplement = "throw new Exception(\"Not implement\");";

        private string GenConstance()
        {
            string code = "  ";
            code += " private int PageSize = 10; " + _NewLine;

            return code;
        }
 
        private string GenPageLoad()
        {
            string code = "  ";
            code += " protected void Page_Load(object sender, EventArgs e) " + _NewLine;
            code += "{" + _NewLine;
            code += " if (!IsPostBack) " + _NewLine;
            code += "  { " + _NewLine;
            code += "  this.GetPageWise(1); " + _NewLine;
            code += "  } " + _NewLine;
            code += " } " + _NewLine;

            return code;
        }

      
        private string GenGetPageWise()
        {
            string code = "  ";
            code += " private void GetPageWise(int pageIndex) " + _NewLine;
            code += "{" + _NewLine;
            code += " " + _TableName + "Db _" + _TableName + " = new " + _TableName + "Db();" + _NewLine;
            code += "  var result = _" + _TableName + ".GetPageWise(pageIndex, PageSize);  " + _NewLine;
            code += "  int recordCount = result[0].RecordCount; " + _NewLine;
            code += " this.PopulatePager(recordCount, pageIndex);  " + _NewLine;
            code += "  rpt" + _TableName + "Data.DataSource = result; " + _NewLine;
            code += "  rpt" + _TableName + "Data.DataBind(); " + _NewLine;
            code += " } " + _NewLine;

            return code;
        }

      
        private string GenPopulatePager()
        {
            string code = "  ";
            code += " private void PopulatePager(int recordCount, int currentPage) " + _NewLine;
            code += "  {" + _NewLine;
            code += "  double dblPageCount = (double)((decimal)recordCount / (PageSize)); " + _NewLine;
            code += "  int pageCount = (int)Math.Ceiling(dblPageCount);";
            code += "  List<ListItem> pages = new List<ListItem>(); " + _NewLine;
            code += " if (pageCount > 0) " + _NewLine;
            code += " { " + _NewLine;
            code += "  ListItem First = new ListItem(\"<i class='material-icons'>chevron_left</i>\", \"1\", currentPage > 1); " + _NewLine;
            code += "   pages.Add(First); " + _NewLine;
            code += " for (int i = 1; i <= pageCount; i++)  " + _NewLine;
            code += " { " + _NewLine;
            code += "  pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage)); " + _NewLine;
            code += "  } " + _NewLine;
            code += "    pages.Add(new ListItem(\"<i class='material-icons'>chevron_right</i>\", pageCount.ToString(), currentPage < pageCount));" + _NewLine;
            code += "   } " + _NewLine;

            code += " rpt" + _TableName + "Pager.DataSource = pages; " + _NewLine;
            code += " rpt" + _TableName + "Pager.DataBind();  " + _NewLine;
            code += "  }" + _NewLine;
            return code;
        }

      
        private string GenPageChange()
        {
            string code = "  ";
            code += " protected void Page_Changed(object sender, EventArgs e) " + _NewLine;
            code += " { " + _NewLine;
            code += "   int pageIndex = int.Parse((sender as LinkButton).CommandArgument);" + _NewLine;
            code += "  this.GetPageWise(pageIndex); " + _NewLine;
            code += " } " + _NewLine;
            code += "  " + _NewLine;

            return code;
        }

    

        private string GenPaggerClass()
        {
            string code = "  ";
            code += " public string PaggerClass(object Enabled, object Value) " + _NewLine;
            code += "{  " + _NewLine;
            code += "  string output = \"\"; " + _NewLine;
            code += "       Boolean isfirst = Value.ToString().ToLower().Contains(\"chevron_left\"); " + _NewLine;
            code += "   Boolean isend = Value.ToString().ToLower().Contains(\"chevron_right\"); " + _NewLine;
            code += "  if ((isfirst == true) && (Enabled.ToString().ToLower() == \"false\")) " + _NewLine;
            code += "   { " + _NewLine;
            code += " return \"disabled\"; " + _NewLine;
            code += "   } " + _NewLine;

            code += "  if ((isend == true) && (Enabled.ToString().ToLower() == \"false\")) " + _NewLine;
            code += "   { " + _NewLine;
            code += " return \"disabled\"; " + _NewLine;
            code += "   } " + _NewLine;

            code += "  " + _NewLine;
            code += "   string classLidisabled = \"active\"; " + _NewLine;
            code += "    string classpageitem = \"waves-effect\";  " + _NewLine;
            code += "   if (Enabled.ToString().ToLower() == \"false\")" + _NewLine;
            code += " { " + _NewLine;
            code += "   output = classLidisabled; " + _NewLine;
            code += " } " + _NewLine;
            code += "   else " + _NewLine;
            code += "   { output = classpageitem; } " + _NewLine;
            code += " return output; " + _NewLine;
            code += "  }" + _NewLine;
            return code;
        }

        private string T()
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

        public void Gen()
        {
            string _code = "";
            _code += GenConstance();

            _code += GenPageLoad();
            _code += GenPageChange();
            _code += GenGetPageWise();
            _code += GenPopulatePager();

            _code += GenPaggerClass();
            _FileCode.writeFile(_TableName + "AspxTableCodeBehine", _code, _fileType);
        }
    }
}

 