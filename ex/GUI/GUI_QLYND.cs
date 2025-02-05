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
namespace ex.GUI
{
    public partial class GUI_QLYND : Form
    {
        public GUI_QLYND()
        {
            InitializeComponent();
        }
       

        private DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung(); 

        private void LoadData()
        {
            dataGridView1.DataSource = DAL_NguoiDung.GetNguoiDung();
        }

        private void GUI_QLYND_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadCbo();
        }

        private void LoadCbo()
        {
            comboAccount.Items.Clear();
            comboAccount.Items.Add("admin");
            comboAccount.Items.Add("user");
            comboAccount.DropDownStyle = ComboBoxStyle.DropDownList;

        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            NguoiDung nguoiDung = new NguoiDung
            {
                Ten = textName.Text,
                Email = textEmail.Text,
                SDT = textSDT.Text,
                UserName = textuser.Text,
                Pass = textpass.Text,
                Access = comboAccount.Text
            };
            if (DAL_NguoiDung.ThemNguoiDung(nguoiDung))
            {
                MessageBox.Show("Thêm người dùng thành công !");
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm người dùng thất bại !");
            }
            LoadData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
           
                int id = int.Parse(textID.Text);
                if (DAL_NguoiDung.XoaNguoiDung(id))
                {
                    MessageBox.Show("Xóa người dùng thành công !");
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại !"); 
                }
                LoadData() ;

            
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string keyword = textSearch.Text.Trim();
            DataTable dt = DAL_NguoiDung.TimKiemTheoTen(keyword);
            if(dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy người dùng !");
            }
        }
        private void buttonClearSearch_Click(Object sender, EventArgs e)
        {
            textSearch.Clear();
            LoadData();
        }

        

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            
                NguoiDung nguoiDung = new NguoiDung
                {
                    ID = int.Parse(textID.Text),
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
                    LoadData() ;
                }
                else
                {
                    MessageBox.Show("Sửa người dùng thất bại !");
                }
                LoadData() ;
             
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    textID.Text = row.Cells["ID"].Value.ToString();
                    textName.Text = row.Cells["Ten"].Value.ToString();
                    textEmail.Text = row.Cells["email"].Value.ToString();
                    textSDT.Text = row.Cells["SDT"].Value.ToString();
                    textuser.Text = row.Cells["UserName"].Value.ToString();
                    textpass.Text = row.Cells["Pass"].Value.ToString();
                    comboAccount.Text = row.Cells["Access"].Value.ToString();
                   
                }
            }
            catch { }
            
        }
    }
}
