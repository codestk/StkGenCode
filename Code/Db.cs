using FirebirdSql.Data.FirebirdClient;
using System.Data;

using System.Data.SqlClient;

namespace StkGenCode.Code
{
    public class Db
    {
        public static DataSet GetSchemaSqlServer(string connectstring, string tableName)
        {
            var sql = "select * from " + tableName + " where 1=0;";
            SqlConnection fbConnection = new SqlConnection { ConnectionString = connectstring };
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, fbConnection);
            adapter.Fill(ds);

            //For Get Lenght
            var dataTables = adapter.FillSchema(ds, SchemaType.Source);
            ds.Tables.Clear();
            ds.Tables.Add(dataTables[0]);

            return ds;
        }

        public static DataSet GetSchemaFireBird(string connectstring, string tableName)
        {
            var sql = "select * from " + tableName + " where 1=0;";
            FbConnection fbConnection = new FbConnection { ConnectionString = connectstring };

            fbConnection.Open();
            DataSet ds = new DataSet();

            FbDataAdapter adapter = new FbDataAdapter(sql, fbConnection);
            adapter.Fill(ds);

            //For Get Lenght
            var dataTables = adapter.FillSchema(ds, SchemaType.Source);
            ds.Tables.Clear();
            ds.Tables.Add(dataTables[0]);

            return ds;
        }
    }
}