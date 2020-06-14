using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab08
{
    public partial class NhanVienMain : Form
    {
        DBE dbe = new DBE();
        public NhanVienMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn muốn thoát chương trình ?", "Thông báo",MessageBoxButtons.OKCancel)==System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnPhongBan_Click(object sender, EventArgs e)
        {
            PhongBanMain newPB = new PhongBanMain();
            newPB.ShowDialog();
            
        }
        
        public void LoadData()
        {
            var listNV = from nv in dbe.NhanViens select new { MaNV = nv.MaNV, HoTen=nv.HoTen,NgaySinh=nv.NgaySinh,DiaChi=nv.DiaChi,MaPB=nv.PhongBan.TenPB };
            DgvNhanVien.DataSource = listNV.ToList();
            txbHoTen.DataBindings.Clear();
            txbHoTen.DataBindings.Add(new Binding("Text", DgvNhanVien.DataSource, "HoTen"));
            txbMaNV.DataBindings.Clear();
            txbMaNV.DataBindings.Add(new Binding("Text", DgvNhanVien.DataSource, "MaNV"));
            txbDiaChi.DataBindings.Clear();
            txbDiaChi.DataBindings.Add(new Binding("Text", DgvNhanVien.DataSource, "DiaChi"));
            CBphongBan.DataSource = dbe.PhongBans.ToList();
            CBphongBan.DisplayMember = "TenPB";
            CBphongBan.DataBindings.Clear();
            CBphongBan.DataBindings.Add(new Binding("Text", DgvNhanVien.DataSource, "MaPB"));
            DTPngaySinh.DataBindings.Clear();
            DTPngaySinh.DataBindings.Add(new Binding("Text", DgvNhanVien.DataSource, "NgaySinh"));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DBE dbeadd = new DBE();
            string maNhanVien = txbMaNV.Text;
            NhanVien NV = dbeadd.NhanViens.Where(nv => nv.MaNV == maNhanVien).SingleOrDefault();
            if (NV == null)
            {
                NhanVien item = new NhanVien();
                item.MaNV = maNhanVien;
                item.HoTen = txbHoTen.Text;
                item.NgaySinh = DTPngaySinh.Value;
                item.DiaChi = txbDiaChi.Text;
                item.MaPB = ((PhongBan)CBphongBan.SelectedValue).MaPB;
                dbeadd.NhanViens.Add(item);
                dbeadd.SaveChanges();
                LoadData();
            } else
            {
                MessageBox.Show("Trùng mã nhân viên. Vui lòng kiểm tra lại", "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DBE dbedelete = new DBE();
            string MaNhanVien = txbMaNV.Text;
            NhanVien NV = dbedelete.NhanViens.Where(nv => nv.MaNV == MaNhanVien).SingleOrDefault();
            if(NV!=null)
            {
                dbedelete.NhanViens.Remove(NV);
                dbedelete.SaveChanges();
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại mã nhân viên", "Lỗi");
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DBE dbeupdate = new DBE();
            string MaNhanVien = txbMaNV.Text;
            NhanVien NV = dbeupdate.NhanViens.Where(nv => nv.MaNV == MaNhanVien).SingleOrDefault();
            if(NV!=null)
            {
                NV.HoTen = txbHoTen.Text;
                NV.NgaySinh = DTPngaySinh.Value;
                NV.DiaChi = txbDiaChi.Text;
                NV.MaPB = ((PhongBan)CBphongBan.SelectedValue).MaPB;
                dbeupdate.SaveChanges();
                LoadData();

            } else
            {
                MessageBox.Show("Vui lòng kiểm tra lại", "Lỗi");
            }
        }

        private void NhanVienMain_VisibleChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
