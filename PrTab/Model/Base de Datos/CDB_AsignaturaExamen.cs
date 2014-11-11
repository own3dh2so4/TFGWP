using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_AsignaturaExamen:ConexionDataBase
    {
        public CDB_AsignaturaExamen()
            : base("Asignatura.sqlite")
        {
            dbConn.CreateTable<Asignatura>();
        }

        public void insertAll(List<Asignatura> list)
        {
            foreach (var asignatura in list)
                dbConn.InsertOrReplace(list);
        }

        public void insert(Asignatura asignatura)
        {
            dbConn.InsertOrReplace(asignatura);
        }

        public List<Asignatura> getAsignaturasExamen()
        {
            return dbConn.Query<Asignatura>("select * from Asignatura");
        }

    }
}
