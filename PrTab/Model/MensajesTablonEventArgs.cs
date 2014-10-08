using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    public class MensajesTablonEventArgs :EventArgs
    {
        public IList<MensajeTablon> mensajes { get; set; }
        public MensajesTablonEventArgs(IList<MensajeTablon> listaMensajes)
        {
            this.mensajes = listaMensajes;
        }
    }
}
