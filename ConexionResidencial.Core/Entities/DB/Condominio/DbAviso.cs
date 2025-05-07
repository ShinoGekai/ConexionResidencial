using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbAviso
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
        public int IdUsuario { get; set; }
        public int IdCondominio { get; set; }
    }
}
