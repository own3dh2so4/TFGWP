using PrTab.Model.Comunicacion;
using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.ViewModel
{
    class TemasAsignaturaViewModel:NotificationenabledObject
    {
        private List<Tema> temasAsignatura;
        private string idAsignatura;
        Comunicacion_Examen servicioTema = new Comunicacion_Examen();

        public List<Tema> Temas
        {
            get
            {
                return temasAsignatura;
            }
            private set
            {
                temasAsignatura = value;
                this.OnPropertyChanged("Temas");
            }
        }

        public TemasAsignaturaViewModel( string tema)
        {
            idAsignatura = tema;
            temasAsignatura = new List<Tema>();
            servicioTema.getTemasCompletado += (s, a) =>
                {
                    Temas = a.temas;
                };
            servicioTema.getThemeFromDataBase(tema);
        }

        public async void cargarTemasServidor()
        {
            await servicioTema.getThemeFromServer(idAsignatura);
        }
    }
}
