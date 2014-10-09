using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    /**
     * Clase que implemente el evento que lanza el objeto cuando cambia para notificar a la vista 
     * de que ha cambiado, este evento lo escucha el ViewModel.
     * */
    public class MensajesTablonEventArgs :EventArgs
    {
        //Lista con los mensajes modificados.
        public IList<MensajeTablon> mensajes { get; set; }
        //Constructor de la clase.
        public MensajesTablonEventArgs(IList<MensajeTablon> listaMensajes)
        {
            this.mensajes = listaMensajes;
        }
    }
}
