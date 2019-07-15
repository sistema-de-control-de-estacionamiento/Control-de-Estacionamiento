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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ControlDeEstacionamiento
{
    /// <summary>
    /// Lógica de interacción para ingresarVehiculo.xaml
    /// </summary>
    public partial class ingresarVehiculo : UserControl
    {
        SqlConnection sqlConnection = new SqlConnection(@"server = (local)\sqlexpress;
            integrated security = true; database = Estacionamiento");
        public ingresarVehiculo()
        {
            InitializeComponent();
            //cmbTipoVehiculo.ItemsSource = typeof(Colors).GetProperties();
        }


        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

            int x = 0;
            if (cmbTipoVehiculo.Text == "Turismo")
            {
                x = 1;
            }
            if (cmbTipoVehiculo.Text == "Pick-up")
            {
                x = 2;
            }
            else if(cmbTipoVehiculo.Text == "Camioneta")
            {
                x = 3;
            }
            else if (cmbTipoVehiculo.Text == "Camión")
            {
                x = 4;
            }
            else if (cmbTipoVehiculo.Text == "Bus")
            {
                x = 5;
            }
            else if (cmbTipoVehiculo.Text == "Rastra")
            {
                x = 6;
            }
            else if (cmbTipoVehiculo.Text == "Motocicleta")
            {
                x = 7;
            }

            try
            {
                string query = @"INSERT INTO Vehiculo.VehiculoIngresado
                                 VALUES (@numPlaca, @idTipoVehiculo, @estado, @descripcion)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                sqlCommand.Parameters.AddWithValue("@numPlaca", txtnumeroPlaca.Text);
                sqlCommand.Parameters.AddWithValue("@idTipoVehiculo", x);
                sqlCommand.Parameters.AddWithValue("@estado", 0);
                sqlCommand.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}
