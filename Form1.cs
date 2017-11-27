using StkGenCode.Code;
using System;
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
        //private string _constr;

        //private string _path;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="_ds">dataset</param>
        /// <param name="tableName"></param>
        /// <param name="columnDropDown">Clumn:Table;</param>
        /// <param name="exceptionColumn">Column;Column;Column;</param>
        /// <param name="columnAndLabelSet">ProductLineId:แหล่งที่มา; </param>
        //private void Gen(DataSet _ds, string tableName, string columnDropDown = "", string exceptionColumn = "",string columnAndLabelSet="")
        //{
        //    var f = new FileCode { Path = _path };

        //    //ใช้สำหรับทำ  Label
        //    var textTemplate = new TextTemplate();
        //    textTemplate.ColumnAndLabelSet = columnAndLabelSet;

        //    //Drop Down List
        //    List<MappingColumn> dropColumnsColumn = null;
        //    if (columnDropDown != "")
        //        dropColumnsColumn = MappingColumn.ExtractMappingColumn(columnDropDown);

        //    List<string> exceptionColumnX = null;
        //    exceptionColumnX = exceptionColumn.Split(';').ToList();

        //    var aspxFromCodeaspx = new AspxFromCode
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName,
        //        DropColumns = dropColumnsColumn
        //       ,ExceptionColumn = exceptionColumnX,
        //        LabelTemplate = textTemplate

        //    };
        //    aspxFromCodeaspx.Gen();

        //    var aspxFromCodeBehide = new AspxFromCodeBehide
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName,
        //        DropColumns = dropColumnsColumn
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    aspxFromCodeBehide.Gen();

        //    #region Picture Module

        //    if (aspxFromCodeaspx.HavePicture())
        //    {
        //        var ApiControllerPath = _path + @"AppCode\Services\Api\";
        //        f.Path = ApiControllerPath;
        //        var ApiController = new ImageApiController
        //        {
        //            FileCode = f,
        //            Ds = _ds,
        //            TableName = tableName
        //            ,
        //            ExceptionColumn = exceptionColumnX
        //        };
        //        ApiController.Gen();

        //        var HandlerPath = _path;
        //        f.Path = HandlerPath;
        //        var Handler = new ImageHandler
        //        {
        //            FileCode = f,
        //            Ds = _ds,
        //            TableName = tableName
        //            ,
        //            ExceptionColumn = exceptionColumnX
        //        };
        //        Handler.Gen();

        //        var DbCodeImagePath = _path + @"AppCode\Business\"; ;
        //        f.Path = DbCodeImagePath;
        //        var _DbCodeImage = new DbCodeImage
        //        {
        //            FileCode = f,
        //            Ds = _ds,
        //            TableName = tableName
        //            ,
        //            ExceptionColumn = exceptionColumnX
        //        };
        //        _DbCodeImage.Gen();
        //    }

        //    #endregion Picture Module

        //    f.Path = _path;
        //    var aspxTableCodeFilterColumn = new AspxTableCodeFilterColumn
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName,
        //        DropColumns = dropColumnsColumn,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    aspxTableCodeFilterColumn.Gen();

        //    var aspxTableCodeFilterColumnCodeBehide =
        //        new AspxTableCodeFilterColumnCodeBehide
        //        {
        //            FileCode = f,
        //            Ds = _ds,
        //            TableName = tableName,
        //            DropColumns = dropColumnsColumn
        //            ,
        //            ExceptionColumn = exceptionColumnX
        //        };
        //    //_AspxTableCodeFilterColumnCodeBehide.AspxFromCodeBehide = _AspxFromCodeBehide;
        //    aspxTableCodeFilterColumnCodeBehide.Gen();

        //    //Asmx Service
        //    string pathPageServiceAsmx = _path + @"\Services\";
        //    f.Path = pathPageServiceAsmx;
        //    var pageService = new PageService
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    pageService.Gen();

        //    //var pathPageServiceCodeBehide = _path + @"App_Code\Services\";

        //    var pathPageServiceCodeBehide = _path + @"\Services\";
        //    f.Path = pathPageServiceCodeBehide;
        //    //f.Path = pathPageServiceCodeBehide;
        //    var pageServiceCodeBehide = new PageServiceCodeBehide
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    pageServiceCodeBehide.Gen();

        //    var pathSQlScript = _path + @"SQL\";
        //    f.Path = pathSQlScript;
        //    var storeProCode = new StoreProCode
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    storeProCode.Gen();

        //    //Gen Javascript

        //    var pathJsU = _path + @"Js_U\";
        //    f.Path = pathJsU;
        //    var jsCode = new JsCode
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    jsCode.Gen();

        //    //========Folder Code

        //    var pathBuCode = _path + @"AppCode\Business\";
        //    f.Path = pathBuCode;
        //    var pcode = new PropertiesCode
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    pcode.Gen();

        //    var pathPropertiesCValidate = _path + @"AppCode\Business\";
        //    f.Path = pathPropertiesCValidate;
        //    var propertiesCodeValidatetor = new PropertiesCodeValidatetor()
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX
        //    };
        //    propertiesCodeValidatetor.Gen();

        //    //DbCode _DbCode = new DbCode();
        //    //_DbCode._FileCode = F;
        //    //_DbCode._ds = _ds;
        //    //_DbCode._TableName = _TableName;
        //    //_DbCode.Gen();

        //    //var dbCodeFireBird = new DbCodeFireBird
        //    //{
        //    //    FileCode = f,
        //    //    Ds = _ds,
        //    //    TableName = tableName
        //    //};
        //    //dbCodeFireBird.Gen();

        //    var dbCodeSqlServer = new DbCodeSqlServer
        //    {
        //        FileCode = f,
        //        Ds = _ds,
        //        TableName = tableName
        //        ,
        //        ExceptionColumn = exceptionColumnX

        //    };
        //    dbCodeSqlServer.Gen();
        //}

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
            Generator generator = new Generator();
            generator.Constr = txtConstring.Text;
            generator.Path = textBox1.Text;

            string connectionstring = "";

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

            //ClearFile();
            txtConstring.Text = @"Data Source=.;Initial Catalog=WebApp;User ID=sa;Password=P@ssw0rd";
            txtConstring.Text = @"Data Source=.;Initial Catalog=App;User ID=sa;Password=P@ssw0rd";
            connectionstring = txtConstring.Text;
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

            //DataSet TestValidate = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TestValidate");
            //Gen(TestValidate, "TestValidate", ColumnDropDown);

            //===================================================================================================
            //DataSet Source = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Source");
            //Gen(Source, "Source", ColumnDropDown);

            //DataSet Category = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Category");
            //Gen(Category, "Category", ColumnDropDown);

            //DataSet TestFormula = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "TestFormula");
            //Gen(TestFormula, "TestFormula", ColumnDropDown);

            //DataSet Line = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Line");
            //Gen(Line, "Line", ColumnDropDown);

            //DataSet LineSize = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "LineSize");
            //Gen(LineSize, "LineSize", ColumnDropDown);

            //DataSet CustomerBrand = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "CustomerBrand");
            //Gen(CustomerBrand, "CustomerBrand", ColumnDropDown);

            generator.ClearFile();
            DataSet Contain = Db.GetSchemaSqlServer(connectionstring, "Contain");
            generator.Gen(Contain, "Contain", ColumnDropDown);


            //=========================================================================================================
            DataSet productLine = Db.GetSchemaSqlServer(connectionstring, "ProductLine");
            string productLineLabelSet =
                "ProductLineId:รหัส;SourceID:แหล่งที่มา;CategoryID:ประเภท;LineID:ไลน์ผลิต;ContainID:ขนาดบรรจุ;LineSizeID:ขนาดเส้น;CustomerBrandID:ตรา/ลูกค้า;TestFormulaID:รสชาติสูตร;ManufacturingDate:วันผลิต;ExpectItems:จำนวน;CreateDate:วันที่ทำคำสั่ง;OutHouse:สินค้าภายนอก";
            string productColumnHide = "CreateDate;ProcessItems;";
            string productMapDropDown =
                "SourceID:Source;CategoryID:Category;LineID:Line;ContainID:Contain;LineSizeID:LineSize;CustomerBrandID:CustomerBrand;TestFormulaID:TestFormula;";
            generator.Gen(productLine, "ProductLine", productMapDropDown, productColumnHide, productLineLabelSet);

            //=====================================================================================================
            DataSet ProductPlanning = Db.GetSchemaSqlServer(connectionstring, "ProductPlanning");
            string ProductPlanningMapDropDown = "ProductLineId:ProductLine;UnitTypeId:UnitType;";


          

            string ProductPlanningLabelSet =
                "UnitTypeId:หน่วย;";
            string ProductPlanningColumnHide = "ProductPlanningID;";


            generator.Gen(ProductPlanning, "ProductPlanning", ProductPlanningMapDropDown, ProductPlanningColumnHide, ProductPlanningLabelSet);


            //=================================================================================================
            DataSet UnitType = Db.GetSchemaSqlServer(connectionstring, "UnitType");
            string UnitTypePlanningMapDropDown = "ProductLineId:ProductLine;UnitTypeId:UnitType;";

            string UnitTypeLabelSet =
                "UnitTypeId:หน่วย;";
            string UnitTypetColumnHide = "ProductPlanningID;";

            generator.Gen(UnitType, "UnitType", "", "", UnitTypeLabelSet);

            //DataSet Contact = StkGenCode.Code.Db.GetSchemaSqlServer(txtConstring.Text, "Contact");

            //string ContactLabelSet =
            //    "ProductLineId:รหัส;SourceID:แหล่งที่มา;LineID:ไลน์ผลิต;ContainID:ขนาดบรรจุ;LineSizeID:ขนาดเส้น;CustomerBrandID:ตรา/ลูกค้า;TestFormulaID:รสชาติสูตร;ManufacturingDate:วันผลิต;ExpectItems:จำนวน;CreateDate:วันที่ทำคำสั่ง";
            //string
            //generator.Gen(Contact, "Contact", "SourceID:Source;LineID:Line;ContainID:Contain;LineSizeID:LineSize;CustomerBrandID:CustomerBrand;TestFormulaID:TestFormula;", , ContactLabelSet);

            //,[SourceID]
            //,[LineID]
            //,[TestFormulaID]
            //,[ContainID]
            //,[LineSizeID]
            //,[CustomerBrandID]
            //,[ManufacturingDate]
            //,[ExpectItems]
            //,[ProcessItems]

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

        //private void ClearFile()
        //{
        //    var f = new FileCode { Path = _path };
        //    f.ClearAllFile();
        //}
    }
}