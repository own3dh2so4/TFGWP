using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_Facultad: ConexionDataBase
    {
        public CDB_Facultad()
            : base("Facultad.sqlite")
        {
            dbConn.CreateTable<Facultad>();
        }

        public void insertAll(List<Facultad> list)
        {
            foreach (var facultad in list)
                dbConn.InsertOrReplace(list);
        }

        public void insert(Facultad facultad)
        {
            dbConn.InsertOrReplace(facultad);
        }

        public Facultad selectById(int id)
        {
            return dbConn.Query<Facultad>("select * from Facultad where identificador = " + id)[0];
        }
    }
}
