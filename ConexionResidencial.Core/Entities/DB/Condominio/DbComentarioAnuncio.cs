using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbComentarioAnuncio
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdAnuncio { get; set; }
        public string Mensaje { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}