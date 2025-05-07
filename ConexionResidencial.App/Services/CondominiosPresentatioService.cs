using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConexionResidencial.Applications.Interfaces;
using ConexionResidencial.App.Interfaces;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.View.Condominio;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.Request.Condominio;

namespace ConexionResidencial.Services
{
    public class CondominiosPresentatioService : ICondominiosPresentationService
    {
        private readonly ICondominiosService _listadoService;
        private readonly IWebHostEnvironment _host;

        public CondominiosPresentatioService(IWebHostEnvironment host, ICondominiosService listadoService)
        {
            _listadoService = listadoService;
        }

        public ViewCondominio getAnuncios(int condominio)
        {
            try
            {
                return _listadoService.getAnuncios(condominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int getCondominio(string guid)
        {
            try
            {
                return _listadoService.getCondominio(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ViewCondominio deleteAnuncio(int idAnuncio)
        {
            try
            {
                int result = _listadoService.deleteAnuncio(idAnuncio);

                return _listadoService.getAnuncios(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ViewCondominio createAnuncio(DbAnuncio anuncio)
        {
            try
            {
                bool result = _listadoService.createAnuncio(anuncio);
                return _listadoService.getAnuncios(anuncio.IdCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ViewCondominioUsuario getUsuario(string usuario, string clave)
        {
            try
            {
                return _listadoService.getUsuario(usuario, clave);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool createSuscripcion(PushSubscriptionModel subscription, int idCondominio, int idUsuario, int tipoSuscripcion)
        {
            try
            {
                return _listadoService.createSuscripcion(subscription, idCondominio, idUsuario, tipoSuscripcion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DbSuscripcion> obtenerNotificaciones(int idCondominio, int idAnuncio)
        {
            try
            {
                return _listadoService.obtenerNotificaciones(idCondominio, idAnuncio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool eliminarSus(int idUsuario, int tipoSuscripcion)
        {
            try
            {
                return _listadoService.eliminarSus(idUsuario, tipoSuscripcion);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ViewCondominio darLike(int idAnuncio, bool like)
        {
            try
            {
                int result = _listadoService.darLike(idAnuncio, like);
                return _listadoService.getAnuncios(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewVotacion> crearVotacion(RequestVotacion votacion)
        {
            try
            {
                var result = _listadoService.crearVotacion(votacion);
                return _listadoService.getVotaciones(votacion.IdCondominio, votacion.IdUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewVotacion> cambiarEstadoVotacion(int idVotacion, bool estado, int idCondominio, int idUsuario)
        {
            try
            {
                var result = _listadoService.cambiarEstadoVotacion(idVotacion, estado);
                return _listadoService.getVotaciones(idCondominio, idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewVotacion> votarEnVotacion(int idOpcionVotacion, int idUsuario, int idCondominio)
        {
            try
            {
                var result = _listadoService.votarEnVotacion(idOpcionVotacion, idUsuario);
                return _listadoService.getVotaciones(idCondominio, idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewVotacion> getVotaciones(int condominio, int idUsuario)
        {
            try
            {
                return _listadoService.getVotaciones(condominio, idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool createComentarioAnuncio(DbComentarioAnuncio comentarioAnuncio, int idCondominio)
        {
            try
            {
                return _listadoService.createComentarioAnuncio(comentarioAnuncio, idCondominio);
                //return _listadoService.getAnuncios(idCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ViewAnuncio getAnuncioPorId(int idAnuncio)
        {
            try
            {
                return _listadoService.getAnuncioPorId(idAnuncio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool editUsuarioPorId(DbUsuarioCondominio usuarioCondominio)
        {
            try
            {
                return _listadoService.editUsuarioPorId(usuarioCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ViewUsuarioCondominio getUsuarioPorId(int idUsuario, PushSubscriptionModel subscription, int idCondominio)
        {
            try
            {
                return _listadoService.getUsuarioPorId(idUsuario, subscription, idCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewUsuarioCondominio> getUsuarios(int idCondominio)
        {
            try
            {
                return _listadoService.getUsuarios(idCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DbAviso> crearAviso(DbAviso aviso, bool eliminar)
        {
            try
            {
                var result = _listadoService.crearAviso(aviso, eliminar);
                return _listadoService.getAvisos(aviso.Fecha.Month, aviso.IdCondominio, aviso.Fecha.Year);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DbAviso> getAvisos(int mes, int idCondominio, int anio)
        {
            try
            {
                return _listadoService.getAvisos(mes, idCondominio, anio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DbEmergencia> crearEmergencia(DbEmergencia emergencia, bool eliminar)
        {
            try
            {
                var result = _listadoService.crearEmergencia(emergencia, eliminar);
                return _listadoService.getEmergencias(emergencia.IdCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<DbEmergencia> getEmergencias(int idCondominio)
        {
            try
            {
                return _listadoService.getEmergencias(idCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ViewCondominio updateNormas(string normas, int idCondominio)
        {
            try
            {
                var result = _listadoService.updateNormas(normas, idCondominio);
                return _listadoService.getAnuncios(idCondominio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewNotificaciones> getNotificaciones(int idUsuario)
        {
            try
            {
                return _listadoService.getNotificaciones(idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ViewAnuncio> getAnunciosPorUsuario(int idUsuario)
        {
            try
            {
                return _listadoService.getAnunciosPorUsuario(idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
