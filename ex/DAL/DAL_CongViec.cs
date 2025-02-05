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
    public class DAL_CongViec : DBConnect 
    {
        public DataTable GetCongViec()
        {
            string query = "select * from [WM].[dbo].[CongViec]";
            return GetDataTable(query);
        }

        public DataTable GetCongViecWithUid(int u_id, string loai)
        {
            string query = "select * from [WM].[dbo].[CongViec] where [U_id] = @U_id and [LoaiCongViec] = @loai";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@U_id", u_id);
                    command.Parameters.AddWithValue("loai", loai);
                    
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

        public DataTable GetCongViecWithNhom(string nhomcode)
        {
            string query = "select ID, MoTa,NgayBatDau,NgayKetThuc,TienDo,U_id FROM [WM].[dbo].[CongViec] where [Nhom_code] = @Nhom_code and [LoaiCongViec] = 'teamwork'";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@Nhom_code", nhomcode);
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
        public DataTable GetCongViecWithNhom_member(int u_id, string nhomcode)
        {
            string query = "select ID, MoTa,NgayBatDau,NgayKetThuc,TienDo, FeedBack from [WM].[dbo].[CongViec] where [Nhom_code] = @nhom and [LoaiCongViec] = 'teamwork' and [U_id] = @U_id";
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@U_id", u_id);
                    command.Parameters.AddWithValue("@nhom", nhomcode);

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
        }

        public bool ThemCongViec(CongViec cv)
        {
            string query = @"insert into [WM].[dbo].[CongViec] (MoTa, NgayBatDau, NgayKetThuc, TienDo, U_id, Nhom_code,  LoaiCongViec, FeedBack) 
                                values (@MoTa, @NgayBatDau, @NgayKetThuc, @TienDo, @U_id, @Nhom_code,  @loai, @fb)";

            try
            {
                Connect() ;
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@MoTa", cv.MoTa);
                    command.Parameters.AddWithValue("@NgayBatDau", cv.NgayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", cv.NgayKetThuc);
                    command.Parameters.AddWithValue("@TienDo", cv.TienDo);
                    command.Parameters.AddWithValue("@U_id", cv.U_id);
                    command.Parameters.AddWithValue("@loai", cv.LoaiCongViec);
                    command.Parameters.AddWithValue("@Nhom_code", cv.Nhom_code);
                    command.Parameters.AddWithValue("@fb", cv.FeedBack);
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
        public bool ThemCongViecCaNhan(CongViec cv)
        {
            string query = @"insert into [WM].[dbo].[CongViec] (MoTa, NgayBatDau, NgayKetThuc, TienDo, U_id, LoaiCongViec) 
                                values (@MoTa, @NgayBatDau, @NgayKetThuc, @TienDo, @U_id, 'personal')";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@MoTa", cv.MoTa);
                    command.Parameters.AddWithValue("@NgayBatDau", cv.NgayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", cv.NgayKetThuc);
                    command.Parameters.AddWithValue("@TienDo", cv.TienDo);
                    command.Parameters.AddWithValue("@U_id", cv.U_id);
                    
                   
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
        public bool SuaCongViec(CongViec cv)
        {
            string query = @"
                update [WM].[dbo].[CongViec] set 
                    MoTa = @MoTa, 
                    NgayBatDau = @NgayBatDau, 
                    NgayKetThuc = @NgayKetThuc, 
                    TienDo = @TienDo, 
                    Nhom_code = @Nhom_code, 
                    U_id = @U_id ,
                    LoaiCongViec = @loai,
                    FeedBack = @fb
                where ID = @ID";

            try
            {
                Connect() ;
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID", cv.ID);
                    command.Parameters.AddWithValue("@MoTa", cv.MoTa);
                    command.Parameters.AddWithValue("@NgayBatDau", cv.NgayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", cv.NgayKetThuc);
                    command.Parameters.AddWithValue("@TienDo", cv.TienDo);
                    command.Parameters.AddWithValue("@Nhom_code", cv.Nhom_code);
                    command.Parameters.AddWithValue("@U_id", cv.U_id);
                    command.Parameters.AddWithValue("@loai", cv.LoaiCongViec);
                    command.Parameters.AddWithValue("@fb", cv.FeedBack);
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

        
        public bool SuaCongViecCaNhan(CongViec cv)
        {
            string query = @"
                update [WM].[dbo].[CongViec] set 
                    MoTa = @MoTa, 
                    NgayBatDau = @NgayBatDau, 
                    NgayKetThuc = @NgayKetThuc, 
                    TienDo = @TienDo, 
                    U_id = @U_id ,
                    LoaiCongViec = 'personal'
                where ID = @ID";

            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID", cv.ID);
                    command.Parameters.AddWithValue("@MoTa", cv.MoTa);
                    command.Parameters.AddWithValue("@NgayBatDau", cv.NgayBatDau);
                    command.Parameters.AddWithValue("@NgayKetThuc", cv.NgayKetThuc);
                    command.Parameters.AddWithValue("@TienDo", cv.TienDo);
                    command.Parameters.AddWithValue("@U_id", cv.U_id);
                    
                    
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


        public bool XoaCongViec(int id)
        {
            string query = "Delete from [WM].[dbo].[CongViec] where ID = @ID";

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

        public void XoaCongViecNhom(string nhomcode)
        {
            string query = "delete from [WM].[dbo].[CongViec] where  LoaiCongViec = 'teamwork' and Nhom_code = @nhomcode ";
            try
            {
                Connect() ;
                using (SqlCommand command = new SqlCommand(query, sqlCon))
                {
                    
                    command.Parameters.AddWithValue("@nhomcode", nhomcode);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally { Disconnect(); }
        }







    }








}
