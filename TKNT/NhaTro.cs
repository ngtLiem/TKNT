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
    public partial class NhaTro : Form
    {
        public SqlConnection conn = new SqlConnection();
        Ham func = new Ham();

        public string username;
        public string machutro;
        public string manhatro;

        public NhaTro(string user)
        {
            InitializeComponent();
            labelHelloNT.Text = "Hello, " + user;
            username = user;
            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        
        private void NhaTro_Load(object sender, EventArgs e)
        {
            func.KetNoi(conn);
            func.HienthiDulieuDG(dataGridView1, "select * from NHA_TRO where CNT_MA = (select CNT_MA from CHU_NHA_TRO where username = '"+username+"')", conn);
            string sql = "select ct.CNT_MA, nt.NT_MA from CHU_NHA_TRO ct, NHA_TRO nt where username = '" + username + "'";
            SqlCommand comd = new SqlCommand(sql, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                machutro = reader.GetValue(0).ToString();
               // manhatro = reader.GetValue(1).ToString();
            }
            reader.Close();
            btnLuu.Enabled = false;
            txtChutro.Enabled = false;
            txtMatro.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string matro = txtMatro.Text;
            if (matro == "") 
            {
                MessageBox.Show("Vui lòng chọn nhà trọ muốn thêm phòng.");
            } 
            else
            {
                PhongTro phongtro = new PhongTro(username, matro);
                phongtro.ShowDialog();
            }
            
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMatro.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtChutro.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTentro.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDiachi.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMota.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql_maxnt = "SELECT MAX(SUBSTRING(NT_MA, 3, 2)) FROM NHA_TRO";
            SqlCommand comd = new SqlCommand(sql_maxnt, conn);
            SqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                int mantmoi = Convert.ToInt16(reader.GetValue(0).ToString()) + 1;
                if (mantmoi < 10)
                {

                    txtMatro.Text = "NT0" + mantmoi;
                }
                else
                {
                    txtMatro.Text = "NT" + mantmoi;
                }
                txtChutro.Text = machutro;
                txtTentro.Text = "";
                txtMota.Text = "";
                txtDiachi.Text = "";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                button1.Enabled = false;
                button6.Enabled = false;
                
            }
            reader.Close();
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string matro = txtMatro.Text;
            string chutro = txtChutro.Text;
            string tentro = txtTentro.Text;
            string diachi = txtDiachi.Text;
            string mota = txtMota.Text;

            string sql_insert = "insert into NHA_TRO values('"+matro+"', '"+chutro+"', N'"+tentro+"', N'"+diachi+"', N'"+mota+"')";
            func.CapNhat(sql_insert, conn);
            MessageBox.Show("Thêm nhà trọ mới thành công.");
            NhaTro nt = new NhaTro(username);
            nt.ShowDialog();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string matro = txtMatro.Text;
            string chutro = txtChutro.Text;
            string tentro = txtTentro.Text;
            string diachi = txtDiachi.Text;
            string mota = txtMota.Text;

            string sql = "update NHA_TRO set NT_TEN = N'" + tentro + "', NT_DIACHI = N'" + diachi + "', NT_MOTA = N'" + mota + "' where NT_MA = '" + matro + "' and CNT_MA = '" + chutro + "'";
            func.CapNhat(sql, conn);
            MessageBox.Show("Cập nhật nhà trọ "+matro+" thành công.");
            func.HienthiDulieuDG(dataGridView1, "select * from NHA_TRO where CNT_MA = (select CNT_MA from CHU_NHA_TRO where username = '" + username + "')", conn);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string manhatro = selectedRow.Cells["NT_MA"].Value.ToString();

                try
                {
                    string sql = "DELETE FROM NHA_TRO WHERE NT_MA = @manhatro";

                    SqlCommand comd = new SqlCommand(sql, conn);
                    comd.Parameters.AddWithValue("@manhatro", manhatro);

                    int rowsAffectedNhaTro = comd.ExecuteNonQuery();

                    if (rowsAffectedNhaTro > 0)
                    {
                        MessageBox.Show("Xóa nhà trọ thành công.");

                        // Hiển thị lại dữ liệu sau khi xóa
                        func.HienthiDulieuDG(dataGridView1, "select * from NHA_TRO where CNT_MA = (select CNT_MA from CHU_NHA_TRO where username = '" + username + "')", conn);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa dữ liệu cho mã nhà trọ: " + manhatro);
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

        private void button6_Click(object sender, EventArgs e)
        {
            ChuTro ct = new ChuTro(username);
            ct.ShowDialog();
        }
    }
}
