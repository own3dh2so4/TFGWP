using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Comunicacion
{
    class Comunicacion_Examen
    {
        public event EventHandler<ExamenEventArgs> getExanenCompletado;

        //Aqui creare el metodo para pedir examen y cuando este lanzare el evento.
    }
}
