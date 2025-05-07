using ConexionResidencial.Core.Entities.DB.Condominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.Request.Condominio
{
    public class ViewVotacion
    {
        public int Id { get; set; }
        public string Cabecera { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public List<ViewOpcionVotacion> OpcionesVotacion { get; set; }
        public int Total { get; set; }
    }
}
