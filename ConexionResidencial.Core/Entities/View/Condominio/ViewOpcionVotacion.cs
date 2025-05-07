using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class ViewOpcionVotacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<ViewSeleccionVotacion> Votaciones { get; set; }
    }
}
  