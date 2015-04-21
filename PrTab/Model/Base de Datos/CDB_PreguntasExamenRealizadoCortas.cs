using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_PreguntasExamenRealizadoCortas : ConexionDataBase
    {
        public CDB_PreguntasExamenRealizadoCortas()
            : base("PreguntaRespondidaCorta.sqlite")
        {
            dbConn.CreateTable<PreguntaRespondidaCorta>();
        }

        public void insertAll(List<PreguntaRespondidaCorta> list)
        {
            foreach (var l in list)
                dbConn.InsertOrReplace(l);
        }

        public List<PreguntaRespondidaCorta> getExamenCorregido()
        {
            return dbConn.Query<PreguntaRespondidaCorta>("select * from PreguntaRespondidaCorta");
        }

        public void deleteAll()
        {
            dbConn.DeleteAll<PreguntaRespondidaCorta>();
        }
    }
}
