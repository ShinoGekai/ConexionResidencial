using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities.Request;
using ConexionResidencial.Core.Entities.Request.Condominio;
using ConexionResidencial.Core.Entities.View;
using ConexionResidencial.Core.Entities.View.Condominio;
using Microsoft.AspNetCore.Mvc;

namespace ConexionResidencial.App.Interfaces
{
    public interface ICondominiosPresentationService
    {
        ViewCondominio getAnuncios(int condominio);
        int getCondominio(string guid);
        ViewCondominio deleteAnuncio(int idAnuncio);
        ViewCondominio createAnuncio(DbAnuncio anuncio);
        ViewCondominioUsuario getUsuario(string usuario, string clave);
        bool createSuscripcion(PushSubscriptionModel subscription, int idCondominio, int idUsuario, int tipoSuscripcion);
        List<DbSuscripcion> obtenerNotificaciones(int idCondominio, int idAnuncio);
        bool eliminarSus(int idUsuario, int tipoSuscripcion);
        ViewCondominio darLike(int idAnuncio, bool like);
        List<ViewVotacion> crearVotacion(RequestVotacion votacion);
        List<ViewVotacion> cambiarEstadoVotacion(int idVotacion, bool estado, int idCondominio, int idUsuario);
        List<ViewVotacion> votarEnVotacion(int idOpcionVotacion, int idUsuario, int idCondominio);
        List<ViewVotacion> getVotaciones(int condominio, int idUsuario);
        bool createComentarioAnuncio(DbComentarioAnuncio comentarioAnuncio, int idCondominio);
        ViewAnuncio getAnuncioPorId(int idAnuncio);
        bool editUsuarioPorId(DbUsuarioCondominio usuarioCondominio);
        ViewUsuarioCondominio getUsuarioPorId(int idUsuario, PushSubscriptionModel subscription, int idCondominio);
        List<ViewUsuarioCondominio> getUsuarios(int idCondominio);
        List<DbAviso> crearAviso(DbAviso aviso, bool eliminar);
        List<DbAviso> getAvisos(int mes, int idCondominio, int anio);
        List<DbEmergencia> crearEmergencia(DbEmergencia emergencia, bool eliminar);
        List<DbEmergencia> getEmergencias(int idCondominio);
        ViewCondominio updateNormas(string normas, int idCondominio);
        List<ViewNotificaciones> getNotificaciones(int idUsuario);
        List<ViewAnuncio> getAnunciosPorUsuario(int idUsuario);
    }
}
