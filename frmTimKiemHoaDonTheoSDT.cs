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
    public partial class frmTimKiemHoaDonTheoSDT : Form
    {
        public frmTimKiemHoaDonTheoSDT()
        {
            InitializeComponent();
        }
        clsBanHang c = new clsBanHang();
        private void txtDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dgvDanhSach.DataSource = c.layDuLieuTheoThuTuc("timKiemHoaDonTheoSDT", "@dienthoai", txtDienThoai.Text);
            }
        }

        private void frmTimKiemHoaDonTheoSDT_Load(object sender, EventArgs e)
        {

        }

        private void txtDienThoai_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
