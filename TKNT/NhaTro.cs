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
    public partial class NhaTro : Form
    {
        public NhaTro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PhongTro phongtro = new PhongTro();
            phongtro.ShowDialog();
        }
    }
}
