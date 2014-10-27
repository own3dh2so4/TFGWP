using Newtonsoft.Json.Linq;
using PrTab.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrTab.Model
{
    /**
     * Clase encargada de realizar la peticion al servidor de los mensajes del tablon.
     * 
     * */
    public class Comunicacion_MensajesTablon
    {
        //Evento que lanza cuando tiene todos los mensajes disponibles.
        public event EventHandler<MensajesTablonEventArgs> getMensajesTablonCompletado;

        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "MensajesTablon.sqlite"));
        private SQLiteConnection dbConn;

        //Metodo por el cual se obtienen los mensajes del servidro
        //TODO AHORA MISMO ESTO NO FUNCIONA CON EL SERVIDOR.
        public void getMensajesTablon()
        {

            /// Create the database connection.
            dbConn = new SQLiteConnection(DB_PATH);
            /// Create the table Task, if it doesn't exist.
            dbConn.CreateTable<MensajeTablon>();

            //var mess = dbConn.Table<MensajeTablon>().Where(c => c.identificador != null).ToList();

            var mess = dbConn.Query<MensajeTablon>("select * from MensajeTablon order by identificador DESC;");

            if (getMensajesTablonCompletado != null)
            {
                getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mess));
            }

            /// Create the table Task, if it doesn't exist.
            //dbConn.CreateTable<MensajeTablon>();

            /*IList<MensajeTablon> mensajes =  new List<MensajeTablon>();
            for (int i = 0; i < 20; i++)
                mensajes.Add(new MensajeTablon {identificador=i, nombre = "David" + i.ToString(), mensaje = "Hola" + i.ToString(), foto = "fotos/foto.jpg" });
            if(getMensajesTablonCompletado != null)
            {
                getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mensajes));
            }

            dbConn.InsertAll(mensajes);*/
        }

        public async void getMensajesTablonFromServer( string idFacultad)
        {
            dbConn = new SQLiteConnection(DB_PATH);
            var idMensaje = dbConn.Query<MensajeTablon>("select MAX(identificador) as identificador from MensajeTablon where identificadorTablon = "+ idFacultad + ";");

            List<MensajeTablon> mensajesNuevos = new List<MensajeTablon>();
            var idemax = idMensaje[0].identificador;
            string response = await Comunicacion.getMensajes(AplicationSettings.getToken(),idemax+"",idFacultad);
            JObject json = JObject.Parse(response);
            if ((string)json.SelectToken("error") == "200")
            {
                JArray jArray = (JArray)json["data"];
                foreach(var mensaje in jArray)
                {
                    JObject userInfo = (JObject)mensaje.SelectToken("usuario");
                    mensajesNuevos.Add(new MensajeTablon(Convert.ToInt32((string)mensaje.SelectToken("pk")),  
                        Convert.ToInt32((string)userInfo.SelectToken("pk")),
                        (string)userInfo.SelectToken("username"),
                        (string)mensaje.SelectToken("texto"),
                        "fotos/foto.jpg",
                        Convert.ToInt32((string)mensaje.SelectToken("fecha_creacion")),
                        Convert.ToInt32(idFacultad)));
                }

                //Dar la vuelta al array que recibes para mostrarlos en orden
                List<MensajeTablon> mensajesNuevosVuelta = new List<MensajeTablon>();
                for (int i = mensajesNuevos.Count - 1; i >= 0; i--)
                {

                    mensajesNuevosVuelta.Add(mensajesNuevos[i]);
                }


                dbConn.InsertAll(mensajesNuevos);

                if (getMensajesTablonCompletado != null)
                {
                    getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mensajesNuevosVuelta));
                }
            }
            
            
        }

        public async void postMensajeTablon (string mensaje, string idFacultad)
        {
            string response = await Comunicacion.postMensaje(AplicationSettings.getToken(), mensaje, idFacultad);
            JObject json = JObject.Parse(response);
            if ((string)json.SelectToken("error") == "200")
            {
                List<MensajeTablon> mensajesNuevos = new List<MensajeTablon>();
                //HAY QUE PONER EL ID DEL USUARIO CORRECTO!!!!
                JArray mensajeJArray = (JArray)json.SelectToken("data");

                foreach (var mensajeJson in mensajeJArray)
                    mensajesNuevos.Add(new MensajeTablon(Convert.ToInt32((string)mensajeJson.SelectToken("pk")),
                        Convert.ToInt32((string)mensajeJson.SelectToken("usuario").SelectToken("pk")),
                        AplicationSettings.getUsuario(),                                    
                        mensaje,
                        "fotos/foto.jpg",
                        Convert.ToInt32((string)mensajeJson.SelectToken("fecha_creacion")), 
                        Convert.ToInt32(idFacultad)));

                dbConn = new SQLiteConnection(DB_PATH);
                dbConn.InsertAll(mensajesNuevos);

                if (getMensajesTablonCompletado != null)
                {
                    getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mensajesNuevos));
                }            
            }
            
        }
    }
}
