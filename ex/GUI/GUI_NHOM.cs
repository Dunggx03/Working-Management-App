using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ex.DAL;
using ex.DTO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace ex.GUI
{
    public partial class GUI_NHOM : Form
    {
        private DAL_THAMGIA DAL_THAMGIA = new DAL_THAMGIA();
        private DAL_QLYNHOM DAL_QLYNHOM = new DAL_QLYNHOM();
        private DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung();
        private DAL_CongViec DAL_CongViec = new DAL_CongViec();
        public GUI_NHOM()
        {
            InitializeComponent();
        }

        private void GUI_NHOM_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCbo();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = DAL_THAMGIA.GetThamGiaWithUid(Program.uid);
            dataGridView2.DataSource = DAL_THAMGIA.GetNguoiDungThamGia(textCode.Text);
            if (int.TryParse(textUid.Text, out int uid))
            {
                string nhomCode = textCode.Text;
                dataGridView3.DataSource = DAL_CongViec.GetCongViecWithNhom_member(uid, nhomCode);

            }
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            groupBox4.Text = "Danh sách thành viên : " + DAL_THAMGIA.SoLuongThanhVien(textCode.Text);
        }

        private void LoadCbo()
        {
            comboUser.DataSource = DAL_NguoiDung.GetUser(Program.uid);
            comboUser.DisplayMember = "Ten";
            comboUser.ValueMember = "ID";

            comboRole.Items.Clear();
            comboRole.Items.Add("leader");
            comboRole.Items.Add("member");
            comboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void create_Click(object sender, EventArgs e)
        {
            var nhom = new Nhom
            {
                Code = textCode.Text,
                TenNhom = textName.Text
            };
            var ThamGia = new ThamGia
            {
                Nhom_code = textCode.Text,
                U_id = Program.uid,
                Vaitro = "leader"
            };
            if (DAL_QLYNHOM.CheckCode(nhom.Code))
            {
                MessageBox.Show("Mã code đã tồn tại !");
                return;
            }
            bool result = DAL_THAMGIA.ThemNhomWithLeader(nhom, ThamGia);
            if (result)
            {
                MessageBox.Show("Nhóm đã được thêm thành công !");
                LoadData(); 
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm nhóm.");
            }
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DAL_CongViec.XoaCongViecNhom(textCode.Text);
            bool result = DAL_THAMGIA.XoaNhomWithLeader(textCode.Text);
            if (result)
            {
                MessageBox.Show("Nhóm đã được xóa thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa nhóm.");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var nhom = new Nhom
            {
                Code = textCode.Text,
                TenNhom = textName.Text
            };
            var ThamGia = new ThamGia
            {
                Nhom_code = textCode.Text,
                U_id = Program.uid,
                Vaitro = "leader"
            };

            bool result = DAL_THAMGIA.UpdateNhomWithLeader(nhom, ThamGia);
            if (result)
            {
                MessageBox.Show("Nhóm được cập nhập thành công !");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật nhóm.");
            }
        }
        private string selectedRole;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textCode.Text = row.Cells["Nhom_code"].Value.ToString();
                textName.Text = row.Cells["TenNhom"].Value.ToString();


                string vaiTro = dataGridView1.Rows[e.RowIndex].Cells["VaiTro"].Value.ToString();
                selectedRole = vaiTro;

                if (vaiTro.ToLower() == "leader")
                {
                    buttonUpdate.Visible = true;  
                    buttonDelete.Visible = true; 
                }
                else
                {
                    buttonUpdate.Visible = false; 
                    buttonDelete.Visible = false;  
                }
            }
        }



        private void buttonIn4_Click(object sender, EventArgs e)
        {
            string nhomCode = textCode.Text;
            DataTable tv = DAL_THAMGIA.GetNguoiDungThamGia(nhomCode);
            if(tv.Rows.Count >= 0)
            {
                dataGridView2.DataSource = tv;
            }
            LoadCbo();
            groupBox4.Text = "Danh sách thành viên : " + DAL_THAMGIA.SoLuongThanhVien(textCode.Text);
        }

 
        

     

/*        private void LoadCongViecNhom(int uid, string nhomCode)
        {
            dataGridView3.DataSource = DAL_CongViec.GetCongViecWithNhom_member(uid, nhomCode);  

        }*/

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                comboUser.Text = row.Cells["Ten"].Value.ToString();
                textUid.Text = row.Cells["ID"].Value.ToString();
                comboRole.Text = row.Cells["VaiTro"].Value.ToString();
              //  string vaiTro = dataGridView2.Rows[e.RowIndex].Cells["VaiTro"].Value.ToString();


                if (selectedRole != "leader")
                {
                    comboUser.Enabled = false;
                    buttonClear.Visible = false;
                    Add.Visible = false;
                }
                else
                {
                    comboUser.Enabled = true;
                    buttonClear.Visible = true;
                    Add.Visible = true;
                }



            }
        }

        private void Add_Click_1(object sender, EventArgs e)
        {
            if (comboUser.SelectedValue != null)
            {
                int memberId = (int)comboUser.SelectedValue;

                var thamGia = new ThamGia
                {
                    Nhom_code = textCode.Text,
                    U_id = int.Parse(textUid.Text),
                    Vaitro = comboRole.Text
                };

                if (DAL_QLYNHOM.IsMemberInGroup(memberId, textCode.Text))
                {
                    MessageBox.Show("Thành viên đã có trong nhóm !");
                    return;
                }

                bool result = DAL_THAMGIA.ThemThanhVien(thamGia);
                if (result)
                {
                    MessageBox.Show("Thành viên đã được thêm thành công vào nhóm!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm thành viên vào nhóm.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thành viên.");
            }
        }

        private void comboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboUser.SelectedValue != null && int.TryParse(comboUser.SelectedValue.ToString(), out int selectedUserId))
            {
                textUid.Text = selectedUserId.ToString();
            }
        }

        private void buttonClear_Click_1(object sender, EventArgs e)
        {
            if (comboUser.SelectedValue != null && int.TryParse(comboUser.SelectedValue.ToString(), out int userId))
            {
                bool result = DAL_THAMGIA.XoaThanhVien(userId, textCode.Text);
                if (result)
                {
                    MessageBox.Show("Xóa thành viên thành công !");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa !");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thành viên hợp lệ.");
            }
        }

        private void buttonCV_Click_1(object sender, EventArgs e)
        {
            string nhomCode = textCode.Text;
            int uid = int.Parse(textUid.Text);
            DataTable cv = DAL_CongViec.GetCongViecWithNhom_member(uid, nhomCode);
            if (cv.Rows.Count >= 0)
            {
                dataGridView3.DataSource = cv;

                if (selectedRole != "leader")
                {
                    textMoTa.Enabled = false;
                    textFb.Enabled = false;
                    dtpNgayBatDau.Enabled = false;
                    dtpNgayKetThuc.Enabled = false;
                    btnDelete.Visible = false;
                    btnAdd.Visible = false;
                    if(uid == Program.uid)
                    {
                        Tiendo.Enabled = true;
                        trackBar1.Enabled = true;
                    }
                    else
                    {
                        Tiendo.Enabled = false;
                        trackBar1.Enabled = false;
                    }
                }
                else
                {
                    textMoTa.Enabled = true;
                    textFb.Enabled = true;
                    dtpNgayBatDau.Enabled = true;
                    dtpNgayKetThuc.Enabled = true;
                    btnDelete.Visible = true;
                    btnAdd.Visible = true;
                    Tiendo.Enabled = false;
                    trackBar1.Enabled = false;
                }
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView2.CurrentRow;
                int selectedUserId = Convert.ToInt32(row.Cells["ID"].Value);

                var congViec = new CongViec
                {
                    U_id = selectedUserId,
                    MoTa = textMoTa.Text,
                    TienDo = Tiendo.Text,
                    NgayBatDau = dtpNgayBatDau.Value,
                    NgayKetThuc = dtpNgayKetThuc.Value,
                    LoaiCongViec = "teamwork",
                    Nhom_code = textCode.Text,
                    FeedBack = textFb.Text

                };

                if (DAL_CongViec.ThemCongViec(congViec))
                {
                    MessageBox.Show("Thêm công việc thành công!");

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Thêm công việc thất bại !");
                }
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            if (dataGridView2.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView2.CurrentRow;
              

                var congViec = new CongViec
                {
                    ID = int.Parse(textID.Text),
                    U_id = int.Parse(textUid.Text),
                    MoTa = textMoTa.Text,
                    TienDo = Tiendo.Text,
                    NgayBatDau = dtpNgayBatDau.Value,
                    NgayKetThuc = dtpNgayKetThuc.Value,
                    LoaiCongViec = "teamwork",
                    Nhom_code = textCode.Text,
                    FeedBack = textFb.Text

                };

                if (DAL_CongViec.SuaCongViec(congViec))
                {
                    MessageBox.Show("Cập nhập công việc thành công !");

                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhập công việc thất bại !");
                }
            }
        }

        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                textMoTa.Text = row.Cells["MoTa"].Value.ToString();
                textFb.Text = row.Cells["FeedBack"].Value.ToString();
                textID.Text = row.Cells["ID"].Value.ToString();
                Tiendo.Text = row.Cells["TienDo"].Value.ToString();
                dtpNgayBatDau.Text = row.Cells["NgayBatDau"].Value.ToString();
                dtpNgayKetThuc.Text = row.Cells["NgayKetThuc"].Value.ToString();
                


                
            }
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            Tiendo.Text = trackBar1.Value.ToString() + "%";
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

            int id = int.Parse(textID.Text);
            if (DAL_CongViec.XoaCongViec(id))
            {
                MessageBox.Show("Xóa công việc thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Xóa công việc thất bại.");
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }  
}
