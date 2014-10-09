﻿using PrTab.Model;
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
            servicioMensajes.getMensajesTablonCompletado += (s, a) =>
            {
                mensajes = new ObservableCollection<MensajeTablon>(a.mensajes);
                this.OnPropertyChanged("Mensajes");
            };
            this.getMensajesTablon = new ActionCommand(this.onGetMensajesTablon);

        }

        //Cargar mensajes.
        private void onGetMensajesTablon()
        {
            
            servicioMensajes.getMensajesTablon();
        }
    }
}

