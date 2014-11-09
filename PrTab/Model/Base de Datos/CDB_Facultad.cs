using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_Facultad: ConexionDataBase
    {
        public CDB_Facultad()
            : base("Facultad.sqlite")
        {
            dbConn.CreateTable<Facultad>();
        }

        public void insertAll(List<Facultad> list)
        {
            dbConn.InsertAll(list);
        }

        public void insert(Facultad facultad)
        {
            List<Facultad> fac = dbConn.Query<Facultad>("select * from Facultad where identificador =" + facultad.identificador);
            if (fac.Count==0)
                dbConn.Insert(facultad);
        }
    }
}
