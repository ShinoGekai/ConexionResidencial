using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbOpcionVotacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdVotacion { get; set; }
    }
}
  