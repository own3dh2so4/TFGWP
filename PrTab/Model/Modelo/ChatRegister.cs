using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class ChatRegister
    {
        public string token { get; set; }
        public string room { get; set; }

        public ChatRegister(string token, string room)
        {
            this.token = token;
            this.room = room;
        }

    }
}
