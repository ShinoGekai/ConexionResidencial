using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.View.Condominio
{
    public class ViewCondominioUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public string? clave { get; set; }
        public List<ViewCondominio> Condominios { get; set;}
    }
}
