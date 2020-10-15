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
    public partial class Form2 : Form
    {
        Auto currentAuto;
        SQLite sqlite = new SQLite();
        DataRow dataRowDetail;
        DataTable dataTbleServices;
        bool exist_auto_detail;
        int autoDetailId;
        int serviceId;

        public Form2(Auto auto)
        {
            InitializeComponent();
            currentAuto = auto;

            lblDominio.Text = currentAuto.dominio;
            txtDominio.Text = currentAuto.dominio;
            txtModelo.Text = currentAuto.modelo;
            txtPeriodo.Text = currentAuto.periodo.ToString();
            txtPropietario.Text = currentAuto.propietario;
            txtEmail.Text = currentAuto.email;
            txtTelefono.Text = currentAuto.telefono;

        }
        private void btnCarEdit_Click(object sender, EventArgs e)
        {
            txtDominio.Enabled = true;
            txtPeriodo.Enabled = true;
            txtModelo.Enabled = true;
            txtPropietario.Enabled = true;
            txtTelefono.Enabled = true;
            txtEmail.Enabled = true;
            txtPeriodo.Enabled = true;

            btnCarCancel.Enabled = true;
            btnCarSave.Enabled = true;
            btnDelete.Enabled = true;
            btnCarEdit.Enabled = false;
        }
        private void btnCarCancel_Click(object sender, EventArgs e)
        {
            lblDominio.Text = currentAuto.dominio;
            txtDominio.Text = currentAuto.dominio;
            txtModelo.Text = currentAuto.modelo;
            txtPeriodo.Text = currentAuto.periodo.ToString();
            txtPropietario.Text = currentAuto.propietario;
            txtEmail.Text = currentAuto.email;
            txtTelefono.Text = currentAuto.telefono;

            txtDominio.Enabled = false;
            txtPeriodo.Enabled = false;
            txtModelo.Enabled = false;
            txtPropietario.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtPeriodo.Enabled = false;

            btnCarCancel.Enabled = false;
            btnCarSave.Enabled = false;
            btnDelete.Enabled = false;
            btnCarEdit.Enabled = true;
        }
        private void btnCarSave_Click(object sender, EventArgs e)
        {
            int periodo;
            if (txtPeriodo.Text == string.Empty)
                periodo = 0;
            else
                periodo = Int32.Parse(txtPeriodo.Text);

            sqlite.updateAuto(currentAuto.id, txtDominio.Text, txtModelo.Text, txtPropietario.Text, txtTelefono.Text, txtEmail.Text, periodo);

            txtDominio.Enabled = false;
            txtPeriodo.Enabled = false;
            txtModelo.Enabled = false;
            txtPropietario.Enabled = false;
            txtTelefono.Enabled = false;
            txtEmail.Enabled = false;
            txtPeriodo.Enabled = false;
            btnCarCancel.Enabled = false;
            btnCarSave.Enabled = false;
            btnDelete.Enabled = false;
            btnCarEdit.Enabled = true;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //DETAILS
            this.WindowState = FormWindowState.Maximized;

            dataRowDetail = sqlite.getDetail(currentAuto.id);
            if (dataRowDetail == null)
            {
                exist_auto_detail = false;

                txtLubricante.Enabled = true;
                txtLitros.Enabled = true;
                txtFiltroAceite.Enabled = true;
                txtFiltroAire.Enabled = true;
                txtFiltroAireInterno.Enabled = true;
                txtFiltroCombustible.Enabled = true;
                txtLubricanteCaja.Enabled = true;
                txtLubricanteDiferencial.Enabled = true;

                btnDetailSave.Enabled = true;
                btnDetailCancel.Enabled = false;
                btnDetailEdit.Enabled = false;
                txtLubricante.Focus();
            }
            else
            {
                exist_auto_detail = true;

                int id = Convert.ToInt32(dataRowDetail["id"]);
                int auto_id = Convert.ToInt32(dataRowDetail["auto_id"]);
                string lubricante = dataRowDetail.Field<string>("lubricante");
                string litros = dataRowDetail.Field<string>("litros");
                string filtro_aceite = dataRowDetail.Field<string>("filtro_aceite");
                string filtro_aire = dataRowDetail.Field<string>("filtro_aire");
                string filtro_aire_interno = dataRowDetail.Field<string>("filtro_aire_interno");
                string filtro_combustible = dataRowDetail.Field<string>("filtro_combustible");
                string lubricante_caja = dataRowDetail.Field<string>("lubricante_caja");
                string lubricante_diferencial = dataRowDetail.Field<string>("lubricante_diferencial");

                autoDetailId = id;
                txtLubricante.Text = lubricante;
                txtLitros.Text = litros;
                txtFiltroAceite.Text = filtro_aceite;
                txtFiltroAire.Text = filtro_aire;
                txtFiltroAireInterno.Text = filtro_aire_interno;
                txtFiltroCombustible.Text = filtro_combustible;
                txtLubricanteCaja.Text = lubricante_caja;
                txtLubricanteDiferencial.Text = lubricante_diferencial;
            }



            //SERVICES
            dataTbleServices = sqlite.getServices(currentAuto.id);
            dataGridView1.DataSource = dataTbleServices;

            DataGridViewColumn column1 = dataGridView1.Columns[0];
            column1.Width = 90;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            column2.Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            column3.Width = 70;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            column4.Width = 450;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            column5.Visible = false;


        }
        private void btnDetailEdit_Click(object sender, EventArgs e)
        {
            txtLubricante.Enabled = true;
            txtLitros.Enabled = true;
            txtFiltroAceite.Enabled = true;
            txtFiltroAire.Enabled = true;
            txtFiltroAireInterno.Enabled = true;
            txtFiltroCombustible.Enabled = true;
            txtLubricanteCaja.Enabled = true;
            txtLubricanteDiferencial.Enabled = true;

            btnDetailCancel.Enabled = true;
            btnDetailSave.Enabled = true;
            btnDetailEdit.Enabled = false;
        }
        private void btnDetailCancel_Click(object sender, EventArgs e)
        {
            string lubricante = dataRowDetail.Field<string>("lubricante");
            string litros = dataRowDetail.Field<string>("litros");
            string filtro_aceite = dataRowDetail.Field<string>("filtro_aceite");
            string filtro_aire = dataRowDetail.Field<string>("filtro_aire");
            string filtro_aire_interno = dataRowDetail.Field<string>("filtro_aire_interno");
            string filtro_combustible = dataRowDetail.Field<string>("filtro_combustible");
            string lubricante_caja = dataRowDetail.Field<string>("lubricante_caja");
            string lubricante_diferencial = dataRowDetail.Field<string>("lubricante_diferencial");

            txtLubricante.Text = lubricante;
            txtLitros.Text = litros;
            txtFiltroAceite.Text = filtro_aceite;
            txtFiltroAire.Text = filtro_aire;
            txtFiltroAireInterno.Text = filtro_aire_interno;
            txtFiltroCombustible.Text = filtro_combustible;
            txtLubricanteCaja.Text = lubricante_caja;
            txtLubricanteDiferencial.Text = lubricante_diferencial;

            txtLubricante.Enabled = false;
            txtLitros.Enabled = false;
            txtFiltroAceite.Enabled = false;
            txtFiltroAire.Enabled = false;
            txtFiltroAireInterno.Enabled = false;
            txtFiltroCombustible.Enabled = false;
            txtLubricanteCaja.Enabled = false;
            txtLubricanteDiferencial.Enabled = false;

            btnDetailSave.Enabled = false;
            btnDetailCancel.Enabled = false;
            btnDetailEdit.Enabled = true;
        }
        private void btnDetailSave_Click(object sender, EventArgs e)
        {
            if (exist_auto_detail == true)
            {
                sqlite.updateAutoDetail(autoDetailId, txtLubricante.Text, txtLitros.Text, txtFiltroAceite.Text, txtFiltroAire.Text, txtFiltroAireInterno.Text, txtFiltroCombustible.Text, txtLubricanteCaja.Text, txtLubricanteDiferencial.Text, currentAuto.id);

                txtLubricante.Enabled = false;
                txtLitros.Enabled = false;
                txtFiltroAceite.Enabled = false;
                txtFiltroAire.Enabled = false;
                txtFiltroAireInterno.Enabled = false;
                txtFiltroCombustible.Enabled = false;
                txtLubricanteCaja.Enabled = false;
                txtLubricanteDiferencial.Enabled = false;

                btnDetailCancel.Enabled = false;
                btnDetailSave.Enabled = false;
                btnDetailEdit.Enabled = true;
            }
            else
            {
                sqlite.insertAutoDetail(txtLubricante.Text, txtLitros.Text, txtFiltroAceite.Text, txtFiltroAire.Text, txtFiltroAireInterno.Text, txtFiltroCombustible.Text, txtLubricanteCaja.Text, txtLubricanteDiferencial.Text, currentAuto.id);
                txtLubricante.Enabled = false;
                txtLitros.Enabled = false;
                txtFiltroAceite.Enabled = false;
                txtFiltroAire.Enabled = false;
                txtFiltroAireInterno.Enabled = false;
                txtFiltroCombustible.Enabled = false;
                txtLubricanteCaja.Enabled = false;
                txtLubricanteDiferencial.Enabled = false;

                btnDetailCancel.Enabled = false;
                btnDetailSave.Enabled = false;
                btnDetailEdit.Enabled = true;

                exist_auto_detail = true;


                dataRowDetail = sqlite.getDetail(currentAuto.id);
                
            }
        }
        private void btnServiceSave_Click(object sender, EventArgs e)
        {

            if (txtKm.Text == string.Empty)
            {
                MessageBox.Show("Ingrese los kilómetros");
                return;
            }

            if (txtNotas.Text == string.Empty)
            {
                MessageBox.Show("Ingrese una nota");
                return;
            }

            if (txtResponsable.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un responsable");
                return;
            }

            DateTime date = pickerDate.Value;
            int km = int.Parse(txtKm.Text);
            string notas = txtNotas.Text;
            string responsable = txtResponsable.Text;

            sqlite.insertService(km, date, responsable, notas, currentAuto.id);
            sqlite.updateAutoLlamada(currentAuto.id, false, DateTime.Today);

            dataTbleServices = null;
            dataTbleServices = sqlite.getServices(currentAuto.id);
            dataGridView1.DataSource = dataTbleServices;

            txtKm.Text = string.Empty;
            txtNotas.Text = string.Empty;
            pickerDate.Value = DateTime.Today;
            txtResponsable.Text = string.Empty;
        }
        private void txtKm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea eliminar el auto " + currentAuto.dominio, "Alerta", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                sqlite.eliminarAuto(currentAuto.id);
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
                lblDominio.Text = currentAuto.dominio;
                txtDominio.Text = currentAuto.dominio;
                txtModelo.Text = currentAuto.modelo;
                txtPeriodo.Text = currentAuto.periodo.ToString();
                txtPropietario.Text = currentAuto.propietario;
                txtEmail.Text = currentAuto.email;
                txtTelefono.Text = currentAuto.telefono;

                txtDominio.Enabled = false;
                txtPeriodo.Enabled = false;
                txtModelo.Enabled = false;
                txtPropietario.Enabled = false;
                txtTelefono.Enabled = false;
                txtEmail.Enabled = false;
                txtPeriodo.Enabled = false;

                btnCarCancel.Enabled = false;
                btnCarSave.Enabled = false;
                btnDelete.Enabled = false;
                btnCarEdit.Enabled = true;
            }
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dataGridView1.Rows[e.RowIndex].Cells[1].Value != null)
                {

                    string fecha = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string km = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string responsable = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string notas = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    serviceId = int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                    btnServiceDelete.Enabled = true;
                    button1.Enabled = true;
                    btnServiceSave.Enabled = false;

                    pickerDate.Value = DateTime.Parse(fecha);
                    txtKm.Text = km;
                    txtNotas.Text = notas;
                    txtResponsable.Text = responsable;
                }
            }
        }

        private void btnServiceDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea eliminar servicio?", "Alerta", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                sqlite.eliminarService(serviceId);
                dataTbleServices = null;
                dataTbleServices = sqlite.getServices(currentAuto.id);
                dataGridView1.DataSource = dataTbleServices;

                pickerDate.Value = DateTime.Today;
                txtKm.Text = string.Empty;
                txtNotas.Text = string.Empty;
                txtResponsable.Text = string.Empty;


                btnServiceDelete.Enabled = false;
                button1.Enabled = false;
                btnServiceSave.Enabled = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                pickerDate.Value = DateTime.Today;
                txtKm.Text = string.Empty;
                txtNotas.Text = string.Empty;
                txtResponsable.Text = string.Empty;


                btnServiceDelete.Enabled = false;
                button1.Enabled = false;
                btnServiceSave.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnServiceDelete.Enabled = false;
            button1.Enabled = false;
            btnServiceSave.Enabled = true;


            pickerDate.Value = DateTime.Today;
            txtKm.Text = string.Empty;
            txtNotas.Text = string.Empty;
            txtResponsable.Text = string.Empty;
        }
    }
}
