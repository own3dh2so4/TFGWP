using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_PreguntasExamenRealizadoParejas:ConexionDataBase
    {
        public CDB_PreguntasExamenRealizadoParejas()
            : base("PreguntaParejasRespondida.sqlite")
        {
            dbConn.CreateTable<PreguntaParejasRespondida>();
        }

        public void insertAll(List<PreguntaParejasRespondida> list)
        {
            foreach (var l in list)
                dbConn.InsertOrReplace(l);
        }

        public List<PreguntaParejasRespondida> getExamenCorregido()
        {
            return dbConn.Query<PreguntaParejasRespondida>("select * from PreguntaParejasRespondida");
        }

        public void deleteAll()
        {
            dbConn.DeleteAll<PreguntaParejasRespondida>();
        }
    }
}
