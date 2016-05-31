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
            FileCode F = new FileCode { Path = _path };

            List<MappingColumn> _MappingColumn = null;
            if (columnDropDown != "")
                _MappingColumn = MappingColumn.ExtractMappingColumn(columnDropDown);

            AspxFromCode _AspxFromCodeaspx = new AspxFromCode
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName,
                _MappingColumn = _MappingColumn
            };
            _AspxFromCodeaspx.Gen();

            AspxFromCodeBehide aspxFromCodeBehide = new AspxFromCodeBehide
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName,
                _MappingColumn = _MappingColumn
            };
            aspxFromCodeBehide.Gen();

            //AspxTableCode _AspxTableCode = new AspxTableCode();
            //_AspxTableCode._FileCode = F;
            //_AspxTableCode._ds = _ds;
            //_AspxTableCode._TableName = _TableName;
            //_AspxTableCode._MappingColumn = _MappingColumn;
            //_AspxTableCode.Gen();

            //AspxTableCodeBehine aspxTableCodeBehine = new AspxTableCodeBehine
            //{
            //    _FileCode = F,
            //    _ds = _ds,
            //    _TableName = _TableName,
            //    _MappingColumn = _MappingColumn
            //};

            AspxTableCodeFilterColumn aspxTableCodeFilterColumn = new AspxTableCodeFilterColumn
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName,
                _MappingColumn = _MappingColumn,
                AspxFromCodeaspx = _AspxFromCodeaspx
            };
            aspxTableCodeFilterColumn.Gen();

            AspxTableCodeFilterColumnCodeBehide aspxTableCodeFilterColumnCodeBehide =
                new AspxTableCodeFilterColumnCodeBehide
                {
                    _FileCode = F,
                    _ds = _ds,
                    _TableName = _TableName,
                    _MappingColumn = _MappingColumn
                };
            //_AspxTableCodeFilterColumnCodeBehide.AspxFromCodeBehide = _AspxFromCodeBehide;
            aspxTableCodeFilterColumnCodeBehide.Gen();

            PageService pageService = new PageService
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            pageService.Gen();

            string pathPageServiceCodeBehide = _path + @"App_Code\Services\";
            F.Path = pathPageServiceCodeBehide;
            PageServiceCodeBehide pageServiceCodeBehide = new PageServiceCodeBehide
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            pageServiceCodeBehide.Gen();

            string pathSQlScript = _path + @"SQL\";
            F.Path = pathSQlScript;
            StoreProCode storeProCode = new StoreProCode
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            storeProCode.Gen();

            //Gen Javascript

            string pathJs_U = _path + @"Js_U\";
            F.Path = pathJs_U;
            JsCode jsCode = new JsCode
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            jsCode.Gen();

            //========Folder Code

            string pathBuCode = _path + @"App_Code\Business\";
            F.Path = pathBuCode;
            PropertiesCode Pcode = new PropertiesCode
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            Pcode.Gen();

            //DbCode _DbCode = new DbCode();
            //_DbCode._FileCode = F;
            //_DbCode._ds = _ds;
            //_DbCode._TableName = _TableName;
            //_DbCode.Gen();

            DbCodeFireBird _DbCodeFireBird = new DbCodeFireBird
            {
                _FileCode = F,
                _ds = _ds,
                _TableName = _TableName
            };
            _DbCodeFireBird.Gen();
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

            ClearFile();

            txtConstring.Text = @"Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            ClearFile();
            string ColumnDropDown = "EM_FLAG:STK_USER_FLAG;";
            DataSet _ds_STK_USER = StkGenCode.Code.Db.GetSchemaFireBird(txtConstring.Text, "STK_USER");
            Gen(_ds_STK_USER, "STK_USER", ColumnDropDown);
            //DataSet _ds_STK_TYPE = StkGenCode.Code.Db.GetSchemaFireBird(txtConstring.Text, "STK_TYPE");
            //Gen(_ds_STK_TYPE, "STK_TYPE", ColumnDropDown);

            DataSet _ds_STK_USER_FLAG = StkGenCode.Code.Db.GetSchemaFireBird(txtConstring.Text, "STK_USER_FLAG");
            Gen(_ds_STK_USER_FLAG, "STK_USER_FLAG", ColumnDropDown);
            //=================================================================================================

            //ClearFile();
            //txtConstring.Text = @"Data Source=NODE-PC;Initial Catalog=WEBAPP;User ID=sa;Password=P@ssw0rd";

            //string ColumnDropDown = "CategoryID:TradeFromCategory;TermId:TradeFromTerm";

            //DataSet _dsTradeFromFile = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromFile");
            //Gen(_dsTradeFromFile, "TradeFromFile", ColumnDropDown);

            //DataSet _dsTradeFromTerm = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromTerm");
            //Gen(_dsTradeFromTerm, "TradeFromTerm");

            //DataSet _dsTradeFromCategory = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TradeFromCategory");
            //Gen(_dsTradeFromCategory, "TradeFromCategory");

            //Copy ไฟล์=================================================================================================================
            System.Diagnostics.Process.Start(@"C:\Users\Node\Desktop\copy.bat");

            this.Close();
        }

        private void ClearFile()
        {
            FileCode f = new FileCode { Path = _path };
            f.ClearAllFile();
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