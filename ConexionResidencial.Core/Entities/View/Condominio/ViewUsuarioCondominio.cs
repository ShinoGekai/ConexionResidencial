using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.View.Condominio
{
    public class ViewUsuarioCondominio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool TieneSuscripcionAnuncios { get; set; }
        public bool TieneSuscripcionMensajes { get; set; }
        public bool TieneSuscripcionVotaciones { get; set; }
        public bool TieneSuscripcionAvisos { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public string? Imagen { get; set; }
        public string? clave { get; set; }
    }
}
