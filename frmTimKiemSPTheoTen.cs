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
    public partial class frmTimKiemSPTheoTen : Form
    {
        public frmTimKiemSPTheoTen()
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

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * FROM SanPham WHERE TenSP like '%" + txtTenSP.Text + "%'";
            HienThiDuLieu(sql, dgvDanhSach);
        }

        private void frmTimKiemSPTheoTen_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult KQ;
            KQ = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (KQ == DialogResult.No)
                e.Cancel = true;
        }
    }
}
