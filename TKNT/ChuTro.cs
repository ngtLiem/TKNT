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
            NhaTro nhatro = new NhaTro(username);
            nhatro.ShowDialog();
        }

        private void ChuTro_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);

            string sql = "select * from CHU_NHA_TRO ct, TAI_KHOAN tk where tk.USERNAME=ct.USERNAME and tk.USERNAME='" + username + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read() && txtMaCT.Text == "")
            {
                // Hien thi hinh anh
                string link = reader.GetValue(7).ToString();
                link = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + link;
                pictureCT.Image = new Bitmap(link);

                // Hien thi thong tin nguoi thue tro
                txtMaCT.Text = reader.GetValue(0).ToString();
                txtUser.Text = reader.GetValue(1).ToString();
                txtHoten.Text = reader.GetValue(2).ToString();
                string date = reader.GetValue(3).ToString();
                dateTimeBirthdayCT.Text = string.Format("{0:MM/dd/yyyy}", date);
                txtDiachi.Text = reader.GetValue(4).ToString();
                txtPhone.Text = reader.GetValue(5).ToString();
                txtEmail.Text = reader.GetValue(6).ToString();
               
            }
            
            txtMaCT.Enabled = false;
            txtUser.Enabled = false;
            txtHoten.Enabled = false;
            txtPhone.Enabled = false;
            dateTimeBirthdayCT.Enabled = false;
            txtEmail.Enabled = false;
            txtDiachi.Enabled = false;
            btnLoad_CT.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            reader.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }

        private void btnLoad_CT_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            DialogResult dialogresult = openFD.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                string filename = openFD.FileName;
                pictureCT.Image = new Bitmap(filename);

                label_pic_ct.Text = filename;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnLoad_CT.Enabled = true;
            txtUser.Enabled = false;
            txtHoten.Enabled = true;
            txtPhone.Enabled = true;
            dateTimeBirthdayCT.Enabled = true;
            txtEmail.Enabled = true;
            txtDiachi.Enabled = true;
            string sql_maxnd = "SELECT MAX(SUBSTRING(CNT_MA, 3, 2)) FROM CHU_NHA_TRO";
            SqlCommand comd = new SqlCommand(sql_maxnd, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read() && txtMaCT.Text == "")
            {
                int mandmoi = Convert.ToInt16(reader.GetValue(0).ToString()) + 1;
                if (mandmoi < 10)
                {

                    txtMaCT.Text = "CT0" + mandmoi;
                }
                else
                {
                    txtMaCT.Text = "CT" + mandmoi;
                }
                txtUser.Text = username;
                txtHoten.Text = "";
                txtPhone.Text = "";
                dateTimeBirthdayCT.Text = "";
                txtEmail.Text = "";
                txtDiachi.Text = "";
                btnLuu.Enabled = true;

                reader.Close();
            }
            else
            {
                reader.Close();
                string sql = "select * from CHU_NHA_TRO ct, TAI_KHOAN tk where tk.USERNAME=ct.USERNAME and tk.USERNAME='" + username + "'";
                SqlCommand comd_sql = new SqlCommand(sql, conn);
                SqlDataReader reader_sql = comd_sql.ExecuteReader();
                if (reader_sql.Read())
                {
                    // Hien thi hinh anh
                    string link = reader_sql.GetValue(7).ToString();
                    link = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + link;
                    pictureCT.Image = new Bitmap(link);

                    // Hien thi thong tin nguoi thue tro
                    txtMaCT.Text = reader_sql.GetValue(0).ToString();
                    txtUser.Text = reader_sql.GetValue(1).ToString();
                    txtHoten.Text = reader_sql.GetValue(2).ToString();
                    string date = reader_sql.GetValue(3).ToString();
                    dateTimeBirthdayCT.Text = string.Format("{0:MM/dd/yyyy}", date);
                    txtDiachi.Text = reader_sql.GetValue(4).ToString();
                    txtPhone.Text = reader_sql.GetValue(5).ToString();
                    txtEmail.Text = reader_sql.GetValue(6).ToString();
                }
                btnLuu.Enabled = false;
                reader_sql.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mact = txtMaCT.Text;
            string username = txtUser.Text;
            string hoten = txtHoten.Text;
            string phone = txtPhone.Text;
            DateTime ngaysinh = Convert.ToDateTime(dateTimeBirthdayCT.Text);
            string date = string.Format("{0:MM/dd/yyyy}", ngaysinh);
            string email = txtEmail.Text;
            string diachi = txtDiachi.Text;
            string link_anh = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + Path.GetFileName(label_pic_ct.Text);
            File.Copy(label_pic_ct.Text, link_anh);

            string sql = "insert into CHU_NHA_TRO values('" + mact + "', '" + username + "', N'" + hoten + "', '" + date + "', N'" + diachi + "', '"+phone+"', '" + email + "', '" + Path.GetFileName(label_pic_ct.Text) + "')";
            func.CapNhat(sql, conn);
            MessageBox.Show("Cập nhật thông tin thành công.");
            ChuTro ct = new ChuTro(username);
            ct.ShowDialog();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMaCT.Enabled = false;
            txtUser.Enabled = false;

            string link_anh = AppDomain.CurrentDomain.BaseDirectory + "\\images\\" + Path.GetFileName(label_pic_ct.Text);
            File.Copy(label_pic_ct.Text, link_anh);

            DateTime ngaysinh = Convert.ToDateTime(dateTimeBirthdayCT.Text);
            string date = string.Format("{0:MM/dd/yyyy}", ngaysinh);
            string sql = "update CHU_NHA_TRO set CNT_HOTEN=N'" + txtHoten.Text + "', CNT_SDT='" + txtPhone.Text + "', CNT_NGAYSINH='" + date + "' , CNT_DIACHI=N'" + txtDiachi.Text + "', CNT_EMAIL=N'" + txtEmail.Text + "', CNT_AVATAR = '" + Path.GetFileName(label_pic_ct.Text) + "' where CNT_MA = '" + txtMaCT.Text + "' ";
            func.CapNhat(sql, conn);
            MessageBox.Show("Cập nhật thông tin thành công.");
            ChuTro ct = new ChuTro(username);
            ct.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?");
            string sql = "delete from CHU_NHA_TRO where CNT_MA = '" + txtMaCT.Text + "'";
            string sql_tk = "delete from TAI_KHOAN where USERNAME = '" + username + "'";
            func.CapNhat(sql, conn);
            func.CapNhat(sql_tk, conn);
            MessageBox.Show("Xóa tài khoản thành công.");
            DangNhap dn = new DangNhap();
            dn.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrangChu tc = new TrangChu(username);
            tc.ShowDialog();
        }





    }
}
