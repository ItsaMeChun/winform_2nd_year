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
    public partial class frmTimKiemTheoNCC : Form
    {
        public frmTimKiemTheoNCC()
        {
            InitializeComponent();
        }

        private void frmTimKiemTheoNCC_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult KQ;
            KQ = MessageBox.Show("Bạn Có Muốn Thoát Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (KQ == DialogResult.No)
                e.Cancel = true;
        }
        DataSet dsNCC = new DataSet();
        DataSet ds = new DataSet();
        clsBanHang c = new clsBanHang();
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }
        Boolean flag = false; 
        private void frmTimKiemTheoNCC_Load(object sender, EventArgs e)
        {
            string sql = "select * FROM nhacungcap";
            dsNCC = c.layDuLieu(sql);
            cboNCC.DataSource = dsNCC.Tables[0];
            cboNCC.DisplayMember = "TenNcc";
            cboNCC.ValueMember = "MaNCC";
            cboNCC.SelectedIndex = -1;
            flag = true;
        }

        private void cboNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                if (cboNCC.SelectedIndex != -1)
                {
                    string sql = "select * FROM SanPham WHERE mancc like '%" + cboNCC.SelectedValue.ToString() + "%'";
                    HienThiDuLieu(sql, dgvDanhSach);
                }
            }
        }
    }
}
