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
    public partial class DangNhap : Form
    {
        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();
        public string role;

        public DangNhap()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dk = new DangKy();
            dk.ShowDialog();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tendn = txtUser.Text;
            string matkhau = txtPass.Text;
           
            string sql = "select ROLE from TAI_KHOAN where USERNAME = '" + tendn + "' and MATKHAU = '" + matkhau + "' ";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                role = reader.GetValue(0).ToString();
                if (role == "Chủ nhà trọ")
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    DangNhap dn = new DangNhap();
                    dn.Close();
                    ChuTro ct = new ChuTro(tendn);
                    ct.ShowDialog();
                    TrangChu tc = new TrangChu(tendn);
                    
                }
                else if (role == "Người dùng")
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    NguoiDung nd = new NguoiDung(tendn);
                    nd.ShowDialog();
                    DangNhap dn = new DangNhap();
                    dn.Close();
                }

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng! Vui lòng thử lại sau.");
            } 
             reader.Close();
        }

       
    }
}
