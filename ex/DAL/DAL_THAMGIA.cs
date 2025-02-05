using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ex.DTO;
using System.Data;
using System.Data.SqlClient;
namespace ex.DAL
{
    public class DAL_THAMGIA : DBConnect
    {
        public DataTable GetThamGia()
        {
            string query = "select * from [WM].[dbo].[ThamGia]";
            return GetDataTable(query);
        }
        public DataTable GetNguoiDungThamGia(string nhomcode)
        {
            string query = @"
                    select nd.ID, nd.Ten,tg.VaiTro
                    from [WM].[dbo].[NguoiDung] nd
                    inner join [WM].[dbo].[ThamGia] tg
                    on nd.ID = tg.U_id
                    where tg.Nhom_code = @code ";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@code", nhomcode);
                    using(SqlDataAdapter adapter = new SqlDataAdapter(command))
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
        }
 /*       public DataTable GetLeader(string code)
        {
            string query = @"select nd.ID, nd.Ten as Leader
                            from [WM].[dbo].[NguoiDung] nd
                            inner join [WM].[dbo].[ThamGia] tg on nd.ID = tg.U_id
                            where tg.Nhom_code = @code and tg.VaiTro = 'leader' ";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(@query, sqlCon))
                {
                    command.Parameters.AddWithValue("@code", code);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally { Disconnect(); }

        }*/
        public DataTable GetThamGiaWithUid(int u_id)
        {
            string query = @"select tg.Nhom_code, n.TenNhom, tg.VaiTro
                            from [WM].[dbo].[ThamGia] tg
                            inner join [WM].[dbo].[Nhom] n on n.Code = tg.Nhom_code
                            where tg.U_id = @U_id";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
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
                Console.WriteLine(ex.Message);
                return null;
            }
            finally 
            {
            Disconnect(); 
            }
        }
   /*     public DataTable GetThamGiaWithNhom(string nhomcode)
        {
            string query = @"
                            select n.TenNhom, n.Code, nd.ID, nd.Ten
                            from [WM].[dbo].[NguoiDung] as nd
                            inner join [WM].[dbo].[ThamGia] as tg on nd.ID = tg.U_id
                            inner join [WM].[dbo].[Nhom] as n on n.Code = tg.Nhom_code
                            where n.Code = @code
                            order by n.Code";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@code", nhomcode);
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
                Disconnect() ;
            }
        }*/
        public DataTable GetNhomWithLeader(string nhomcode)
        {
            string query = @"
                    select n.TenNhom, n.Code, nd.ID, nd.Ten as Leader
                    from  [WM].[dbo].[NguoiDung] as nd 
                    inner join [WM].[dbo].[ThamGia] as tg on nd.ID = tg.U_id 
                    inner join [WM].[dbo].[Nhom] as n     on n.Code = tg.Nhom_code
                    where tg.VaiTro = 'leader' ";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@code", nhomcode);
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
        }
        
        public bool ThemNhomWithLeader(Nhom nhom, ThamGia thamGia)
        {
            
            string queryNhom = @"
                                INSERT INTO [WM].[dbo].[Nhom] (Code, TenNhom) 
                                VALUES (@NhomCode, @TenNhom);";

            
            string queryThamGia = @"
                                    INSERT INTO [WM].[dbo].[ThamGia] (VaiTro, Nhom_code, U_id) 
                                    VALUES ('leader', @NhomCode, @LeaderId);";

            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                Connect();
                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        // Thêm nhóm mới
                        using (SqlCommand commandNhom = new SqlCommand(queryNhom, sqlCon, transaction))
                        {
                            commandNhom.Parameters.AddWithValue("@NhomCode", nhom.Code);
                            commandNhom.Parameters.AddWithValue("@TenNhom", nhom.TenNhom);
                            commandNhom.ExecuteNonQuery();
                        }

                        // Thêm leader vào bảng ThamGia với vai trò cố định là "leader"
                        using (SqlCommand commandThamGia = new SqlCommand(queryThamGia, sqlCon, transaction))
                        {
                            commandThamGia.Parameters.AddWithValue("@NhomCode", nhom.Code);
                            commandThamGia.Parameters.AddWithValue("@LeaderId", thamGia.U_id);
                            commandThamGia.ExecuteNonQuery();
                        }

                        // Xác nhận giao dịch
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Hủy giao dịch nếu có lỗi
                        transaction.Rollback();
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // In thông báo lỗi nếu có ngoại lệ
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                // Đóng kết nối cơ sở dữ liệu
                Disconnect();
            }
        }
        ///

        public bool ThemThamGia(ThamGia thamGia)
        {
            string query = @"
                INSERT INTO [WM].[dbo].[ThamGia] (Vaitro, Nhom_code, U_id) 
                VALUES (@Vaitro, @Nhom_code, @U_id)";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Vaitro", thamGia.Vaitro);
                    command.Parameters.AddWithValue("@Nhom_code", thamGia.Nhom_code);
                    command.Parameters.AddWithValue("@U_id", thamGia.U_id);

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

        public bool SuaThamGia(ThamGia thamGia)
        {
            string query = @"
                UPDATE [WM].[dbo].[ThamGia] SET 
                    Vaitro = @Vaitro
                     
                WHERE Nhom_code = @Nhom_code and U_id = @U_id";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Vaitro", thamGia.Vaitro);
                    command.Parameters.AddWithValue("@Nhom_code", thamGia.Nhom_code);
                    command.Parameters.AddWithValue("@U_id ", thamGia.U_id);
                    
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

        public bool UpdateNhomWithLeader(Nhom nhom, ThamGia thamGia)
        {
            string queryUpdateNhom = @"update [WM].[dbo].[Nhom] set TenNhom = @TenNhom where Code = @Nhomcode";

            string queryUpdateThamGia = @"update [WM].[dbo].[ThamGia] 
                                          set VaiTro = 'leader', U_id = @LeaderID
                                          where Nhom_code = @Nhomcode ";
            try
            {
                
                Connect();
                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        
                        using (SqlCommand commandUpdateNhom = new SqlCommand(queryUpdateNhom, sqlCon, transaction))
                        {
                            commandUpdateNhom.Parameters.AddWithValue("@Nhomcode", nhom.Code);
                            commandUpdateNhom.Parameters.AddWithValue("@TenNhom", nhom.TenNhom);
                            commandUpdateNhom.ExecuteNonQuery();
                        }

                        
                        using (SqlCommand commandUpdateThamGia = new SqlCommand(queryUpdateThamGia, sqlCon, transaction))
                        {
                            commandUpdateThamGia.Parameters.AddWithValue("@NhomCode", nhom.Code);
                            commandUpdateThamGia.Parameters.AddWithValue("@LeaderId", thamGia.U_id);
                            commandUpdateThamGia.ExecuteNonQuery();
                        }

                        
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        transaction.Rollback();
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
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


        public bool XoaNhomWithLeader(string nhomCode)
        {
            
            string queryXoaThamGia = @"
                                    DELETE FROM [WM].[dbo].[ThamGia] 
                                    WHERE Nhom_code = @NhomCode ";

            
            string queryXoaNhom = @"
                                    DELETE FROM [WM].[dbo].[Nhom] 
                                    WHERE Code = @NhomCode ";

            try
            {
                
                Connect();
                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        
                        using (SqlCommand commandXoaThamGia = new SqlCommand(queryXoaThamGia, sqlCon, transaction))
                        {
                            commandXoaThamGia.Parameters.AddWithValue("@NhomCode", nhomCode);
                            commandXoaThamGia.ExecuteNonQuery();
                        }

                        
                        using (SqlCommand commandXoaNhom = new SqlCommand(queryXoaNhom, sqlCon, transaction))
                        {
                            commandXoaNhom.Parameters.AddWithValue("@NhomCode", nhomCode);
                            commandXoaNhom.ExecuteNonQuery();
                        }

                        
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        transaction.Rollback();
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
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


        public bool ThemThanhVien(ThamGia thamGia)
        {
            // Chuỗi truy vấn SQL để thêm thành viên vào bảng ThamGia với vai trò cố định là "member"
            string queryThamGia = @"
                           INSERT INTO [WM].[dbo].[ThamGia] (VaiTro, Nhom_code, U_id) 
                           VALUES ('member', @NhomCode, @U_id);";

            try
            {
                
                Connect();
                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        
                        using (SqlCommand commandThamGia = new SqlCommand(queryThamGia, sqlCon, transaction))
                        {
                            commandThamGia.Parameters.AddWithValue("@U_id", thamGia.U_id);
                            commandThamGia.Parameters.AddWithValue("@NhomCode", thamGia.Nhom_code);
                            commandThamGia.ExecuteNonQuery();
                        }

                        // Xác nhận giao dịch
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        transaction.Rollback();
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
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
        public bool ChinhSuaThanhVien(ThamGia thamGia)
        {
            string queryUpdateThamGia = @"
                                        UPDATE [WM].[dbo].[ThamGia]
                                        SET VaiTro = 'member'
                                        WHERE Nhom_code = @NhomCode AND U_id = @U_id";

            try
            {
                Connect();
                using (SqlCommand commandUpdateThamGia = new SqlCommand(queryUpdateThamGia, sqlCon))
                {
                    commandUpdateThamGia.Parameters.AddWithValue("@NhomCode", thamGia.Nhom_code);
                    commandUpdateThamGia.Parameters.AddWithValue("@U_id", thamGia.U_id);
                    commandUpdateThamGia.ExecuteNonQuery();
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

        public bool XoaThanhVien(int u_id, string nhomCode)
        {
            string queryDelete = @"
                                    DELETE FROM [WM].[dbo].[ThamGia]
                                    WHERE U_id = @U_id AND Nhom_code = @NhomCode";

            try
            {
                Connect();
                using (SqlCommand commandDelete = new SqlCommand(queryDelete, sqlCon))
                {
                    commandDelete.Parameters.AddWithValue("@U_id", u_id);
                    commandDelete.Parameters.AddWithValue("@NhomCode", nhomCode);
                    commandDelete.ExecuteNonQuery();
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

        public int SoLuongThanhVien(string nhomcode)
        {
            string query = @"select count(*) from [WM].[dbo].[ThamGia] where Nhom_code = @nhomcode";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@nhomcode", nhomcode);

                    int count = (int)command.ExecuteScalar();
                    return count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally { Disconnect(); }
        }

    }
}
