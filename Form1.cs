using StkGenCode.Code;
using StkGenCode.Code.Column;
using StkGenCode.Code.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace StkGenCode
{
    public partial class Form1 : Form
    {
        private readonly DataSet _ds = null;
        //List All Table to display

        //Write file by path
        private string _constr;

        private string _path;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="_ds"></param>
        /// <param name="tableName"></param>
        /// <param name="columnDropDown">Clumn:Table;</param>
        private void Gen(DataSet _ds, string tableName, string columnDropDown = "")
        {
            var f = new FileCode { Path = _path };

            List<MappingColumn> dropColumnsColumn = null;
            if (columnDropDown != "")
                dropColumnsColumn = MappingColumn.ExtractMappingColumn(columnDropDown);

            var aspxFromCodeaspx = new AspxFromCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn
            };
            aspxFromCodeaspx.Gen();



            var aspxFromCodeBehide = new AspxFromCodeBehide
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn
            };
            aspxFromCodeBehide.Gen();

            #region Picture Module

            if (aspxFromCodeaspx.HavePicture())
            {
                var ApiControllerPath = _path + @"AppCode\Services\Api\";
                f.Path = ApiControllerPath;
                var ApiController = new ImageApiController
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                };
                ApiController.Gen();

                var HandlerPath = _path;
                f.Path = HandlerPath;
                var Handler = new ImageHandler
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                };
                Handler.Gen();

                var DbCodeImagePath = _path + @"AppCode\Business\"; ;
                f.Path = DbCodeImagePath;
                var _DbCodeImage = new DbCodeImage
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                };
                _DbCodeImage.Gen();
            }

            #endregion Picture Module

            f.Path = _path;
            var aspxTableCodeFilterColumn = new AspxTableCodeFilterColumn
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn
                //AspxFromCodeaspx = _AspxFromCodeaspx
            };
            aspxTableCodeFilterColumn.Gen();

            var aspxTableCodeFilterColumnCodeBehide =
                new AspxTableCodeFilterColumnCodeBehide
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName,
                    DropColumns = dropColumnsColumn
                };
            //_AspxTableCodeFilterColumnCodeBehide.AspxFromCodeBehide = _AspxFromCodeBehide;
            aspxTableCodeFilterColumnCodeBehide.Gen();

            //Asmx Service
            string pathPageServiceAsmx = _path + @"\Services\";
            f.Path = pathPageServiceAsmx;
            var pageService = new PageService
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            pageService.Gen();

            //var pathPageServiceCodeBehide = _path + @"App_Code\Services\";

            var pathPageServiceCodeBehide = _path + @"\Services\";
            f.Path = pathPageServiceCodeBehide;
            //f.Path = pathPageServiceCodeBehide;
            var pageServiceCodeBehide = new PageServiceCodeBehide
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            pageServiceCodeBehide.Gen();

            var pathSQlScript = _path + @"SQL\";
            f.Path = pathSQlScript;
            var storeProCode = new StoreProCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            storeProCode.Gen();

            //Gen Javascript

            var pathJsU = _path + @"Js_U\";
            f.Path = pathJsU;
            var jsCode = new JsCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            jsCode.Gen();

            //========Folder Code

            var pathBuCode = _path + @"AppCode\Business\";
            f.Path = pathBuCode;
            var pcode = new PropertiesCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            pcode.Gen();

           
            var pathPropertiesCValidate = _path + @"AppCode\Business\";
            f.Path = pathPropertiesCValidate;
            var propertiesCodeValidatetor = new PropertiesCodeValidatetor() 
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            propertiesCodeValidatetor.Gen();


            //DbCode _DbCode = new DbCode();
            //_DbCode._FileCode = F;
            //_DbCode._ds = _ds;
            //_DbCode._TableName = _TableName;
            //_DbCode.Gen();

            //var dbCodeFireBird = new DbCodeFireBird
            //{
            //    FileCode = f,
            //    Ds = _ds,
            //    TableName = tableName
            //};
            //dbCodeFireBird.Gen();

            var dbCodeSqlServer = new DbCodeSqlServer
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
            };
            dbCodeSqlServer.Gen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string con = @"Server=localhost;User=SYSDBA;Password=masterket;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");
            //Gen("fxrates_family");
            //this.Close();
            //button1_Click(sender, e);
            btnGen_Click(sender, e);
            //btnGen_Click()
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //string con = @"Server=localhost;User=SYSDBA;Password=P@ssw0rd;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //var _ds = Db.GetDataFireBird(con, "MPO_FISH");

 

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

            foreach (DataRow item in _ds.Tables[0].Rows)
            {
                checkedListBox1.Items.Add(item["TABLE_NAME"].ToString(), false);
            }
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            _constr = txtConstring.Text;
            _path = textBox1.Text;

            #region
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

            //ClearFile();

            //txtConstring.Text =
            //    @"Server=localhost;User=SYSDBA;Password=masterkey;Database=C:\temp\FireBird\FISHWEIGHT.FDB";
            //ClearFile();
            //var columnDropDown = "EM_FLAG:STK_USER_FLAG;";
            //var dsStkUser = Db.GetSchemaFireBird(txtConstring.Text, "STK_USER");
            //Gen(dsStkUser, "STK_USER", columnDropDown);

            //var dsStkUserFlag = Db.GetSchemaFireBird(txtConstring.Text, "STK_USER_FLAG");
            //Gen(dsStkUserFlag, "STK_USER_FLAG", columnDropDown);
            //=================================================================================================
            #endregion

            ClearFile();
            txtConstring.Text = @"Data Source=.;Initial Catalog=WebApp;User ID=sa;Password=P@ssw0rd";
            txtConstring.Text = @"Data Source=.;Initial Catalog=App;User ID=sa;Password=P@ssw0rd";

            //Ice Work
            //string ColumnDropDown = "DepCode:Department;Status:AccountStatus;";
            string ColumnDropDown = "";

            //DataSet AccountRegistration = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "AccountRegistration");
            //Gen(AccountRegistration, "AccountRegistration", ColumnDropDown);

            //DataSet Department = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Department");
            //Gen(Department, "Department", ColumnDropDown);

            //DataSet AccountStatus = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "AccountStatus");
            //Gen(AccountStatus, "AccountStatus", ColumnDropDown);

            //DataSet AccountRegistration = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "AccountRegistration");
            //Gen(AccountRegistration, "AccountRegistration", ColumnDropDown);

            //DataSet APP_USER = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "APP_USER");
            //Gen(APP_USER, "APP_USER", ColumnDropDown);


            DataSet TestValidate = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TestValidate");
            Gen(TestValidate, "TestValidate", ColumnDropDown);



            DataSet Source = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Source");
            Gen(Source, "Source", ColumnDropDown);

            DataSet Category = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Category");
            Gen(Category, "Category", ColumnDropDown);

         
            //end  ice work

            //location work

            //end location

            //string ColumnDropDown = "SupplierID:Suppliers;CategoryID:Categories;";
            //DataSet Categories = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Categories");
            //Gen(Categories, "Categories", ColumnDropDown);

            //DataSet Suppliers = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Suppliers");
            //Gen(Suppliers, "Suppliers", ColumnDropDown);

            //DataSet Products = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Products");
            //Gen(Products, "Products", ColumnDropDown);

            //Copy ไฟล์=================================================================================================================
            Process.Start(@"D:\GitWorkSpace\StkGenCode\copy.bat");
            Console.ReadLine();
            Close();
        }

        private void ClearFile()
        {
            var f = new FileCode { Path = _path };
            f.ClearAllFile();
        }
    }
}