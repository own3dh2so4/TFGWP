using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_PreguntasExamenRealizadoMulti : ConexionDataBase
    {
        public CDB_PreguntasExamenRealizadoMulti()
            : base("PreguntaMultiRespondida.sqlite")
        {
            dbConn.CreateTable<PreguntaMultiRespondida>();
        }

        public void insertAll(List<PreguntaMultiRespondida> list)
        {
            foreach (var l in list)
                dbConn.InsertOrReplace(l);
        }

        public void insert(PreguntaMultiRespondida p)
        {
            dbConn.InsertOrReplace(p);
        }

        public List<PreguntaMultiRespondida> getExamenCorregido()
        {
            return dbConn.Query<PreguntaMultiRespondida>("select * from PreguntaMultiRespondida");
        }

        public void deleteAll()
        {
            dbConn.DeleteAll<PreguntaMultiRespondida>();
        }
    }
}
