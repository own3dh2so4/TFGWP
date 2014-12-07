using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class CacheImagen
    {

        [PrimaryKey]
        public string url { get; set; }
        public string date { get; set; }

        public CacheImagen()
        {

        }

        public CacheImagen(string u, string d)
        {
            url = HttpUtility.UrlDecode(u);
            date = d;
        }
    }
}
