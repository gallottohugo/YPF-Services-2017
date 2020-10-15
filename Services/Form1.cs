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
    public partial class Form1 : Form
    {

        SQLite sqlite = new SQLite();
        DataTable dataTableAutos = new DataTable();

        public Form1()
        {
            InitializeComponent();
            dataTableAutos = sqlite.getAutos();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un dominio");
                txtSearch.Focus();
            }
            else
            {
                
                DataRow result = sqlite.getAuto(txtSearch.Text);

                if (result == null)
                {
                    MessageBox.Show("No exite el vehiculo");
                    txtSearch.Text = string.Empty;
                    txtSearch.Focus();
                }
                else
                {
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

                    txtSearch.Text = string.Empty;
                    Form2 frm = new Form2(auto);
                    frm.ShowDialog();
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDominio.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un dominio");
                return;
            }

            if (txtPeriodo.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un periodo");
                txtDominio.Focus();
                return;
            }

            string dominio = txtDominio.Text;
            string query = "dominio = '" + dominio + "'";
            DataRow[] result = dataTableAutos.Select(query);

            if (result.Count() > 0)
            {
                MessageBox.Show("El automovil ya existe");
                txtSearch.Text = string.Empty;
                txtSearch.Focus();
            }
            else
            {
                string modelo = txtModelo.Text;
                string propietario = txtPropietario.Text;
                string email = txtEmail.Text;
                string telefono = txtTelefono.Text;

                int periodo;
                if (txtPeriodo.Text == string.Empty)
                    periodo = 0;
                else
                    periodo = int.Parse(txtPeriodo.Text);

                sqlite.insertAuto(dominio, modelo, propietario, telefono, email, periodo);
            }

            blockNewCar();
          
            dataTableAutos = null;
            dataTableAutos = sqlite.getAutos();

            btnNew.Focus();

        }

        public void blockNewCar()
        {
            txtDominio.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPropietario.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPeriodo.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtSearchModelo.Text = string.Empty;


            txtDominio.Enabled = false;
            txtModelo.Enabled = false;
            txtPropietario.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtPeriodo.Enabled = false;
            btnSave.Enabled = false;
            btnCancelar.Enabled = false;

            txtSearch.Enabled = true;
            btnSearch.Enabled = true;
            btnNew.Enabled = true;
            btnSearchModelo.Enabled = true;
            txtSearchModelo.Enabled = true;
            btnClients.Enabled = true;
        }

        public void unblockNewCar()
        {
            txtDominio.Enabled = true;
            txtModelo.Enabled = true;
            txtPropietario.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            txtPeriodo.Enabled = true;
            btnSave.Enabled = true;
            btnCancelar.Enabled = true;

            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            btnNew.Enabled = false;
            btnSearchModelo.Enabled = false;
            txtSearchModelo.Enabled = false;
            btnClients.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            unblockNewCar();


            txtDominio.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            blockNewCar();
            txtSearch.Focus();
        }

        private void txtPeriodo_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnClients_Click(object sender, EventArgs e)
        {

            DataTable newDataTable = new DataTable();
            DataColumn column1;

            column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "dominio";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "propietario";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = Type.GetType("System.Int32");
            column1.ColumnName = "periodo";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "telefono";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = Type.GetType("System.String");
            column1.ColumnName = "email";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = Type.GetType("System.String");
            column1.ColumnName = "last_service";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = Type.GetType("System.Int32");
            column1.ColumnName = "km";
            column1.ReadOnly = true;
            newDataTable.Columns.Add(column1);


            column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "llamada_fecha";
            newDataTable.Columns.Add(column1);

            column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.Boolean");
            column1.ColumnName = "llamada";
            newDataTable.Columns.Add(column1);

            DataTable allClients = sqlite.getClients();
            foreach (DataRow row in allClients.Rows)
            {
                string dominio = row.Field<string>("dominio");
                int periodo = Convert.ToInt32(row["periodo"]);
                string propietario = row.Field<string>("propietario");
                string telefono  = row.Field<string>("telefono");
                string email = row.Field<string>("email");
                DateTime llamada_fecha = Convert.ToDateTime(row["llamada_fecha"]);
                bool llamada = Convert.ToBoolean(row["llamada"]);
                DateTime last_service = Convert.ToDateTime(row["last_service"]);  
                int km = Convert.ToInt32(row["km"]);


                DateTime newDate = last_service.AddMonths(periodo);
                if (newDate < DateTime.Today)
                {
                    DataRow newRow = newDataTable.NewRow();
                    newRow["dominio"] = dominio;
                    newRow["propietario"] = propietario;
                    newRow["periodo"] = periodo;
                    newRow["telefono"] = telefono;
                    newRow["email"] = email;
                    newRow["last_service"] = last_service.ToString();
                    newRow["km"] = km;
                    newRow["llamada_fecha"] = llamada_fecha.ToString();
                    newRow["llamada"] = llamada;
                    newDataTable.Rows.Add(newRow);
                }
            }

            Form3 frm = new Form3(newDataTable);
            frm.ShowDialog();
        }

        private void btnSearchModelo_Click(object sender, EventArgs e)
        {
            if (txtSearchModelo.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un modelo");
                txtSearchModelo.Focus();
                return;    
            }

            Form5 frm = new Form5(txtSearchModelo.Text);
            frm.ShowDialog();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }
    }
}
