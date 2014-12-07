using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_CacheImagen:ConexionDataBase
    {
        public CDB_CacheImagen()
            :base("CacheImage.sqlite")
        {
            dbConn.CreateTable<CacheImagen>();
        }

        public void insert(CacheImagen a)
        {
            dbConn.InsertOrReplace(a);
        }

        public CacheImagen getCacheImage(string url)
        {
            var resp = dbConn.Query<CacheImagen>("select * from Asignatura where url = " + url);
            if (resp.Count > 0)
                return resp[0];
            else
                return null;
        }
    }
}
