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
        double tiempoinicial;
        double tiempofinal;
        double frecuenciamuestreo;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Graficar_Click(object sender, RoutedEventArgs e)
        {
            /*
            double amplitud = double.Parse(txtAmplitud.Text);
            double fase = double.Parse(txtFase.Text);
            double frecuencia = double.Parse(txtFrecuencia.Text);
            */

            /*
            senoidal = new SeñalSenoidal(amplitud, fase, frecuencia);
            */
            Señal señal;
            switch(CbTipoSeñal.SelectedIndex)
            {
                case 0: // Parabolica bolica
                    señal = new SeñalParabolica();
       
                    break;
                case 1: //Senoidal
                    double amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);
                    double fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                    double frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
                    señal = new SeñalSenoidal(amplitud, fase, frecuencia);
       
                    break;
                case 2:
                    double alpha = double.Parse(((ConfiguracionSeñalExponencial)(panelConfiguracion.Children[0])).txtAlpha.Text);
                    señal = new SeñalExponencial(alpha);
                    break;
                case 3:
                    string rutaArchivo = ((ControlAudio)(panelConfiguracion.Children[0])).txtRutaArchivo.Text;
                    señal = new SeñalAudio(rutaArchivo);
                    txtTiempo_Inicial.Text = señal.TiempoInicial.ToString();
                    txtTiempo_Final.Text = señal.TiempoInicial.ToString();
                    txtFrecuenciaMuestreo.Text = señal.FrecuenciaMuestreo.ToString();
                    break;
                default:
                    señal = null;
                    break;
            }

            if (CbTipoSeñal.SelectedIndex != 3 && señal != null)
            {
                señal.TiempoInicial = tiempoinicial;

                señal.TiempoFinal = tiempofinal;

                señal.FrecuenciaMuestreo = frecuenciamuestreo;
            }


            señal.construirSeñal();


            /*
            SeñalSigno signo = new SeñalSigno();
            */
         
            double amplitudMaxima = señal.AmplitudMaxima;
            plnGrafica.Points.Clear();
        
           
            foreach(Muestra muestra1 in señal.Muestras)
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

        private void CbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracion.Children.Clear();
            switch(CbTipoSeñal.SelectedIndex)
            {
                case 0:

                    break;
                case 1:
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalSenoidal());
                    break;
                case 2:
                    panelConfiguracion.Children.Add(new ConfiguracionSeñalExponencial());
                    break;
                case 3:
                    panelConfiguracion.Children.Add(new ControlAudio());
                    break;

            }
        }
    }
}
