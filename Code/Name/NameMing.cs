using System.Data;

namespace StkGenCode.Code
{
    public class FileName
    {
        public DataSet _ds;
        public string _TableName;

        //Code
        //auth_dtWeb --------------------------------------------------------------------------------------
        public string AspxFromCodeName()
        {
            string name = "";
            name = string.Format("{0}Web.aspx", _TableName);
            return name;
        }

        public string AspxFromCodeBehideName()
        {
            string name = "";
            name = string.Format("{0}Web.aspx.cs", _TableName);
            return name;
        }

        //=================================================================================================

      
        public string AspxTableCodeName()
        {
            string name = "";
            name = string.Format("{0}List.aspx", _TableName);
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeBehineName()
        {
            string name = "";
            name = string.Format("{0}List.aspx.cs", _TableName);
            return name;
        }
        //===================================================================================================
        public string AspxTableCodeFilterColumnName()
        {
            string name = "";
            name = string.Format("{0}ListFilter.aspx", _TableName);
            return name;
        }

        //auth_dtList.aspx.cs
        public string AspxTableCodeFilterColumnBehineName()
        {
            string name = "";
            name = string.Format("{0}ListFilter.aspx.cs", _TableName);
            return name;
        }
        //

        //Service NAme
        public string PageServiceName()
        {
            //AutoCompleteService.asmx
            string name = "";
            name = string.Format("{0}Service.asmx", _TableName);
            return name;
        }

        public string PageServiceCodeBehideName()
        {
            //AutoCompleteService.asmx
            string name = "";
            name = string.Format("{0}Service.cs", _TableName);
            return name;
        }




        //Sp_auth_dtStore
        public string DbCodeName()
        {
            string name = "";
            name = string.Format("{0}Db.cs", _TableName);
            return name;
        }

        public string PropertiesCodeName()
        {
            string name = "";
            name = string.Format("{0}.cs", _TableName);
            return name;
        }

     



        //=================================================================================================
        //DataBase
        //Sp_auth_dtStore
        public string StoreProCodeName()
        {
            string name = "";
            name = string.Format("Store_{0}.sql", _TableName);
            return name;
        }


        //Js File
        public string JsCodeName()
        {
            string name = "";
            name = string.Format("{0}.js", _TableName);
            return name;
        }


    }
}