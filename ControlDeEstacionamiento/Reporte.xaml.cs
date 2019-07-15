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

namespace ControlDeEstacionamiento
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : UserControl
    {
        public Reporte()
        {
            InitializeComponent();
        }

        private void MostrarReporte()
        {
            //try
            //{
            //    // El query ha realizar en la BD
            //    string query = "SELECT        Vehiculo.VehiculoIngresado.numeroPlaca as Numero_Placa, Vehiculo.VehiculoIngresado.idTipoVehiculo, " +
            //        "Vehiculo.TipoVehiculo.tipo as Tipo_Vehiculo, Vehiculo.VehiculoIngresado.estado as Estacionado , " +
            //        "Vehiculo.VehiculoIngresado.descripcion as Descripcion FROM Vehiculo.TipoVehiculo INNER JOIN " +
            //        "Vehiculo.VehiculoIngresado ON Vehiculo.TipoVehiculo.Id = Vehiculo.VehiculoIngresado.idTipoVehiculo";

            //    // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
            //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

            //    using (sqlDataAdapter)
            //    {
            //        // Objecto en C# que refleja una tabla de una BD
            //        DataTable tablaVehiculo = new DataTable();

            //        // Llenar el objeto de tipo DataTable
            //        sqlDataAdapter.Fill(tablaVehiculo);

            //        // ¿Quién es la referencia de los datos para el ListBox (popular)
            //        lstVehiculos.ItemsSource = tablaVehiculo.DefaultView;
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //}
        }

        private void DateReporte_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MostrarReporte();
        }
    }
}
