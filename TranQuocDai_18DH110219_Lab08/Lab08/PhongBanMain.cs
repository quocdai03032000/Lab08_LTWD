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
    public partial class PhongBanMain : Form
    {
        DBE dbe = new DBE();
        public PhongBanMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn muốn thoát chương trình ?", "Thông Báo",MessageBoxButtons.OKCancel)==System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }
        void LoadData()
        {
            DGVPhongBan.AutoGenerateColumns = false;
            DGVPhongBan.DataSource = dbe.PhongBans.ToList();
            txbMaPB.DataBindings.Clear();
            txbMaPB.DataBindings.Add(new Binding("Text", DGVPhongBan.DataSource, "MaPB"));
            txbTenPB.DataBindings.Clear();
            txbTenPB.DataBindings.Add(new Binding("Text", DGVPhongBan.DataSource, "TenPB"));
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DBE dbeadd = new DBE();
            string MaPhongBan = txbMaPB.Text;
            PhongBan pb = dbeadd.PhongBans.Where(p => p.MaPB == MaPhongBan).SingleOrDefault();
            if(pb==null)
            {
                PhongBan item = new PhongBan();
                item.MaPB = MaPhongBan;
                item.TenPB = txbTenPB.Text;
                dbeadd.PhongBans.Add(item);
                dbeadd.SaveChanges();
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui long kiem tra lai");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            string MaPhongBan = txbMaPB.Text;
            PhongBan pb = dbe.PhongBans.Where(p => p.MaPB == MaPhongBan).SingleOrDefault();
            if (pb != null)
            {
                if(pb.NhanViens.Count>0)
                {
                    MessageBox.Show("Hãy đổi nhân viên của phòng ban này");
                }
                else
                {
                    dbe.PhongBans.Remove(pb);
                    dbe.SaveChanges();
                    LoadData();
                }
            }else
            {
                MessageBox.Show("Vui long kiem tra lai");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaPhongBan = txbMaPB.Text;
            PhongBan pb = dbe.PhongBans.Where(p => p.MaPB == MaPhongBan).SingleOrDefault();
            if (pb != null)
            {
                if(pb.NhanViens.Count>0)
                {
                    foreach(NhanVien nv in pb.NhanViens)
                    {
                        nv.MaPB =MaPhongBan;
                    }
                }
                pb.MaPB = MaPhongBan;
                pb.TenPB = txbTenPB.Text;
                dbe.SaveChanges();
                LoadData();
            }
            else
            {
                MessageBox.Show("Vui long kiem tra lai");
            }
        }
    }
}
