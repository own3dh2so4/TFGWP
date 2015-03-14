using PrTab.Model.Modelo.ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_MensajesServerMessage: ConexionDataBase
    {

        public CDB_MensajesServerMessage()
            :base("MensajeServerMessage.sqlite")
        {
            dbConn.CreateTable<MensajeServerMensaje>();
        }

        public void insertAll(List<MensajeServerMensaje> list)
        {
            foreach (var uni in list)
                dbConn.InsertOrReplace(uni);
        }

        public void insert(MensajeServerMensaje uni)
        {
            dbConn.InsertOrReplace(uni);
        }

        public List<MensajeServerMensaje> getMessagesFromRoom(string room)
        {
            var ret = dbConn.Query<MensajeServerMensaje>("select * from MensajeServerMensaje where room = '"+ room+"'");
            foreach (var b in ret)
            {
                dbConn.Delete(b);
                dbConn.Query<MensajeServerMensaje>("delete from MensajeServerMensaje where room = '" + room + "'");
            }
            return ret;
        }

        
    }
}
