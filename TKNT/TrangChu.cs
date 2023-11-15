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
        public string machutro;
        public string manhatro;

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
            NhaTro nt = new NhaTro(username);
            nt.ShowDialog();
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "select ct.CNT_MA, nt.NT_MA from CHU_NHA_TRO ct, NHA_TRO nt where username = '" + username + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                machutro = reader.GetValue(0).ToString();
                manhatro = reader.GetValue(1).ToString();
            }
            reader.Close();

            PhongTro pt = new PhongTro(username, manhatro);
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
            string tiennghi = txtTiennghi.Text;
            string diachi = txtDiaChi.Text;
            if (txtMucGia.Text != "")
            {
                int mucgia = Convert.ToInt32(txtMucGia.Text);
                string sql = "select nt.NT_MA, nt.NT_TEN, nt.NT_DIACHI, pt.PT_MA, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, pt.PT_MOTA from NHA_TRO nt join PHONG_TRO pt on nt.NT_MA = pt.NT_MA where pt.PT_MOTA like '%"+tiennghi+"%' or pt.PT_GIA like '%"+mucgia+"%' or nt.NT_DIACHI like '%"+diachi+"%';";

                func.HienthiDulieuDG(dataGridViewSearch, sql, conn);

            }
            else
            {
                string mucgia = " ";
                string sql = "select nt.NT_MA, nt.NT_TEN, nt.NT_DIACHI, pt.PT_MA, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, pt.PT_MOTA from NHA_TRO nt join PHONG_TRO pt on nt.NT_MA = pt.NT_MA where pt.PT_MOTA like '%" + tiennghi + "%' or pt.PT_GIA like '%" + mucgia + "%' or nt.NT_DIACHI like '%" + diachi + "%';";
                func.HienthiDulieuDG(dataGridViewSearch, sql, conn);
            }
            
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienthiDulieuDG(dataGridViewSearch, "select nt.NT_MA, nt.NT_TEN, nt.NT_DIACHI, pt.PT_MA, pt.PT_TEN, pt.PT_DIENTICH, pt.PT_GIA, pt.PT_MOTA from NHA_TRO nt join PHONG_TRO pt on nt.NT_MA = pt.NT_MA", conn);
            //func.LoadComb(combTienNghi, "select * from TIEN_NGHI", conn, "TN_TEN", "TN_MA");
        }

      

       

       
        
       
        
    }
}
