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
        const string baseURL = "http://192.168.0.2:80/";
        const string logearUsuario = "loginuser";
        const string parametro_NombreUsuario = "user";
        const string parametro_ContraseñaUsuario = "pass";

        public static async Task<string> loguearUsuario(string usuario, string contraseña)
        {
            Uri_Get url = new Uri_Get(baseURL+logearUsuario);
            url.GetData(parametro_NombreUsuario,usuario);
            url.GetData(parametro_ContraseñaUsuario,contraseña);
            return await client.GetStringAsync(url.getUri());
        }
    }
}
