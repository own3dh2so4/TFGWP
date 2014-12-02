using Microsoft.Phone.Shell;
using PrTab.Model;
using PrTab.Model.Comunicacion;
using PrTab.Model.Modelo;
using PrTab.Utiles;
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

        System.Windows.Visibility visibilidadMensaje;
        public System.Windows.Visibility VisibilidadMensaje
        {
            get
            {
                return visibilidadMensaje;
            }
            private set
            {
                visibilidadMensaje = value;
                
            }
        }

        string mensajeMostrar="";

        public string MensajeMostrar
        {
            get
            {
                return mensajeMostrar;
            }
            private set
            {
                mensajeMostrar = value;
                
            }
        }

        private void mostarMensaje(string menssage)
        {
            VisibilidadMensaje = Visibility.Visible;
            mensajeMostrar = menssage;
            this.OnPropertyChanged("VisibilidadMensaje");
            this.OnPropertyChanged("MensajeMostrar");
        }

        private void ocultarMensaje()
        {
            visibilidadMensaje = Visibility.Collapsed;
            mensajeMostrar = "";
            this.OnPropertyChanged("VisibilidadMensaje");
            this.OnPropertyChanged("MensajeMostrar");
        }

        //Getter y Setter
        public ObservableCollection<MensajeTablon> Mensajes
        {
            get
            {
                if (mensajes == null)
                {
                    mensajes = new ObservableCollection<MensajeTablon>();
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
        /*public ICommand getMensajesTablon
        {
            get;
            private set;
        }

        public ICommand postMensajesTablon
        {
            get;
            private set;
        }*/
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
                servicioMensajes.getMensajesTablonFromServer(AplicationSettings.getIdTablonMensajes());
                //this.OnPropertyChanged("Mensajes");
            };

            servicioMensajes.getMensajesTablonServerCompletado += (s, a) =>
            {
                insertarNuevosMensajes(a.mensajes);
                this.OnPropertyChanged("Mensajes");
            };
            /*this.getMensajesTablon = new ActionCommand<string>(this.onGetMensajesTablon);
            this.postMensajesTablon = new ActionCommand<string>(this.onPostMensajesTablon);*/

            visibilidadMensaje =  Visibility.Collapsed;
            servicioMensajes.getMensajesTablon();

        }


        private void insertarNuevosMensajes(IList<MensajeTablon> nuevosMensajes)
        {
            for (int i = 0; i < nuevosMensajes.Count; i++)
                mensajes.Insert(i, nuevosMensajes[i]);
        }

        //Cargar mensajes.
        private async void onGetMensajesTablon(string useless)
        {
            mostarMensaje("Cargando mensajes");
            await servicioMensajes.getMensajesTablonFromServer(AplicationSettings.getIdTablonMensajes());
            //servicioMensajes.getMensajesTablon();
            //System.Threading.Thread.Sleep(5000);
            ocultarMensaje();
        }

        private async void onPostMensajesTablon(string message)
        {
            mostarMensaje("Enviando mensaje");
            await servicioMensajes.postMensajeTablon(message, AplicationSettings.getIdTablonMensajes());
            ocultarMensaje();
        }

        public async void CargarMensajesTablon()
        {
            mostarMensaje("Cargando mensajes");
            await servicioMensajes.getMensajesTablonFromServer(AplicationSettings.getIdTablonMensajes());
            //servicioMensajes.getMensajesTablon();
            //System.Threading.Thread.Sleep(5000);
            ocultarMensaje();
        }

        public async void EnviarMensajeTablon(string message)
        {
            mostarMensaje("Enviando mensaje");
            await servicioMensajes.postMensajeTablon(message, AplicationSettings.getIdTablonMensajes());
            ocultarMensaje();
        }

        public async Task<bool> addFavMessage(MensajeTablon m)
        {
            foreach (var men in mensajes)
            {
                if (men.identificador == m.identificador)
                {
                    if (men.userFav)
                        men.numFav--;
                    else
                        men.numFav++;
                    men.userFav = !men.userFav;
                    await servicioMensajes.favMesajeTablon(men);
                    this.OnPropertyChanged("Mensajes");
                    return true;
                }
            }

            return false;
        }
    }
}

