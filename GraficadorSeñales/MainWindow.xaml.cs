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
           /* double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);*/
            double tiempoinicial = double.Parse(txtTiempo_Inicial.Text);
            double tiempofinal = double.Parse(txtTiempo_Final.Text);
            double frecuenciamuestreo = double.Parse(txtFrecuenciaMuestreo.Text);
            /* senoidal = new SeñalSenoidal(amplitud, fase, frecuencia);*/
            SeñalParabolica parabolica = new SeñalParabolica();
            double periodoMuestreo = 1.0 / frecuenciamuestreo;
            double amplitudMaxima = 0.0;
            plnGrafica.Points.Clear();
        
            for (double i = tiempoinicial; i <= tiempofinal; i += periodoMuestreo)
            {
                double valorMuestra = parabolica.evaluar(i);
                if(Math.Abs(valorMuestra) >= amplitudMaxima)
                {
                    amplitudMaxima = Math.Abs(valorMuestra);
                }
                Muestra muestra = new Muestra(i, valorMuestra);

                parabolica.muestras.Add(muestra);
            }
            foreach(Muestra muestra1 in parabolica.muestras)
            {
                plnGrafica.Points.Add(adaptarCoordenadas(muestra1.X,muestra1.Y,tiempoinicial,amplitudMaxima));
            }
            lblLimiteSuperior.Text = amplitudMaxima.ToString();
            lblLimiteInferior.Text = "-" + amplitudMaxima.ToString();
            pnlEjeX.Points.Clear();
            pnlEjeX.Points.Add(adaptarCoordenadas(tiempoinicial, 0.0, tiempoinicial,amplitudMaxima));
            pnlEjeX.Points.Add(adaptarCoordenadas(tiempofinal, 0.0, tiempoinicial,amplitudMaxima));
            pnlEjeY.Points.Clear();
            pnlEjeY.Points.Add(adaptarCoordenadas(0.0,amplitudMaxima,tiempoinicial,amplitudMaxima));
            pnlEjeY.Points.Add(adaptarCoordenadas(0.0, -amplitudMaxima, tiempoinicial, amplitudMaxima));
        }
        public Point adaptarCoordenadas(double x,double y,double tiempoInicial, double amplitudMaxima)
        {
            return new Point((x - tiempoInicial) * scrGrafica.Width, (- 1 * (y * ( ( ( scrGrafica.Height / 2.0 ) ) - 25 ) / amplitudMaxima ) ) + ( scrGrafica.Height / 2f ) );
        }

  
    }
}
