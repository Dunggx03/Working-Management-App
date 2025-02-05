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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace ex.GUI
{
    public partial class GUI_LOGIN : Form
    {
        DAL_NguoiDung DAL_NguoiDung = new DAL_NguoiDung();
        public GUI_LOGIN()
        {
            InitializeComponent();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            if (MessageBox.Show("Thoát chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
               this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UserName = TextUserName.Text;
            string Pass = textBox1.Text;
            int uid;
            bool result = DAL_NguoiDung.AuthenticateUser(UserName, Pass, out uid);
            if (result)
            {   this.Hide();
                
                Program.uid = uid;
                
                MessageBox.Show("Đăng nhập thành công!");
                GUI_ADHOME f = new GUI_ADHOME();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Không hợp lệ !");
            }

        }


    }
}
