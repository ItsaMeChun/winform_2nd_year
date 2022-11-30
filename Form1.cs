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
    public partial class frmLoaiSanPham : Form
    {
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void txtMaLoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) == false && char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
        }

        private void txtTenLoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
                e.Handled = true;
        }

        private void cboTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            cboTinhTrang.Items.Add("Không Hoạt Động");
            cboTinhTrang.Items.Add("Hoạt Động");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLoaiSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult KQ;
            KQ = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (KQ == DialogResult.No)
                e.Cancel = true;
        }
    }
}
