using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ex.DTO;
using System.Data.SqlClient;

namespace ex.DAL
{
    public class DAL_QLYNHOM : DBConnect
    {
        public DataTable GetNhom()
        {
            string query = "select * from [WM].[dbo].[Nhom]";
            return GetDataTable (query);
        }

     
 /*       public DataTable GetNhomWithUid (int u_id)
        {
            string query = @"
                            select n.TenNhom, n.Code, nd.ID, nd.Ten
                            from [WM].[dbo].[NguoiDung] as nd
                            inner join [WM].[dbo].[ThamGia] as tg on nd.ID = tg.U_id
                            inner join [WM].[dbo].[Nhom] as n on n.Code = tg.Nhom_code
                            where nd.ID = @u_id
                            order by nd.ID";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID", u_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }*/

  /*      public bool ThemNhom(Nhom nhom)
        {
            string query = @"
                Insert into [WM].[dbo].[Nhom] (Code, TenNhom) 
                values (@Code, @TenNhom)";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Code", nhom.Code);
                    command.Parameters.AddWithValue("@TenNhom", nhom.TenNhom);
            
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                Disconnect() ;
            }
        }
 

        public bool SuaNhom(Nhom nhom)
        {
            string query = @"
                update [WM].[dbo].[Nhom] SET 
                    TenNhom = @TenNhom 
                where Code = @Code";
            try
            {
                Connect() ;
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Code", nhom.Code);
                    command.Parameters.AddWithValue("@TenNhom", nhom.TenNhom);
                

                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        public bool XoaNhom(string code)
        {
            string query = "DELETE FROM [WM].[dbo].[Nhom] WHERE Code = @Code";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Code", code);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                Disconnect ();
            }
        }*/
        public bool CheckCode(string code)
        {
            string query = "select count (*) from [WM].[dbo].[Nhom] where Code = @Code";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Code", code);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
                return false;
            }
            finally { Disconnect(); }
        }


        public bool IsMemberInGroup(int u_id, string nhomCode)
        {
            string query = @"
                            SELECT COUNT(*)
                            FROM [WM].[dbo].[ThamGia]
                            WHERE U_id = @U_id AND Nhom_code = @Nhom_code";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@U_id", u_id);
                    command.Parameters.AddWithValue("@Nhom_code", nhomCode);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Disconnect();
            }
        }

        
        

    }
}
