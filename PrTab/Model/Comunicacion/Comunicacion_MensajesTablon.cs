using Newtonsoft.Json.Linq;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using PrTab.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrTab.Model.Comunicacion
{
    /**
     * Clase encargada de realizar la peticion al servidor de los mensajes del tablon.
     * 
     * */
    public class Comunicacion_MensajesTablon
    {
        //Evento que lanza cuando tiene todos los mensajes disponibles.
        public event EventHandler<MensajesTablonEventArgs> getMensajesTablonCompletado;

        public event EventHandler<MensajesTablonServerEventArgs> getMensajesTablonServerCompletado;

        //public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "MensajesTablon.sqlite"));
        //private SQLiteConnection dbConn;
        private CDB_MensajeTablon  dataBase = new CDB_MensajeTablon();

        //Metodo por el cual se obtienen los mensajes del servidro
        //TODO AHORA MISMO ESTO NO FUNCIONA CON EL SERVIDOR.
        public void getMensajesTablon()
        {

            /// Create the database connection.
            //dbConn = new SQLiteConnection(DB_PATH);
            /// Create the table Task, if it doesn't exist.
            //dbConn.CreateTable<MensajeTablon>();

            //var mess = dbConn.Table<MensajeTablon>().Where(c => c.identificador != null).ToList();

            //var mess = dbConn.Query<MensajeTablon>("select * from MensajeTablon order by identificador DESC;");

            var mess = dataBase.getAllOrderByIDDesc();

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

        public async Task<bool> getMensajesTablonFromServer( string idFacultad)
        {
            //dbConn = new SQLiteConnection(DB_PATH);
            //var idMensaje = dbConn.Query<MensajeTablon>("select MAX(identificador) as identificador from MensajeTablon where identificadorTablon = "+ idFacultad + ";");

            List<MensajeTablon> mensajesNuevos = new List<MensajeTablon>();
            //int idemax = idMensaje[0].identificador;
            var asd = dataBase.getMAXIdFormMensajeTablon(idFacultad);
            string response = await Comunicacion.getMensajes(AplicationSettings.getToken(),dataBase.getMAXIdFormMensajeTablon(idFacultad)+"",idFacultad);
            JObject json = JObject.Parse(response);
            if ((string)json.SelectToken("error") == "200")
            {
                List<int> idUsuariosDistintos = new List<int>();

                JArray jArray = (JArray)json["data"];
                foreach(var mensaje in jArray)
                {
                    JObject userInfo = (JObject)mensaje.SelectToken("usuario");
                    string asdasdasdsa = (string)userInfo.SelectToken("image");
                    int idUsuarioActual = Convert.ToInt32((string)userInfo.SelectToken("pk"));
                    mensajesNuevos.Add(new MensajeTablon(Convert.ToInt32((string)mensaje.SelectToken("pk")),
                        idUsuarioActual,
                        (string)userInfo.SelectToken("username"),
                        (string)mensaje.SelectToken("texto"),
                        Comunicacion.baseURL + Comunicacion.imagenesPerfil + "/" + (string)userInfo.SelectToken("image"),
                        Convert.ToInt32((string)mensaje.SelectToken("fecha_creacion")),
                        Convert.ToInt32(idFacultad)));
                    if (idUsuariosDistintos.Contains(idUsuarioActual))
                    {
                        idUsuariosDistintos.Add(idUsuarioActual);

                    }

                    
                }

                //Dar la vuelta al array que recibes para mostrarlos en orden
                /*List<MensajeTablon> mensajesNuevosVuelta = new List<MensajeTablon>();
                for (int i = mensajesNuevos.Count - 1; i >= 0; i--)
                {

                    mensajesNuevosVuelta.Add(mensajesNuevos[i]);
                }*/


                //dbConn.InsertAll(mensajesNuevos);
                dataBase.insertAll(mensajesNuevos);

                if (getMensajesTablonServerCompletado != null)
                {
                    getMensajesTablonServerCompletado(this, new MensajesTablonServerEventArgs(mensajesNuevos));
                }
                return true;
            }
            return false;
            
        }

        public async Task<bool> postMensajeTablon (string mensaje, string idFacultad)
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
                        Comunicacion.baseURL + Comunicacion.imagenesPerfil + "/" + "pic_image_" + (string)mensajeJson.SelectToken("usuario").SelectToken("pk") + ".jpg",
                        Convert.ToInt32((string)mensajeJson.SelectToken("fecha_creacion")), 
                        Convert.ToInt32(idFacultad)));

                //dbConn = new SQLiteConnection(DB_PATH);
                //dbConn.InsertAll(mensajesNuevos);
                dataBase.insertAll(mensajesNuevos);

                if (getMensajesTablonServerCompletado != null)
                {
                    getMensajesTablonServerCompletado(this, new MensajesTablonServerEventArgs(mensajesNuevos));
                }

                return true;
            }
            return false;
        }
    }
}
