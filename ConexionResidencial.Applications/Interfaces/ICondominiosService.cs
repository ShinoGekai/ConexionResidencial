using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities.Request;
using ConexionResidencial.Core.Entities.Request.Condominio;
using ConexionResidencial.Core.Entities.View;
using ConexionResidencial.Core.Entities.View.Condominio;

namespace ConexionResidencial.Applications.Interfaces
{
    public interface ICondominiosService
    {
        ViewCondominio getAnuncios(int condominio);
        int getCondominio(string guid);
        int deleteAnuncio(int idAnuncio);
        bool createAnuncio(DbAnuncio anuncio);
        ViewCondominioUsuario getUsuario(string usuario, string clave);
        bool createSuscripcion(PushSubscriptionModel subscription, int idCondominio, int idUsuario, int tipoSuscripcion);
        List<DbSuscripcion> obtenerNotificaciones(int idCondominio, int idAnuncio);
        bool eliminarSus(int idUsuario, int tipoSuscripcion);
        int darLike(int idAnuncio, bool like);
        bool crearVotacion(RequestVotacion votacion);
        bool cambiarEstadoVotacion(int idVotacion, bool estado);
        bool votarEnVotacion(int idOpcionVotacion, int idUsuario);
        List<ViewVotacion> getVotaciones(int condominio, int idUsuario);
        bool createComentarioAnuncio(DbComentarioAnuncio comentarioAnuncio, int idCondominio);
        ViewAnuncio getAnuncioPorId(int idAnuncio);
        bool editUsuarioPorId(DbUsuarioCondominio usuarioCondominio);
        ViewUsuarioCondominio getUsuarioPorId(int idUsuario, PushSubscriptionModel subscription, int idCondominio);
        List<ViewUsuarioCondominio> getUsuarios(int idCondominio);
        bool crearAviso(DbAviso aviso, bool eliminar);
        List<DbAviso> getAvisos(int mes, int idCondominio, int anio);
        bool crearEmergencia(DbEmergencia emergencia, bool eliminar);
        List<DbEmergencia> getEmergencias(int idCondominio);
        bool updateNormas(string normas, int idCondominio);
        List<ViewNotificaciones> getNotificaciones(int idUsuario);
        List<ViewAnuncio> getAnunciosPorUsuario(int idUsuario);
    }
}