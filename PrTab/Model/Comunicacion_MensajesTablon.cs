using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    public class Comunicacion_MensajesTablon
    {
        public event EventHandler<MensajesTablonEventArgs> getMensajesTablonCompletado;

        public void getMensajesTablon()
        {
            IList<MensajeTablon> mensajes =  new List<MensajeTablon>();
            for (int i = 0; i < 20; i++)
                mensajes.Add(new MensajeTablon { nombre = "David" + i.ToString(), mensaje = "Hola" + i.ToString(), foto = "fotos/foto.jpg" });
            if(getMensajesTablonCompletado != null)
            {
                getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mensajes));
            }
        }
    }
}
