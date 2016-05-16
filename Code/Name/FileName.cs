#region

using System.Data;

#endregion

namespace StkGenCode.Code.Name
{
    public class FileName
    {
        public DataSet _ds;
        public string _TableName;

        //Code
        //auth_dtWeb --------------------------------------------------------------------------------------
        public string AspxFromCodeName()
        {
            string name = $"{_TableName}Web.aspx";
            return name;
        }

        public string AspxFromCodeBehideName()
        {
            string name = $"{_TableName}Web.aspx.cs";
            return name;
        }

        //=================================================================================================

        public string AspxTableCodeName()
        {
            string name = $"{_TableName}List.aspx";
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeBehineName()
        {
            string name = $"{_TableName}List.aspx.cs";
            return name;
        }

        //===================================================================================================
        public string AspxTableCodeFilterColumnName()
        {
            string name = $"{_TableName}ListFilter.aspx";
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeFilterColumnBehineName()
        {
            string name = $"{_TableName}ListFilter.aspx.cs";
            return name;
        }

        //

        //Service NAme
        public string PageServiceName()
        {
            //AutoCompleteService.asmx
            var name = $"{_TableName}Service.asmx";
            return name;
        }

        public string PageServiceCodeBehideName()
        {
            //AutoCompleteService.asmx
            string name = $"{_TableName}Service.cs";
            return name;
        }

        //Sp_auth_dtStore
        public string DbCodeName()
        {
            string name = $"{_TableName}Db.cs";
            return name;
        }

        public string PropertiesCodeName()
        {
            string name = $"{_TableName}.cs";
            return name;
        }

        //=================================================================================================
        //DataBase
        //Sp_auth_dtStore
        public string StoreProCodeName()
        {
            string name = $"Store_{_TableName}.sql";
            return name;
        }

        //Js File
        public string JsCodeName()
        {
            string name = $"{_TableName}.js";
            return name;
        }
    }
}