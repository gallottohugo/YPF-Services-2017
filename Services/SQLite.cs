using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Data;




namespace Services
{
    public class SQLite
    {
        SQLiteConnection connection;

        public void openConnection()
        {
            connection = new SQLiteConnection("Data Source= C:\\services\\servicesDB.s3db;Version=3");
            connection.Open();
        }
        public void closeConnection()
        {
            connection.Close();
        }
        public void createDataBase()
        {
            try
            {
                this.openConnection();
                String command_autos = "Create Table autos (id INTEGER PRIMARY KEY AUTOINCREMENT,dominio VARCHAR,modelo VARCHAR,propietario VARCHAR,telefono string,email VARCHAR,periodo Integer,llamada BOOLEAN,llamada_fecha DATE)";
                String command_auto_details = "Create Table auto_details (id INTEGER PRIMARY KEY AUTOINCREMENT,lubricante VARCHAR, litros VARCHAR,filtro_aceite VARCHAR, filtro_aire VARCHAR, filtro_aire_interno VARCHAR, filtro_combustible VARCHAR, lubricante_caja VARCHAR, lubricante_diferencial VARCHAR,auto_id INTEGER,FOREIGN KEY(auto_id) REFERENCES autos(id))";
                String command_services = "Create Table services (id INTEGER PRIMARY KEY AUTOINCREMENT,km integer, fecha DATE, responsable VARCHAR(30), notas VARCHAR, auto_id INTEGER,FOREIGN KEY(auto_id) REFERENCES autos(id))";

                //SQLITE COMMAND SIRVE PARA EJECUTAR UNA CONEXION A LA BASE DE DAATOS.
                //RECIBE DOS PARAMETROS, LA CONEXION Y LA CONSUTA
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command_autos, connection);
                SQLiteCommand sQLiteCommand1 = new SQLiteCommand(command_auto_details, connection);
                SQLiteCommand sQLiteCommand2 = new SQLiteCommand(command_services, connection);

                int error = sQLiteCommand.ExecuteNonQuery();
                int error1 = sQLiteCommand1.ExecuteNonQuery();
                int error2 = sQLiteCommand2.ExecuteNonQuery();
                if (error < 0 || error1 < 0 || error2 < 0)
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al crear las tablas");

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public void insertAuto(string dominio, string modelo, string propietario, string telefono, string email, int periodo)
        {
            try
            {
                this.openConnection();
                string command = "Insert into autos (dominio, modelo, propietario, telefono, email, periodo) values (?,?,?,?,?,?)";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);

                sQLiteCommand.Parameters.Add(new SQLiteParameter("dominio", dominio));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("modelo", modelo));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("propietario", propietario));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("telefono", telefono));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("email", email));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("periodo", periodo));


                int error = sQLiteCommand.ExecuteNonQuery();
                if (error < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al grabar los datos");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("se grabo correctamente");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }

            this.closeConnection();
        }
        public void updateAuto(int id, string dominio, string modelo, string propietario, string telefono, string email, int periodo)
        {
            try
            {
                this.openConnection();
                string command = "Update autos set dominio = ?, modelo = ?, propietario = ?, telefono = ?, email = ?, periodo = ? where id = ?";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);

                sQLiteCommand.Parameters.Add(new SQLiteParameter("dominio", dominio));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("modelo", modelo));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("propietario", propietario));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("telefono", telefono));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("email", email));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("periodo", periodo));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("id", id));


                int error = sQLiteCommand.ExecuteNonQuery();
                if (error < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al grabar los datos");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("se grabo correctamente");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }

            this.closeConnection();
        }
        public DataTable getAutos()
        {
            DataTable dataTable = new DataTable();
            
            try
            {
                this.openConnection();
                string command = "Select * from autos";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataTable;
        }
        public DataRow getDetail(int auto_id)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;
            try
            {
                this.openConnection();
                string command = "Select * from auto_details where auto_id = " + auto_id.ToString();

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);


                if (dataTable.Rows.Count > 0)
                {
                    string query = "auto_id = " + auto_id;
                    DataRow[] result = dataTable.Select(query);
                    dataRow = result.First();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataRow;
        }
        public DataRow getAuto(string dominio)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow = null;
            try
            {
                this.openConnection();
                string command = "Select * from autos where dominio = '" + dominio + "'";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    string query = "dominio = '" + dominio + "'";
                    DataRow[] result = dataTable.Select(query);
                    dataRow = result.First();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataRow;
        }
        public DataTable getServices(int auto_id)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.openConnection();
                string command = "Select fecha, km, responsable, notas, id from services where auto_id = " + auto_id.ToString() + " order by fecha";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataTable;
        }
        public void insertAutoDetail(string lubricante, string litros, string filtro_aceite, string filtro_aire, string filtro_aire_interno, string filtro_combustible, string lubricante_caja, string lubricante_diferencial, int auto_id)
        {
            try
            {
                this.openConnection();
                string command = "Insert into auto_details (lubricante, litros, filtro_aceite, filtro_aire, filtro_aire_interno, filtro_combustible, lubricante_caja, lubricante_diferencial, auto_id) values (?,?,?,?,?,?,?,?,?)";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);
                
                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante", lubricante));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("litros", litros));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aceite", filtro_aceite));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aire", filtro_aire));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aire_interno", filtro_aire_interno));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_combustible", filtro_combustible));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante_caja", lubricante_caja));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante_diferencial", lubricante_diferencial));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("auto_id", auto_id));

                int error = sQLiteCommand.ExecuteNonQuery();
                if (error < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al grabar los datos");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("se grabo correctamente");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }

            this.closeConnection();
        }
        public void updateAutoDetail(int id, string lubricante, string litros, string filtro_aceite, string filtro_aire, string filtro_aire_interno, string filtro_combustible, string lubricante_caja, string lubricante_diferencial, int auto_id)
        {
            try
            {
                this.openConnection();
                string command = "update auto_details set lubricante = ?, litros = ?, filtro_aceite = ?, filtro_aire = ?, filtro_aire_interno = ?, filtro_combustible = ?, lubricante_caja = ?, lubricante_diferencial = ?, auto_id = ? where id = ?";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);

                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante", lubricante));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("litros", litros));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aceite", filtro_aceite));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aire", filtro_aire));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_aire_interno", filtro_aire_interno));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("filtro_combustible", filtro_combustible));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante_caja", lubricante_caja));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("lubricante_diferencial", lubricante_diferencial));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("auto_id", auto_id));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("id", id));

                int error = sQLiteCommand.ExecuteNonQuery();
                if (error < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al grabar los datos");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("se grabo correctamente");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
        }
        public void insertService(int km, DateTime date, string responsable, string notas, int auto_id)
        {
            try
            {
                this.openConnection();
                string command = "Insert into services (km, fecha, responsable, notas, auto_id) values (?,?,?,?,?)";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);

                sQLiteCommand.Parameters.Add(new SQLiteParameter("km", km));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("fecha", date));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("responsable", responsable));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("notas", notas));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("auto_id", auto_id));

                int error = sQLiteCommand.ExecuteNonQuery();

                Auto auto = getAutoById(auto_id);
                if (auto.llamamda_fecha != null)
                {
                    if (date > auto.llamamda_fecha)
                    {
                        updateAutoLlamada(auto.id, false, DateTime.Today);
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
        }
        public DataTable getClients()
        {
            DataTable dataTable = new DataTable();
            try
            {

                /*
                Listado de servis. 
                
                En ese mismo reporte mostrar el campo "llamada" con la posibilidad de cambiar el 
                valor, si esta en true, mostrar la fecha de llamada.
                
                Ordenar por "SERVI - fecha DESC" con los "AUTO - llamada" debajo de todo. 
                
                Mostrar los datos del usuario y servi en el reporte con la posibilidad de exportar todos los mails a un excel.

                Cuando el periodo de mes supera la cantidad de meses desde el último servi al dia de 
                la fecha llamar. 
                */

                this.openConnection();
                string command = "Select a.dominio, a.periodo, a.propietario, a.telefono, a.email, a.llamada_fecha, a.llamada, MAX(s.fecha) as 'last_service', s.km from autos a INNER JOIN services s ON s.auto_id = a.id group by a.id"; 

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataTable;
        }
        public DataTable getMails()
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.openConnection();
                string command = "Select email from autos";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataTable;
        }
        public void updateAutoLlamada(int id_auto, bool llamda, DateTime llamada_fecha)
        {
            try
            {
                this.openConnection();

                DateTime dateToday = DateTime.Today;
                string command = "Update autos set llamada = ?, llamada_fecha = ? where id = ?";
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);

                sQLiteCommand.Parameters.Add(new SQLiteParameter("llamada", llamda));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("llamada_fecha", llamada_fecha));
                sQLiteCommand.Parameters.Add(new SQLiteParameter("id", id_auto));

                int error = sQLiteCommand.ExecuteNonQuery();
                if (error < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Ocurrio un error al grabar los datos");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("se grabo correctamente");
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public DataTable getCarsByModel(string model)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.openConnection();
                string command = "Select * from autos where modelo like '%" + model + "%'";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return dataTable;
        }
        public void eliminarAuto(int auto_id)
        {
            try
            {
                this.openConnection();
                String command_autos = "Delete from autos where id = " + auto_id.ToString();
                String command_auto_details = "Delete from auto_details where auto_id = " + auto_id.ToString();
                String command_services = "Delete from services where auto_id = ?" + auto_id.ToString();

                SQLiteCommand sQLiteCommand = new SQLiteCommand(command_autos, connection);
                SQLiteCommand sQLiteCommand1 = new SQLiteCommand(command_auto_details, connection);
                SQLiteCommand sQLiteCommand2 = new SQLiteCommand(command_services, connection);

                int error = sQLiteCommand.ExecuteNonQuery();
                int error1 = sQLiteCommand1.ExecuteNonQuery();
                int error2 = sQLiteCommand2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public void eliminarService(int service_id)
        {
            try
            {
                this.openConnection();
                String command= "Delete from services where id = " + service_id.ToString();
                SQLiteCommand sQLiteCommand = new SQLiteCommand(command, connection);
                int error = sQLiteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public Auto getAutoById(int id)
        {
            DataTable dataTable = new DataTable();
            Auto newAuto = new Auto();
            DataRow row = null;

            try
            {
                this.openConnection();
                string command = "Select * from autos where id = '" + id.ToString() + "'";

                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(command, connection);
                sQLiteDataAdapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    string query = "id = " + id.ToString();
                    DataRow[] result = dataTable.Select(query);
                    row = result.First();


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
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                System.Windows.Forms.MessageBox.Show("entro al catch");
            }
            this.closeConnection();
            return newAuto;
        }
    }
}
