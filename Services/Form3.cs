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
    public partial class Form3 : Form
    {
        SQLite sqlite = new SQLite();
        DataTable dataTableClients = new DataTable();
        bool changeCheckBoxValue;
        DataGridViewCell datagridViewCell;

        public Form3(DataTable dataTableC)
        {
            InitializeComponent();
            dataTableClients = dataTableC;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //dataTableClients = sqlite.getClients();
            //dataGridView1.DataSource = dataTableClients;
            dataGridView1.DataSource = dataTableClients;
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.AllowUserToAddRows = false;




            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if ((bool)dataGridView1.Rows[row.Index].Cells[8].Value == true)
                {
                    this.dataGridView1.Rows[row.Index].Cells[8].ReadOnly = true;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.RowIndex >= 0)
            {
                if (this.dataGridView1.CurrentCell.Value != null)
                {
                    if ((bool)this.dataGridView1.CurrentCell.Value == false)
                    {
                        DialogResult dialogResult = MessageBox.Show("Seguro lo llamó?", "Alerta", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string dominio = (string)this.dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                            DataRow row = sqlite.getAuto(dominio);
                            int id_value = Convert.ToInt32(row["id"]);
                            sqlite.updateAutoLlamada(id_value, true, DateTime.Today);

                            this.dataGridView1.Rows[e.RowIndex].Cells[7].Value = DateTime.Now;
                            this.dataGridView1.Rows[e.RowIndex].Cells[8].Value = true;
                            this.dataGridView1.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            datagridViewCell = this.dataGridView1.Rows[e.RowIndex].Cells[8];
                            changeCheckBoxValue = true;
                            panel1.Focus();
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (changeCheckBoxValue == true)
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[8].Value = false;
                changeCheckBoxValue = false;
            }
        }
    }
}
