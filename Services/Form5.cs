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
    public partial class Form5 : Form
    {
        SQLite sqlite = new SQLite();
        string model;
        public Form5(string model_car)
        {
            InitializeComponent();
            model = model_car;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dataGridView1.DataSource = sqlite.getCarsByModel(model);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    string dominio = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    DataRow result = sqlite.getAuto(dominio);
                    DataRow row = result;
                    int id_value = Convert.ToInt32(row["id"]);
                    string dominio_value = row.Field<string>("dominio");
                    string modelo_value = row.Field<string>("modelo");
                    string propietario_value = row.Field<string>("propietario");
                    string telefono_value = row.Field<string>("telefono");
                    string email_value = row.Field<string>("email");
                    int periodo_value = Convert.ToInt32(row["periodo"]);

                    Auto auto = new Auto();
                    auto.id = id_value;
                    auto.dominio = dominio_value;
                    auto.modelo = modelo_value;
                    auto.propietario = propietario_value;
                    auto.telefono = telefono_value;
                    auto.email = email_value;
                    auto.periodo = periodo_value;

                    Form2 frm = new Form2(auto);
                    frm.ShowDialog();
                }
            }
        }
    }
}
