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

namespace PrTab.ViewModel
{
    class ExamenApuntesViewModel : NotificationenabledObject
    {
        public ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();

        public ObservableCollection<Documento> Documentos
        {
            get
            {
                return documentos;
            }

            private set
            {
                documentos = value;
                this.OnPropertyChanged("Documentos");
            }
        }

        public ExamenApuntesViewModel(string idSubject, string idTheme, string type)
        {
            obtenerExamenApuntes(idSubject, idTheme, type);
        }

        private async void obtenerExamenApuntes(string idSubject, string idTheme, string type)
        {
            var doc = await Comunicacion.getDocuments(AplicationSettings.getToken(), idSubject, idTheme, type);

            foreach (var d in doc)
                documentos.Add(d);
            this.OnPropertyChanged("Documentos");
        }
    }
}
