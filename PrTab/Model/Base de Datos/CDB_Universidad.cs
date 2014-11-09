using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_Universidad: ConexionDataBase
    {

        public CDB_Universidad()
            :base("Universidad.sqlite")
            {
                dbConn.CreateTable<Universidad>();
            }

        public void insertAll(List<Universidad> list)
        {
            dbConn.InsertAll(list);
        }

        public void insert(Universidad uni)
        {
            List<Universidad> unis = dbConn.Query<Universidad>("select * from Universidad where identificador =" + uni.identificador);
            if (unis.Count==0)
                dbConn.Insert(uni);
        }
    }
}
