using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_MensajeTablon: ConexionDataBase
    {
        public CDB_MensajeTablon()
            : base("MensajesTablon.sqlite")
        {
            dbConn.CreateTable<MensajeTablon>();
        }

        public List<MensajeTablon> getAllOrderByIDDesc()
        {
            return dbConn.Query<MensajeTablon>("select * from MensajeTablon order by identificador DESC;");
        }

        public int getMAXIdFormMensajeTablon(string idFacultad)
        {
            var id = dbConn.Query<MensajeTablon>("select MAX(identificador) as identificador from MensajeTablon where identificadorTablon = " + idFacultad + ";");
            return id[0].identificador;   
        }

        public void insertAll(List<MensajeTablon> list)
        {
            dbConn.InsertAll(list);
        }

        public void insert(MensajeTablon mensaje)
        {
            dbConn.Insert(mensaje);
        }

    }
}
