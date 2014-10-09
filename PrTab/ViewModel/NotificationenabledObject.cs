using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace PrTab.ViewModel
{
    /**
     * Calse de la que heredan todas aquellas que tienen un objeto que va a salir en la vista
     * y hay que notificar cuando cambia.
     * */
    public class NotificationenabledObject : INotifyPropertyChanged
    {
        //Evento que se lanza cuando cambia.
        public event PropertyChangedEventHandler PropertyChanged;

        //Metodo para lanzar el evento.
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
