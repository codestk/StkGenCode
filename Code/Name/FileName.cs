#region

using System.Data;

#endregion

namespace StkGenCode.Code.Name
{
    public class FileName
    {
        public DataSet Ds;
        public string TableName;

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

        //

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
            string name = $"{TableName}Service.cs";
            return name;
        }

        //Sp_auth_dtStore
        public string DbCodeName()
        {
            string name = $"{TableName}Db.cs";
            return name;
        }

        public string PropertiesCodeName()
        {
            string name = $"{TableName}.cs";
            return name;
        }

        //=================================================================================================
        //DataBase
        //Sp_auth_dtStore
        public string StoreProCodeName()
        {
            string name = $"Store_{TableName}.sql";
            return name;
        }

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
    }
}