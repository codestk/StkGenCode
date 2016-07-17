using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StkGenCode.Code.Template
{
   public class DbCodeImage :CodeBase
    {

        string Using()
        {
            string code = "";
            code += "using System.Collections.Generic;" + NewLine;
            code += "using System.Data;" + NewLine;
            return code;
        }
        string BeginClass()
        {
            string code = "";
            code += "public class CategoriesImageDb : DataAccess" + NewLine;
            code += " {" + NewLine;
            return code;
        }
        string EndClass()
        {
            return "}"+NewLine;
        }



        string GetPicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();

            string columnPicture = GetColumnPicture();
            code += "public IDataReader GetPicture(string id)";
            code += "    {";
            code += "        string sql = \"SELECT "+ columnPicture + " FROM "+TableName+" where "+ primaryKey + " = @"+ primaryKey + ";\";";
            code += "        var prset = new List<IDataParameter>();";
            code += "        prset.Add(Db.CreateParameterDb(\"@" + primaryKey + "\", id));";
            code += "        Db.OpenFbData();";
            code += "        return Db.FbExecuteReader(sql, prset, CommandType.Text);";
            code += "    }";
            return code;
        }

        string SavePicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();
            string columnPicture = GetColumnPicture();
            code += " public bool SavePicture(string id, byte[] Picture)";
            code += "    {";
            code += "        string sql = \"UPDATE  " + TableName + " SET "+ columnPicture + " = @"+ primaryKey + "  WHERE " + primaryKey + " = @" + primaryKey + "\";";
            code += "";
            code += "        var prset = new List<IDataParameter>();";
            code += "        prset.Add(Db.CreateParameterDb(\"@" + primaryKey + "\", id));";
            code += "        prset.Add(Db.CreateParameterDb(\"@"+ columnPicture + "\", Picture));";
            code += "";
            code += "";
            code += "        int output = Db.FbExecuteNonQuery(sql, prset);";
            code += "        if (output != 1)";
            code += "        {";
            code += "            throw new System.Exception(\"Update\" + this.ToString());";
            code += "        }";
            code += "        return true;";
            code += "";
            code += "    }";
            return code;
        }
        string DeletePicture()
        {
            string code = "";
            string primaryKey = Ds.Tables[0].PrimaryKey[0].ToString();
            string columnPicture = GetColumnPicture();
            code += "  public bool DeletePicture(string id)";
            code += "    {";
            code += "        string sql = \"UPDATE " + TableName + " SET  "+ columnPicture + " =null  WHERE "+ primaryKey + "=@"+ primaryKey + "\";";
            code += "";
            code += "        var prset = new List<IDataParameter>();";
            code += "        prset.Add(Db.CreateParameterDb(\"@"+ primaryKey + "\", id));";
            code += "";
            code += "";
            code += "";
            code += "        int output = Db.FbExecuteNonQuery(sql, prset);";
            code += "        if (output != 1)";
            code += "        {";
            code += "            throw new System.Exception(\"Update\" + this.ToString());";
            code += "        }";
            code += "        return true;";
            code += "";
            code += "    }";
            return code;
        }
      

        public override void Gen()
        {
            string code = "";
            InnitProperties();

            code += Using();
            
            code += BeginClass();

            code += GetPicture();

            code += SavePicture();

            code += DeletePicture();
             
            code += EndClass();

            FileCode.WriteFile(FileName.DbCodeName(), code);
        }

       
    }
}
