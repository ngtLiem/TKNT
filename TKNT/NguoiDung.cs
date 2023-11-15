using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

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

            string sql = "select * from NGUOI_THUE_TRO nd, TAI_KHOAN tk where tk.USERNAME=nd.USERNAME and tk.USERNAME='"+username+"'";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read() && txtMaSo.Text == "")
            {
                // Hien thi hinh anh
                string link = reader.GetValue(7).ToString();
                link = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + link;
                pictureND.Image = new Bitmap(link);

                // Hien thi thong tin nguoi thue tro
                txtMaSo.Text = reader.GetValue(0).ToString();
                txtUser.Text = reader.GetValue(1).ToString();
                txtHoten.Text = reader.GetValue(2).ToString();
                txtPhone.Text = reader.GetValue(3).ToString();
                string date = reader.GetValue(4).ToString();
                dateTimeBirthday.Text = string.Format("{0:MM/dd/yyyy}", date);
                txtEmail.Text = reader.GetValue(5).ToString();
                txtDiachi.Text = reader.GetValue(6).ToString();

               

                

            }
            txtMaSo.Enabled = false;
            txtUser.Enabled = false;
            txtHoten.Enabled = false;
            txtPhone.Enabled = false;
            dateTimeBirthday.Enabled = false;
            txtEmail.Enabled = false;
            txtDiachi.Enabled = false;
            btnLoad_ND.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            reader.Close();
            
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }

        private void btnLoad_ND_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            DialogResult dialogresult = openFD.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                string filename = openFD.FileName;
                pictureND.Image = new Bitmap(filename);

                label_pic_nd.Text = filename;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnLoad_ND.Enabled = true;
            txtUser.Enabled = false;
            txtHoten.Enabled = true;
            txtPhone.Enabled = true;
            dateTimeBirthday.Enabled = true;
            txtEmail.Enabled = true;
            txtDiachi.Enabled = true;
            string sql_maxnd = "SELECT MAX(SUBSTRING(NTT_MA, 3, 2)) FROM NGUOI_THUE_TRO";
            SqlCommand comd = new SqlCommand(sql_maxnd, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read() && txtMaSo.Text == "")
            {
                int mandmoi = Convert.ToInt16(reader.GetValue(0).ToString()) + 1;
                if (mandmoi < 10)
                {

                    txtMaSo.Text = "ND0" + mandmoi;
                }
                else
                {
                    txtMaSo.Text = "ND" + mandmoi;
                }
                txtUser.Text = username;
                
                txtHoten.Text = "";
                txtPhone.Text = "";
                dateTimeBirthday.Text = "";
                txtEmail.Text = "";
                txtDiachi.Text = "";
                btnLuu.Enabled = true;
                
            }
            else 
            {
                reader.Close();
                string sql = "select * from NGUOI_THUE_TRO nd, TAI_KHOAN tk where tk.USERNAME=nd.USERNAME and tk.USERNAME='"+username+"'";
                SqlCommand comd_sql = new SqlCommand(sql, conn);
                SqlDataReader reader_sql = comd_sql.ExecuteReader();
                if (reader_sql.Read())
                {
                    // Hien thi hinh anh
                    string link = reader_sql.GetValue(7).ToString();
                    link = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + link;
                    pictureND.Image = new Bitmap(link);

                    // Hien thi thong tin nguoi thue tro
                    txtMaSo.Text = reader_sql.GetValue(0).ToString();
                    txtUser.Text = reader_sql.GetValue(1).ToString();
                    txtHoten.Text = reader_sql.GetValue(2).ToString();
                    txtPhone.Text = reader_sql.GetValue(3).ToString();
                    string date = reader_sql.GetValue(4).ToString();
                    dateTimeBirthday.Text = string.Format("{0:MM/dd/yyyy}", date);
                    txtEmail.Text = reader_sql.GetValue(5).ToString();
                    txtDiachi.Text = reader_sql.GetValue(6).ToString();

                }
                btnLuu.Enabled = false;
                reader_sql.Close();
                
            } 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mand = txtMaSo.Text;
            string username = txtUser.Text;
            string hoten = txtHoten.Text;
            string phone = txtPhone.Text;
            DateTime ngaysinh = Convert.ToDateTime(dateTimeBirthday.Text);
            string date = string.Format("{0:MM/dd/yyyy}", ngaysinh);
            string email = txtEmail.Text;
            string diachi = txtDiachi.Text;
            string link_anh = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + Path.GetFileName(label_pic_nd.Text);
            File.Copy(label_pic_nd.Text, link_anh);

            string sql = "insert into NGUOI_THUE_TRO values('" + mand + "', '" + username + "', N'" + hoten + "', '" + phone + "', '" + date + "', '" + email + "', N'" + diachi + "', '" + Path.GetFileName(label_pic_nd.Text) + "')";
            func.CapNhat(sql, conn);
            MessageBox.Show("Cập nhật thông tin thành công.");
            NguoiDung nd = new NguoiDung(username);
            nd.ShowDialog();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaSo.Enabled = false;
            txtUser.Enabled = false;

            string link_anh = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + Path.GetFileName(label_pic_nd.Text);
            File.Copy(label_pic_nd.Text, link_anh);

            DateTime ngaysinh = Convert.ToDateTime(dateTimeBirthday.Text);
            string date = string.Format("{0:MM/dd/yyyy}", ngaysinh);
            string sql = "update NGUOI_THUE_TRO set NTT_HOTEN=N'" + txtHoten.Text + "', NTT_SDT='" + txtPhone.Text + "', NTT_NGAYSINH='" + date + "' , NTT_DIACHI=N'" + txtDiachi.Text + "', NTT_EMAIL=N'" + txtEmail.Text + "', NTT_AVATAR = '" + Path.GetFileName(label_pic_nd.Text) + "' where NTT_MA = '" + txtMaSo.Text + "' ";
            func.CapNhat(sql, conn);
            MessageBox.Show("Cập nhật thông tin thành công.");
            NguoiDung nd = new NguoiDung(username);
            nd.ShowDialog();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?");
            string sql = "delete from NGUOI_THUE_TRO where NTT_MA = '" + txtMaSo.Text + "'";
            string sql_tk = "delete from TAI_KHOAN where USERNAME = '"+username+"'";
            func.CapNhat(sql, conn);
            func.CapNhat(sql_tk, conn);
            MessageBox.Show("Xóa tài khoản thành công.");
            DangNhap dn = new DangNhap();
            dn.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }

       

      
    }
}
