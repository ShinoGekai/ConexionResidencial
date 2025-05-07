using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbEmergencia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Telefono { get; set; }
        public int IdCondominio { get; set; }
        public string? Direccion { get; set; }
    }
}
