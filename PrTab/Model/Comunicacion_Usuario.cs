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
    class Comunicacion_Usuario
    {

        public async Task<bool> LoguearUsuario (string usuario, string contraseña)
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
    }
}
