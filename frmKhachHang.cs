using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace _431_396_CDTH19E
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag != 0)
            {
                DialogResult KQ;
                KQ = MessageBox.Show("Bạn Có Muốn Lưu Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (KQ == DialogResult.Yes)
                    btnLuu_Click(sender, e);
            }
        }

        DataSet ds = new DataSet();
        clsBanHang c = new clsBanHang();
        int flag = 0;
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }

        void xuLyChucNang(Boolean t)
        {
            btnXoa.Enabled = t;
            btnThem.Enabled = t;
            btnSua.Enabled = t;
            btnLuu.Enabled = !t;
            btnLuu.Focus();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Không hoạt động");
            HienThiDuLieu("select * from KhachHang where trangthai = 1", dgvDanhSach);
            HienThiTextBox(0, ds);
            btnLuu.Enabled = false;
            txtMaKh.ReadOnly = true;
        }

        void HienThiTextBox(int vt, DataSet ds)
        {
            txtMaKh.Text = ds.Tables[0].Rows[vt]["makh"].ToString();
            txtTenKh.Text = ds.Tables[0].Rows[vt]["tenkh"].ToString();
            txtDiaChi.Text = ds.Tables[0].Rows[vt]["diachi"].ToString();
            txtSDT.Text = ds.Tables[0].Rows[vt]["phone"].ToString();
            txtFax.Text = ds.Tables[0].Rows[vt]["sofax"].ToString();
            txtMail.Text = ds.Tables[0].Rows[vt]["dcmail"].ToString();
            if (ds.Tables[0].Rows[vt]["trangthai"].ToString() == "1")
                cboTrangThai.SelectedIndex = 0;
            else
                cboTrangThai.SelectedIndex = 1;
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = e.RowIndex;
            HienThiTextBox(vt, ds);
        }

        string themMaKH()
        {
            DataTable dt = c.layDuLieu("select MaKH from KhachHang order by MaKH").Tables[0];
            string maKH = "";
            if (dt.Rows.Count == 0)
            {
                maKH = "KH01";
            }
            else
                maKH = "KH0" + (dt.Rows.Count + 1).ToString();
            return maKH;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            flag = 1;
            txtMaKh.Text = themMaKH();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            xuLyChucNang(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            flag = 3;
            xuLyChucNang(false);
        }

        public string MaKH = "";
        public string TenKH = "";
        public string SDT = "";

        private void btnLuu_Click(object sender, EventArgs e)
        {
            xuLyChucNang(true);
            string sql = " ";
            if (flag == 1)
            {
                sql = "insert into KhachHang values(N'" + txtMaKh.Text + "',N'" + txtTenKh.Text + "',N'" + txtDiaChi.Text + "',N'" + txtSDT.Text + "',N'" + txtFax.Text + "',N'" + txtMail.Text + "', 1)";
            }
            if (flag == 2)
            {
                sql = "update KhachHang set Tenkh =N'" + txtTenKh.Text + "', DiaChi = N'" + txtDiaChi.Text + "', Phone =N'" + txtSDT.Text + "', SoFax = '" + txtFax.Text + "', DCMail='" + txtMail.Text + "' where MaKH = '" + txtMaKh.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update KhachHang set TrangThai = 0 where MaKh = '" + txtMaKh.Text + "'";
            }
            if (c.capNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmKhachHang_Load(sender, e);
                MaKH = txtMaKh.Text;
                SDT = txtSDT.Text;
                TenKH = txtTenKh.Text;
            }
            else MessageBox.Show("Không thể cập nhật!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            flag = 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from khachhang where makh like N'%" + txtTimKiem.Text + "%' or tenkh like '%" + txtTimKiem.Text + "%'";
                HienThiDuLieu(sql, dgvDanhSach);
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMail_Leave(object sender, EventArgs e)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$";
            if (Regex.IsMatch(txtMail.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.txtMail, "Sai Định Dạng Email!");
            }
        }
    }
}
