using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class UpdateMensajesEventArgs: EventArgs
    {

        public IList<UpdateMensajeTablon> mensajes { get; set; }

        public UpdateMensajesEventArgs(IList<UpdateMensajeTablon> listaUpdate)
        {
            this.mensajes = listaUpdate;
        }
    }
}
