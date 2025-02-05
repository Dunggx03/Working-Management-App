using ex.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ex.DTO;
using static System.Runtime.CompilerServices.RuntimeHelpers;
namespace ex.GUI
{
    public partial class GUI_QLYNHOM : Form
    {
        public GUI_QLYNHOM()
        {
            InitializeComponent();
        }
        private DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung();
        private DAL_QLYNHOM DAL_QLYNHOM = new DAL_QLYNHOM();
        private DAL_THAMGIA DAL_ThamGia = new DAL_THAMGIA();
        private DAL_CongViec DAL_CongViec = new DAL_CongViec();

        private void GUI_QLYNHOM_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCbo();
            
        }

        private void LoadData()
        {
            
            dataGridView2.DataSource = DAL_ThamGia.GetNguoiDungThamGia(textCode.Text);
            dataGridView1.DataSource = DAL_ThamGia.GetNhomWithLeader(textCode.Text);
            dataGridView3.DataSource = DAL_CongViec.GetCongViecWithNhom(textCode.Text);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void LoadCbo()
        {
            comboLeader.DataSource = DAL_NguoiDung.GetNguoiDung();
            comboLeader.DisplayMember = "Ten";
            comboLeader.ValueMember = "ID";

            comboAccount.DataSource = DAL_NguoiDung.GetAvailMember();
            comboAccount.DisplayMember = "Ten";
            comboAccount.ValueMember = "ID";

            comboRole.Items.Clear();
            comboRole.Items.Add("leader");
            comboRole.Items.Add("member");
            comboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboLeader.SelectedValue != null)
            {
                int leaderId = (int)comboLeader.SelectedValue;


                var nhom = new Nhom
                {
                    Code = textCode.Text,
                    TenNhom = textName.Text
                };
                var thamGia = new ThamGia
                {
                    Nhom_code = textCode.Text,
                    U_id = leaderId,
                    Vaitro = "Leader"
                };
                if (DAL_QLYNHOM.CheckCode(nhom.Code))
                {
                    MessageBox.Show("Mã code đã tồn tại !");
                    return;
                }

                bool result = DAL_ThamGia.ThemNhomWithLeader(nhom, thamGia);
                if (result)
                {
                    MessageBox.Show("Nhóm đã được thêm thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm nhóm.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng làm Leader.");
            }
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            DAL_CongViec.XoaCongViecNhom(textCode.Text);

            bool result = DAL_ThamGia.XoaNhomWithLeader(textCode.Text);
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
            if (comboLeader.SelectedValue != null)
            {
                int leaderId = (int)comboLeader.SelectedValue;

                var nhom = new Nhom
                {
                    Code = textCode.Text,
                    TenNhom = textName.Text
                };

                var thamGia = new ThamGia
                {
                    Nhom_code = textCode.Text, 
                    U_id = leaderId,
                    Vaitro = "leader" // Vai trò cố định là "leader"
                };


                bool result = DAL_ThamGia.UpdateNhomWithLeader(nhom, thamGia);
                if (result)
                {
                    MessageBox.Show("Nhóm đã được cập nhật thành công!");
                    LoadData() ;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật nhóm.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng làm Leader.");
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textCode.Text = row.Cells["Code"].Value.ToString();
                textName.Text = row.Cells["TenNhom"].Value.ToString();
                comboLeader.Text = row.Cells["Leader"].Value.ToString();
                
            }
                
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                comboRole.Text = row.Cells["VaiTro"].Value.ToString() ;
                comboAccount.Text = row.Cells["Ten"].Value.ToString() ;

            }
        }

        private void buttonIn4_Click(object sender, EventArgs e)
        {
            string nhomCode = textCode.Text;
            DataTable tv = DAL_ThamGia.GetNguoiDungThamGia(nhomCode);
            if(tv.Rows.Count >= 0)
            {
                dataGridView2.DataSource = tv;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboAccount.SelectedValue != null)
            {
                int memberId = (int)comboAccount.SelectedValue;

                var thamGia = new ThamGia
                {
                    Nhom_code = textCode.Text,
                    U_id = memberId,
                    Vaitro = comboRole.Text
                };
                int u_id = (int)comboAccount.SelectedValue;
                if (DAL_QLYNHOM.IsMemberInGroup(u_id, textCode.Text))
                {
                    MessageBox.Show("Thành viên đã có trong nhóm !");
                    return;
                }

                bool result = DAL_ThamGia.ThemThamGia(thamGia);
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

        private void buttonDele_Click(object sender, EventArgs e)
        {
            bool result = DAL_ThamGia.XoaThanhVien((int)comboAccount.SelectedValue, textCode.Text);
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

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var thamGia = new ThamGia
            {
                Nhom_code = textCode.Text,
                U_id = (int)comboAccount.SelectedValue,
                Vaitro = comboRole.Text
            };
            comboAccount.Enabled = false;
            bool result = DAL_ThamGia.SuaThamGia(thamGia);
            if (result)
            {
                MessageBox.Show("Thành viên được cập nhập thành công !");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật thành viên.");
            }
        }

        private void buttonCV_Click(object sender, EventArgs e)
        {
            string nhomCode = textCode.Text;
            DataTable cv = DAL_CongViec.GetCongViecWithNhom(nhomCode);
            if (cv.Rows.Count >= 0)
            {
                dataGridView3.DataSource = cv;
            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
