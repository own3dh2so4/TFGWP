using PrTab.Model.Base_de_Datos;
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
    class AsignaturasExamenViewModel : NotificationenabledObject
    {
        public ObservableCollection<Asignatura> asignaturas;

        private CDB_AsignaturaExamen BD_Asignaturas = new CDB_AsignaturaExamen();

        public ObservableCollection<Asignatura> Asignaturas
        {
            get
            {
                if (asignaturas == null)
                    asignaturas = new ObservableCollection<Asignatura>();
                if (DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0; i < 10; i++)
                        asignaturas.Add(new Asignatura(i,"asignatura"+i,"asi"+i,i,"año"+i));
                }
                return asignaturas;
            }
            private set
            {
                asignaturas = value;
                this.OnPropertyChanged("Asignaturas");
            }
        }

        public AsignaturasExamenViewModel()
        {
            asignaturas = new ObservableCollection<Asignatura>();
            obtenerAsignaturas();
        }

        private void obtenerAsignaturas()
        {
            var asignaturasFromDB = BD_Asignaturas.getAsignaturasExamen();
            foreach (var asignaturaInsertar in asignaturasFromDB)
            {
                Asignaturas.Add(asignaturaInsertar);
            }
        }
    }
}
