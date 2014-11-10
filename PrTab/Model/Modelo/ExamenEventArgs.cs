using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class ExamenEventArgs : EventArgs
    {
        public List<Pregunta> preguntas { get; set; }

        public ExamenEventArgs(List<Pregunta> listaPreguntas)
        {
            this.preguntas = listaPreguntas;
        }
    }
}
