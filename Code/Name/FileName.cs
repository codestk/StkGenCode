#region

using System.Data;

#endregion

namespace StkGenCode.Code.Name
{
    public class FileName
    {
        public DataSet Ds;
        public string TableName;

        #region asp.net
        //Code
        //auth_dtWeb --------------------------------------------------------------------------------------
        public string AspxFromCodeName()
        {
            string name = $"{TableName}Web.aspx";
            return name;
        }

        public string AspxFromCodeBehideName()
        {
            string name = $"{TableName}Web.aspx.cs";
            return name;
        }

        //=================================================================================================

        public string AspxTableCodeName()
        {
            string name = $"{TableName}List.aspx";
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeBehineName()
        {
            string name = $"{TableName}List.aspx.cs";
            return name;
        }

        //===================================================================================================
        public string AspxTableCodeFilterColumnName()
        {
            string name = $"{TableName}ListFilter.aspx";
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeFilterColumnBehineName()
        {
            string name = $"{TableName}ListFilter.aspx.cs";
            return name;
        }
 

    
        //Sp_auth_dtStore
        public string DbCodeName()
        {
            string name = $"{TableName}Db.cs";
            return name;
        }
        public string DbCodeImageName()
        {
            //CategoriesImageDb.cs
            string name = $"{TableName}ImageDb.cs";
            return name;
        }


        public string PropertiesCodeName()
        {
            string name = $"{TableName}.cs";
            return name;
        }
        #endregion
        #region Service
        //var apiService = "api/CatImageController/";
        //var handlerService = "ImageHandler.ashx";

        public   string ImageHandlerName()
        {
            string name = $"{TableName}ImageHandler.ashx";
            return name;
        }
        public   string ControllerName()
        {
            string name = $"{TableName}ImageController.cs";
            return name;
        }

        //Service NAme
        public string PageServiceName()
        {
            //AutoCompleteService.asmx
            var name = $"{TableName}Service.asmx";
            return name;
        }

        public string PageServiceCodeBehideName()
        {
            //AutoCompleteService.asmx
            string name = $"{TableName}Service.asmx.cs";
            return name;
        }


        #endregion

        #region DataBase
        //=================================================================================================
        //DataBase
        //Sp_auth_dtStore
        public string StoreProCodeName()
        {
            string name = $"Store_{TableName}.sql";
            return name;
        }
        #endregion

        #region JavaScript
        //Js File
        public string JsCodeName()
        {
            string name = $"{TableName}.js";
            return name;
        }

        public static string JsCodeName(string jsCodeName)
        {
            string name = $"{jsCodeName}.js";
            return name;
        }
        #endregion
    }
}