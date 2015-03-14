using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo.ChatServer
{
    class MensajeServerError:MensajeServer
    {
        public string error_msg { get; set; }
        public MensajeServerError() { }
        

        public MensajeServerError(string error)
        {
            error_msg = error;
            this.type = "error";
        }
    }
}
