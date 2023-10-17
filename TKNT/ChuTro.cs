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
    public partial class ChuTro : Form
    {
        public ChuTro()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NhaTro nhatro = new NhaTro();
            nhatro.ShowDialog();
        }
    }
}
