using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;	

namespace TKNT
{
    public partial class DangKy : Form
    {
        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        

        public DangKy()
        {
            InitializeComponent();
            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DangKy_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tendn = txtUser.Text;
            string matkhau = txtPass.Text;
            string cfmatkhau = txtCfpass.Text;
            string vaitro = combRole.Text;

            if (matkhau == cfmatkhau)
            {
                string sql = "insert into TAI_KHOAN values('" + tendn + "', '" + matkhau + "', N'" + vaitro + "')";
                func.CapNhat(sql, conn);
                MessageBox.Show("Tạo tài khoản thành công! Hãy đăng nhập để vào trang chủ.");
                DangKy dangky = new DangKy();
                dangky.Close();
                DangNhap dangnhap = new DangNhap();
                dangnhap.ShowDialog();
            }
            else
            {
                MessageBox.Show("Mật khẩu không khớp! Vui lòng thử lại");
            }
        }
    }
}
