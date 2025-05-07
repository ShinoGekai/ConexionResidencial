using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB
{
    public class DbSuscripcion
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
        public int IdCondominio { get; set; }
        public int IdUsuario { get; set; }
        public int TipoSuscripcion { get; set; }
    }
}
