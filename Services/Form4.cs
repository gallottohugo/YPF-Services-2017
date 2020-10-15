using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services
{
    public partial class Form4 : Form
    {
        SQLite sQLite = new SQLite();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = sQLite.getMails();

            DataGridViewColumn column1 = dataGridView1.Columns[0];
            column1.Width = 425;
        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            dataGridView1.DataSource = sQLite.getMails();

            DataGridViewColumn column1 = dataGridView1.Columns[0];
            column1.Width = 425;

        }
    }
}
