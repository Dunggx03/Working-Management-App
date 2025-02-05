using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex.DTO
{
    public class NguoiDung
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string UserName { get; set; }
        public string Pass {  get; set; }
        public string Access { get; set; }

    }

    public class CongViec
    {
        public int ID { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TienDo { get; set; }
        public string Nhom_code { get; set; }
        public int U_id { get; set; }
        public string LoaiCongViec { get; set; }
        public string FeedBack { get; set; }
    }


    public class Nhom
    {
        public string Code { get; set; }
        public string TenNhom { get; set; }
    }

    public class ThamGia
    {
        public int Id { get; set; }
        public string Vaitro { get; set; }
        public string Nhom_code { get; set; }
        public int U_id { get; set; }
    }

    public class NguoiDungThamGia
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string Vaitro { get; set; }
        public string Nhom_code { get; set; }
    }   


  
}
