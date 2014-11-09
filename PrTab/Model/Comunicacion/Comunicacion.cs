﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Comunicacion
{
    public static class Comunicacion
    {
        static HttpClient client = new HttpClient();
        const string baseURL = "http://192.168.0.2:80/";
        //const string baseURL = "http://www.bsodsoftware.me/";

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
        //Parametros opcionales
        const string parametroOpcional_ModeloMovilRegistro = "model";
        const string parametroOpcional_PlataformaMovilRegistro = "platform";
        const string parametroOpcional_DimensionesPantallaRegistro = "displaysize";

        const string consultarUsuario = "checkuser";
        const string parametro_NombreUsuarioConsultar = "user";


        const string consularEmail = "checkemail";
        const string parametro_EmailUsuarioConsulta = "email";

        const string postearMensaje = "sendmensajetablon";
        const string parametro_postMensajeToken = "token";
        const string parametro_postMensajeMensaje = "message";
        const string parametro_postMensajeFacultad = "idfaculty";

        const string getMensajesTablon = "getmensajestablon";
        const string parametro_getMensajeToken = "token";
        const string parametro_getMensajeIdLastMensaje = "idmessage";
        const string parametro_getMensajeIdFaculty = "idfaculty";

        const string getExamenAsignatura = "getexamen";
        const string parametro_getExamenAsignaturaToken = "token";
        const string parametro_getExamenAsignaturaAsignatura = "subject";
        const string parametro_getExamenAsignaturaNumeroPreguntas = "numberofquestions";
        const string parametroOpcional_getExamenAsignaturaTema = "theme";


        public static async Task<string> getExamen(string token, string asignatura, string numeroPreguntas)
        {
            Uri_Get url = new Uri_Get(baseURL+getExamenAsignatura);
            url.GetData(parametro_getExamenAsignaturaToken, token);
            url.GetData(parametro_getExamenAsignaturaAsignatura, asignatura);
            url.GetData(parametro_getExamenAsignaturaNumeroPreguntas, numeroPreguntas);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> getMensajes(string token, string idMensaje, string idFacultad)
        {
            Uri_Get url = new Uri_Get(baseURL + getMensajesTablon);
            url.GetData(parametro_getMensajeToken, token);
            url.GetData(parametro_getMensajeIdLastMensaje, idMensaje);
            url.GetData(parametro_getMensajeIdFaculty, idFacultad);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> postMensaje(string token, string mensaje, string facultad)
        {
            Uri_Get url = new Uri_Get(baseURL + postearMensaje);
            url.GetData(parametro_postMensajeToken, token);
            url.GetData(parametro_postMensajeMensaje, mensaje);
            url.GetData(parametro_postMensajeFacultad, facultad);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> consultaEmail(string email)
        {
            Uri_Get url = new Uri_Get(baseURL + consularEmail);
            url.GetData(parametro_EmailUsuarioConsulta, email);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> consultaUsuario(string usuario)
        {
            Uri_Get url = new Uri_Get(baseURL + consultarUsuario);
            url.GetData(parametro_NombreUsuarioConsultar, usuario);
            return await client.GetStringAsync(url.getUri());
        }

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
        public static async Task<string> registrarUsuario(string usuario, string contraseña, string email, string uni, string facul, string model, string platform, string displaysize)
        {
            Uri_Get url = new Uri_Get(baseURL + registUsuario);
            url.GetData(parametro_NombreRegistro, usuario);
            url.GetData(parametro_ContraseñaRegistro, contraseña);
            url.GetData(parametro_emailRegistro, email);
            url.GetData(parametro_universidadRegistro, uni);
            url.GetData(parametro_facultadRegistro, facul);
            url.GetData(parametroOpcional_ModeloMovilRegistro, model);
            url.GetData(parametroOpcional_PlataformaMovilRegistro, platform);
            url.GetData(parametroOpcional_DimensionesPantallaRegistro, displaysize);
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
                    string nombre = (string)jArray[i].SelectToken("nombre");
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
                    string nombre = (string)jArray[i].SelectToken("nombre");
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
                    //JObject fields = (JObject)jArray[i].SelectToken("fields");
                    string nombre = (string)jArray[i].SelectToken("nombre");
                    int id = Convert.ToInt32((string)jArray[i].SelectToken("pk"));
                    facultades.Add(new Facultad(nombre, id, idUni));
                }
            }

            return facultades;
        }

        
    }
}
