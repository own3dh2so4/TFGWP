using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    /**
     * Clase que reprensenta los mensajes que se publican en el tablon.
     * 
     * 
     * */
    public class MensajeTablon
    {
        //Nombre del usuario que puso el mensaje.
        public string nombre { get; set; }
        //Mensaje del usuario.
        public string mensaje { get; set; }
        //String con la direcion local de donde se ha guardado la foto del usuario.
        public string foto { get; set; }
    }
}
