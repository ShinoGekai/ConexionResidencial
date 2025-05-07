using ConexionResidencial.Core.Entities.DB.Condominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.View.Condominio
{
    public class ViewAnuncio
    {
        public int Id { get; set; }
        public int IdCondominio { get; set; }
        public string Cabecera { get; set; }
        public string? Descripcion { get; set; }
        public string? Organizador { get; set; }
        public string? Telefono { get; set; }
        public string? Amedida { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int IdTipo { get; set; }
        public int IdUsuario { get; set; }
        public int Likes { get; set; }
        public int CantComentarios { get; set; }
        public bool Activo { get; set; }
        public List<DbComentarioAnuncio>? Comentarios { get; set; }
    }
}
