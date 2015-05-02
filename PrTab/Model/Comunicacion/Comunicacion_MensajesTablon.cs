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

        public event EventHandler<UpdateMensajesEventArgs> updateMensajesTablonCompletado;

        //public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "MensajesTablon.sqlite"));
        //private SQLiteConnection dbConn;
        private CDB_MensajeTablon  dataBase = new CDB_MensajeTablon();

        //Metodo por el cual se obtienen los mensajes del servidro
        //TODO AHORA MISMO ESTO NO FUNCIONA CON EL SERVIDOR.
        public void getMensajesTablon()
        {

            var mess = dataBase.getAllOrderByIDDesc();

            if (getMensajesTablonCompletado != null)
            {
                getMensajesTablonCompletado(this, new MensajesTablonEventArgs(mess));
            }
        }

        public async Task<bool> getMensajesTablonFromServer( string idFacultad)
        {

            List<MensajeTablon> mensajesNuevos = new List<MensajeTablon>();
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
                    mensajesNuevos.Add(new MensajeTablon(
                        Convert.ToInt32((string)mensaje.SelectToken("pk")),
                        idUsuarioActual,
                        (string)userInfo.SelectToken("username"),
                        (string)mensaje.SelectToken("texto"),
                        Comunicacion.baseURL  +  (string)userInfo.SelectToken("image"),
                        Convert.ToInt32((string)mensaje.SelectToken("fecha_creacion")),
                        Convert.ToInt32(idFacultad),
                        Convert.ToInt32((string)mensaje.SelectToken("num_fav")),
                        Convert.ToBoolean((string)mensaje.SelectToken("user_favorited"))));
                    if (idUsuariosDistintos.Contains(idUsuarioActual))
                    {
                        idUsuariosDistintos.Add(idUsuarioActual);

                    }                   
                }

                dataBase.insertAll(mensajesNuevos);

                if (getMensajesTablonServerCompletado != null)
                {
                    getMensajesTablonServerCompletado(this, new MensajesTablonServerEventArgs(mensajesNuevos));
                }
                return true;
            }
            else if ((string)json.SelectToken("error") == "201")
            {
                if (getMensajesTablonServerCompletado != null)
                {
                    getMensajesTablonServerCompletado(this, new MensajesTablonServerEventArgs(mensajesNuevos));
                }
            }
            return false;
            
        }

        public async Task<bool> updateMensajesTablon (int idInit, int idFin, int idFaculty)
        {
            string response = await Comunicacion.updateMessages(AplicationSettings.getToken(), idInit+"", idFin+"",idFaculty+"");
            JObject json = JObject.Parse(response);
            if ((string)json.SelectToken("error") == "200")
            {
                List<UpdateMensajeTablon> listUpdate = new List<UpdateMensajeTablon>();
                JArray mensajeJArray = (JArray)json.SelectToken("data");
                foreach (var m in mensajeJArray)
                {
                    listUpdate.Add(new UpdateMensajeTablon(Convert.ToInt32((string)m.SelectToken("pk")),
                        Convert.ToInt32((string)m.SelectToken("num_fav")),
                        Convert.ToBoolean((string)m.SelectToken("user_favorited")),
                        Convert.ToBoolean((string)m.SelectToken("borrado"))));
                }

                dataBase.updateInfoMensajesTablon(listUpdate);

                if (updateMensajesTablonCompletado != null)
                {
                    updateMensajesTablonCompletado(this, new UpdateMensajesEventArgs(listUpdate));
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
                {
                        mensajesNuevos.Add(new MensajeTablon(Convert.ToInt32((string)mensajeJson.SelectToken("pk")),
                            Convert.ToInt32((string)mensajeJson.SelectToken("usuario").SelectToken("pk")),
                            (string)mensajeJson.SelectToken("usuario").SelectToken("username"),
                            mensaje,
                            Comunicacion.baseURL +  "media/users/pic_image_" + (string)mensajeJson.SelectToken("usuario").SelectToken("pk") + ".jpg",
                            Convert.ToInt32((string)mensajeJson.SelectToken("fecha_creacion")),
                            Convert.ToInt32(idFacultad),
                            0,
                            false));
                }

                //dbConn = new SQLiteConnection(DB_PATH);
                //dbConn.InsertAll(mensajesNuevos);
                if (AplicationSettings.GetAnonimo())
                {
                    mensajesNuevos[0].nombre = "Anónimo";
                    mensajesNuevos[0].foto = Comunicacion.baseURL + "media/users/no_image.png";
                }
                dataBase.insertAll(mensajesNuevos);

                if (getMensajesTablonServerCompletado != null)
                {
                    getMensajesTablonServerCompletado(this, new MensajesTablonServerEventArgs(mensajesNuevos));
                }

                return true;
            }
            return false;
        }


        public async Task<bool> favMesajeTablon (MensajeTablon message)
        {
            string response = await Comunicacion.favoriteMessage(AplicationSettings.getToken(), message.identificador+"");
            JObject json = JObject.Parse(response);
            if ((string)json.SelectToken("error") == "200")
            {
                dataBase.updateMessage(message);
                return true;
            }
            return false;
        }

        public async Task<bool> borrarMensaje(string idMensaje)
        {
            string response = await Comunicacion.deleteMensaje(AplicationSettings.getToken(), idMensaje);
            JObject json = JObject.Parse(response);
            if((string)json.SelectToken("error")=="200")
            {
                return true;
            }
            return false;
        }
    }
}
