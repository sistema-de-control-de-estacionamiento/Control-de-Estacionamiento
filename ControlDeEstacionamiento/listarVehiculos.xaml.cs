using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Agregando los namespaces necesarios para SQL Server
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ControlDeEstacionamiento
{
    /// <summary>
    /// Lógica de interacción para listarVehiculos.xaml
    /// </summary>
    public partial class listarVehiculos : UserControl
    {

        SqlConnection sqlconnection;
        SqlConnection sqlconnection2 = new SqlConnection(@"server = (local)\sqlexpress;
            integrated security = true; database = Estacionamiento");
        string placaSeleccionada;
        string estado;
        DateTime fechaAnt;
        double cobro;
        string tipoCarro;

        public listarVehiculos()
        {
            
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ControlDeEstacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
            MostrarVehiculos();
        }

        private void MostrarVehiculos()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "SELECT        Vehiculo.VehiculoIngresado.numeroPlaca as Numero_Placa, Vehiculo.VehiculoIngresado.idTipoVehiculo, " +
                    "Vehiculo.TipoVehiculo.tipo as Tipo_Vehiculo, Vehiculo.VehiculoIngresado.estado as Estacionado , " +
                    "Vehiculo.VehiculoIngresado.descripcion as Descripcion FROM Vehiculo.TipoVehiculo INNER JOIN " +
                    "Vehiculo.VehiculoIngresado ON Vehiculo.TipoVehiculo.Id = Vehiculo.VehiculoIngresado.idTipoVehiculo";

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaVehiculo = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaVehiculo);

                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    lstVehiculos.ItemsSource = tablaVehiculo.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalida_Click(object sender, RoutedEventArgs e)
        {

            DateTime fecha = DateTime.Now;
           
            if (estado != "1")
            {

                try
                {
                    
                    SqlCommand Comando = new SqlCommand(@"SELECT horaDeIngreso FROM [Vehiculo].[Detalle] WHERE numeroPlacaVehiculo = @num and horaSalida is null", sqlconnection2);
                    Comando.Parameters.Add("@num", SqlDbType.NVarChar);
                    Comando.Parameters["@num"].Value = placaSeleccionada;

                    sqlconnection2.Open();
                    fechaAnt = Convert.ToDateTime(Comando.ExecuteScalar());
                    
                    MessageBox.Show(fechaAnt.ToString());

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection2.Close();
                }
                 
                // Traer el tipo de vehiculo
                try
                {

                    SqlCommand Comando = new SqlCommand(@"SELECT Vehiculo.TipoVehiculo.tipo FROM Vehiculo.VehiculoIngresado INNER JOIN Vehiculo.TipoVehiculo ON 
                        Vehiculo.VehiculoIngresado.idTipoVehiculo = Vehiculo.TipoVehiculo.Id where Vehiculo.VehiculoIngresado.numeroPlaca = @numr", sqlconnection2);
                    Comando.Parameters.Add("@numr", SqlDbType.NVarChar);
                    Comando.Parameters["@numr"].Value = placaSeleccionada;

                    sqlconnection2.Open();
                    tipoCarro = Comando.ExecuteScalar().ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection2.Close();
                }

                var horas = (DateTime.Now - fechaAnt).TotalHours;

                if(horas < 1)
                {
                    cobro = 20;
                }else if(horas > 1 && horas <= 2)
                {
                    cobro = 30;
                }
                else if (horas > 2 && horas <= 4)
                {
                    if(horas > 2 && horas <= 3)
                    {
                        cobro = 60;
                    }
                    else
                    {
                        cobro = 70;
                    }
                }
                else if (horas > 4)
                {
                    cobro = horas * 15;
                }

                if(tipoCarro == "turismo" || tipoCarro == "Pick-up" || tipoCarro == "Camioneta")
                {
                    cobro = cobro;
                }
                else if(tipoCarro == "Camión" || tipoCarro == "Bus" || tipoCarro == "Rastra")
                {
                    cobro = cobro * 2;
                }
                else if (tipoCarro == "Motocicleta")
                {
                    cobro = cobro / 2;
                }

                
                try
                {
                    string query = @"UPDATE [Vehiculo].[Detalle] set horaSalida = @horaSalida, totalCobro = @TotalCobro WHERE horaSalida is null  AND numeroPlacaVehiculo = @numero";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection2);

                    sqlconnection2.Open();

                    sqlCommand.Parameters.AddWithValue("@horaSalida", fecha);
                    sqlCommand.Parameters.AddWithValue("@TotalCobro", cobro);
                    sqlCommand.Parameters.AddWithValue("@numero", placaSeleccionada);



                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection2.Close();
                    MostrarVehiculos();
                }

                // Actualizar tabla de vehiculo
                try
                {
                    string query = "UPDATE Vehiculo.VehiculoIngresado SET estado = 1 WHERE numeroPlaca = @numero";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    sqlconnection.Open();

                    sqlCommand.Parameters.AddWithValue("@numero", placaSeleccionada);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    // Actualizar tabla de vehiculo
                    try
                    {
                        string query = "UPDATE Vehiculo.VehiculoIngresado SET estado = 0 WHERE numeroPlaca = @numero";

                        SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                        sqlconnection.Open();

                        sqlCommand.Parameters.AddWithValue("@numero", placaSeleccionada);
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        sqlconnection.Close();
                        MostrarVehiculos();
                        MessageBox.Show("El numero de horas fue: " + horas.ToString() + "\n El total a pagar es: " + cobro.ToString());
                    }
                }
            }
        }

        private void BtnIngreso_Click(object sender, RoutedEventArgs e)
        {

            DateTime fecha = DateTime.Now;

            if (estado != "0")
            {
                
                try
                {
                    string query = @"INSERT INTO [Vehiculo].[Detalle]([horaDeIngreso],[numeroPlacaVehiculo]) VALUES (@horaDeIngreso, @numeroPlacaVehiculo)";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection2);

                    sqlconnection2.Open();

                    sqlCommand.Parameters.AddWithValue("@horaDeIngreso", fecha);
                    sqlCommand.Parameters.AddWithValue("@numeroPlacaVehiculo", placaSeleccionada);



                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection2.Close();
                }

                // Actualizar tabla de vehiculo
                try
                {
                    string query = "UPDATE Vehiculo.VehiculoIngresado SET estado = 1 WHERE numeroPlaca = @numero";

                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    sqlconnection.Open();

                    sqlCommand.Parameters.AddWithValue("@numero", placaSeleccionada);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    MostrarVehiculos();
                }
            }

        }

        private void LstVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                placaSeleccionada = row_selected["Numero_Placa"].ToString();
                estado = row_selected["Estacionado"].ToString();
            }
        }
    }
}
