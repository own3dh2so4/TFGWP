using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeServerStatus : MensajeServer
    {

        public string status { get; set; }

        public MensajeServerStatus()
        {

        }

        public MensajeServerStatus(string status)
        {
            type = "status";
            this.status = status;
        }
    }
}
