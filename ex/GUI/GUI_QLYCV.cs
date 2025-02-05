 using ex.DAL;
using ex.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ex.GUI
{
    public partial class GUI_QLYCV : Form
    {
        public GUI_QLYCV()
        {
            InitializeComponent();
        }

        private DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung();
        private DAL_CongViec DAL_CongViec = new DAL_CongViec();


        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void GUI_QLYCV_Load(object sender, EventArgs e)
        {
            LoadCongViec(Program.uid);
            LoadThongTin(Program.uid);
        }
        private void LoadThongTin(int uid)
        {
            DataTable nd = DAL_NguoiDung.GetNguoiDungWithID(uid);
            textID.Text = nd.Rows[0]["ID"].ToString();
            textName.Text = nd.Rows[0]["Ten"].ToString();
            textEmail.Text = nd.Rows[0]["Email"].ToString();
            textSDT.Text = nd.Rows[0]["SDT"].ToString();
            textuser.Text = nd.Rows[0]["UserName"].ToString();
            textpass.Text = nd.Rows[0]["Pass"].ToString() ;
            comboAccount.Text = nd.Rows[0]["Access"].ToString();
        }
        private void LoadCongViec(int uid)
        {
            dataGridView1.DataSource = DAL_CongViec.GetCongViecWithUid(uid,"personal");
            dataGridView1.Columns["Nhom_code"].Visible = false;
            dataGridView1.Columns["U_id"].Visible = false;
            dataGridView1.Columns["FeedBack"].Visible  = false;
            dataGridView1.Columns["LoaiCongViec"].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textID.Text, out int id))
            {
                NguoiDung nguoiDung = new NguoiDung
                {
                    ID = id,
                    Ten = textName.Text,
                    Email = textEmail.Text,
                    SDT = textSDT.Text,
                    UserName = textuser.Text,
                    Pass = textpass.Text,
                    Access = comboAccount.Text
                };
                if (DAL_NguoiDung.SuaNguoiDung(nguoiDung))
                {
                    MessageBox.Show("Sửa người dùng thành công !");
                    LoadThongTin(int.Parse(textID.Text));
                }
                else
                {
                    MessageBox.Show("Sửa người dùng thất bại !");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textIDcv.Text = row.Cells["ID"].Value.ToString();
                textMoTa.Text = row.Cells["Mota"].Value.ToString();
                Tiendo.Text = row.Cells["TienDo"].Value.ToString();
                dtpNgayBatDau.Text = row.Cells["NgayBatDau"].Value.ToString();
                dtpNgayKetThuc.Text = row.Cells["NgayKetThuc"].Value.ToString();
                textLoai.Text = row.Cells["LoaiCongViec"].Value.ToString();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Tiendo.Text = trackBar1.Value.ToString() + "%";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { 
            var congViec = new CongViec
            { 
                
                MoTa = textMoTa.Text,
                TienDo = Tiendo.Text,
                NgayBatDau = dtpNgayBatDau.Value,
                NgayKetThuc = dtpNgayKetThuc.Value,
                LoaiCongViec = "personal",
                U_id = int.Parse(textID.Text)
            };

            if (DAL_CongViec.ThemCongViecCaNhan(congViec))
            {
                MessageBox.Show("Thêm công việc thành công!");
                LoadCongViec(congViec.U_id);
            }
            else
            {
                MessageBox.Show("Thêm công việc thất bại !");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var congViec = new CongViec
            {
                ID = int.Parse (textIDcv.Text),
                MoTa = textMoTa.Text,
                NgayBatDau = dtpNgayBatDau.Value,
                NgayKetThuc = dtpNgayKetThuc.Value,
                TienDo = Tiendo.Text,
                U_id = int.Parse(textID.Text),
                LoaiCongViec = "personal",
            };

            if (DAL_CongViec.SuaCongViecCaNhan(congViec))
            {
                MessageBox.Show("Cập nhật công việc thành công!");
                LoadCongViec(congViec.U_id);
            }
            else
            {
                MessageBox.Show("Cập nhật công việc thất bại.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textIDcv.Text);
            if (DAL_CongViec.XoaCongViec(id))
            {
                MessageBox.Show("Xóa công việc thành công!");
                LoadCongViec(int.Parse(textID.Text));
            }
            else
            {
                MessageBox.Show("Xóa công việc thất bại.");
            }
        }
    }
}
