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
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
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
        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            cboTrangThai.Items.Add("Hoạt Động");
            cboTrangThai.Items.Add("Ngừng Cung Cấp");
            HienThiDuLieu("select * from NhaCungCap where trangthai = 1", dgvDanhSach);
            HienThiTextBox(0, ds);
            btnLuu.Enabled = false;
            txtMaNCC.ReadOnly = true;
        }
        void HienThiTextBox(int vt, DataSet ds)
        {
            txtMaNCC.Text = ds.Tables[0].Rows[vt]["mancc"].ToString();
            txtTenNCC.Text = ds.Tables[0].Rows[vt]["tenncc"].ToString();
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
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(flag!=0)
            {
                DialogResult KQ;
                KQ = MessageBox.Show("Bạn Có Muốn Lưu Không?", "Hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (KQ == DialogResult.Yes)
                    btnLuu_Click(sender, e);
            } 
        }

        private void dgvDanhSach_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

        string themMaNCC()
        {
            DataTable dt = c.layDuLieu("select MaNCC from NhaCungCap order by MaNCC").Tables[0];
            string maNCC = "";
            if (dt.Rows.Count == 0)
            {
                maNCC = "NCC01";
            }
            else
                maNCC = "NCC0" + (dt.Rows.Count + 1).ToString();
            return maNCC;
        }

        int flag = 0;
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = themMaNCC();
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
            XuLyChucNang(true);
            string sql = " ";
            if (flag == 1)
            {
                sql = "insert into nhacungcap values ('" + txtMaNCC.Text + "',N'" + txtTenNCC.Text + "',N'" + txtDiaChi.Text + "',N'" + txtSDT.Text + "',N'" + txtFax.Text + "',N'" + txtMail.Text +"', 1)"; 
            }
            if (flag == 2)
            {
                sql = "update nhacungcap set tenncc=N'" + txtTenNCC.Text + "', diachi=N'" + txtDiaChi.Text + "', phone=N'" + txtSDT.Text + "', sofax=N'" + txtFax.Text + "', dcmail=N'" + txtMail.Text + "' where mancc='" + txtMaNCC.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update nhacungcap set TrangThai = 0 where mancc = '" + txtMaNCC.Text + "'";
            }
            if(c.capNhatDuLieu(sql)!=0)
            {
                MessageBox.Show("Cập Nhật Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmNhaCungCap_Load(sender, e);
            }
            else MessageBox.Show("Không thể cập nhật!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            flag = 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from nhacungcap where mancc like N'%" + txtTimKiem.Text + "%' or tenncc like '%" + txtTimKiem.Text + "%'";
                HienThiDuLieu(sql, dgvDanhSach);
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
