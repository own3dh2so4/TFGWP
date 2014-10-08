using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.ViewModel
{
    public static class AplicationSettings
    {
        const string nombreUsuario = "Usuario";
        const string contraseñaUsuario = "Contraseña";

        public static void RegistrarUsuario(string usuario, string contraseña)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(nombreUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(nombreUsuario);
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains(contraseñaUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(contraseñaUsuario);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(nombreUsuario, usuario);
            IsolatedStorageSettings.ApplicationSettings.Add(contraseñaUsuario, contraseña);
        }

        public static string getUsuario()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(nombreUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[nombreUsuario].ToString();
            return ret;
        }

        public static string getContraseña()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(contraseñaUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[contraseñaUsuario].ToString();
            return ret;
        }

    }

        
}
