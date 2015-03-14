using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeMovilRoom
    {
        public string room { get; set; }
        public string type { get; set; }

        public MensajeMovilRoom()
        {

        }

        public MensajeMovilRoom(string room)
        {
            this.room = room;
            type = "room";
        }
    }
}
