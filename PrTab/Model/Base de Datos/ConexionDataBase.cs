using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace PrTab.Model.Base_de_Datos
{
    abstract class ConexionDataBase
    {
        protected static string DB_PATH;
        protected SQLiteConnection dbConn;

        protected ConexionDataBase(string path)
        {
            DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, path));
            dbConn = new SQLiteConnection(DB_PATH);
        }

    }
}
