using Newtonsoft.Json.Linq;
using PrTab.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model
{
    public static class Comunicacion_Usuario
    {

        public static async Task<bool> LoguearUsuario (string usuario, string contraseña)
        {            
            string result = await Comunicacion.loguearUsuario(usuario, contraseña);
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                AplicationSettings.setToken((string)o.SelectToken("token"));
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
    }
}
