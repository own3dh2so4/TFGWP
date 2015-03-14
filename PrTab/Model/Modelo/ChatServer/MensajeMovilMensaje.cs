using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeMovilMensaje
    {
        public string message { get; set; }
        public string room { get; set; }
        public string type { get; set; }
        

        public MensajeMovilMensaje()
        {

        }

        public MensajeMovilMensaje(string mensaje, string room)
        {
            this.message = mensaje;
            this.room = room;
            this.type = "message";

        }

    }
}
