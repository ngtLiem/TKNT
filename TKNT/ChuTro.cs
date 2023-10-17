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
    public partial class ChuTro : Form
    {
        public string username;

        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public ChuTro(string user)
        {
            InitializeComponent();
            labelHelloCT.Text = "Hello, " + user;
            username = user;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NhaTro nhatro = new NhaTro();
            nhatro.ShowDialog();
        }

        private void ChuTro_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }
    }
}
