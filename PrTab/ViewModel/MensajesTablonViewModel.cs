using PrTab.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PrTab.ViewModel
{
    /**
     * Clase que implementa el ViewModel del patron MVVM para el apartado de Mensajes del Tablon.
     * */
    public class MensajesTablonViewModel : NotificationenabledObject
    {
        //Mensajes del tablon
        public ObservableCollection<MensajeTablon> mensajes;

        //Getter y Setter
        public ObservableCollection<MensajeTablon> Mensajes
        {
            get
            {
                if (mensajes == null)
                {
                    mensajes = new ObservableCollection<MensajeTablon>();
                }
                if (DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0; i < 10; i++)
                        mensajes.Add(new MensajeTablon { nombre = "David" + i.ToString(), mensaje = "Hola" + i.ToString(), foto = "fotos/foto.jpg" });
                }
                return mensajes;
            }
            set
            {
                mensajes = value;
                this.OnPropertyChanged();
            }
        }

        //Objeto que sde comunica con el servidor
        Comunicacion_MensajesTablon servicioMensajes = new Comunicacion_MensajesTablon();

       
        //Comando que se ejecuta al pulsar el boton
        public ICommand getMensajesTablon
        {
            get;
            private set;
        }

        public ICommand postMensajesTablon
        {
            get;
            private set;
        }
        /*public ICommand GetMensajesTablon
        {
            get
            {
                if (getMensajesTablon == null)
                {
                    getMensajesTablon = new ActionCommand(() =>
                    {
                        servicioMensajes.getMensajesTablon();
                    });
                }

                return getMensajesTablon;
            }
        }*/

        //Constructor.
        public MensajesTablonViewModel()
        {
            mensajes = new ObservableCollection<MensajeTablon>();
            servicioMensajes.getMensajesTablonCompletado += (s, a) =>
            {
                //mensajes = new ObservableCollection<MensajeTablon>(a.mensajes);
                insertarNuevosMensajes(a.mensajes);
                this.OnPropertyChanged("Mensajes");
            };
            this.getMensajesTablon = new ActionCommand<string>(this.onGetMensajesTablon);
            this.postMensajesTablon = new ActionCommand<string>(this.onPostMensajesTablon);
            servicioMensajes.getMensajesTablon();

        }

        private void insertarNuevosMensajes(IList<MensajeTablon> nuevosMensajes)
        {
            for (int i = 0; i < nuevosMensajes.Count; i++)
                mensajes.Insert(i, nuevosMensajes[i]);
        }

        //Cargar mensajes.
        private void onGetMensajesTablon(string useless)
        {
            
            servicioMensajes.getMensajesTablonFromServer("1");
            //servicioMensajes.getMensajesTablon();
        }

        private void onPostMensajesTablon(string message)
        {
            servicioMensajes.postMensajeTablon(message, "1");
        }
    }
}

