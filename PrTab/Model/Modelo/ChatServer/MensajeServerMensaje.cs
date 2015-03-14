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
        public string message { get; set; }
        [PrimaryKey]
        public string room { get; set; }
        [PrimaryKey]
        public string name { get; set; }
        [PrimaryKey]
        public int userID { get; set; }

        public MensajeServerMensaje() { }

        public MensajeServerMensaje(string message, string room, string name, int userID )
        {
            this.message = message;
            this.room = room;
            this.name = name;
            this.userID = userID;
            type = "message";
        }
    }
}
