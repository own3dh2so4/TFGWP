using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    /**
     * Clase encargada de realizar la peticion al servidor de los mensajes del tablon.
     * 
     * */
    public class Comunicacion_MensajesTablon
    {
        //Evento que lanza cuando tiene todos los mensajes disponibles.
        public event EventHandler<MensajesTablonEventArgs> getMensajesTablonCompletado;

        

        //Metodo por el cual se obtienen los mensajes del servidro
        //TODO AHORA MISMO ESTO NO FUNCIONA CON EL SERVIDOR.
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
