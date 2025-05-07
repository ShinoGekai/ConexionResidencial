using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbVotacion
    {
        public int Id { get; set; }
        public string Cabecera { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public int IdCondominio { get; set; }
    }
}
