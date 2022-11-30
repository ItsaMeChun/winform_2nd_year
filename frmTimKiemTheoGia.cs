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
    public partial class frmTimKiemTheoGia : Form
    {
        public frmTimKiemTheoGia()
        {
            InitializeComponent();
        }

        private void txtNhapGiaTu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) == false && char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
        }

        private void txtNhapGiaDen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) == false && char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
        }

        private void frmTimKiemTheoGia_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult KQ;
            KQ = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (KQ == DialogResult.No)
                e.Cancel = true;
        }
        DataSet ds = new DataSet();
        clsBanHang c = new clsBanHang();
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql = "select MaSp, TenSp, MoTa, SoLuong, DonGia, mancc from SanPham Where DonGia Between ";
            sql += txtNhapGiaTu.Text + " and " + txtNhapGiaDen.Text;
            HienThiDuLieu(sql, dgvDanhSach);
        }

        private void txtNhapGiaTu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhapGiaDen_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
