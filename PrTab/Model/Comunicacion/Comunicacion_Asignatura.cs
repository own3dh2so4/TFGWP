using Newtonsoft.Json.Linq;
using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Comunicacion
{
    public static class Comunicacion_Asignatura
    {
        public static async Task<List<Asignatura>> getAsignaturas(string token, string año, string facultad)
        {
            string result = await Comunicacion.getAsignaturas(token, año, facultad);
            List<Asignatura> ret = new List<Asignatura>();
            JObject o = JObject.Parse(result);
            if ((string)o.SelectToken("error") == "200")
            {
                JArray asignaturas = (JArray)o["data"];
                foreach (var a in asignaturas)
                {
                    ret.Add(new Asignatura(Convert.ToInt32((string)a.SelectToken("pk")),
                        (string)a.SelectToken("nombre"),
                        (string)a.SelectToken("abreviatura"),
                        Convert.ToInt32(facultad), 
                        año));
                }
            }
            return ret;
        }
    }
}
