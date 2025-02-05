using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ex.DTO;
using System.Collections;

namespace ex.DAL
{
    public class DAL_NguoiDung : DBConnect
    {
        public DataTable GetNguoiDung()
        {
            string query = @"select * from [WM].[dbo].[NguoiDung] ";
            return GetDataTable(query);
        }

    
        public DataTable GetNguoiDungWithID(int u_id)
        {
            string query = "select * from [WM].[dbo].[NguoiDung] where [ID] = @U_id";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                        //sqlCon.Open();
                        command.Parameters.AddWithValue("@U_id", u_id);
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
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                Disconnect();
            }
        }

        public bool AuthenticateUser(string username, string password, out int uid)
        {
            uid = 0;
            try
            {
                Connect(); 
                string query = "select * from NguoiDung where UserName = @UserName and Pass = @Pass";

                using (SqlCommand command = new SqlCommand(query, sqlCon))
                { 
                        command.Parameters.AddWithValue("@UserName", username);
                        command.Parameters.AddWithValue("@Pass", password);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        uid = int.Parse(reader["ID"].ToString());
                        Program.uid = uid;
                        return reader.HasRows;
                    }
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                return false;
            }
            finally
            {
                Disconnect() ;
            }
        }
        public bool ThemNguoiDung(NguoiDung nd)
        {
            string query = @"
                            insert into [WM].[dbo].[NguoiDung] (Ten, Email, SDT, UserName, Pass, Access)
                            values (@Ten, @Email, @SDT, @UserName, @Pass, @Access)";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                   
                    command.Parameters.AddWithValue("@Ten", nd.Ten);
                    command.Parameters.AddWithValue("@Email", nd.Email);
                    command.Parameters.AddWithValue("@SDT", nd.SDT);
                    command.Parameters.AddWithValue("@UserName", nd.UserName);
                    command.Parameters.AddWithValue("@Pass", nd.Pass);
                    command.Parameters.AddWithValue("@Access", nd.Access);

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
        public bool SuaNguoiDung(NguoiDung nd)
        {
            string query = @"
                             update [WM].[dbo].[NguoiDung] set
                                    Ten = @Ten,
                                    Email = @Email,
                                    SDT = @SDT,
                                    UserName = @UserName,
                                    Pass = @Pass,
                                    Access = @Access
                             Where ID = @ID";
            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID", nd.ID);
                    command.Parameters.AddWithValue("@Ten", nd.Ten);
                    command.Parameters.AddWithValue("@Email", nd.Email);
                    command.Parameters.AddWithValue("@SDT", nd.SDT);
                    command.Parameters.AddWithValue("@UserName", nd.UserName);
                    command.Parameters.AddWithValue("@Pass", nd.Pass);
                    command.Parameters.AddWithValue("@Access", nd.Access);

                    command.ExecuteNonQuery();
                    return true;
                }
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
        public bool XoaNguoiDung(int id)
        {
            string query = "delete [WM].[dbo].[NguoiDung] where ID = @ID";

            try
            {
                Connect() ;
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID", id);
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
        public DataTable GetAvailMember()
        {
            string query = @"select Ten, ID 
                        from [WM].[dbo].[NguoiDung] where Access <> 'admin' ";
            return GetDataTable(query);
           
        }
        public DataTable GetUser(int u_id)
        {
            string query = @"select Ten, ID 
                        from [WM].[dbo].[NguoiDung] where Access <> 'admin' and ID <> @u_id";
            try
            {
                Connect();
                using(SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@u_id", u_id);
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
            finally { Disconnect(); }
        } 


        public DataTable TimKiemTheoTen(string ten)
        {
            string query = "select * from [WM].[dbo].[NguoiDung] where ten like @Ten";
            try
            {
                Connect();
                using(SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Ten", "%" + ten + "%");
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();  
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null ;
            }
            finally { Disconnect(); }
        }

    }
}
