using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeMovilLogin
    {
        public string token { get; set; }
        public string room { get; set; }
        public string type{ get; set; }
        
        public MensajeMovilLogin()
        {

        }
        public MensajeMovilLogin(string token, string room)
        {
            this.token = token;
            this.room = room;
            this.type = "login";
        }
    }
}
