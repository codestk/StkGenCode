using System.Data;
using System.Data.SqlClient;

namespace StkGenCode.Code
{
    public class Db
    {
        public static DataSet GetData(string connectstring, string TableName)
        {
            string sql;

            sql = "select * from " + TableName + " where 1=0;";
            SqlConnection _FbConnection = new SqlConnection();
            _FbConnection.ConnectionString = connectstring;
            DataSet ds = new DataSet();

            SqlDataAdapter adapter = new SqlDataAdapter(sql, _FbConnection);
            adapter.Fill(ds);
            return ds;
        }
    }
}