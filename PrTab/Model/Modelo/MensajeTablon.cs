using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace PrTab.Model.Modelo
{
    /**
     * Clase que reprensenta los mensajes que se publican en el tablon.
     * 
     * 
     * */
    public class MensajeTablon
    {
        
        [PrimaryKey]
        public int identificador { get; set; }
        public int identificadorUsuario { get; set; }
        //Nombre del usuario que puso el mensaje.
        public string nombre { get; set; }
        //Mensaje del usuario.
        public string mensaje { get; set; }
        //String con la direcion local de donde se ha guardado la foto del usuario.
        public string foto { get; set; }
        public DateTime fecha { get; set; }
        public int identificadorTablon { get; set; }

        public MensajeTablon()
        {

        }

        public MensajeTablon(int id, int idUsuario, string nombreUsuario, string mensajeUsuario, string fotoUsuario, int fechamensaje, int idTablon)
        {
            identificador = id;
            identificadorUsuario = idUsuario;
            nombre = nombreUsuario;
            mensaje = mensajeUsuario;
            if (fotoUsuario != "")
                foto = fotoUsuario;
            else
                foto = "noimage.png";
            //Recibe fecha en formato UNIX y lo pasa a DateTime
            fecha = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            fecha = fecha.AddSeconds( fechamensaje ).ToLocalTime();
            identificadorTablon = idTablon;
        }

        public MensajeTablon(int id, int idUsuario, string nombreUsuario, string mensajeUsuario, string fotoUsuario, DateTime fechamensaje, int idTablon)
        {
            identificador = id;
            identificadorUsuario = idUsuario;
            nombre = nombreUsuario;
            mensaje = mensajeUsuario;
            if (fotoUsuario != "")
                foto = fotoUsuario;
            else
                foto = "noimage.png";
            fecha = fechamensaje;
            identificadorTablon = idTablon;
        }
    }
}
