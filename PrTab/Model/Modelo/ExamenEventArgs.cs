using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class ExamenEventArgs : EventArgs
    {
        public List<PreguntaInterface> preguntas { get; set; }

        public ExamenEventArgs(List<PreguntaInterface> listaPreguntas)
        {
            this.preguntas = listaPreguntas;
        }
    }
}
