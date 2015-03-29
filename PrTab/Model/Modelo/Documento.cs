using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Documento
    {
        public string idAsignatura { get; set; }
        public string idTema { get; set; }
        public string ano { get; set; }
        public string nombreUsuario { get; set; }
        public string fotoUsuario { get; set; }
        public List<string> imagenes { get; set; }

        public Documento()
        {

        }
        public Documento(string idAsi, string idT, string a, string nu, string fu, List<string> i)
        {
            this.idAsignatura = idAsi;
            this.idTema = idT;
            this.ano = a;
            this.nombreUsuario = nu;
            this.fotoUsuario = Comunicacion.Comunicacion.baseURL + fu;
            this.imagenes = i;
        }
    }
}
