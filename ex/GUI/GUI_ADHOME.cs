using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ex.DAL;


namespace ex.GUI
{
    public partial class GUI_ADHOME : Form
    {   
        DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung();
        public string quyen = "";
        public GUI_ADHOME()
        {
            InitializeComponent();
            DataTable nd = DAL_NguoiDung.GetNguoiDungWithID(Program.uid);
            quyen = nd.Rows[0]["Access"].ToString();
            wlcome.Text = "Welcome, " + Program.uid;
            if (quyen == "user")
            {
                button1.Text = "Cá nhân";
                button2.Text = "Nhóm";
            }
            if (quyen == "admin")
            {
                button1.Text = "Quản lý người dùng";
                button2.Text = "Quản lý nhóm";
            }

        }

        private void KetNoi()
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-QK1JQ42\SQLEXPRESS;Initial Catalog=WM;Integrated Security=True");
            try
            {
                sqlConnection.Open();
            }     
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
           }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null) { currentFormChild.Close(); }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(childForm);
            pictureBox1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (quyen == "user")
            {
                OpenChildForm(new GUI_QLYCV());
                label2.Text = button1.Text;
            }
            if (quyen == "admin")
            {
                OpenChildForm(new GUI_QLYND());
                label2.Text = button1.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (quyen == "user")
            {
                OpenChildForm(new GUI_NHOM());
                label2.Text = button2.Text;
            }
            if (quyen == "admin")
            {
                OpenChildForm(new GUI_QLYNHOM());
                label2.Text = button2.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Program.uid != 0)
            {
                this.Hide();
                Program.uid = 0;
                MessageBox.Show("Đăng xuất thành công !");
                GUI_LOGIN f = new GUI_LOGIN();
                f.ShowDialog();
                this.Close();   
            }
        }
    }
}
