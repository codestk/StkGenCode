using StkGenCode.Code;
using StkGenCode.Code.Template;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace StkGenCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //List All Table to display
        private void button1_Click(object sender, EventArgs e)
        {

            //string con = @"Server=localhost;User=SYSDBA;Password=P@ssw0rd;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");
        

            string sql;
            sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            SqlConnection _FbConnection = new SqlConnection();
            _FbConnection.ConnectionString = textBox2.Text;
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, _FbConnection);
            adapter.Fill(ds);
            //checkedListBox1.DataSource = ds.Tables[0];

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                checkedListBox1.Items.Add(item["TABLE_NAME"].ToString(), false);
            }
        }

        //Write file by path
        private string _constr;

        private string _path;

        private void button2_Click(object sender, EventArgs e)
        {
            _constr = textBox2.Text;
            _path = textBox1.Text;

            //string _TableName = "mktsum_log";
           // string _TableName = "";

            //foreach (var item in checkedListBox1.CheckedItems)
            //{
            //    _TableName = item.ToString();
            //    Gen(_TableName);
            //}
            //Gen("fxrates_family");
            Gen("STK_USER");
           
            //MessageBox.Show("Ok");

            System.Diagnostics.Process.Start(@"C:\Users\Node\Desktop\copy.bat");



       


            this.Close();
        }

        private void Gen(string _TableName)
        {
            StkGenCode.Code.FileCode F = new FileCode();
            F.path = _path;
            F.ClearAllFile();

            Db _db = new Db();
            var _ds = Db.GetData(_constr, _TableName);

            AspxFromCode _AspxFromCodeaspx = new AspxFromCode();
            _AspxFromCodeaspx._FileCode = F;
            _AspxFromCodeaspx._ds = _ds;
            _AspxFromCodeaspx._TableName = _TableName;
            _AspxFromCodeaspx.Gen();

            AspxFromCodeBehide _AspxCodeBehide = new AspxFromCodeBehide();
            _AspxCodeBehide._FileCode = F;
            _AspxCodeBehide._ds = _ds;
            _AspxCodeBehide._TableName = _TableName;
            _AspxCodeBehide.Gen();

            AspxTableCode _AspxTableCode = new AspxTableCode();
            _AspxTableCode._FileCode = F;
            _AspxTableCode._ds = _ds;
            _AspxTableCode._TableName = _TableName;
            _AspxTableCode.Gen();

            AspxTableCodeBehine _AspxTableCodeBehine = new AspxTableCodeBehine();
            _AspxTableCodeBehine._FileCode = F;
            _AspxTableCodeBehine._ds = _ds;
            _AspxTableCodeBehine._TableName = _TableName;
            _AspxTableCodeBehine.Gen();

            AspxTableCodeFilterColumn _AspxTableCodeFilterColumn = new AspxTableCodeFilterColumn();
            _AspxTableCodeFilterColumn._FileCode = F;
            _AspxTableCodeFilterColumn._ds = _ds;
            _AspxTableCodeFilterColumn._TableName = _TableName;
            _AspxTableCodeFilterColumn.Gen();

             

            AspxTableCodeFilterColumnCodeBehide _AspxTableCodeFilterColumnCodeBehide = new AspxTableCodeFilterColumnCodeBehide();
            _AspxTableCodeFilterColumnCodeBehide._FileCode = F;
            _AspxTableCodeFilterColumnCodeBehide._ds = _ds;
            _AspxTableCodeFilterColumnCodeBehide._TableName = _TableName;
            _AspxTableCodeFilterColumnCodeBehide.Gen();


            PageService _PageService = new PageService();
            _PageService._FileCode = F;
            _PageService._ds = _ds;
            _PageService._TableName = _TableName;
            _PageService.Gen();


            string pathPageServiceCodeBehide = _path + @"App_Code\Services\";
            F.path = pathPageServiceCodeBehide;
            PageServiceCodeBehide _PageServiceCodeBehide = new PageServiceCodeBehide();
            _PageServiceCodeBehide._FileCode = F;
            _PageServiceCodeBehide._ds = _ds;
            _PageServiceCodeBehide._TableName = _TableName;
            _PageServiceCodeBehide.Gen();
         

            string pathSQlScript = _path + @"SQL\";
            F.path = pathSQlScript;
            StoreProCode _StoreProCode = new StoreProCode();
            _StoreProCode._FileCode = F;
            _StoreProCode._ds = _ds;
            _StoreProCode._TableName = _TableName;
            _StoreProCode.Gen();

            //Gen Js
          
            string pathJs_U = _path + @"Js_U\";
            F.path = pathJs_U;
            JsCode _JsCode = new JsCode();
            _JsCode._FileCode = F;
            _JsCode._ds = _ds;
            _JsCode._TableName = _TableName;
            _JsCode.Gen();

             
            //========Foder Cdoe

            string pathBuCode = _path + @"App_Code\Code\Business\";
            F.path = pathBuCode;
            PropertiesCode Pcode = new PropertiesCode();
            Pcode._FileCode = F;
            Pcode._ds = _ds;
            Pcode._TableName = _TableName;
            Pcode.Gen();
           
            DbCode _DbCode = new DbCode();
            _DbCode._FileCode = F;
            _DbCode._ds = _ds;
            _DbCode._TableName = _TableName;
            _DbCode.Gen();
        }

        //Delete All Find in folder
        private void ClearAllFile(string patch)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(patch);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            // File.Delete(path);
        }

        #region apsxCode

        //Gen Aspx File

        #endregion apsxCode

        private void Form1_Load(object sender, EventArgs e)
        {
            //string con = @"Server=localhost;User=SYSDBA;Password=masterket;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");
            //Gen("fxrates_family");
            //this.Close();
            //button1_Click(sender, e);
            button2_Click (sender, e);
        }
    }
}