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
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : UserControl
    {
        string totalVehiculos;
        string totalCobro;
        SqlConnection sqlconnection;
        SqlConnection sqlconnection2 = new SqlConnection(@"server = (local)\sqlexpress;
            integrated security = true; database = Estacionamiento");
        public Reporte()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["ControlDeEstacionamiento.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
        }

        private void MostrarReporte()
        {
            try
            {
                // El query ha realizar en la BD
                string query = "Select * from Estacionamiento.Vehiculo.Detalle where MONTH(horaSalida) =@mes and DAY(horaSalida)=@dia and YEAR(horaSalida)=@ano";


                SqlCommand cmd = new SqlCommand(query, sqlconnection);
                cmd.Parameters.AddWithValue("@mes", dateReporte.SelectedDate.Value.ToString("MM"));
                cmd.Parameters.AddWithValue("@dia", dateReporte.SelectedDate.Value.ToString("dd"));
                cmd.Parameters.AddWithValue("@ano", dateReporte.SelectedDate.Value.ToString("yyyy"));
                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                

                using (sqlDataAdapter)
                {
                    // Objecto en C# que refleja una tabla de una BD
                    DataTable tablaVehiculo = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaVehiculo);

                    // ¿Quién es la referencia de los datos para el ListBox (popular)
                    lstReporte.ItemsSource = tablaVehiculo.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            // Totales
            try
            {

                SqlCommand Comando = new SqlCommand(@"  Select sum(totalCobro) from Vehiculo.Detalle Where horaSalida is not null and
                                                    MONTH(horaSalida) =@mesesito and DAY(horaSalida)=@diita and YEAR(horaSalida)=@anito", sqlconnection2);
                Comando.Parameters.Add("@mesesito", SqlDbType.NVarChar);
                Comando.Parameters["@mesesito"].Value = dateReporte.SelectedDate.Value.ToString("MM");
                Comando.Parameters.Add("@diita", SqlDbType.NVarChar);
                Comando.Parameters["@diita"].Value = dateReporte.SelectedDate.Value.ToString("dd");
                Comando.Parameters.Add("@anito", SqlDbType.NVarChar);
                Comando.Parameters["@anito"].Value = dateReporte.SelectedDate.Value.ToString("yyyy");

                sqlconnection2.Open();
                totalVehiculos = Convert.ToString(Comando.ExecuteScalar());

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection2.Close();
            }

            //total cobro
            try
            {

                SqlCommand Comando = new SqlCommand(@"  Select count(numeroPlacaVehiculo) from Vehiculo.Detalle Where horaSalida is not null and
                                                    MONTH(horaSalida) =@mesesito and DAY(horaSalida)=@diita and YEAR(horaSalida)=@anito", sqlconnection2);
                Comando.Parameters.Add("@mesesito", SqlDbType.NVarChar);
                Comando.Parameters["@mesesito"].Value = dateReporte.SelectedDate.Value.ToString("MM");
                Comando.Parameters.Add("@diita", SqlDbType.NVarChar);
                Comando.Parameters["@diita"].Value = dateReporte.SelectedDate.Value.ToString("dd");
                Comando.Parameters.Add("@anito", SqlDbType.NVarChar);
                Comando.Parameters["@anito"].Value = dateReporte.SelectedDate.Value.ToString("yyyy");

                sqlconnection2.Open();
                totalCobro = Convert.ToString(Comando.ExecuteScalar());



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection2.Close();
                MessageBox.Show("El total de vehiculos fue: " + totalCobro + "\n El total Cobrado fue: " + totalVehiculos);
            }

        }

        private void DateReporte_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarReporte();
        }
    }
}
