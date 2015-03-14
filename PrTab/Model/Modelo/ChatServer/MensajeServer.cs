using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeServer
    {
        public string type { get; set; }

        public MensajeServer() { }

        public MensajeServer(string tipo)
        {
            type = tipo;
        }
    }
}
