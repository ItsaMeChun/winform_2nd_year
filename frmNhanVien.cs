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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        int flag = 0;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
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
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            XuLyChucNang(true);
            cboTrangThai.Items.Add("Hoạt Động");
            cboTrangThai.Items.Add("Ngừng Hoạt Động");
            HienThiDuLieu("select * from nhanvien where trangthai = 1", dgvDanhSach);
            HienThiTextBox(0, ds);
            btnLuu.Enabled = false;
            txtMaNV.ReadOnly = true;
        }

        void HienThiTextBox(int vt, DataSet ds)
        {
            txtMaNV.Text = ds.Tables[0].Rows[vt]["manv"].ToString();
            txtTenNV.Text = ds.Tables[0].Rows[vt]["tennv"].ToString();
            dtpNgaySinh.Text = ds.Tables[0].Rows[vt]["ngaysinh"].ToString();
            txtDiaChi.Text = ds.Tables[0].Rows[vt]["diachi"].ToString();
            txtPhone.Text = ds.Tables[0].Rows[vt]["phone"].ToString();
            if (ds.Tables[0].Rows[vt]["gioitinh"].ToString() == "1")
            {
                radNam.Checked = true;
            }
            else
            {
                radNu.Checked = true;
            }
            if (ds.Tables[0].Rows[vt]["trangthai"].ToString() == "1")
            {
                cboTrangThai.SelectedIndex = 0;
            }
            else
            {
                cboTrangThai.SelectedIndex = 1;
            }   
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = e.RowIndex;
            HienThiTextBox(vt, ds);
        }

        void XuLyChucNang(Boolean t)
        {
            btnThem.Enabled = t;
            btnXoa.Enabled = t;
            btnSua.Enabled = t;
            btnLuu.Enabled = !t;
        }

        string themMaNV()
        {
            DataTable dt = c.layDuLieu("select MaNV from nhanvien order by MaNV").Tables[0];
            string maNV = "";
            if (dt.Rows.Count == 0)
            {
                maNV = "NV01";
            }
            else
                maNV = "NV0" + (dt.Rows.Count + 1).ToString();
            return maNV;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = themMaNV();
            XuLyChucNang(false);
            flag = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            XuLyChucNang(false);
            flag = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            XuLyChucNang(false);
            flag = 3;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            XuLyChucNang(true);
            if (flag == 1)
            {
                if (radNam.Checked)
                    sql = "insert into nhanvien values ('" + txtMaNV.Text + "',N'" + txtTenNV.Text + "', 1 ,'" + dtpNgaySinh.Value.ToShortDateString() + "',N'" + txtDiaChi.Text + "',N'" + txtPhone.Text + "', 1)";
                else 
                    sql = "insert into nhanvien values ('" + txtMaNV.Text + "',N'" + txtTenNV.Text + "', 2 ,'" + dtpNgaySinh.Value.ToShortDateString() + "',N'" + txtDiaChi.Text + "',N'" + txtPhone.Text + "', 1)";
            }
            if (flag == 2)
            {
                if (radNam.Checked)
                    sql = "update nhanvien set tennv=N'" + txtTenNV.Text + "', gioitinh = 1 , ngaysinh ='" + dtpNgaySinh.Value.ToShortDateString() + "', diachi =N'" + txtDiaChi.Text + "', phone =N'" + txtPhone.Text + "' where MaNV ='" + txtMaNV.Text + "'";
                else
                    sql = "update nhanvien set tennv=N'" + txtTenNV.Text + "', gioitinh = 2 , ngaysinh ='" + dtpNgaySinh.Value.ToShortDateString() + "', diachi =N'" + txtDiaChi.Text + "', phone =N'" + txtPhone.Text + "' where MaNV ='" + txtMaNV.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update nhanvien set TrangThai = 0 where manv = '" + txtMaNV.Text + "'";
            }
            if (c.capNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập Nhật Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmNhanVien_Load(sender, e);
            }
            else MessageBox.Show("Không thể cập nhật!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            flag = 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from nhanvien where manv like N'%" + txtTimKiem.Text + "%' or tennv like '%" + txtTimKiem.Text + "%'";
                HienThiDuLieu(sql, dgvDanhSach);
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
