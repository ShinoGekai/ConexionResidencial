using ConexionResidencial.Applications.Interfaces;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Repositories;
using ConexionResidencial.Core.Entities.View.Condominio;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.Request.Condominio;

namespace ConexionResidencial.Applications.Services
{
    public class CondominioService : ICondominiosService
    {
        private readonly ICondominiosRepository _listadoRepository;
        public CondominioService(ICondominiosRepository listadoRepository)
        {
            _listadoRepository = listadoRepository;
        }

        public ViewCondominio getAnuncios(int condominio)
        {
            try
            {
                ViewCondominio resultado = new ViewCondominio();
                resultado = _listadoRepository.getAnuncios(condominio);
                return resultado;
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
                int resultado = 0;
                resultado = _listadoRepository.getCondominio(guid);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int deleteAnuncio(int idAnuncio)
        {
            try
            {
                int resultado = 0;
                resultado = _listadoRepository.deleteAnuncio(idAnuncio);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool createAnuncio(DbAnuncio anuncio)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.createAnuncio(anuncio);
                return resultado;
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
                ViewCondominioUsuario resultado = new ViewCondominioUsuario();
                resultado = _listadoRepository.getUsuario(usuario, clave);
                return resultado;
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
                bool resultado = false;
                resultado = _listadoRepository.createSuscripcion(subscription, idCondominio, idUsuario, tipoSuscripcion);
                return resultado;
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
                List<DbSuscripcion> resultado = new List<DbSuscripcion>();
                resultado = _listadoRepository.obtenerNotificaciones(idCondominio, idAnuncio);
                return resultado;
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
                bool resultado = false;
                resultado = _listadoRepository.eliminarSus(idUsuario, tipoSuscripcion);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int darLike(int idAnuncio, bool like)
        {
            try
            {
                int resultado = 0;
                resultado = _listadoRepository.darLike(idAnuncio, like);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool crearVotacion(RequestVotacion votacion)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.crearVotacion(votacion);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool cambiarEstadoVotacion(int idVotacion, bool estado)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.cambiarEstadoVotacion(idVotacion, estado);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool votarEnVotacion(int idOpcionVotacion, int idUsuario)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.votarEnVotacion(idOpcionVotacion, idUsuario);
                return resultado;
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
                List<ViewVotacion> resultado = new List<ViewVotacion>();
                resultado = _listadoRepository.getVotaciones(condominio, idUsuario);
                return resultado;
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
                bool resultado = false;
                resultado = _listadoRepository.createComentarioAnuncio(comentarioAnuncio, idCondominio);
                return resultado;
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
                ViewAnuncio resultado = new ViewAnuncio();
                resultado = _listadoRepository.getAnuncioPorId(idAnuncio);
                return resultado;
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
                bool resultado = false;
                resultado = _listadoRepository.editUsuarioPorId(usuarioCondominio);
                return resultado;
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
                ViewUsuarioCondominio resultado = new ViewUsuarioCondominio();
                resultado = _listadoRepository.getUsuarioPorId(idUsuario, subscription, idCondominio);
                return resultado;
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
                List<ViewUsuarioCondominio> resultado = new List<ViewUsuarioCondominio>();
                resultado = _listadoRepository.getUsuarios(idCondominio);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool crearAviso(DbAviso aviso, bool eliminar)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.crearAviso(aviso, eliminar);
                return resultado;
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
                List<DbAviso> resultado = new List<DbAviso>();
                resultado = _listadoRepository.getAvisos(mes, idCondominio, anio);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool crearEmergencia(DbEmergencia emergencia, bool eliminar)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.crearEmergencia(emergencia, eliminar);
                return resultado;
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
                List<DbEmergencia> resultado = new List<DbEmergencia>();
                resultado = _listadoRepository.getEmergencias(idCondominio);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool updateNormas(string normas, int idCondominio)
        {
            try
            {
                bool resultado = false;
                resultado = _listadoRepository.updateNormas(normas, idCondominio);
                return resultado;
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
                List<ViewNotificaciones> resultado = new List<ViewNotificaciones>();
                resultado = _listadoRepository.getNotificaciones(idUsuario);
                return resultado;
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
                List<ViewAnuncio> resultado = new List<ViewAnuncio>();
                resultado = _listadoRepository.getAnunciosPorUsuario(idUsuario);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
