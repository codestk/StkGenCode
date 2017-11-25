using StkGenCode.Code.Column;
using StkGenCode.Code.Label;
using StkGenCode.Code.Template;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StkGenCode.Code
{
    public class Generator
    {
        public string Constr;

        public string Path;

        public void Gen(DataSet _ds, string tableName, string columnDropDown = "", string exceptionColumn = "", string columnAndLabelSet = "")
        {
            

            var f = new FileCode { Path = Path };

            //ใช้สำหรับทำ  Label
            var textTemplate = new TextTemplate {ColumnAndLabelSet = columnAndLabelSet};

            //Drop Down List
            List<MappingColumn> dropColumnsColumn = null;
            if (columnDropDown != "")
                dropColumnsColumn = MappingColumn.ExtractMappingColumn(columnDropDown);

            List<string> exceptionColumnX = null;
            exceptionColumnX = exceptionColumn.Split(';').ToList();
            //====================================================================================================
            //Code.aspx
            var aspxFromCodeaspx = new AspxFromCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn
               ,
                ExceptionColumn = exceptionColumnX,
                LabelTemplate = textTemplate
            };
            aspxFromCodeaspx.Gen();
            //=================================================================================================
            //Code.cs
            var aspxFromCodeBehide = new AspxFromCodeBehide
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn
                ,
                ExceptionColumn = exceptionColumnX
            };
            aspxFromCodeBehide.Gen();

            //===================================================================================================

            #region Picture Module

            if (aspxFromCodeaspx.HavePicture())
            {
                var ApiControllerPath = Path + @"AppCode\Services\Api\";
                f.Path = ApiControllerPath;
                var ApiController = new ImageApiController
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                    ,
                    ExceptionColumn = exceptionColumnX
                };
                ApiController.Gen();

                var HandlerPath = Path;
                f.Path = HandlerPath;
                var handler = new ImageHandler
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                    ,
                    ExceptionColumn = exceptionColumnX
                };
                handler.Gen();

                var DbCodeImagePath = Path + @"AppCode\Business\"; ;
                f.Path = DbCodeImagePath;
                var _DbCodeImage = new DbCodeImage
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName
                    ,
                    ExceptionColumn = exceptionColumnX
                };
                _DbCodeImage.Gen();
            }

            #endregion Picture Module


            //===================================================================================================
            //Table.aspx
            f.Path = Path;
            var aspxTableCodeFilterColumn = new AspxTableCodeFilterColumn
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                DropColumns = dropColumnsColumn,
                ExceptionColumn = exceptionColumnX,
                LabelTemplate = textTemplate
            };
            aspxTableCodeFilterColumn.Gen();
            //==================================================================================================
            //Table.cs
            var aspxTableCodeFilterColumnCodeBehide =
                new AspxTableCodeFilterColumnCodeBehide
                {
                    FileCode = f,
                    Ds = _ds,
                    TableName = tableName,
                    DropColumns = dropColumnsColumn,
                    ExceptionColumn = exceptionColumnX
                };
         
            aspxTableCodeFilterColumnCodeBehide.Gen();
            //==================================================================================================
            //Service.asmx 
            string pathPageServiceAsmx = Path + @"\Services\";
            f.Path = pathPageServiceAsmx;
            var pageService = new PageService
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
                ,
                ExceptionColumn = exceptionColumnX
            };
            pageService.Gen();

            //==================================================================================================
            // Service.CS

            var pathPageServiceCodeBehide = Path + @"\Services\";
            f.Path = pathPageServiceCodeBehide;
        
            var pageServiceCodeBehide = new PageServiceCodeBehide
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName,
                ExceptionColumn = exceptionColumnX
            };
            pageServiceCodeBehide.Gen();

            //====================================================================================================
            //Store.sql
            var pathSQlScript = Path + @"SQL\";
            f.Path = pathSQlScript;
            var storeProCode = new StoreProCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
                ,
                ExceptionColumn = exceptionColumnX
            };
            storeProCode.Gen();
            //===================================================================================================
            //Gen Javascript
            // Table.js
            var pathJsU = Path + @"Js_U\";
            f.Path = pathJsU;
            var jsCode = new JsCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
                ,
                ExceptionColumn = exceptionColumnX
            };
            jsCode.Gen();

            //========Folder Code
            // BuTable.cs
            var pathBuCode = Path + @"AppCode\Business\";
            f.Path = pathBuCode;
            var pcode = new PropertiesCode
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
                ,
                ExceptionColumn = exceptionColumnX
            };
            pcode.Gen();

            //==================================================================================================
            // Validatotr.cs
            var pathPropertiesCValidate = Path + @"AppCode\Business\";
            f.Path = pathPropertiesCValidate;
            var propertiesCodeValidatetor = new PropertiesCodeValidatetor()
            {
                FileCode = f,
                Ds = _ds,
                TableName = tableName
                ,
                ExceptionColumn = exceptionColumnX
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
                ,
                ExceptionColumn = exceptionColumnX
            };
            dbCodeSqlServer.Gen();
        }

        public void ClearFile()
        {
            if (Path == null)
            {
                throw new Exception("path is not null.");
            }

            var f = new FileCode { Path = Path };
            f.ClearAllFile();
        }
    }
}