using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace StkGenCode.Code
{
    public class Db
    {
        public static DataSet GetSchemaSqlServer(string connectstring, string tableName)
        {
            var sql = "select * from " + tableName + " where 1=0;";
            var fbConnection = new SqlConnection {ConnectionString = connectstring};
            var ds = new DataSet();

            var adapter = new SqlDataAdapter(sql, fbConnection);
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
            var fbConnection = new FbConnection {ConnectionString = connectstring};

            fbConnection.Open();
            var ds = new DataSet();

            var adapter = new FbDataAdapter(sql, fbConnection);
            adapter.Fill(ds);

            //For Get Lenght
            var dataTables = adapter.FillSchema(ds, SchemaType.Source);
            ds.Tables.Clear();
            ds.Tables.Add(dataTables[0]);

            return ds;
        }
    }
}