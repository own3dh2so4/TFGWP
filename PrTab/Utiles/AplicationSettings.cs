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

        const string idTablonMensajes = "TablonMensajesID";

        const string idFacultadUsuario = "FacultadID";

        const string idUniversidadUsuario = "UniversidadID";

        const string idUsuarioRegistrado = "UsuarioID";

        const string numeroPreguntasExamen = "NumeroPreguntasExamen";

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

        public static string getNumeroDePreguntasExamen()
        {
            string ret = "10";
            if (IsolatedStorageSettings.ApplicationSettings.Contains(numeroPreguntasExamen))
                ret = IsolatedStorageSettings.ApplicationSettings[numeroPreguntasExamen].ToString();
            return ret;
        }

        public static void setNumeroDePreguntasExamen(string numPreguntas)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(numeroPreguntasExamen))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(numeroPreguntasExamen);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(idTablonMensajes, numPreguntas);
        }

        public static string getIdTablonMensajes()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idTablonMensajes))
                ret = IsolatedStorageSettings.ApplicationSettings[idTablonMensajes].ToString();
            return ret;
        }

        public static void setIdTablonMensaje(string id)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idTablonMensajes))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(idTablonMensajes);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(idTablonMensajes, id);
        }

        public static string getIdFacultadUsuario()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idFacultadUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[idFacultadUsuario].ToString();
            return ret;
        }

        public static void setIdFacultadUsuario(string id)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idFacultadUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(idFacultadUsuario);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(idFacultadUsuario, id);
        }

        public static string getIdUniversidadUsuario()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idUniversidadUsuario))
                ret = IsolatedStorageSettings.ApplicationSettings[idUniversidadUsuario].ToString();
            return ret;
        }

        public static void setIdUniversidadUsuario(string id)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idUniversidadUsuario))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(idUniversidadUsuario);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(idUniversidadUsuario, id);
        }

        public static string getIdUsuario()
        {
            string ret = null;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idUsuarioRegistrado))
                ret = IsolatedStorageSettings.ApplicationSettings[idUsuarioRegistrado].ToString();
            return ret;
        }

        public static void setIdUsuario(string id)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(idUsuarioRegistrado))
            {
                IsolatedStorageSettings.ApplicationSettings.Remove(idUsuarioRegistrado);
            }
            IsolatedStorageSettings.ApplicationSettings.Add(idUsuarioRegistrado, id);
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
