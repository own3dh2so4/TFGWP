using PrTab.Model.Base_de_Datos;
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

        private async void obtenerAsignaturas()
        {
            var asignaturasFromDB = BD_Asignaturas.getAsignaturasExamen();
            if (asignaturasFromDB.Count == 0)
            {
                asignaturasFromDB = await Comunicacion_Asignatura.getAsignaturaFavoritasFromServer(AplicationSettings.getToken());
            }
            
            foreach (var asignaturaInsertar in asignaturasFromDB)
            {
                Asignaturas.Add(asignaturaInsertar);
            }
        }

        public async void sincronizarFavConServer()
        {
            var asignaturasFromDB = await Comunicacion_Asignatura.getAsignaturaFavoritasFromServer(AplicationSettings.getToken());
            var a = new ObservableCollection<Asignatura>();
            foreach (var asignaturaInsertar in asignaturasFromDB)
            {
                a.Add(asignaturaInsertar);
            }
            Asignaturas = a;
        }
    }
}
