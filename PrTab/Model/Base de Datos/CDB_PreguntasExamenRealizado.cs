using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_PreguntasExamenRealizado:ConexionDataBase
    {
        public CDB_PreguntasExamenRealizado()
            :base("PreguntaExamenRealizado.sqlite")
        {
            dbConn.CreateTable<PreguntaRespondida>();
        }


        public void insertAll(List<PreguntaRespondida> list)
        {
            foreach (var asignatura in list)
                dbConn.InsertOrReplace(asignatura);
        }

        public void insert(PreguntaRespondida asignatura)
        {
            dbConn.InsertOrReplace(asignatura);
        }

        public List<PreguntaRespondida> getExamenCorregido()
        {
            return dbConn.Query<PreguntaRespondida>("select * from PreguntaRespondida");
        }

        public void deleteAll()
        {
            dbConn.DeleteAll<PreguntaRespondida>();
        }
    }
}
