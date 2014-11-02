using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Utiles
{
    /**
     * Clase que encagada de guardar la información de la aplicacion.
     * */
    public static class AplicationSettings
    {
        //Nombre con el que se guarda el usuario.
        const string nombreUsuario = "Usuario";
        //Nombre con el que se guarda la contraseña.
        const string contraseñaUsuario = "Contraseña";

        const string tokenUsuario = "Token";

        const string errorServer = "Error";

        //Al logear un nuevo usuario y ser los datos correctos, estos se guardan en el movil
        //para no volver a pedirlos.
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

        //Devuelve el nombre del usuario registrado.
        public static string getUsuario()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(nombreUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[nombreUsuario].ToString();
            return ret;
        }

        //Devuelve la contraseña del usuario registrado.
        public static string getContraseña()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(contraseñaUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[contraseñaUsuario].ToString();
            return ret;
        }

        public static void setToken(string token)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(tokenUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(tokenUsuario);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(tokenUsuario, token);
        }

        public static string getToken()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(tokenUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[tokenUsuario].ToString();
            return ret;
        }


        public static void setErrorServer(string error)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(errorServer))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(errorServer);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(errorServer, error);
        }

        public static string getErrorServer()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(errorServer))
                ret = IsolatedStorageSettings.ApplicationSettings[errorServer].ToString();
            return ret;
        }

        public static void logout()
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(nombreUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(nombreUsuario);
            }
            if (IsolatedStorageSettings.ApplicationSettings.Contains(contraseñaUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(contraseñaUsuario);
            }
        }


    }

        
}
