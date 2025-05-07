using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.DB.Condominio
{
    public class DbUsuarioCondominio
    {
        public int Id { get; set; }
        public int IdCondominio { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string? Imagen { get; set; }
    }
}
