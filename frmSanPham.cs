using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _431_396_CDTH19E
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
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
        DataSet dsLoaiSP = new DataSet();
        DataSet dsNCC = new DataSet();
        clsBanHang c = new clsBanHang();
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }
        void HienThiTextBox(int vt, DataSet ds)
        {
            txtMaSP.Text = ds.Tables[0].Rows[vt]["masp"].ToString();
            txtTenSP.Text = ds.Tables[0].Rows[vt]["tensp"].ToString();
            txtMoTa.Text = ds.Tables[0].Rows[vt]["mota"].ToString();
            cboLoaiSP.Text = ds.Tables[0].Rows[vt]["maloai"].ToString();
            string h = txtImage.Text = ds.Tables[0].Rows[vt]["image"].ToString();
            string filename = Path.GetFullPath("Image") + @"\";
            filename += txtImage.Text;
            Bitmap a = new Bitmap(filename);
            picImage.Image = a;
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            txtSoLuong.Text = ds.Tables[0].Rows[vt]["soluong"].ToString();
            txtDonGia.Text = ds.Tables[0].Rows[vt]["dongia"].ToString();
            cboNCC.Text = ds.Tables[0].Rows[vt]["mancc"].ToString();
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

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = Path.GetFullPath("Image") + @"\";
            if (o.ShowDialog() == DialogResult.Cancel)
                o.ShowDialog();

            Bitmap a = new Bitmap(o.FileName);
            picImage.Image = a;
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;

            string[] ten;
            ten = o.FileName.Split('\\');
            txtImage.Text = ten[ten.Count() - 1];

        }

        void loadDanhSach_DataGridView()
        {
            ds = c.layDuLieu("Select * from SanPham where trangthai = 1");
            dgvDanhSach.DataSource = ds.Tables[0];
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            XuLyChucNang(true);
            cboTrangThai.Items.Add("Hoạt Động");
            cboTrangThai.Items.Add("Ngừng Hoạt Động");
            loadDanhSach_DataGridView();
            HienThiDuLieu("select * from SanPham where trangthai = 1", dgvDanhSach);
            HienThiTextBox(0, ds);
            dsLoaiSP = c.layDuLieu("Select MaLoai From LoaiSP");
            dsNCC = c.layDuLieu("Select MaNCC From NhaCungCap");
        }
        void XuLyChucNang(Boolean t)
        {
            btnThem.Enabled = t;
            btnXoa.Enabled = t;
            btnSua.Enabled = t;
            btnLuu.Enabled = !t;
            txtDonGia.Enabled = !t;
            txtImage.Enabled = !t;
            txtMoTa.Enabled = !t;
            txtSoLuong.Enabled = !t;
            txtTenSP.Enabled = !t;
            txtMaSP.Enabled = false;
            cboLoaiSP.Enabled = !t;
            cboNCC.Enabled = !t;
            cboTrangThai.Enabled = !t;
        }

        int flag = 0;

        string themMaSP()
        {
            DataTable dt = c.layDuLieu("select MaSP from SanPham order by MaSP").Tables[0];
            string maSP = "";
            if (dt.Rows.Count == 0)
            {
                maSP = "SP01";
            }
            else
                maSP = "SP0" + (dt.Rows.Count + 1).ToString();
            return maSP;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = themMaSP();
            txtMaSP.ReadOnly = true;
            XuLyChucNang(false);
            flag = 1;
            cboLoaiSP.DataSource = dsLoaiSP.Tables[0];
            cboLoaiSP.DisplayMember = "MaLoai";
            cboLoaiSP.SelectedIndex = 0;
            cboNCC.DataSource = dsNCC.Tables[0];
            cboNCC.DisplayMember = "MaNCC";
            cboNCC.SelectedIndex = 0;
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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from sanpham where tensp like N'%" + txtTimKiem.Text + "%' or maloai like '%" + txtTimKiem.Text + "%' or masp like '%" + txtTimKiem.Text + "%'";
                HienThiDuLieu(sql, dgvDanhSach);
            }
            catch
            {
                MessageBox.Show("Vui lòng thử lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = " ";
            XuLyChucNang(true);
            if (flag == 1)
            {
                sql = "INSERT [dbo].[SanPham] ([MaSp], [TenSp], [MoTa], [image], [SoLuong], [DonGia], [mancc], [trangthai], [maloai]) values ('" + txtMaSP.Text + "',N'" + txtTenSP.Text + "',N'" + txtMoTa.Text + "','" + txtImage.Text + "'," + txtSoLuong.Text + "," + txtDonGia.Text + ",'" + cboNCC.Text + "', 1" + ",'" + cboLoaiSP.Text + "')";
            }
            if (flag == 2)
            {
                sql = "update SanPham set tensp = '" + txtTenSP.Text + "', mota = '" + txtMoTa.Text + "', soluong =  " + txtSoLuong.Text + " , dongia =  " + txtDonGia.Text + " , mancc = '" + cboNCC.Text + "', maloai = '" + cboLoaiSP.Text + "', image = '" + txtImage.Text + "' where masp = '" + txtMaSP.Text + "'";
            }
            if (flag == 3)
            {
                sql = "update sanpham set trangthai = 0 where masp='" + txtMaSP.Text + "'";
            }
            if (c.capNhatDuLieu(sql) != 0)
            {
                MessageBox.Show("Cập Nhật Thành Công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmSanPham_Load(sender, e);
            }
            else MessageBox.Show("Không thể cập nhật!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            flag = 0;
        }

        private void tbPrice_Scroll(object sender, EventArgs e)
        {
            lblPrice.Text = string.Format("{0:00.000}", tbPrice.Value.ToString());
            string sql = "select * from sanpham where dongia between 1000 and " + lblPrice.Text + "";
            HienThiDuLieu(sql, dgvDanhSach);
        }
    }
}
