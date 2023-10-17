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

        
    }
}
