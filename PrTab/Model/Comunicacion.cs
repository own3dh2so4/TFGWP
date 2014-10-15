using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    public static class Comunicacion
    {
        static HttpClient client = new HttpClient();
        //const string baseURL = "http://192.168.0.2:80/";
        const string baseURL = "http://www.bsodsoftware.me/";

        const string logearUsuario = "loginuser";
        const string parametro_NombreUsuario = "user";
        const string parametro_ContraseñaUsuario = "pass";

        const string getPrvicias = "getprovincias";

        const string getUniversidades = "getunis";
        const string parametro_IdProvincia = "id";

        const string getFacultades = "getfacultades";
        const string parametro_IdUniversidad = "id";

        const string registUsuario = "register";
        const string parametro_NombreRegistro = "user";
        const string parametro_ContraseñaRegistro = "pass";
        const string parametro_emailRegistro = "email";
        const string parametro_universidadRegistro = "university";
        const string parametro_facultadRegistro = "faculty";


        public static async Task<string> registrarUsuario(string usuario, string contraseña, string email, string uni, string facul)
        {
            Uri_Get url = new Uri_Get(baseURL + registUsuario);
            url.GetData(parametro_NombreRegistro, usuario);
            url.GetData(parametro_ContraseñaRegistro, contraseña);
            url.GetData(parametro_emailRegistro, email);
            url.GetData(parametro_universidadRegistro, uni);
            url.GetData(parametro_facultadRegistro, facul);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> loguearUsuario(string usuario, string contraseña)
        {
            Uri_Get url = new Uri_Get(baseURL+logearUsuario);
            url.GetData(parametro_NombreUsuario,usuario);
            url.GetData(parametro_ContraseñaUsuario,contraseña);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<List<Provincia>> getProvicias()
        {            
            List<Provincia> provincias = new List<Provincia>();
            Uri_Get url = new Uri_Get(baseURL + getPrvicias);
            string result = await client.GetStringAsync(url.getUri());
            JObject json = JObject.Parse(result);
            if ((string)json.SelectToken("error") == "200")
            {
                JArray jArray = (JArray)json["data"];
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject fields = (JObject)jArray[i].SelectToken("fields");
                    string nombre = (string)fields.SelectToken("nombre");
                    int id = Convert.ToInt32((string)jArray[i].SelectToken("pk"));
                    provincias.Add(new Provincia(nombre, id));
                }
            }            
            return provincias;
        }

        public static async Task<List<Universidad>> getUniversidadesProvincia(int idProv)
        {
            List<Universidad> universidades = new List<Universidad>();
            Uri_Get url = new Uri_Get(baseURL + getUniversidades);
            url.GetData(parametro_IdProvincia,idProv+"");
            string result = await client.GetStringAsync(url.getUri());
            JObject json = JObject.Parse(result);
            if ((string)json.SelectToken("error") == "200")
            {
                JArray jArray = (JArray)json["data"];
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject fields = (JObject)jArray[i].SelectToken("fields");
                    string nombre = (string)fields.SelectToken("nombre");
                    int id = Convert.ToInt32((string)jArray[i].SelectToken("pk"));
                    universidades.Add(new Universidad(nombre, id, idProv));
                }
            }

            return universidades;
        }

        public static async Task<List<Facultad>> getFacultadesUniversidad(int idUni)
        {
            List<Facultad> facultades = new List<Facultad>();
            Uri_Get url = new Uri_Get(baseURL + getFacultades);
            url.GetData(parametro_IdUniversidad, idUni + "");
            string result = await client.GetStringAsync(url.getUri());
            JObject json = JObject.Parse(result);
            if ((string)json.SelectToken("error") == "200")
            {
                JArray jArray = (JArray)json["data"];
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject fields = (JObject)jArray[i].SelectToken("fields");
                    string nombre = (string)fields.SelectToken("nombre");
                    int id = Convert.ToInt32((string)jArray[i].SelectToken("pk"));
                    facultades.Add(new Facultad(nombre, id, idUni));
                }
            }

            return facultades;
        }

        
    }
}
