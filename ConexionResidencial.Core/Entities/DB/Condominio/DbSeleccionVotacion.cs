using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbSeleccionVotacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdOpcion { get; set; }
    }
}
  