using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;

namespace StkGenCode.Code
{
    public class Db
    {
        public static DataSet GetSchemaSqlServer(string connectstring, string TableName)
        {
            string sql;

            sql = "select * from " + TableName + " where 1=0;";
            SqlConnection _FbConnection = new SqlConnection();
            _FbConnection.ConnectionString = connectstring;
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, _FbConnection);
            adapter.Fill(ds);

            //For Get Lenght
            var dataTables = adapter.FillSchema(ds, SchemaType.Source);
            ds.Tables.Clear();
            ds.Tables.Add(dataTables[0]);

            return ds;
        }




        public static DataSet GetSchemaFireBird(string connectstring, string TableName)
        {
            string sql;

            sql = "select * from " + TableName + " where 1=0;";
            FbConnection _FbConnection = new FbConnection();
            _FbConnection.ConnectionString = connectstring;

            _FbConnection.Open();
            DataSet ds = new DataSet();

            FbDataAdapter adapter = new FbDataAdapter(sql, _FbConnection);
            adapter.Fill(ds);

            //For Get Lenght
            var dataTables = adapter.FillSchema(ds, SchemaType.Source);
            ds.Tables.Clear();
            ds.Tables.Add(dataTables[0]);

            return ds;
        }

    }
}