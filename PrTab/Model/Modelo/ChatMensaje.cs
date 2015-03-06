using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class ChatMensaje
    {
        public string message { get; set; }
        public string room { get; set; }
        public string name { get; set; }
        public int userID { get; set; }


        public ChatMensaje(string message, string room, string name, int userID)
        {
            this.message = message;
            this.room = room;
            this.name = name;
            this.userID = userID;
        }

        public ChatMensaje()
        {        }
    }
}
