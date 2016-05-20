using CoreDb;
using StkGenCode.Code;
using StkGenCode.Code.Column;
using StkGenCode.Code.Template;
using System;
using System.Collections.Generic;
using System.Data;
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

        //Write file by path
        private string _constr;

        private string _path;

        /// <summary>
        ///
        /// </summary>
        /// <param name="_ds"></param>
        /// <param name="_TableName"></param>
        /// <param name="columnDropDown">Clumn:Table;</param>
        private void Gen(DataSet _ds, string _TableName, string columnDropDown = "")
        {
            FileCode F = new FileCode();
            F.Path = _path;
            //F.ClearAllFile();

            List<MappingColumn> _MappingColumn = null;
            if (columnDropDown != "")
                _MappingColumn = MappingColumn.ExtractMappingColumn(columnDropDown);

            AspxFromCode _AspxFromCodeaspx = new AspxFromCode();
            _AspxFromCodeaspx._FileCode = F;
            _AspxFromCodeaspx._ds = _ds;
            _AspxFromCodeaspx._TableName = _TableName;
            _AspxFromCodeaspx._MappingColumn = _MappingColumn;
            _AspxFromCodeaspx.Gen();

            AspxFromCodeBehide _AspxFromCodeBehide = new AspxFromCodeBehide();
            _AspxFromCodeBehide._FileCode = F;
            _AspxFromCodeBehide._ds = _ds;
            _AspxFromCodeBehide._TableName = _TableName;
            _AspxFromCodeBehide._MappingColumn = _MappingColumn;
            _AspxFromCodeBehide.Gen();

            //AspxTableCode _AspxTableCode = new AspxTableCode();
            //_AspxTableCode._FileCode = F;
            //_AspxTableCode._ds = _ds;
            //_AspxTableCode._TableName = _TableName;
            //_AspxTableCode._MappingColumn = _MappingColumn;
            //_AspxTableCode.Gen();

            AspxTableCodeBehine _AspxTableCodeBehine = new AspxTableCodeBehine();
            _AspxTableCodeBehine._FileCode = F;
            _AspxTableCodeBehine._ds = _ds;
            _AspxTableCodeBehine._TableName = _TableName;
            _AspxTableCodeBehine._MappingColumn = _MappingColumn;
            //_AspxTableCodeBehine.Gen();

            AspxTableCodeFilterColumn _AspxTableCodeFilterColumn = new AspxTableCodeFilterColumn();
            _AspxTableCodeFilterColumn._FileCode = F;
            _AspxTableCodeFilterColumn._ds = _ds;
            _AspxTableCodeFilterColumn._TableName = _TableName;
            _AspxTableCodeFilterColumn._MappingColumn = _MappingColumn;
            _AspxTableCodeFilterColumn.AspxFromCodeaspx = _AspxFromCodeaspx;
            _AspxTableCodeFilterColumn.Gen();

            AspxTableCodeFilterColumnCodeBehide _AspxTableCodeFilterColumnCodeBehide = new AspxTableCodeFilterColumnCodeBehide();
            _AspxTableCodeFilterColumnCodeBehide._FileCode = F;
            _AspxTableCodeFilterColumnCodeBehide._ds = _ds;
            _AspxTableCodeFilterColumnCodeBehide._TableName = _TableName;
            _AspxTableCodeFilterColumnCodeBehide._MappingColumn = _MappingColumn;
            //_AspxTableCodeFilterColumnCodeBehide.AspxFromCodeBehide = _AspxFromCodeBehide;
            _AspxTableCodeFilterColumnCodeBehide.Gen();

            PageService _PageService = new PageService();
            _PageService._FileCode = F;
            _PageService._ds = _ds;
            _PageService._TableName = _TableName;
            _PageService.Gen();

            string pathPageServiceCodeBehide = _path + @"App_Code\Services\";
            F.Path = pathPageServiceCodeBehide;
            PageServiceCodeBehide _PageServiceCodeBehide = new PageServiceCodeBehide();
            _PageServiceCodeBehide._FileCode = F;
            _PageServiceCodeBehide._ds = _ds;
            _PageServiceCodeBehide._TableName = _TableName;
            _PageServiceCodeBehide.Gen();

            string pathSQlScript = _path + @"SQL\";
            F.Path = pathSQlScript;
            StoreProCode _StoreProCode = new StoreProCode();
            _StoreProCode._FileCode = F;
            _StoreProCode._ds = _ds;
            _StoreProCode._TableName = _TableName;
            _StoreProCode.Gen();

            //Gen Javascript

            string pathJs_U = _path + @"Js_U\";
            F.Path = pathJs_U;
            JsCode _JsCode = new JsCode();
            _JsCode._FileCode = F;
            _JsCode._ds = _ds;
            _JsCode._TableName = _TableName;
            _JsCode.Gen();

            //========Folder Code

            string pathBuCode = _path + @"App_Code\Business\";
            F.Path = pathBuCode;
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

            //DbCodeFireBird _DbCodeFireBird = new DbCodeFireBird();
            //_DbCodeFireBird._FileCode = F;
            //_DbCodeFireBird._ds = _ds;
            //_DbCodeFireBird._TableName = _TableName;
            //_DbCodeFireBird.Gen();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //string con = @"Server=localhost;User=SYSDBA;Password=masterket;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");
            //Gen("fxrates_family");
            //this.Close();
            //button1_Click(sender, e);
            btnGen_Click(sender, e);
        }

        private DataSet ds = null;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //string con = @"Server=localhost;User=SYSDBA;Password=P@ssw0rd;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");

            string sqlServer = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            string sqlFireBird = "select rdb$relation_name as TABLE_NAME from rdb$relations where rdb$view_blr is null  and(rdb$system_flag is null or rdb$system_flag = 0);";

            var connecStionstring = txtConstring.Text;

            //DataAccessLayer Db = null;
            //DataSet ds = null;
            //if (rsSqlServer.Checked)
            //{
            //    connecStionstring = "Data Source=NODE-PC;Initial Catalog=WEBAPP;User ID=sa;Password=P@ssw0rd";
            //    Db = new DataBaseSql(connecStionstring);
            //    ds = Db.GetDataSet(sqlServer);
            //}
            //else if (rdFireBird.Checked)
            //{
            //    connecStionstring = @"Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //    Db = new DataBaseFireBird(connecStionstring);
            //    ds = Db.GetDataSet(sqlFireBird);
            //}
            //else
            //{
            //    throw new Exception("Fails");
            //}

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                checkedListBox1.Items.Add(item["TABLE_NAME"].ToString(), false);
            }
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            _constr = txtConstring.Text;
            _path = textBox1.Text;

            //SetDb();
            // string _TableName = "";

            //foreach (var item in checkedListBox1.CheckedItems)
            //{
            //    _TableName = item.ToString();
            //    Gen(_TableName);
            //}
            //Gen("fxrates_family");
            //Db.GetData();
            //Gen("STK_USER");
            //txtConstring.Text = @"Data Source=NODE-PC;Initial Catalog=WEBAPP;User ID=sa;Password=P@ssw0rd";
            //DataSet _ds = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "STK_USER");

            //txtConstring.Text = @"Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //ClearFile();
            //DataSet _ds = StkGenCode.Code.Db.GetSchemaFireBird(txtConstring.Text, "STK_USER");
            //Gen(_ds, "STK_USER");
            //DataSet _ds_ds = StkGenCode.Code.Db.GetSchemaFireBird(txtConstring.Text, "STK_TYPE");
            //Gen(_ds_ds, "STK_TYPE");
            //=================================================================================================
            ClearFile();
            txtConstring.Text = @"Data Source=NODE-PC;Initial Catalog=WEBAPP;User ID=sa;Password=P@ssw0rd";

            string ColumnDropDown = "CategoryID:TradeFromCategory;TermId:TradeFromTerm";

            DataSet _dsTradeFromFile = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromFile");
            Gen(_dsTradeFromFile, "TradeFromFile", ColumnDropDown);

            DataSet _dsTradeFromTerm = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromTerm");
            Gen(_dsTradeFromTerm, "TradeFromTerm");

            DataSet _dsTradeFromCategory = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromCategory");
            Gen(_dsTradeFromCategory, "TradeFromCategory");

            //Copy ไฟล์
            System.Diagnostics.Process.Start(@"C:\Users\Node\Desktop\copy.bat");

            this.Close();
        }

        private void ClearFile()
        {
            FileCode F = new FileCode();
            F.Path = _path;
            F.ClearAllFile();
        }

        private void SetDb()
        {
            string sqlServer = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            string sqlFireBird = "select rdb$relation_name as TABLE_NAME from rdb$relations where rdb$view_blr is null  and(rdb$system_flag is null or rdb$system_flag = 0);";

            var connecStionstring = txtConstring.Text;

            //if (rsSqlServer.Checked)
            //{
            //    connecStionstring = "Data Source=NODE-PC;Initial Catalog=WEBAPP;User ID=sa;Password=P@ssw0rd";
            //    Db = new DataBaseSql(connecStionstring);
            //    ds = Db.GetDataSet(sqlServer);
            //}
            //else if (rdFireBird.Checked)
            //{
            //    connecStionstring = @"Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //    Db = new DataBaseFireBird(connecStionstring);
            //    ds = Db.GetDataSet(sqlFireBird);
            //}
            //else
            //{
            //    throw new Exception("Fails");
            //}
        }
    }
}