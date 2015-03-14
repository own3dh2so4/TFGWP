using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeServerMensaje : MensajeServer
    {
        [PrimaryKey]
        public int pk{ get; set; }
        public string message { get; set; }
        public string room { get; set; }
        public string name { get; set; }
        public int userID { get; set; }

        public MensajeServerMensaje() { }

        public MensajeServerMensaje(string message, string room, string name, int userID )
        {
            this.message = message;
            this.room = room;
            this.name = name;
            this.userID = userID;
            type = "message";
            pk = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
