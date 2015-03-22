using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrTab.Model.Comunicacion
{
    public static class Comunicacion
    {
        //para evirtar que windows phone me cache las llamadas tengo que poner esto
        const string unixTime = "tiempo";
        private static string unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds+"";


        static HttpClient client = new HttpClient();
        public const string baseURL = "http://192.168.0.2:80/";
        //public const string baseURL = "http://www.bsodsoftware.me/";

        public const string imagenesPerfil = "media";

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
        const string parametro_postMensajeAnonimo = "anonymus";

        const string getMensajesTablon = "getmensajestablon";
        const string parametro_getMensajeToken = "token";
        const string parametro_getMensajeIdLastMensaje = "idmessage";
        const string parametro_getMensajeIdFaculty = "idfaculty";

        const string getExamenAsignatura = "getexams";
        const string parametro_getExamenAsignaturaToken = "token";
        const string parametro_getExamenAsignaturaAsignatura = "subject";
        const string parametro_getExamenAsignaturaNumeroPreguntas = "numberofquestions";
        const string parametroOpcional_getExamenAsignaturaTema = "theme";

        const string getAsignatras = "getsubjects";
        const string parametro_getAsignaturaToken = "token";
        const string parametro_getAsignaturaAño = "year";
        const string parametro_getAsignaturaFacultad = "idfaculty";

        const string getThemes = "getthemes";
        const string parametro_getThemeToken = "token";
        const string parametro_getThemeAsignatura = "subject";

        const string setResults = "sendresult";
        const string parametro_sendResultsToken = "token";
        const string parametro_sendResultIdTest = "idtest";
        const string parametro_sendResultQuestions = "questions";
        const string parametro_sendResultaTime = "time";

        const string addfavsubject = "favoritesubject";
        const string parametro_addfavsubjectToken = "token";
        const string parametro_addfavsubjectIdSubject = "subject";

        const string getfavsubjects = "getfavsubjects";
        const string parametro_getfavsubjectsToken = "token";

        const string deletefavsubject = "favoritesubject";
        const string parametro_deletefavsubjectToken = "token";
        const string parametro_deletefavsubjectIdSubject = "subject";


        const string sendImage = "uploadimageuser";
        const string parametroPOST_sendimageToken = "token";
        const string parametroPOST_sendimageImagen = "image";


        const string favoritemessage = "favoritemessage";
        const string parametro_FavMesToken = "token";
        const string parametro_FavMesIdMessage = "idmessage";

        const string changepassword = "changepassword";
        const string parametro_changepasswordToken = "token";
        const string parametro_changepasswordOldPassword = "oldpass";
        const string parametro_changepasswordNewPassword = "newpass";


        const string changefaculty = "changefaculty";
        const string parametro_changefacultyToken = "token";
        const string parametro_changefacultyPass = "pass";
        const string parametro_changefacultyIdFaculty = "idnewfaculty";


        const string updatemensajes = "updatemessages";
        const string parametro_updateMensajesToken = "token";
        const string parametro_updateMensajesInit = "idmsgstart";
        const string parametro_updateMensajesEnd = "idmsgend";
        const string parametro_updateMensajesIdFaculty = "idfaculty";

        const string deletemessage = "deletemessage";
        const string parametro_deleteMessageToken = "token";
        const string parametro_deleteMessageIdMessage = "idmessage";

        const string crearExam = "createexam";
        const string parametro_CreateExamPost_token = "token";
        const string parametro_CreateExamPost_idSubject = "idsubject";
        const string parametro_CreateExamPost_idTheme = "idtheme";
        const string parametro_CreateExamPost_year = "year";
        const string parametro_CreateExamPost_month = "month";
        const string parametro_CreateExamPost_image = "image";
        const string parametro_CreateExamPost_lastone = "lastone";

        const string actualizarExamen = "uploadexam";
        const string parametro_ActualizarExamen_token = "token";
        const string parametro_ActualizarExamen_number = "number";
        const string parametro_ActualizarExamen_lastone = "lastone";
        const string parametro_ActualizarExamen_image = "image";


        public static async Task<string> deleteMensaje(string token, string idMensaje)
        {
            Uri_Get url = new Uri_Get(baseURL + deletemessage);
            url.GetData(parametro_deleteMessageToken, token);
            url.GetData(parametro_deleteMessageIdMessage, idMensaje);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> updateMessages(string token, string idInit, string idEnd, string idFaculty)
        {
            Uri_Get url = new Uri_Get(baseURL + updatemensajes);
            url.GetData(parametro_updateMensajesToken , token);
            url.GetData(parametro_updateMensajesInit, idInit);
            url.GetData(parametro_updateMensajesEnd, idEnd);
            url.GetData(parametro_updateMensajesIdFaculty, idFaculty);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> cambiarFacultad(string token, string pass, string idFaculty)
        {
            Uri_Get url = new Uri_Get(baseURL + changefaculty);
            url.GetData(parametro_changepasswordToken, token);
            url.GetData(parametro_changefacultyPass, pass);
            url.GetData(parametro_changefacultyIdFaculty, idFaculty);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> cambiarContraseña(string token, string oldPassword, string newPassword)
        {
            Uri_Get url = new Uri_Get(baseURL + changepassword);
            url.GetData(parametro_changepasswordToken, token);
            url.GetData(parametro_changepasswordOldPassword, oldPassword);
            url.GetData(parametro_changepasswordNewPassword, newPassword);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> favoriteMessage(string token, string idMessage)
        {
            Uri_Get url = new Uri_Get(baseURL + favoritemessage);
            url.GetData(parametro_FavMesToken, token);
            url.GetData(parametro_FavMesIdMessage, idMessage);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }



        public static async Task<string> borrarAsignaturasFavoritas(string token, string idAsignatura)
        {
            Uri_Get url = new Uri_Get(baseURL + deletefavsubject);
            url.GetData(parametro_deletefavsubjectToken, token);
            url.GetData(parametro_deletefavsubjectIdSubject, idAsignatura);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        } 


        public static async Task<string> getAsignaturasFavoritas(string token)
        {
            Uri_Get url = new Uri_Get(baseURL + getfavsubjects);
            url.GetData(parametro_addfavsubjectToken, token);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> añadirAsignaturaFavoritos (string token, string idasignatura)
        {
            Uri_Get url = new Uri_Get(baseURL + addfavsubject);
            url.GetData(parametro_addfavsubjectToken, token);
            url.GetData(parametro_addfavsubjectIdSubject, idasignatura);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            string posion = await client.GetStringAsync(url.getUri());
            return posion;
        }

        public static async Task<string> sendResults(string token, string idTest, string questionsJSon, string time)
        {
            Uri_Get url = new Uri_Get(baseURL + setResults);
            url.GetData(parametro_sendResultsToken, token);
            url.GetData(parametro_sendResultIdTest, idTest);
            url.GetData(parametro_sendResultQuestions, questionsJSon);
            url.GetData(parametro_sendResultaTime, time);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> getTemaAsignatura(string token, string asignatura)
        {
            Uri_Get url = new Uri_Get(baseURL + getThemes);
            url.GetData(parametro_getThemeToken, token);
            url.GetData(parametro_getThemeAsignatura, asignatura);
            return await client.GetStringAsync(url.getUri());
        }        
        

        public static async Task<string> getAsignaturas (string token, string año, string facultad)
        {
            Uri_Get url = new Uri_Get(baseURL + getAsignatras);
            url.GetData(parametro_getAsignaturaToken, token);
            url.GetData(parametro_getAsignaturaAño, año);
            url.GetData(parametro_getAsignaturaFacultad, facultad);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> getExamen(string token, string asignatura, string numeroPreguntas)
        {
            Uri_Get url = new Uri_Get(baseURL+getExamenAsignatura);
            url.GetData(parametro_getExamenAsignaturaToken, token);
            url.GetData(parametro_getExamenAsignaturaAsignatura, asignatura);
            url.GetData(parametro_getExamenAsignaturaNumeroPreguntas, numeroPreguntas);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> getExamen(string token, string asignatura,string tema, string numeroPreguntas)
        {
            Uri_Get url = new Uri_Get(baseURL + getExamenAsignatura);
            url.GetData(parametro_getExamenAsignaturaToken, token);
            url.GetData(parametro_getExamenAsignaturaAsignatura, asignatura);
            url.GetData(parametroOpcional_getExamenAsignaturaTema, tema);
            url.GetData(parametro_getExamenAsignaturaNumeroPreguntas, numeroPreguntas);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }

        public static async Task<string> getMensajes(string token, string idMensaje, string idFacultad)
        {
            Uri_Get url = new Uri_Get(baseURL + getMensajesTablon);
            url.GetData(parametro_getMensajeToken, token);
            url.GetData(parametro_getMensajeIdLastMensaje, idMensaje);
            url.GetData(parametro_getMensajeIdFaculty, idFacultad);
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
            return await client.GetStringAsync(url.getUri());
        }


        public static async Task<string> postMensaje(string token, string mensaje, string facultad)
        {
            Uri_Get url = new Uri_Get(baseURL + postearMensaje);
            url.GetData(parametro_postMensajeToken, token);
            url.GetData(parametro_postMensajeMensaje, mensaje);
            url.GetData(parametro_postMensajeFacultad, facultad);
            if (AplicationSettings.GetAnonimo())
                url.GetData(parametro_postMensajeAnonimo,"True");
            else
                url.GetData(parametro_postMensajeAnonimo, "False");
            //Evitar cache
            unixTimestamp = (int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + "";
            url.GetData(unixTime, unixTimestamp);
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
            var result = await client.GetStringAsync(url.getUri());
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



        public static async void sendImagePerfil(string token, byte[] imagen)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            form.Add(new StringContent(token), parametroPOST_sendimageToken);
            //  form.Add(new FormUrlEncodedContent(data), "profile_pic");

            var imagenForm =new ByteArrayContent(imagen, 0, imagen.Count());
            imagenForm.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            form.Add(imagenForm, parametroPOST_sendimageImagen, "nameholder.jpg");
            
            HttpResponseMessage response = await httpClient.PostAsync(baseURL + sendImage, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;

          
        }

        public static async Task<string> createExam (string token,string idSubject, string idTheme, string year, string month, string lastone, byte[] image)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(token), parametro_CreateExamPost_token);
            if (idSubject!=null)
                form.Add(new StringContent(idSubject), parametro_CreateExamPost_idSubject);
            if(idTheme!=null)
                form.Add(new StringContent(idTheme), parametro_CreateExamPost_idTheme);
            form.Add(new StringContent(year), parametro_CreateExamPost_year);
            form.Add(new StringContent(month), parametro_CreateExamPost_month);
            form.Add(new StringContent(lastone), parametro_CreateExamPost_lastone);

            var imagenForm = new ByteArrayContent(image, 0, image.Count());
            imagenForm.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(imagenForm, parametroPOST_sendimageImagen, "nameholder.jpg");
            HttpResponseMessage response = await httpClient.PostAsync(baseURL + crearExam, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(sd);
            if ((string)json.SelectToken("error") == "200" && lastone=="False")
            {
                return (string)json.SelectToken("token");
            }
            return "";
        }

        public static async Task<bool> updateExam(string token, string number, string lastone, byte[] image)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent(token), parametro_ActualizarExamen_token);
            form.Add(new StringContent(lastone), parametro_ActualizarExamen_lastone);
            form.Add(new StringContent(number), parametro_ActualizarExamen_number);

            var imagenForm = new ByteArrayContent(image, 0, image.Count());
            imagenForm.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(imagenForm, parametroPOST_sendimageImagen, "nameholder.jpg");
            HttpResponseMessage response = await httpClient.PostAsync(baseURL + actualizarExamen, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(sd);
            if ((string)json.SelectToken("error") == "200")
            {
                return true;
            }
            return false;
        }




        private static HttpContent CreateFileContent(byte[] stream, string fileName, string contentType)
        {
            var fileContent = new ByteArrayContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"files\"",
                FileName = "\"" + fileName + "\""
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }
    }
}
