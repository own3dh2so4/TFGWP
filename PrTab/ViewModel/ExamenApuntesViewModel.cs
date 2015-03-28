using PrTab.Model.Modelo;
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
                if (DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0; i < 10; i++)
                        documentos.Add(new Documento());
                }
                return documentos;
            }
        }
    }
}
