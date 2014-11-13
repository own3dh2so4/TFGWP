using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_TemaExamen:ConexionDataBase
    {

        public CDB_TemaExamen()
            :base ("TemaExamen.sqlite")
        {
            dbConn.CreateTable<Tema>();
        }

        public void insertAll(List<Tema> list)
        {
            foreach(var t in list)
            {
                dbConn.InsertOrReplace(t);
            }
        }

        public void insert(Tema tema)
        {
            dbConn.InsertOrReplace(tema);
        }

        public List<Tema> getTemasDeAsignaturas(string idAsignatura)
        {
            return dbConn.Query<Tema>("select * from Tema where idAsignatura=" + idAsignatura);
        }
    }
}
