using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbCondominio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string Guid { get; set; }
        public string? Normas { get; set; }
    }
}
