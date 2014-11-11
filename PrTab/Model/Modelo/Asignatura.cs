using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    public class Asignatura
    {
        [PrimaryKey]
        public int identificador{get;set;}
        public string nombre { get; set; }
        public string abreviatura { get; set; }
        public int idFacultad { get; set; }
        public string año { get; set; }

        public Asignatura(int id, string name, string abrevia, int idFacul, string anno)
        {
            identificador = id;
            nombre = name;
            abreviatura = abrevia;
            idFacultad = idFacul;
            año = anno;
        }

        public Asignatura()
        { }

    }
}
