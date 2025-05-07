using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using ConexionResidencial.Core.Repositories;
using ConexionResidencial.Infraestructure.DataBase;
using System.Linq;
using ConexionResidencial.Core.Entities.View;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.Request;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ConexionResidencial.Core.Entities.View.Condominio;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.Request.Condominio;
//using System.ServiceModel.Channels;
using Microsoft.Data.SqlClient.Server;

namespace ConexionResidencial.Infraestructure.Repository
{
    public class CondominiosRepository : ICondominiosRepository
    {
        private readonly DB_Context _context;
        private string? _Connection = null;
        private SqlConnection? _ConexionSql = null;

        public CondominiosRepository(IConfiguration configuration, DB_Context context)
        {
            _context = context;
        }

        public ViewCondominio getAnuncios(int condominio)
        {
            ViewCondominio resultado = new ViewCondominio();
            try
            {
                DbCondominio db = new DbCondominio();

                db = _context.DbCondominio.Where(w => w.Id == condominio).FirstOrDefault();

                if (db != null)
                {
                    resultado.Id = db.Id;
                    resultado.Nombre = db.Nombre;
                    resultado.Logo = db.Logo;
                    resultado.Normas = db.Normas;
                    List<DbAnuncio> anuncio = new List<DbAnuncio>();
                    anuncio = _context.DbAnuncio.Where(w => w.IdCondominio == condominio && w.FechaHasta >= DateTime.Now && w.Activo).OrderByDescending(w => w.Id).ToList();

                    List<ViewAnuncio> viewAnuncios = new List<ViewAnuncio>();
                    foreach (DbAnuncio dbAnuncio in anuncio)
                    {
                        ViewAnuncio _viewAnuncios = new ViewAnuncio();

                        DbLike dbLike = new DbLike();
                        dbLike = _context.DbLike.Where(w => w.IdAnuncio == dbAnuncio.Id).FirstOrDefault();
                        int comentarioAnuncios = 0;
                        comentarioAnuncios = _context.DbComentarioAnuncio.Where(w => w.IdAnuncio == dbAnuncio.Id).OrderBy(w => w.Id).Count();
                        _viewAnuncios.Descripcion = dbAnuncio.Descripcion;
                        _viewAnuncios.FechaHasta = dbAnuncio.FechaHasta;
                        _viewAnuncios.FechaDesde = dbAnuncio.FechaDesde;
                        _viewAnuncios.Id = dbAnuncio.Id;
                        _viewAnuncios.IdCondominio = dbAnuncio.IdCondominio;
                        _viewAnuncios.Cabecera = dbAnuncio.Cabecera;
                        _viewAnuncios.Organizador = dbAnuncio.Organizador;
                        _viewAnuncios.Telefono = dbAnuncio.Telefono;
                        _viewAnuncios.Amedida = dbAnuncio.Amedida;
                        _viewAnuncios.IdTipo = dbAnuncio.IdTipo;
                        _viewAnuncios.IdUsuario = dbAnuncio.IdUsuario;
                        _viewAnuncios.Likes = dbLike != null ? dbLike.Likes : 0;
                        _viewAnuncios.CantComentarios = comentarioAnuncios;
                        _viewAnuncios.Activo = dbAnuncio.Activo;
                        viewAnuncios.Add(_viewAnuncios);
                    }

                    DbAviso dbAviso = _context.DbAviso.Where(w => w.Fecha.Month == DateTime.Now.Month && w.IdCondominio == condominio && w.Fecha.Year == DateTime.Now.Year).FirstOrDefault();
                    resultado.AvisosHoy = dbAviso != null;
                    resultado.anuncios = viewAnuncios;
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public int getCondominio(string guid)
        {
            int resultado = 0;
            try
            {
                DbCondominio db = new DbCondominio();

                db = _context.DbCondominio.Where(w => w.Guid == guid).FirstOrDefault();

                if (db != null)
                {
                    resultado = db.Id;
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public int deleteAnuncio(int idAnuncio)
        {
            int resultado = 0;
            try
            {
                DbAnuncio db = new DbAnuncio();

                db = _context.DbAnuncio.Where(w => w.Id == idAnuncio).FirstOrDefault();

                if (db != null)
                {
                    resultado = db.IdCondominio;
                    _context.Remove(db);
                    _context.SaveChanges();
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public bool createAnuncio(DbAnuncio anuncio)
        {
            bool resultado = false;
            try
            {
                DbAnuncio db = new DbAnuncio();
                db = _context.DbAnuncio.Where(w => w.Id == anuncio.Id).FirstOrDefault();
                if (db != null)
                {
                    db.Id = anuncio.Id;
                    db.FechaDesde = anuncio.FechaDesde;
                    db.FechaHasta = anuncio.FechaHasta;
                    db.IdCondominio = anuncio.IdCondominio;
                    db.Cabecera = anuncio.Cabecera;
                    db.Descripcion = anuncio.Descripcion;
                    db.Organizador = anuncio.Organizador;
                    db.Telefono = anuncio.Telefono;
                    db.Amedida = anuncio.Amedida;
                    db.IdTipo = anuncio.IdTipo;
                    db.Activo = anuncio.Activo;
                    _context.SaveChanges();
                }
                else
                {
                    _context.Add(anuncio);
                    _context.SaveChanges();
                    string tipo = anuncio.IdTipo == 0 ? "Venta" : anuncio.IdTipo == 1 ? "Anuncio" : "Recordatorio";

                    string mensaje = "a creado " + (anuncio.IdTipo == 0 ? ("una nueva " + tipo) : ("un nuevo " + tipo));
                    saveNotificaciones(mensaje, anuncio.IdUsuario, 1, 0, anuncio.IdCondominio);
                }

                return true;
            }
            catch (Exception)
            {
                return resultado;
            }

        }
        public ViewCondominioUsuario getUsuario(string usuario, string clave)
        {
            ViewCondominioUsuario resultado = new ViewCondominioUsuario();
            try
            {
                DbUsuarioCondominio _resultado =_context.DbUsuarioCondominio.Where(w => w.Nombre == usuario && w.Clave == clave).FirstOrDefault();

                if (_resultado != null)
                {
                    resultado.Rol = _resultado.Rol;
                    resultado.Nombre = _resultado.Nombre;
                    resultado.Id = _resultado.Id;
                    resultado.Activo = _resultado.Activo;
                    resultado.clave = _resultado.Clave;

                    List<ViewCondominio> viewCondominio = new List<ViewCondominio>();
                    List<DbCondominioUsuario> dbCondominio = _context.DbCondominioUsuario.Where(w => w.IdUsuario == _resultado.Id).ToList();
                    foreach (DbCondominioUsuario condominio in dbCondominio) {
                        DbCondominio db = _context.DbCondominio.Where(w => w.Id == condominio.IdCondominio).FirstOrDefault();
                        ViewCondominio _viewCondominio = new ViewCondominio();

                        if (db != null)
                        {
                            _viewCondominio.FechaCaducidad = condominio.FechaCaducidad;
                            _viewCondominio.Id = db.Id;
                            _viewCondominio.Nombre = db.Nombre;
                            _viewCondominio.Logo = db.Logo;
                            _viewCondominio.Normas = db.Normas;
                        }
                        viewCondominio.Add(_viewCondominio);
                    }
                    resultado.Condominios = viewCondominio;
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public bool createSuscripcion(PushSubscriptionModel subscription, int idCondominio, int idUsuario, int tipoSuscripcion)
        {
            bool resultado = false;
            try
            {
                DbSuscripcion db = new DbSuscripcion();
                //db = _context.DbSuscripcion.Where(w => w.IdCondominio == idCondominio && w.Endpoint == subscription.Endpoint && w.Auth == subscription.Keys.Auth && w.P256DH == subscription.Keys.P256DH && w.IdUsuario == idUsuario).FirstOrDefault();
                db = _context.DbSuscripcion.Where(w => w.Endpoint == subscription.Endpoint && w.Auth == subscription.Keys.Auth && w.P256DH == subscription.Keys.P256DH && w.TipoSuscripcion == tipoSuscripcion).FirstOrDefault();
                if (db == null)
                {
                    db = new DbSuscripcion();
                    db.Auth = subscription.Keys.Auth;
                    db.P256DH = subscription.Keys.P256DH;
                    db.Endpoint = subscription.Endpoint;
                    db.IdCondominio = idCondominio;
                    db.IdUsuario = idUsuario;
                    db.TipoSuscripcion = tipoSuscripcion;
                    _context.Add(db);
                    _context.SaveChanges();
                }
                else
                {
                    db.IdUsuario = idUsuario;
                    db.IdCondominio = idCondominio;
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return resultado;
            }
        }

        public List<DbSuscripcion> obtenerNotificaciones(int idCondominio, int idAnuncio)
        {
            List<DbSuscripcion> resultado = new List<DbSuscripcion>();
            try
            {
                if (idAnuncio != 0)
                {
                    DbAnuncio dbAnuncio = new DbAnuncio();
                    dbAnuncio = _context.DbAnuncio.Where(w => w.Id == idAnuncio).FirstOrDefault();
                    if (dbAnuncio != null)
                    {
                        resultado = _context.DbSuscripcion.Where(w => w.IdUsuario == dbAnuncio.IdUsuario).ToList();
                    }
                }
                else
                {
                    resultado = _context.DbSuscripcion.Where(w => w.IdCondominio == idCondominio).ToList();
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }
        public bool eliminarSus(int idUsuario, int tipoSuscripcion)
        {
            bool resultado = false;
            try
            {
                DbSuscripcion db = new DbSuscripcion();
                db = _context.DbSuscripcion.Where(w => w.IdUsuario == idUsuario && w.TipoSuscripcion == tipoSuscripcion).FirstOrDefault();
                if (db != null)
                {
                    _context.Remove(db);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }
        }
        public int darLike(int idAnuncio, bool like)
        {
            int resultado = 0;
            try
            {
                DbLike db = new DbLike();
                db = _context.DbLike.Where(w => w.IdAnuncio == idAnuncio).FirstOrDefault();
                if (db != null)
                {
                    db.Likes += 1;
                    _context.SaveChanges();
                }
                else
                {
                    db = new DbLike();
                    db.IdAnuncio = idAnuncio;
                    db.Likes = 1;
                    _context.Add(db);
                    _context.SaveChanges();
                }
                DbAnuncio _db = new DbAnuncio();
                _db = _context.DbAnuncio.Where(w => w.Id == idAnuncio).FirstOrDefault();
                return _db.IdCondominio;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public bool crearVotacion(RequestVotacion votacion)
        {
            bool resultado = false;
            try
            {
                DbVotacion db = new DbVotacion();
                if (votacion.Id == 0)
                {
                    db.Cabecera = votacion.Cabecera;
                    db.Descripcion = votacion.Descripcion;
                    db.Activo = true;
                    db.IdUsuario = votacion.IdUsuario;
                    db.IdCondominio = votacion.IdCondominio;
                    _context.Add(db);
                    _context.SaveChanges();

                    foreach (DbOpcionVotacion opciones in votacion.OpcionesVotacion)
                    {
                        opciones.IdVotacion = db.Id;
                        _context.AddRange(opciones);
                        _context.SaveChanges();
                    }
                    string mensaje = "a creado una nueva votación";
                    saveNotificaciones(mensaje, votacion.IdUsuario, 3, 0, votacion.IdCondominio);
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public bool cambiarEstadoVotacion(int idVotacion, bool estado)
        {
            bool resultado = false;
            try
            {
                DbVotacion db = new DbVotacion();
                db = _context.DbVotacion.Where(w => w.Id == idVotacion).FirstOrDefault();
                if (db != null)
                {
                    db.Activo = estado;
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public bool votarEnVotacion(int idOpcionVotacion, int idUsuario)
        {
            bool resultado = false;
            try
            {
                DbOpcionVotacion db = new DbOpcionVotacion();
                db = _context.DbOpcionVotacion.Where(w => w.Id == idOpcionVotacion).FirstOrDefault();
                if (db != null)
                {
                    List<DbOpcionVotacion> _db = new List<DbOpcionVotacion>();
                    _db = _context.DbOpcionVotacion.Where(w => w.IdVotacion == db.IdVotacion).ToList();

                    foreach (DbOpcionVotacion dbOpcion in _db)
                    {
                        DbSeleccionVotacion seleccion = new DbSeleccionVotacion();
                        seleccion = _context.DbSeleccionVotacion.Where(w => w.IdOpcion == dbOpcion.Id && w.IdUsuario == idUsuario).FirstOrDefault();
                        if (seleccion != null)
                        {
                            _context.Remove(seleccion);
                            _context.SaveChanges();
                        }
                        if (idOpcionVotacion == dbOpcion.Id)
                        {
                            DbSeleccionVotacion _seleccion = new DbSeleccionVotacion();
                            _seleccion.IdOpcion = idOpcionVotacion;
                            _seleccion.IdUsuario = idUsuario;
                            _context.Add(_seleccion);
                            _context.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public List<ViewVotacion> getVotaciones(int condominio, int idUsuario)
        {
            List<ViewVotacion> resultado = new List<ViewVotacion>();
            try
            {
                DbUsuarioCondominio dbUsuario = new DbUsuarioCondominio();
                dbUsuario = _context.DbUsuarioCondominio.Where(w => w.Id == idUsuario).FirstOrDefault();
                if (dbUsuario != null)
                {

                    List<DbVotacion> _db = new List<DbVotacion>();
                    _db = _context.DbVotacion.Where(w => w.IdCondominio == condominio).OrderByDescending(w => w.Id).ToList();// && (w.Activo || (!w.Activo && dbUsuario.Rol == " ADMINISTRADOR"))).OrderByDescending(w => w.Id).ToList();

                    foreach (DbVotacion db in _db)
                    {
                        DbUsuarioCondominio _dbUsuario = new DbUsuarioCondominio();
                        _dbUsuario = _context.DbUsuarioCondominio.Where(w => w.Id == idUsuario).FirstOrDefault();
                        ViewVotacion _resultado = new ViewVotacion();
                        _resultado.Activo = db.Activo;
                        _resultado.Cabecera = db.Cabecera;
                        _resultado.Id = db.Id;
                        _resultado.Descripcion = db.Descripcion;
                        _resultado.Usuario = _dbUsuario != null ? _dbUsuario.Nombre : "";

                        List<DbOpcionVotacion> opcionVotacions = new List<DbOpcionVotacion>();
                        opcionVotacions = _context.DbOpcionVotacion.Where(w => w.IdVotacion == db.Id).ToList();

                        List<ViewOpcionVotacion> _viewOpcion = new List<ViewOpcionVotacion>();
                        int sumaTotal = 0;
                        foreach (DbOpcionVotacion opcion in opcionVotacions)
                        {
                            ViewOpcionVotacion viewOpcion = new ViewOpcionVotacion();
                            viewOpcion.Id = opcion.Id;
                            viewOpcion.Descripcion = opcion.Descripcion;

                            List<DbSeleccionVotacion> seleccionVotacions = new List<DbSeleccionVotacion>();
                            seleccionVotacions = _context.DbSeleccionVotacion.Where(w => w.IdOpcion == opcion.Id).ToList();
                            sumaTotal += seleccionVotacions.Count;
                            List<ViewSeleccionVotacion> usuariosSelect = new List<ViewSeleccionVotacion>();
                            foreach (DbSeleccionVotacion dbSeleccion in seleccionVotacions)
                            {
                                DbUsuarioCondominio _usuario = new DbUsuarioCondominio();
                                _usuario = _context.DbUsuarioCondominio.Where(w => w.Id == dbSeleccion.IdUsuario).FirstOrDefault();
                                ViewSeleccionVotacion _usuariosSelect = new ViewSeleccionVotacion();
                                _usuariosSelect.NombreUsuario = _usuario != null ? _usuario.Nombre : "";
                                _usuariosSelect.IdUsuario = dbSeleccion.IdUsuario;
                                usuariosSelect.Add(_usuariosSelect);
                            }
                            viewOpcion.Votaciones = usuariosSelect;
                            _viewOpcion.Add(viewOpcion);
                        }
                        _resultado.OpcionesVotacion = _viewOpcion;
                        _resultado.Total = sumaTotal;
                        resultado.Add(_resultado);
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }
        public bool createComentarioAnuncio(DbComentarioAnuncio comentarioAnuncio, int idCondominio)
        {
            bool resultado = false;
            try
            {
                comentarioAnuncio.Fecha = DateTime.Now.AddHours(3);
                _context.Add(comentarioAnuncio);
                _context.SaveChanges();
                DbAnuncio db = new DbAnuncio();
                db = _context.DbAnuncio.Where(w => w.Id == comentarioAnuncio.IdAnuncio).FirstOrDefault();
                if (db != null && comentarioAnuncio.IdUsuario != db.IdUsuario)
                {
                    string tipo = db.IdTipo == 0 ? "venta" : db.IdTipo == 1 ? "anuncio" : "recordatorio";

                    string mensaje = "a comentado tu " + tipo + ": "+ db.Cabecera;

                    saveNotificaciones(mensaje, comentarioAnuncio.IdUsuario, 2, db.IdUsuario, idCondominio);
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public ViewAnuncio getAnuncioPorId(int idAnuncio)
        {
            ViewAnuncio resultado = new ViewAnuncio();
            try
            {
                DbAnuncio dbAnuncio = new DbAnuncio();
                dbAnuncio = _context.DbAnuncio.Where(w => w.Id == idAnuncio).FirstOrDefault();
                if (dbAnuncio != null)
                {
                    DbLike dbLike = new DbLike();
                    dbLike = _context.DbLike.Where(w => w.IdAnuncio == dbAnuncio.Id).FirstOrDefault();

                    resultado.Descripcion = dbAnuncio.Descripcion;
                    resultado.FechaHasta = dbAnuncio.FechaHasta;
                    resultado.FechaDesde = dbAnuncio.FechaDesde;
                    resultado.Id = dbAnuncio.Id;
                    resultado.IdCondominio = dbAnuncio.IdCondominio;
                    resultado.Cabecera = dbAnuncio.Cabecera;
                    resultado.Organizador = dbAnuncio.Organizador;
                    resultado.Telefono = dbAnuncio.Telefono;
                    resultado.Amedida = dbAnuncio.Amedida;
                    resultado.IdTipo = dbAnuncio.IdTipo;
                    resultado.Likes = dbLike != null ? dbLike.Likes : 0;
                    
                    List<DbComentarioAnuncio> comentarioAnuncios = new List<DbComentarioAnuncio>();
                    comentarioAnuncios = _context.DbComentarioAnuncio.Where(w => w.IdAnuncio == idAnuncio).OrderBy(w => w.Id).ToList();
                    if (comentarioAnuncios != null)
                    {
                        resultado.Comentarios = comentarioAnuncios;
                        resultado.CantComentarios = comentarioAnuncios.Count();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public bool editUsuarioPorId(DbUsuarioCondominio usuarioCondominio)
        {
            bool resultado = false;
            try
            {
                DbUsuarioCondominio dbUsuario = new DbUsuarioCondominio();
                dbUsuario = _context.DbUsuarioCondominio.Where(w => w.Id == usuarioCondominio.Id).FirstOrDefault();
                if (dbUsuario != null)
                {
                    dbUsuario.Rol = usuarioCondominio.Rol;
                    dbUsuario.Nombre = usuarioCondominio.Nombre;
                    if (usuarioCondominio.Clave.Length > 0)
                        dbUsuario.Clave = usuarioCondominio.Clave;
                    dbUsuario.Direccion = usuarioCondominio.Direccion;
                    dbUsuario.Telefono = usuarioCondominio.Telefono;
                    dbUsuario.Imagen = usuarioCondominio.Imagen;
                    dbUsuario.Activo = usuarioCondominio.Activo;
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public ViewUsuarioCondominio getUsuarioPorId(int idUsuario, PushSubscriptionModel subscription, int idCondominio)
        {
            ViewUsuarioCondominio resultado = new ViewUsuarioCondominio();
            try
            {
                DbUsuarioCondominio _resultado = new DbUsuarioCondominio();
                _resultado = _context.DbUsuarioCondominio.Where(w => w.Id == idUsuario).FirstOrDefault();

                if (_resultado != null)
                {
                    DbSuscripcion suscripcion1 = new DbSuscripcion();
                    DbSuscripcion suscripcion2 = new DbSuscripcion();
                    DbSuscripcion suscripcion3 = new DbSuscripcion();
                    DbSuscripcion suscripcion4 = new DbSuscripcion();
                    suscripcion1 = _context.DbSuscripcion.Where(w => w.IdUsuario == _resultado.Id && w.TipoSuscripcion == 1 && (w.Endpoint == subscription.Endpoint && w.P256DH == subscription.Keys.P256DH && w.Auth == subscription.Keys.Auth && (w.IdCondominio == idCondominio))).FirstOrDefault();
                    suscripcion2 = _context.DbSuscripcion.Where(w => w.IdUsuario == _resultado.Id && w.TipoSuscripcion == 2 && (w.Endpoint == subscription.Endpoint && w.P256DH == subscription.Keys.P256DH && w.Auth == subscription.Keys.Auth && (w.IdCondominio == idCondominio))).FirstOrDefault();
                    suscripcion3 = _context.DbSuscripcion.Where(w => w.IdUsuario == _resultado.Id && w.TipoSuscripcion == 3 && (w.Endpoint == subscription.Endpoint && w.P256DH == subscription.Keys.P256DH && w.Auth == subscription.Keys.Auth && (w.IdCondominio == idCondominio))).FirstOrDefault();
                    suscripcion4 = _context.DbSuscripcion.Where(w => w.IdUsuario == _resultado.Id && w.TipoSuscripcion == 4 && (w.Endpoint == subscription.Endpoint && w.P256DH == subscription.Keys.P256DH && w.Auth == subscription.Keys.Auth && (w.IdCondominio == idCondominio))).FirstOrDefault();
                    resultado.TieneSuscripcionMensajes = suscripcion2 != null;
                    resultado.TieneSuscripcionVotaciones = suscripcion3 != null;
                    resultado.TieneSuscripcionAnuncios = suscripcion1 != null;
                    resultado.TieneSuscripcionAvisos = suscripcion4 != null;
                    resultado.Rol = _resultado.Rol;
                    resultado.Nombre = _resultado.Nombre;
                    resultado.Id = _resultado.Id;
                    resultado.Direccion = _resultado.Direccion;
                    resultado.Telefono = _resultado.Telefono;
                    resultado.FechaCaducidad = _resultado.FechaCaducidad;
                    resultado.Imagen = _resultado.Imagen;
                    resultado.Activo = _resultado.Activo;
                    resultado.clave = _resultado.Clave;
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public List<ViewUsuarioCondominio> getUsuarios(int idCondominio)
        {
            List<ViewUsuarioCondominio> resultado = new List<ViewUsuarioCondominio>();
            try
            {
                List<DbCondominioUsuario> condominioUsuarios = _context.DbCondominioUsuario.Where(w => w.IdCondominio == idCondominio).ToList();
                foreach (DbCondominioUsuario dbCondominioUsuario in condominioUsuarios)
                {
                    DbUsuarioCondominio dbUsuario = _context.DbUsuarioCondominio.Where(w => w.Id == dbCondominioUsuario.IdUsuario).FirstOrDefault();

                    if (dbUsuario != null) {
                        ViewUsuarioCondominio viewUsuario = new ViewUsuarioCondominio();
                        DbSuscripcion suscripcion1 = new DbSuscripcion();
                        DbSuscripcion suscripcion2 = new DbSuscripcion();
                        DbSuscripcion suscripcion3 = new DbSuscripcion();
                        DbSuscripcion suscripcion4 = new DbSuscripcion();
                        suscripcion1 = _context.DbSuscripcion.Where(w => w.IdUsuario == dbUsuario.Id && w.TipoSuscripcion == 1 && (w.IdCondominio == idCondominio)).FirstOrDefault();
                        suscripcion2 = _context.DbSuscripcion.Where(w => w.IdUsuario == dbUsuario.Id && w.TipoSuscripcion == 2 && (w.IdCondominio == idCondominio)).FirstOrDefault();
                        suscripcion3 = _context.DbSuscripcion.Where(w => w.IdUsuario == dbUsuario.Id && w.TipoSuscripcion == 3 && (w.IdCondominio == idCondominio)).FirstOrDefault();
                        suscripcion4 = _context.DbSuscripcion.Where(w => w.IdUsuario == dbUsuario.Id && w.TipoSuscripcion == 4 && (w.IdCondominio == idCondominio)).FirstOrDefault();
                        viewUsuario.TieneSuscripcionMensajes = suscripcion2 != null;
                        viewUsuario.TieneSuscripcionVotaciones = suscripcion3 != null;
                        viewUsuario.TieneSuscripcionAnuncios = suscripcion1 != null;
                        viewUsuario.TieneSuscripcionAvisos = suscripcion4 != null;
                        viewUsuario.Rol = dbUsuario.Rol;
                        viewUsuario.Nombre = dbUsuario.Nombre;
                        viewUsuario.Id = dbUsuario.Id;
                        viewUsuario.Direccion = dbUsuario.Direccion;
                        viewUsuario.Telefono = dbUsuario.Telefono;
                        viewUsuario.FechaCaducidad = dbCondominioUsuario.FechaCaducidad;
                        viewUsuario.Imagen = dbUsuario.Imagen;
                        viewUsuario.Activo = dbUsuario.Activo;
                        resultado.Add(viewUsuario);
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public bool crearAviso(DbAviso aviso, bool eliminar)
        {
            bool resultado = false;
            try
            {
                if (eliminar)
                {
                    DbAviso _aviso = new DbAviso();
                    _aviso = _context.DbAviso.Where(w => w.Id == aviso.Id).FirstOrDefault();
                    if (_aviso != null)
                    {
                        _context.Remove(_aviso);
                        _context.SaveChanges();
                    }
                    return true;
                }
                if (aviso.Id == 0)
                {
                    _context.Add(aviso);
                    _context.SaveChanges();
                    string mensaje = "a creado un nuevo aviso en el calendario";
                    saveNotificaciones(mensaje, aviso.IdUsuario, 4, 0, aviso.IdCondominio);
                }
                else
                {
                    DbAviso _aviso = new DbAviso();
                    _aviso = _context.DbAviso.Where(w => w.Id == aviso.Id).FirstOrDefault();
                    if (_aviso != null)
                    {
                        //_aviso.Fecha = aviso.Fecha;
                        _aviso.Mensaje = aviso.Mensaje;
                        _context.SaveChanges();
                    }
                }
                return true;

            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public List<DbAviso> getAvisos(int mes, int idCondominio, int anio)
        {
            List<DbAviso> resultado = new List<DbAviso>();
            try
            {
                resultado = _context.DbAviso.Where(w => w.Fecha.Month == mes && w.IdCondominio == idCondominio && w.Fecha.Year == anio).ToList();

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }
        public bool crearEmergencia(DbEmergencia emergencia, bool eliminar)
        {
            bool resultado = false;
            try
            {
                if (eliminar)
                {
                    DbEmergencia _emergencia = new DbEmergencia();
                    _emergencia = _context.DbEmergencia.Where(w => w.Id == emergencia.Id).FirstOrDefault();
                    if (_emergencia != null)
                    {
                        _context.Remove(_emergencia);
                        _context.SaveChanges();
                    }
                    return true;
                }
                if (emergencia.Id == 0)
                {
                    _context.Add(emergencia);
                    _context.SaveChanges();
                    string mensaje = "Se han creado un nuevo número de emergencia";
                    saveNotificaciones(mensaje, 0, 6, 0, emergencia.IdCondominio);
                }
                else
                {
                    DbEmergencia _emergencia = new DbEmergencia();
                    _emergencia = _context.DbEmergencia.Where(w => w.Id == emergencia.Id).FirstOrDefault();
                    if (_emergencia != null)
                    {
                        _emergencia.Descripcion = emergencia.Descripcion;
                        _emergencia.Telefono = emergencia.Telefono;
                        _emergencia.Direccion = emergencia.Direccion;
                        _context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return resultado;
            }

        }
        public List<DbEmergencia> getEmergencias(int idCondominio)
        {
            List<DbEmergencia> resultado = new List<DbEmergencia>();
            try
            {
                resultado = _context.DbEmergencia.Where(w => w.IdCondominio == idCondominio).ToList();

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }
        }

        public bool updateNormas(string normas, int idCondominio)
        {
            bool resultado = false;
            try
            {
                DbCondominio dbCondominio = new DbCondominio();
                dbCondominio = _context.DbCondominio.Where(w => w.Id == idCondominio).FirstOrDefault();

                if (dbCondominio != null){
                    dbCondominio.Normas = normas;
                    _context.SaveChanges();
                    string mensaje = "Se han creado/modificado las normas de la comunidad";
                    saveNotificaciones(mensaje, 0, 7, 0, idCondominio);
                }

                return true;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public List<ViewNotificaciones> getNotificaciones(int idUsuario)
        {
            List<ViewNotificaciones> resultado = new List<ViewNotificaciones>();
            try
            {
                DbUsuarioCondominio dbUsuario = new DbUsuarioCondominio();
                dbUsuario = _context.DbUsuarioCondominio.Where(w => w.Id == idUsuario).FirstOrDefault();
                if (dbUsuario != null) {
                    List<DbNotificaciones> dbNotificaciones = new List<DbNotificaciones>();
                    dbNotificaciones = _context.DbNotificaciones.Where(w => w.IdUsuario == idUsuario || (w.IdUsuario == 0 && w.IdCondominio == dbUsuario.IdCondominio)).ToList();
                    foreach (DbNotificaciones notificacion in dbNotificaciones) {
                        ViewNotificaciones _resultado = new ViewNotificaciones();
                        _resultado.Fecha = notificacion.Fecha;
                        _resultado.TipoNotificacion = notificacion.TipoNotificacion;
                        _resultado.Mensaje = "El usuario " + notificacion.Usuario + " " + notificacion.Mensaje;
                        resultado.Add(_resultado);
                    }
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        public List<ViewAnuncio> getAnunciosPorUsuario(int idUsuario)
        {
            List<ViewAnuncio> resultado = new List<ViewAnuncio>();
            try
            {
                List<DbAnuncio> anuncio = new List<DbAnuncio>();
                anuncio = _context.DbAnuncio.Where(w => w.IdUsuario == idUsuario).OrderByDescending(w => w.Id).ToList();
                foreach (DbAnuncio dbAnuncio in anuncio)
                {
                    ViewAnuncio _viewAnuncios = new ViewAnuncio();

                    DbLike dbLike = new DbLike();
                    dbLike = _context.DbLike.Where(w => w.IdAnuncio == dbAnuncio.Id).FirstOrDefault();
                    int comentarioAnuncios = 0;
                    comentarioAnuncios = _context.DbComentarioAnuncio.Where(w => w.IdAnuncio == dbAnuncio.Id).OrderBy(w => w.Id).Count();
                    _viewAnuncios.Descripcion = dbAnuncio.Descripcion;
                    _viewAnuncios.FechaHasta = dbAnuncio.FechaHasta;
                    _viewAnuncios.FechaDesde = dbAnuncio.FechaDesde;
                    _viewAnuncios.Id = dbAnuncio.Id;
                    _viewAnuncios.IdCondominio = dbAnuncio.IdCondominio;
                    _viewAnuncios.Cabecera = dbAnuncio.Cabecera;
                    _viewAnuncios.Organizador = dbAnuncio.Organizador;
                    _viewAnuncios.Telefono = dbAnuncio.Telefono;
                    _viewAnuncios.Amedida = dbAnuncio.Amedida;
                    _viewAnuncios.IdTipo = dbAnuncio.IdTipo;
                    _viewAnuncios.IdUsuario = dbAnuncio.IdUsuario;
                    _viewAnuncios.Likes = dbLike != null ? dbLike.Likes : 0;
                    _viewAnuncios.CantComentarios = comentarioAnuncios;
                    _viewAnuncios.Activo = dbAnuncio.Activo;
                    resultado.Add(_viewAnuncios);
                }

                return resultado;
            }
            catch (Exception)
            {
                return resultado;
            }

        }

        private void saveNotificaciones(string mensaje, int idUsuario, int idTipo, int idUsuario2, int idCondominio)
        {
            try
            {
                DbNotificaciones notificaciones = new DbNotificaciones();

                DbUsuarioCondominio _resultado = new DbUsuarioCondominio();
                _resultado = _context.DbUsuarioCondominio.Where(w => w.Id == idUsuario).FirstOrDefault();
                notificaciones.Fecha = DateTime.Now.AddHours(3);
                notificaciones.Mensaje = mensaje;
                notificaciones.Usuario = _resultado != null ? _resultado.Nombre : "";
                notificaciones.TipoNotificacion = idTipo;
                notificaciones.IdUsuario = idUsuario2;
                notificaciones.IdCondominio = idCondominio;
                _context.Add(notificaciones);
                _context.SaveChanges();

                //1- El usuario x a creado un nuevo anuncio/venta/recordatorio
                //2- El usuario x a comentado tu anuncio/venta/recordatorio
                //3- El usuario x a creado una nueva votación
                //4- El usuario x a creado un nuevo aviso en el calendario
                //5- El usuario x a creado un nuevo numero de emergencia
                //6- El usuario x a creado/modificado las normas de la comunidad
            }
            catch (Exception)
            {
            }
        }
    }
}
