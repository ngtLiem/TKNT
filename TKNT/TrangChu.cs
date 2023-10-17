using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TKNT
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void cậpNhậtThôngTinToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChuTro chutro = new ChuTro();
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
            NguoiDung nd = new NguoiDung();
            nd.ShowDialog();
        }

        private void tìmKiếmNhàTrọToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu();
            tc.ShowDialog();
        }
    }
}
