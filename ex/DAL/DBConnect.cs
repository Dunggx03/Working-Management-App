
using System.Data;
using System.Data.SqlClient;

namespace ex.DAL
{
    public class DBConnect
    {
        public string strCon = @"Data Source=*your DB source*;Initial Catalog=WM;Integrated Security=True";
        public SqlConnection sqlCon;
        public SqlCommand sqlCom;
        public SqlDataAdapter sqlAdap;
        public DataSet ds;

        public void Connect()
        {
            sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        } 

        public void Disconnect()
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }


        // <param name="strSQL">Chuỗi mô tả câu lệnh SQL: Insert, Update, Delete...</param>
        public void ThucThi(string strSQL)         // update n dont return data
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon))
            {
                sqlCon.Open();
                using (SqlCommand sqlCom = new SqlCommand(strSQL, sqlCon))
                {
                    sqlCom.ExecuteNonQuery();
                }
            }
        }
        public void ThucThiPKN(string strSQL)      // query n display data
        {
            using (SqlDataAdapter sqlAdap = new SqlDataAdapter(strSQL, strCon))
            {
                ds = new DataSet();
                sqlAdap.Fill(ds);
                
            }
        }
        public DataTable GetDataTable(string strSelect)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sqlAdap = new SqlDataAdapter(strSelect, strCon))
            {
                ds = new DataSet();
                sqlAdap.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy(); // cf data not changed
                }
            }
            return dt;
        }


        //string query = "SELECT YourStringColumn FROM YourTable WHERE YourPrimaryKeyColumn = @RowId";
        public string GetStringValueForRow(string query)      
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            return string.Empty;
        }

        public int GetIntValueForRow(string query)
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    object result = command.ExecuteScalar();


                    if (result != null && int.TryParse(result.ToString(), out int intValue))
                    {
                        return intValue;
                    }
                }
            }
            return 0;
        }



    }
}
