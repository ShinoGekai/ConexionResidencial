using ConexionResidencial.Core.Entities.DB.Condominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionResidencial.Core.Entities.Request.Condominio
{
    public class RequestNotifAvisos
    {
        public string Mensaje { get; set; }
        public int IdCondominio { get; set; }
        public string Usuario { get; set; }
    }
}
