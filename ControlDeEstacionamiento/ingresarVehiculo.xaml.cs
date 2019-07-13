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
        public ingresarVehiculo()
        {
            InitializeComponent();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"INSERT INTO Vehiculo.Vehiculo
                                 VALUES (@numPlaca, @descripcion)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                sqlConnection.Open();

                sqlCommand.Parameters.AddWithValue("@numPlaca", txtnumeroPlaca);
                sqlCommand.Parameters.AddWithValue("@descripcion", txtDescripcion);

                sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }

        }
    }
}
