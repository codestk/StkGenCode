using StkGenCode.Code.Name;

namespace StkGenCode.Code.Template
{
    public class DbCodeImage : CodeBase
    {
        private string Using()
        {
            string code = "";
            code += "using System.Collections.Generic;" + NewLine;
            code += "using System.Data;" + NewLine;
            code += "using WebApp.Code.Utility;" + NewLine;
            return code;
        }


        private string BeginNameSpace()
        {
            string code = "";
            code+= "namespace WebApp.AppCode.Business" + NewLine;
            code += "{" + NewLine;
            return code;

        }




     

        private string EndNameSpace()
        {
            string code = "";
            
            code += "}" + NewLine;
            return code;

        }

        private string BeginClass()
        {
            string code = "";
            code += $"public class {ClassName.ImageDbName(TableName)} : DataAccess" + NewLine;
            code += " {" + NewLine;
            return code;
        }

        private string EndClass()
        {
            return "}" + NewLine;
        }

        private string GetPicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();

            string columnPicture = GetColumnPicture();
            code += "public IDataReader GetPicture(string id)" + NewLine;
            code += "    {" + NewLine;
            code += "        string sql = \"SELECT " + columnPicture + " FROM " + TableName + " where " + primaryKey + " = @" + primaryKey + ";\";" + NewLine;
            code += "        var prset = new List<IDataParameter>();" + NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@" + primaryKey + "\", id));" + NewLine;
            code += "        Db.OpenFbData();";
            code += "        return Db.FbExecuteReader(sql, prset, CommandType.Text);" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        private string SavePicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();
            string columnPicture = GetColumnPicture();
            code += " public bool SavePicture(string id, byte[] Picture)" + NewLine;
            code += "    {" + NewLine;
            code += "        string sql = \"UPDATE  " + TableName + " SET " + columnPicture + " = @" + columnPicture + "  WHERE " + primaryKey + " = @" + primaryKey + "\";" + NewLine;
            code += "" + NewLine;
            code += "        var prset = new List<IDataParameter>();" + NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@" + primaryKey + "\", id));" + NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@" + columnPicture + "\", Picture));" + NewLine;
            code += "" + NewLine;
            code += "" + NewLine;
            code += "        int output = Db.FbExecuteNonQuery(sql, prset);" + NewLine;
            code += "        if (output != 1)" + NewLine;
            code += "        {" + NewLine;
            code += "            throw new System.Exception(\"Update\" + this.ToString());" + NewLine;
            code += "        }" + NewLine;
            code += "        return true;" + NewLine;
            code += "" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        private string DeletePicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();
            string columnPicture = GetColumnPicture();
            code += "  public bool DeletePicture(string id)" + NewLine;
            code += "    {" + NewLine;
            code += "        string sql = \"UPDATE " + TableName + " SET  " + columnPicture + " =null  WHERE " + primaryKey + "=@" + primaryKey + "\";" + NewLine;
            code += "" + NewLine;
            code += "        var prset = new List<IDataParameter>();" + NewLine;
            code += "        prset.Add(Db.CreateParameterDb(\"@" + primaryKey + "\", id));" + NewLine;
            code += "" + NewLine;

            code += "" + NewLine;
            code += "        int output = Db.FbExecuteNonQuery(sql, prset);" + NewLine;
            code += "        if (output != 1)" + NewLine;
            code += "        {" + NewLine;
            code += "            throw new System.Exception(\"Update\" + this.ToString());" + NewLine;
            code += "        }" + NewLine;
            code += "        return true;" + NewLine;
            code += "" + NewLine;
            code += "    }" + NewLine;
            return code;
        }

        public override void Gen()
        {
            string code = "";
            InnitProperties();

            code += Using();
            code += BeginNameSpace();
            code += BeginClass();

            code += GetPicture();

            code += SavePicture();

            code += DeletePicture();

            code += EndClass();
            code += EndNameSpace();
            FileCode.WriteFile(FileName.DbCodeImageName(), code);
        }
    }
}