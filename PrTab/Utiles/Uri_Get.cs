using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Utiles
{
    class Uri_Get
    {
        private string uri;
        private bool variables;

        public string getUri()
        {
            return uri;
        }

        public Uri_Get(string uri)
        {
            this.uri = uri;
            variables = false;
        }

        public void GetData(string nombre, string dato)
        {
            if (!variables)
            {
                uri = uri + "?" + nombre + "=" + dato;
                variables = true;
            }
            else
                uri = uri + "&" + nombre + "=" + dato;
        }
    }
}
