using PrTab.Model.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrTab.Model.Base_de_Datos
{
    class CDB_AsignaturaCursoAgregar:ConexionDataBase
    {
        public CDB_AsignaturaCursoAgregar()
            : base("AsignaturaCurso.sqlite")
        {
            dbConn.CreateTable<Asignatura>();
        }

        public void insertAll(List<Asignatura> list)
        {
            foreach (var asignatura in list)
                dbConn.InsertOrReplace(asignatura);
        }

        public void insert(Asignatura asignatura)
        {
            dbConn.InsertOrReplace(asignatura);
        }

        public List<Asignatura> getAsignaturasCurso()
        {
            return dbConn.Query<Asignatura>("select * from Asignatura");
        }

        public Asignatura getAsignaturaCurso(string id)
        {
            return dbConn.Query<Asignatura>("select * from Asignatura where identificador= " + id)[0];
        }

        public List<Asignatura> getAsignaturasDelCurso(string curso)
        {
            return dbConn.Query<Asignatura>("select * from Asignatura where año= " + curso);
        }

    }
}
