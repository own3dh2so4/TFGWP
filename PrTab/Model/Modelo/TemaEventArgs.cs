using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Modelo
{
    class TemaEventArgs : EventArgs
    {

        public List<Tema> temas { get; set; }

        public TemaEventArgs(List<Tema> listaTemas)
        {
            this.temas = listaTemas;
        }
    }

    class TemaEventServerArgs : EventArgs
    {

        public List<Tema> temas { get; set; }

        public TemaEventServerArgs(List<Tema> listaTemas)
        {
            this.temas = listaTemas;
        }
    }
}
