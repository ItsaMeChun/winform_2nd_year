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
        int flag = 0;
        DataSet ds = new DataSet();
        clsBanHang c = new clsBanHang();

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
            cboTrangThai.Items.Add("Hoạt Động");
            cboTrangThai.Items.Add("Ngừng Hoạt Động");
            HienThiDuLieu("select * from LoaiSP where trangthai = 1", dgvDanhSach);
            HienThiTextBox(0, ds);
            btnLuu.Enabled = false;
            txtMaLoai.ReadOnly = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLoaiSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag != 0)
            {
                DialogResult KQ;
                KQ = MessageBox.Show("Bạn Có Muốn Lưu Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (KQ == DialogResult.Yes)
                    btnLuu_Click(sender, e);
            }
        }

        string themMaLoaiSP()
        {
            DataTable dt = c.layDuLieu("select MaLoai from LoaiSP order by MaLoai").Tables[0];
            string maLoai = "";
            if (dt.Rows.Count == 0)
            {
                maLoai = "LSP01";
            }
            else
                maLoai = "LSP0" + (dt.Rows.Count + 1).ToString();
            return maLoai;
        }

        void HienThiTextBox(int vt, DataSet ds)
        {
            txtMaLoai.Text = ds.Tables[0].Rows[vt]["maloai"].ToString();
            txtTenLoai.Text = ds.Tables[0].Rows[vt]["tenloai"].ToString();
            if (ds.Tables[0].Rows[vt]["trangthai"].ToString() == "1")
                cboTrangThai.SelectedIndex = 0;
            else
                cboTrangThai.SelectedIndex = 1;
        }

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

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = e.RowIndex;
            HienThiTextBox(vt, ds);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            flag = 1;
            txtMaLoai.Text = themMaLoaiSP();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            flag = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            flag = 3;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            xuLyChucNang(true);
            string sql = " ";
            if (flag == 1)
            {
                sql = "insert into LoaiSP values('" + txtMaLoai.Text + "',N'" + txtTenLoai.Text + "', 1)";
            }
            if (flag == 2)
            {
                sql = "update LoaiSP set TenLoai = N'" + txtTenLoai.Text + "' where MaLoai = '" + txtMaLoai.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update LoaiSP set trangthai = 0 where maloai ='" + txtMaLoai.Text + "'";
            }
            if (c.capNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmLoaiSanPham_Load(sender, e);
            }
            else MessageBox.Show("Không thể cập nhật!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            flag = 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from LoaiSP where maloai like N'%" + txtTimKiem.Text + "%' or tenloai like '%" + txtTimKiem.Text +  "%'";
                HienThiDuLieu(sql, dgvDanhSach);
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
