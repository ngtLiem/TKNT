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
    public partial class QuenMK : Form
    {
        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public QuenMK()
        {
            InitializeComponent();
        }

        private void QuenMK_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
        }

        private void btnLayMK_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string user = txtUser.Text;
            string vaitro = combRole.Text;

            if (vaitro == "Chủ nhà trọ")
            {
                string sql = "select tk.USERNAME, tk.MATKHAU, ct.CNT_EMAIL from TAI_KHOAN tk join CHU_NHA_TRO ct on ct.USERNAME=tk.USERNAME where tk.USERNAME='" + user + "' and ct.CNT_EMAIL='" + email + "'";
                SqlCommand comd = new SqlCommand(sql, conn);
                SqlDataReader reader = comd.ExecuteReader();
                if (reader.Read())
                {
                    string matkhau = reader.GetValue(1).ToString();
                    MessageBox.Show("Mật khẩu cho tài khoản " + user + " là: " + matkhau + "");
                }
                reader.Close();

            }
            else if (vaitro == "Người dùng")
            {
                string sql = "select tk.USERNAME, tk.MATKHAU, nd.NTT_EMAIL from TAI_KHOAN tk join NGUOI_THUE_TRO nd on nd.USERNAME=tk.USERNAME where tk.USERNAME='" + user + "' and nd.NTT_EMAIL='" + email + "'";
                SqlCommand comd = new SqlCommand(sql, conn);
                SqlDataReader reader = comd.ExecuteReader();
                if (reader.Read())
                {
                    string matkhau = reader.GetValue(1).ToString();
                    MessageBox.Show("Mật khẩu cho tài khoản " + user + " là: " + matkhau + "");
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Thông tin tài khoản chưa có hoặc không chính xác. Vui lòng kiểm tra lại!");
            }


            

            
        }
    }
}
