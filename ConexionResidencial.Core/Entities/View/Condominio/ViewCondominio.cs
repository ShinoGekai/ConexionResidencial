using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.DB.Condominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.View.Condominio
{
    public class ViewCondominio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }
        public string? Normas { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public bool? AvisosHoy { get; set; }
        public List<ViewAnuncio> anuncios { get; set; }
    }
}
