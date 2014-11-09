using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_Provincia: ConexionDataBase
    {

        public CDB_Provincia()
            : base("Provincia.sqlite")
        {
            dbConn.CreateTable<Provincia>();
        }

        public void insertAll(List<Provincia> list)
        {
            dbConn.InsertAll(list);
        }

        public void insert(Provincia provincia)
        {
            List<Provincia> prov = dbConn.Query<Provincia>("select * from Provincia where identificador =" +provincia.identificador);
            if (prov.Count==0)  
                dbConn.Insert(provincia);
        }
    }
}
