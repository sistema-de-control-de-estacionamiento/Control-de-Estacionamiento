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
        
        private ingresarVehiculo IngresarVehiculo; 
        public MainWindow()
        {
            InitializeComponent();
            IngresarVehiculo = new ingresarVehiculo();
            

        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }


        // Declarar el UserControl

        

        private void Ingresar_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                ContenedorPrincipal.Children.Add(IngresarVehiculo);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void VentanaPrincipal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
