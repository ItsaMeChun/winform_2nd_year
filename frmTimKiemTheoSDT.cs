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
    public partial class frmTimKiemTheoSDT : Form
    {
        public frmTimKiemTheoSDT()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        clsBanHang c = new clsBanHang();
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }
        private void txtDienThoai_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * FROM KhachHang WHERE Phone like '%" + txtDienThoai.Text + "%'";
            HienThiDuLieu(sql, dgvDanhSach);
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) == false && char.IsDigit(e.KeyChar) == false)
                e.Handled = true;
        }

        private void frmTimKiemTheoSDT_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult KQ;
            KQ = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (KQ == DialogResult.No)
                e.Cancel = true;
        }

        private void frmTimKiemTheoSDT_Load(object sender, EventArgs e)
        {

        }
    }
}
