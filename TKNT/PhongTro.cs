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
    public partial class PhongTro : Form
    {
        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public string username;
        public string machutro;
        public string manhatro;

        public PhongTro(string user, string mant)
        {
            InitializeComponent();
            labelHelloPT.Text = "Hello, " + user;
            username = user;
            manhatro = mant;

        }

        private void PhongTro_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienthiDulieuDG(dataGridView1, "select * from phong_tro where NT_MA = '"+manhatro+"'", conn);
            btnLuu.Enabled = false;
            txtManhatro.Enabled = false;
            txtMaphongtro.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaphongtro.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtManhatro.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            
            txtTenphong.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDientich.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtGia.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMota.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_maxnt = "SELECT MAX(SUBSTRING(PT_MA, 3, 2)) FROM PHONG_TRO";
            SqlCommand comd = new SqlCommand(sql_maxnt, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                int mantmoi = Convert.ToInt16(reader.GetValue(0).ToString()) + 1;
                if (mantmoi < 10)
                {

                    txtMaphongtro.Text = "PT0" + mantmoi;
                }
                else
                {
                    txtMaphongtro.Text = "PT" + mantmoi;
                }
                txtManhatro.Text = manhatro;
                txtTenphong.Text = "";
                txtMota.Text = "";
                txtDientich.Text = "";
                txtGia.Text = "";

                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                

            }
            reader.Close();
            btnLuu.Enabled = true;
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string maphongtro = selectedRow.Cells["PT_MA"].Value.ToString();

                try
                {
                    string sql_XoaPhongTro = "DELETE FROM PHONG_TRO WHERE PT_MA = @maphongtro";

                    SqlCommand cmdPhongTro = new SqlCommand(sql_XoaPhongTro, conn);
                    cmdPhongTro.Parameters.AddWithValue("@maphongtro", maphongtro);

                    int rowsAffectedPhongTro = cmdPhongTro.ExecuteNonQuery();

                    if (rowsAffectedPhongTro > 0)
                    {
                        MessageBox.Show("Xóa thành công.");

                        // Hiển thị lại dữ liệu sau khi xóa
                        func.HienthiDulieuDG(dataGridView1, "select * from phong_tro where NT_MA = '" + manhatro + "'", conn);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa dữ liệu cho mã phòng trọ: " + maphongtro);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng dữ liệu để xóa.");
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maphongtro = txtMaphongtro.Text;
            string manhatro = txtManhatro.Text;
            string tenphong = txtTenphong.Text;
            string dientich = txtDientich.Text;
            string gia = txtGia.Text;
            string mota = txtMota.Text;

            string sql_insert = "insert into PHONG_TRO values ('" + maphongtro + "', '" + manhatro + "', N'" + tenphong + "', N'" + dientich + "', N'" + gia + "', N'" + mota + "')";
            func.CapNhat(sql_insert, conn);
            MessageBox.Show("Thêm phòng trọ mới thành công.");
            func.HienthiDulieuDG(dataGridView1, "select * from phong_tro where NT_MA = '" + manhatro + "'", conn);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            string maphongtro = txtMaphongtro.Text;
            string manhatro = txtManhatro.Text;
            string tenphong = txtTenphong.Text;
            string dientich = txtDientich.Text;
            string gia = txtGia.Text;
            string mota = txtMota.Text;

            // Cập nhật dữ liệu trong bảng PHONG_TRO
            string sqlUpdatePhongTro = "UPDATE PHONG_TRO SET PT_TEN = N'" + tenphong + "', PT_DIENTICH = '" + dientich + "', PT_GIA = '" + gia + "', PT_MOTA = N'" + mota + "' WHERE PT_MA = '" + maphongtro + "' and NT_MA = '"+manhatro+"'";
            func.CapNhat(sqlUpdatePhongTro, conn);
            MessageBox.Show("Cập nhật phòng trọ thành công.");

            // Hiển thị dữ liệu từ bảng PHONG_TRO trong DataGridView
            func.HienthiDulieuDG(dataGridView1, "select * from phong_tro where NT_MA = '" + manhatro + "'", conn);
        
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            NhaTro nhatro = new NhaTro(username);
            nhatro.ShowDialog();
        }

       


    }
}
