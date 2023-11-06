using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TKNT
{   
    

    public partial class TrangChu : Form
    {
        public string username;

        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public TrangChu(string user)
        {
            InitializeComponent();
            labelHelloTC.Text = "Hello, " + user;
            username = user;
        }

        private void cậpNhậtThôngTinToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChuTro chutro = new ChuTro(username);
            chutro.ShowDialog();
        }

        private void thêmNhàTroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhaTro nt = new NhaTro();
            nt.ShowDialog();
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhongTro pt = new PhongTro();
            pt.ShowDialog();
        }

        private void cậpNhậtThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NguoiDung nd = new NguoiDung(username);
            nd.ShowDialog();
        }

        private void tìmKiếmNhàTrọToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tiennghi = combTienNghi.Text;
            string diachi = txtDiaChi.Text;
            if (txtMucGia.Text != "")
            {
                int mucgia = Convert.ToInt32(txtMucGia.Text);
                string sql = "select nt.NT_MA, nt.NT_TEN, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, nt.NT_DIACHI, tn.TN_TEN, pt.PT_MOTA from CHITIET_TN ct join PHONG_TRO pt on ct.PT_MA=pt.PT_MA join TIEN_NGHI tn on ct.TN_MA=tn.TN_MA join NHA_TRO nt on nt.NT_MA=pt.NT_MA where nt.NT_DIACHI like '%" + diachi + "%' or pt.PT_GIA like '%" + mucgia + "%' or tn.TN_TEN like '%" + tiennghi + "%'";
                func.HienthiDulieuDG(dataGridViewSearch, sql, conn);

            }
            else
            {
                string mucgia = " ";
                string sql = "select nt.NT_MA, nt.NT_TEN, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, nt.NT_DIACHI, tn.TN_TEN, pt.PT_MOTA from CHITIET_TN ct join PHONG_TRO pt on ct.PT_MA=pt.PT_MA join TIEN_NGHI tn on ct.TN_MA=tn.TN_MA join NHA_TRO nt on nt.NT_MA=pt.NT_MA where nt.NT_DIACHI like '%" + diachi + "%' or pt.PT_GIA like '%" + mucgia + "%' or tn.TN_TEN like '%" + tiennghi + "%'";
                func.HienthiDulieuDG(dataGridViewSearch, sql, conn);
            }
            
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienthiDulieuDG(dataGridViewSearch, "select nt.NT_MA, nt.NT_TEN, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, nt.NT_DIACHI, tn.TN_TEN, pt.PT_MOTA from CHITIET_TN ct join PHONG_TRO pt on ct.PT_MA=pt.PT_MA join TIEN_NGHI tn on ct.TN_MA=tn.TN_MA join NHA_TRO nt on nt.NT_MA=pt.NT_MA where nt.NT_DIACHI like '%%' or pt.PT_GIA like '%%' or tn.TN_TEN like '%%' order by pt.PT_GIA", conn);
            func.LoadComb(combTienNghi, "select * from TIEN_NGHI", conn, "TN_TEN", "TN_MA");
        }

      

       

       
        
       
        
    }
}
