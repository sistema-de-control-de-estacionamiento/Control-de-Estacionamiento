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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        // Declarar las ventanas hijas
        private ingresarVehiculo IngresarVehiculo;
        private listarVehiculos ListarVehiculos;
        private Reporte ReporteCobros;

        public MainWindow()
        {
            InitializeComponent();

            IngresarVehiculo = new ingresarVehiculo();
            ListarVehiculos = new listarVehiculos();
            ReporteCobros = new Reporte();

        }

        // Lograr que la ventana se pueda mover
        private void VentanaPrincipal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        // Ventana Hija para ingresar un vehículo al estacionamiento
        private void Ingresar_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ContenedorPrincipal.Children.Add(IngresarVehiculo);
                ContenedorPrincipal.Children.Remove(ListarVehiculos);
                ContenedorPrincipal.Children.Remove(ReporteCobros);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        // Ventana Hija para hacer el cobro de un veh;iculo ingresado
        private void Salida_Selected(object sender, RoutedEventArgs e)
        {
            

            try
            {
                listarVehiculos ListarVehiculos1 = new listarVehiculos();
                ContenedorPrincipal.Children.Add(ListarVehiculos1);
                ContenedorPrincipal.Children.Remove(IngresarVehiculo);
                ContenedorPrincipal.Children.Remove(ReporteCobros);

            }
            catch (Exception ex)
            { 

                MessageBox.Show(ex.ToString());
            }

        }

        private void Reporte_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ContenedorPrincipal.Children.Add(ReporteCobros);
                ContenedorPrincipal.Children.Remove(IngresarVehiculo);
                ContenedorPrincipal.Children.Remove(ListarVehiculos);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }

    
}
