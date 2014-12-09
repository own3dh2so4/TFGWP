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

        public MensajeTablon getForId(string idMessage)
        {
            var mensajes = dbConn.Query<MensajeTablon>("select * from MensajeTablon where identificador = " + idMessage + ";");
            if (mensajes.Count>0)
                return mensajes[0];
            return null;
        }

        public void updateMessage(MensajeTablon m)
        {
            dbConn.InsertOrReplace(m);
        }


        public void updateInfoMensajesTablon (List<UpdateMensajeTablon> list)
        {

            foreach (var l in list)
            {
                if (!l.deleted)
                {
                    var m = dbConn.Query<MensajeTablon>("select * from MensajeTablon where identificador = " + l.id + ";")[0];
                    m.numFav = l.numFav;
                    m.userFav = l.userFav;
                    dbConn.InsertOrReplace(m);
                    //dbConn.Execute("update MensajeTablon set" +
                    //                            " numFav = " + l.numFav +
                    //                            " userFav = " + l.userFav +
                    //                            " where identificador = " + l.id);
                }
                else
                    dbConn.Execute("delete from MensajeTablon where identificador = " + l.id);

            }
        }

    }
}
