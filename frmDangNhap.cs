using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _431_396_CDTH19E
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTDN.Focus();
            txtMK.Focus();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dangNhap()
        {
            if (txtTDN.Text.Length == 0 && txtMK.Text.Length == 0)
                MessageBox.Show("Tên Đăng Nhập và Mật Khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTDN.Text.Length == 0)
                MessageBox.Show("Tên Đăng Nhập không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtMK.Text.Length == 0)
                MessageBox.Show("Mật Khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtTDN.Text == "Admin" && txtMK.Text == "123")
            {
                MessageBox.Show("Đăng Nhập thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
                MessageBox.Show("Tên Đăng Nhập hoặc Mật Khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            dangNhap();
            if (txtTDN.Text == "Admin" && txtMK.Text == "123")
            {
                frmGiaoDienChinh f = new frmGiaoDienChinh();
                f.Show();
                this.Close();
            }
        }

        private void txtTDN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_dangnhap_Click(sender, e);
            }
        }

        private void txtMK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_dangnhap_Click(sender, e);
            }
        }
    }
}
