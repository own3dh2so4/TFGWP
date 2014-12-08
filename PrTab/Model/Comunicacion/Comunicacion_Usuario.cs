using Newtonsoft.Json.Linq;
using PrTab.Model.Base_de_Datos;
using PrTab.Model.Modelo;
using PrTab.Utiles;
using PrTab.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Comunicacion
{
    public static class Comunicacion_Usuario
    {


        public static async Task<bool> CambiarContraseña(string antiguaPass, string nuevaPass)
        {
            string result = await Comunicacion.cambiarContraseña(AplicationSettings.getToken(), antiguaPass, nuevaPass);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                return true;
            }
            else
            {
                AplicationSettings.setErrorServer((string)o.SelectToken("error_msg"));
                return false;
            }
        }

        public static async Task<bool> CambiarFacultad(string idFacultad, string pass)
        {
            string result = await Comunicacion.cambiarFacultad(AplicationSettings.getToken(), pass, idFacultad );
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                return true;
            }
            else
            {
                AplicationSettings.setErrorServer((string)o.SelectToken("error_msg"));
                return false;
            }
        }

        public static async Task<bool> LoguearUsuario (string usuario, string contraseña)
        {            
            string result = await Comunicacion.loguearUsuario(usuario, contraseña);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                //AplicationSettings.setToken((string)o.SelectToken("token").SelectToken("token"));
                //return true;
                savesPropertys(o);
                return true;
            }
            return false;
        }

        public static async Task<bool> RegistrarUsuario(string usuario, string contraseña, string correo, string uni, string facul )
        {
            string result = await Comunicacion.registrarUsuario(usuario, contraseña, correo,uni,facul);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setToken((string)o.SelectToken("token"));
                return true;
            }
            else if ((string)o.SelectToken("error") == "406")
            {
                AplicationSettings.setErrorServer((string)o.SelectToken("error_msg"));
            }
            return false;
        }

        public static async Task<bool> RegistrarUsuario(string usuario, string contraseña, string correo, string uni, string facul, string model, string platform, string displaysize)
        {
            string result = await Comunicacion.registrarUsuario(usuario, contraseña, correo, uni, facul, model, platform, displaysize);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                savesPropertys(o);
                return true;
            }
            else if ((string)o.SelectToken("error") == "406")
            {
                AplicationSettings.setErrorServer((string)o.SelectToken("error_msg"));
            }
            return false;
        }

        public static async Task<bool> ExisteUsuario(string user)
        {
            string result = await Comunicacion.consultaUsuario(user);
            JObject o = JObject.Parse(result);
            if((string)o.SelectToken("error")=="200")
            {
                if ((string)o.SelectToken("available") == "0")
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static async Task<bool> ExisteEmail(string email)
        {
            string result = await Comunicacion.consultaEmail(email);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                if ((string)o.SelectToken("available") == "0")
                    return true;
                else
                    return false;
            }
            return false;
        }

        private static void savesPropertys(JObject o)
        {
            //Guardamos en la bd la info de la facultad.
                {
                    Facultad facultad = new Facultad((string)o.SelectToken("faculty").SelectToken("nombre"),
                        Convert.ToInt32((string)o.SelectToken("faculty").SelectToken("pk")),
                        Convert.ToInt32((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("pk")));
                    AplicationSettings.setIdTablonMensaje((string)o.SelectToken("faculty").SelectToken("pk"));
                    CDB_Facultad bdFacultad = new CDB_Facultad();
                    bdFacultad.insert(facultad);
                    AplicationSettings.setIdFacultadUsuario((string)o.SelectToken("faculty").SelectToken("pk"));
                }
                //Guardar universidad 
                {
                    Universidad universidad = new Universidad((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("nombre"),
                        Convert.ToInt32((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("pk")),
                        Convert.ToInt32((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("provincia").SelectToken("pk")));
                    CDB_Universidad bdUniversidad = new CDB_Universidad();
                    bdUniversidad.insert(universidad);
                    AplicationSettings.setIdUniversidadUsuario((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("pk"));
                }
                //Guardar provincia
                {
                    Provincia provincia = new Provincia((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("provincia").SelectToken("nombre"),
                        Convert.ToInt32((string)o.SelectToken("faculty").SelectToken("universidad").SelectToken("provincia").SelectToken("pk")));
                    CDB_Provincia dbProvincia = new CDB_Provincia();
                    dbProvincia.insert(provincia);
                }
                //Guardar idUsuario
                {
                    AplicationSettings.setIdUsuario((string)o.SelectToken("user").SelectToken("pk"));
                }

                //Guardamos el token
                AplicationSettings.setToken((string)(o.SelectToken("token").SelectToken("token")));                
        }
    }
}
