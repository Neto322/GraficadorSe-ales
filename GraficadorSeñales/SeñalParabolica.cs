using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalParabolica
    {
        public List<Muestra> muestras { get; set; }

        public SeñalParabolica()
        {
            muestras = new List<Muestra>();
        }
        public double evaluar(double tiempo)
        {
            double resultado;
           if(tiempo >= 0)
            {
                resultado = (tiempo * tiempo) / 2.0;
            }
           else
            {
                resultado = 0;
            }
            return resultado;
        }

    }
}
