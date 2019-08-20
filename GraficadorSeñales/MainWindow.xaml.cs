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

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SeñalSenoidal senoidal;
       
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Graficar_Click(object sender, RoutedEventArgs e)
        {
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            double tiempoinicial = double.Parse(txtTiempo_Inicial.Text);
            double tiempofinal = double.Parse(txtTiempo_Final.Text);
            double frecuenciamuestreo = double.Parse(txtFrecuenciaMuestreo.Text);
            senoidal = new SeñalSenoidal(amplitud, fase, frecuencia);
            double periodoMuestreo = 1.0 / frecuenciamuestreo;
            plnGrafica.Points.Clear();
            for (double i = tiempoinicial; i<= tiempofinal;i += periodoMuestreo)
            {
                plnGrafica.Points.Add( adaptarCoordenadas(i, senoidal.evaluar(i)));
            }
        }
        public Point adaptarCoordenadas(double x,double y)
        {
            return new Point(x * scrGrafica.Width, (-1 * (y * ((scrGrafica.Height/2) -25) )) + (scrGrafica.Height/2.0) );
        }
    }
}
