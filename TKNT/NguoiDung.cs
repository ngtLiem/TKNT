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
    public partial class NguoiDung : Form
    {
        public string username;

        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public NguoiDung(string user)
        {
            InitializeComponent();
            labelHelloND.Text = "Hello, " + user;
            username = user;
        }

        private void NguoiDung_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }
    }
}
