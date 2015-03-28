using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class Documento
    {
        public string idAsignatura;
        public string idTema;
        public string ano;
        public string nombreUsuario;
        public string fotoUsuario;
        public List<string> imagenes;

        public Documento()
        {

        }
        public Documento(string idAsi, string idT, string a, string nu, string fu, List<string> i)
        {
            this.idAsignatura = idAsi;
            this.idTema = idT;
            this.ano = a;
            this.nombreUsuario = nu;
            this.fotoUsuario = fu;
            this.imagenes = i;
        }
    }
}
