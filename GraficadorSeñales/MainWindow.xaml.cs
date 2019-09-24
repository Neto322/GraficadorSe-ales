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
        double amplitud;
        double fase;
        double frecuencia;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Graficar_Click(object sender, RoutedEventArgs e)
        {

            tiempoinicial = double.Parse(txtTiempo_Inicial.Text);
            tiempofinal = double.Parse(txtTiempo_Final.Text);
            frecuenciamuestreo = double.Parse(txtFrecuenciaMuestreo.Text);


            /*
            senoidal = new SeñalSenoidal(amplitud, fase, frecuencia);
            */
            Señal señal;
            Señal señalResultante;


            switch(CbTipoSeñal.SelectedIndex)
            {
                case 0: // Parabolica bolica
                    señal = new SeñalParabolica();
       
                    break;
                case 1: //Senoidal
                     amplitud = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtAmplitud.Text);
                     fase = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFase.Text);
                     frecuencia = double.Parse(((ConfiguracionSeñalSenoidal)(panelConfiguracion.Children[0])).txtFrecuencia.Text);
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
         
            switch(cbOperacion.SelectedIndex)
            {
                case 0: // Escala De Amplitud
                    double factorDesplaze = double.Parse(((OperacionDesplazamientoAmplitud)panelConfiguracionOperacion.Children[0]).txtDesplazamientoAmplitud.Text);
                    señalResultante = Señal.desplazarAmplitud(señal, factorDesplaze); 
                    break;
                case 1:
                    double factorEscala = double.Parse(((OperacionEscalaAmplitud)panelConfiguracionOperacion.Children[0]).txtFactorEscala.Text);
                    señalResultante = Señal.escalarAmplitud(señal, factorEscala);
                    break;
                default:
                    señalResultante = null;
                    break;
            }

            double amplitudMaxima = (señal.AmplitudMaxima >= señalResultante.AmplitudMaxima) ? señal.AmplitudMaxima : señalResultante.AmplitudMaxima;

          
            plnGrafica.Points.Clear();

            plnGraficaResultante.Points.Clear();
           
            foreach(Muestra muestra1 in señal.Muestras)
            {

                plnGrafica.Points.Add(adaptarCoordenadas(muestra1.X,muestra1.Y,tiempoinicial,amplitudMaxima));

            }
            foreach(Muestra muestra in señalResultante.Muestras)
            {
                plnGraficaResultante.Points.Add(adaptarCoordenadas(muestra.X, muestra.Y, tiempoinicial, amplitudMaxima));
            }
            lblLimiteSuperior.Text = amplitudMaxima.ToString("F");
            lblLimiteInferior.Text = "-" + amplitudMaxima.ToString("F");

            lblLimiteInferiorResultado.Text = "-" + amplitudMaxima.ToString("F");
            lblLimiteSuperiorResultado.Text = amplitudMaxima.ToString("F");


            pnlEjeX.Points.Clear();
            pnlEjeX.Points.Add(adaptarCoordenadas(tiempoinicial, 0.0, tiempoinicial,amplitudMaxima));
            pnlEjeX.Points.Add(adaptarCoordenadas(tiempofinal, 0.0, tiempoinicial,amplitudMaxima));
            pnlEjeY.Points.Clear();
            pnlEjeY.Points.Add(adaptarCoordenadas(0.0,amplitudMaxima,tiempoinicial,amplitudMaxima));
            pnlEjeY.Points.Add(adaptarCoordenadas(0.0, -amplitudMaxima, tiempoinicial, amplitudMaxima));

            pnlEjeXResultante.Points.Clear();
            pnlEjeXResultante.Points.Add(adaptarCoordenadas(tiempoinicial, 0.0, tiempoinicial, amplitudMaxima));
            pnlEjeXResultante.Points.Add(adaptarCoordenadas(tiempofinal, 0.0, tiempoinicial, amplitudMaxima));

            pnlEjeYResultante.Points.Clear();
            pnlEjeYResultante.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima, tiempoinicial, amplitudMaxima));
            pnlEjeYResultante.Points.Add(adaptarCoordenadas(0.0, amplitudMaxima, tiempoinicial, amplitudMaxima));
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
                case 0:// Parabolica
                    
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

        private void CbOperacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            panelConfiguracionOperacion.Children.Clear();
            switch(cbOperacion.SelectedIndex)
            {
                case 0:
                    panelConfiguracionOperacion.Children.Add(new OperacionDesplazamientoAmplitud());
                    break;
                case 1:
                    panelConfiguracionOperacion.Children.Add(new OperacionEscalaAmplitud());
                    break;
                default:

                    break;
            }
        }
    }
}
