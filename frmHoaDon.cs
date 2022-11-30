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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }
        int flag = 0;
        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
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
        DataSet dsNhanVien = new DataSet();
        DataSet dsSanPham = new DataSet();
        clsBanHang c = new clsBanHang();
        void HienThiDuLieu(string sql, DataGridView d)
        {
            ds = c.layDuLieu(sql);
            d.DataSource = ds.Tables[0];
        }


        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            xuLyChucNang(true);
            dsNhanVien = c.layDuLieu("Select MaNV From nhanvien where trangthai = 1");
            dsSanPham = c.layDuLieu("Select MaSP From SanPham where trangthai = 1");
 
            cboTrangThai.Items.Add("Mới");
            cboTrangThai.Items.Add("Đã Giao");
            cboLoaiHD.Items.Add("N");
            cboLoaiHD.Items.Add("X");
            //HienThiDuLieu("select HoaDon.MaHD, MaNV, MaKh, noichuyen, LoaiHD, NgayLapHD, trangthai from HoaDon, CT_HoaDon where HoaDon.MaHD = CT_HoaDon.MaHD", dgvDanhSach);
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = e.RowIndex;

        }

        private void dgvDanhSach2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int vt = e.RowIndex;

        }
        string phatSinhMaHoaDon()
        {
            DataTable dt = c.layDuLieu("Select MaHD from HoaDon Order by MaHD").Tables[0];
            string MaHD = "";
            if(dt.Rows.Count == 0)
            {
                MaHD = "HD01";
            }
            else
            {
                MaHD = "HD0" + (dt.Rows.Count + 1).ToString();
            }
            return MaHD;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            btnLuu.Enabled = false;
            f = 1;
            taoCot_CTHD();
            lblMaHD.Text = phatSinhMaHoaDon();
            cboTrangThai.SelectedIndex = 0;
            cboLoaiHD.SelectedIndex = 0;
            cboMaNV.DataSource = dsNhanVien.Tables[0];
            cboMaNV.DisplayMember = "MaNV";
            cboMaNV.SelectedIndex = 0;
            cboMaSP.DataSource = dsSanPham.Tables[0];
            cboMaSP.DisplayMember = "MaSP";
        }

        void taoCot_CTHD()
        {
            dgvDanhSach_2.Columns.Clear();
            dgvDanhSach_2.Columns.Add("masp", "Mã Sản Phẩm");
            dgvDanhSach_2.Columns.Add("tensp", "Tên Sản Phẩm");
            dgvDanhSach_2.Columns.Add("soluong", "Số Lượng");
            dgvDanhSach_2.Columns.Add("dongia", "Đơn Giá");
            dgvDanhSach_2.Columns.Add("chietkhau", "Chiết Khấu");
            dgvDanhSach_2.Columns.Add("thanhtien", "Thành Tiền");
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            frmKhachHang f = new frmKhachHang();
            f.Show();
            txtDienThoai.Text = f.SDT;
            lblTenKH.Text = f.TenKH;
        }


        string MaKH = "";
        private void btnTimSDT_Click(object sender, EventArgs e)
        {
            DataSet ds = c.layDuLieu("Select * From KhachHang");
            lblTenKH.Text = "";
            foreach (DataRow d in ds.Tables[0].Rows)
            {
                if (d["phone"].ToString() == txtDienThoai.Text)
                {
                    lblTenKH.Text = d["TenKh"].ToString();
                    MaKH = d["MaKH"].ToString();
                    break;
                }
            }
        }



        decimal tongTien = 0;
        int f = 0;
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            decimal tt = 0;
            tt = (decimal)(int.Parse(txtSoLuong.Text) * float.Parse(txtDonGia.Text) - float.Parse(txtChietKhau.Text));
            tongTien += tt;
            txtThanhTien.Text = tongTien.ToString();

            object[] t = { cboMaSP.Text, txtTenSP.Text, txtSoLuong.Text, txtDonGia.Text, txtChietKhau.Text, tt.ToString() };
            dgvDanhSach_2.Rows.Add(t);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            f = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            xuLyChucNang(false);
            f = 3;
        }

        void hienThiHoaDon(string sql)
        {
            dgvDanhSach.DataSource = c.layDuLieu(sql).Tables[0];
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sqlHD = "";
            xuLyChucNang(true);
            if (f == 1)
            {
                sqlHD = "insert into HoaDon values('" + lblMaHD.Text + "','" + dtpNgayLapHD.Value.ToShortDateString() + "','" + cboMaNV.Text + "', NULL,N'" + MaKH + "','" + cboLoaiHD.Text + "'," + tongTien + ", 1)";
                if (c.capNhatDuLieu(sqlHD) > 0)
                {
                    for (int i = 0; i < dgvDanhSach_2.Rows.Count - 1; i++)
                    {
                        string masp = dgvDanhSach_2.Rows[i].Cells[0].Value.ToString();
                        string soluong = dgvDanhSach_2.Rows[i].Cells[2].Value.ToString();
                        string dongia = dgvDanhSach_2.Rows[i].Cells[3].Value.ToString();
                        string chietkhau = dgvDanhSach_2.Rows[i].Cells[4].Value.ToString();

                        string sqlCTHD = "Insert into CT_HoaDon Values('" + lblMaHD.Text + "','" + masp + "'," + soluong + "," + dongia + "," + chietkhau + ")";
                        c.capNhatDuLieu(sqlCTHD);
                    }
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienThiHoaDon("select HoaDon.MaHD, MaNV, MaKh, LoaiHD, NgayLapHD, trangthai from HoaDon, CT_HoaDon where HoaDon.MaHD = CT_HoaDon.MaHD and HoaDon.MaHD = '" + lblMaHD.Text + "'");
                }
            }
            if (f == 2)
            {
                sqlHD = "update HoaDon set LoaiHD = '" + cboLoaiHD.Text + "' where MaHD = '" + lblMaHD.Text + "'";
                if (c.capNhatDuLieu(sqlHD) > 0)
                {
                    for (int i = 0; i < dgvDanhSach_2.Rows.Count - 1; i++)
                    {
                        string masp = dgvDanhSach_2.Rows[i].Cells[0].Value.ToString();
                        string soluong = dgvDanhSach_2.Rows[i].Cells[2].Value.ToString();
                        string dongia = dgvDanhSach_2.Rows[i].Cells[3].Value.ToString();
                        string chietkhau = dgvDanhSach_2.Rows[i].Cells[4].Value.ToString();
                    }
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienThiHoaDon("select HoaDon.MaHD, MaNV, MaKh, LoaiHD, NgayLapHD, trangthai from HoaDon, CT_HoaDon where trangthai = 1 and HoaDon.MaHD = CT_HoaDon.MaHD and HoaDon.MaHD = '" + lblMaHD.Text + "'");
                }
            }
            if (f == 3)
            {
                sqlHD = "update HoaDon set TrangThai = 0 where MaHD = '" + lblMaHD.Text + "'";
                if (c.capNhatDuLieu(sqlHD) > 0)
                {
                    for (int i = 0; i < dgvDanhSach_2.Rows.Count - 1; i++)
                    {
                        dgvDanhSach_2.DataSource = null;
                    }
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hienThiHoaDon("select HoaDon.MaHD, MaNV, MaKh, LoaiHD, NgayLapHD, trangthai from HoaDon, CT_HoaDon where trangthai = 1 and HoaDon.MaHD = CT_HoaDon.MaHD and HoaDon.MaHD = '" + lblMaHD.Text + "'");
                }
                dgvDanhSach_2.Columns.Clear();
            }
            f = 0;

        }

        void xuLyChucNang(Boolean t)
        {
            btnThem.Enabled = t;
            btnXoa.Enabled = t;
            btnSua.Enabled = t;
            btnLuu.Enabled = !t;
            btnThemSP.Enabled = !t;
            cboMaSP.Enabled = !t;
            txtChietKhau.Enabled = !t;
            txtDienThoai.Enabled = !t;
            txtDonGia.Enabled = !t;
            txtSoLuong.Enabled = !t;
            txtTenSP.Enabled = !t;
            txtThanhTien.Enabled = !t;
            btnLuu.Focus();
        }
        private void cboMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sql = "Select * from SanPham where MaSP='" + cboMaSP.Text + "'";
                DataSet dsSanPham = c.layDuLieu(sql);
                txtTenSP.Text = dsSanPham.Tables[0].Rows[0]["tensp"].ToString();
                txtDonGia.Text = dsSanPham.Tables[0].Rows[0]["dongia"].ToString();

                string h = dsSanPham.Tables[0].Rows[0]["image"].ToString();
                string filename = Path.GetFullPath("Image") + @"\";
                filename += h;
                Bitmap a = new Bitmap(filename);
                picImage.Image = a;
                picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
