using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConexionResidencial.App.Interfaces;
using ConexionResidencial.Core.Entities.DB.Condominio;
using ConexionResidencial.Core.Entities;
using WebPush;
using System.Text.Json;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.Request.Condominio;
using ConexionResidencial.Core.Entities.View.Condominio;

namespace ConexionResidencial.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CondominiosController : Controller
    {
        private readonly ICondominiosPresentationService _PresentationService;
        //private static List<PushSubscriptionModel> Subscriptions = new List<PushSubscriptionModel>();
        private const string PublicKey = "BDhWFTbhmhdKANFtk6FZsIE4gQE1eHAiCPvwXsE8UGCKa-U-vVh3cTzOCFtNy01QBc08mP8GcUeCLybWsD-5No0";
        private const string PrivateKey = "QeGziCAtwdRE0Jmzj2EA13w9o3bNvAkH6A3z5c06-aw";
        public CondominiosController(ICondominiosPresentationService presentationService)
        {
            _PresentationService = presentationService;
        }

        [HttpGet("getAnuncios")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getAnuncios(int condominio)
        {
            var list = _PresentationService.getAnuncios(condominio);
            return Ok(list);
        }

        [HttpGet("getCondominio")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getCondominio(string guid)
        {
            var list = _PresentationService.getCondominio(guid);
            return Ok(list);
        }

        [HttpGet("deleteAnuncio/{idAnuncio}")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult deleteAnuncio(int idAnuncio)
        {
            var list = _PresentationService.deleteAnuncio(idAnuncio);
            return Ok(list);
        }

        [HttpPost("getUsuario")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getUsuario(string usuario, string clave, int idCondominio)
        {
            var list = _PresentationService.getUsuario(usuario, clave);
            return Ok(list);
        }

        [HttpGet("saveKey")]
        [AllowAnonymous]
        public IActionResult saveKey(int condominio)
        {
            var keys = VapidHelper.GenerateVapidKeys();

            Console.WriteLine("Public Key:");
            Console.WriteLine(keys.PublicKey);
            Console.WriteLine("Private Key:");
            Console.WriteLine(keys.PrivateKey);
            return Ok(true);
        }

        [HttpGet("obtenerKey")]
        [AllowAnonymous]
        public IActionResult obtenerKey(int condominio)
        {
            return Ok(PublicKey);
        }

        [HttpPost("createAnuncio")]
        [AllowAnonymous]
        [ValidarHeaders]
        public async Task<IActionResult> createAnuncio(DbAnuncio anuncio)
        {
            var list = _PresentationService.createAnuncio(anuncio);
            if (list != null && anuncio.Activo)
            {
                NotificationMessage notificationMessage = new NotificationMessage();
                if (anuncio.Id > 0)
                {
                    string tipo = anuncio.IdTipo == 0 ? "Venta" : anuncio.IdTipo == 1 ? "Anuncio" : "Recordatorio";
                    notificationMessage.Title = anuncio.IdTipo == 0 ? ("Nueva " + tipo) : ("Nuevo " + tipo);
                    notificationMessage.Body = "El vecino " + anuncio.Organizador + " volvió a activar una publicación";
                }
                else
                {
                    string tipo = anuncio.IdTipo == 0 ? "Venta" : anuncio.IdTipo == 1 ? "Anuncio" : "Recordatorio";
                    notificationMessage.Title = anuncio.IdTipo == 0 ? ("Nueva " + tipo) : ("Nuevo " + tipo);
                    notificationMessage.Body = "El vecino " + anuncio.Organizador + " publicó algo nuevo";
                }
                await SendNotification(notificationMessage, anuncio.IdCondominio, 1);
            }
            return Ok(list);
        }

        [HttpPost("guardarSus")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult guardarSus([FromBody] PushSubscriptionModel subscription, [FromQuery] int idCondominio, [FromQuery] int idUsuario, [FromQuery] int tipoSuscripcion)
        {
            var list = _PresentationService.createSuscripcion(subscription, idCondominio, idUsuario, tipoSuscripcion);
            return Ok(list);
        }


        [HttpPost("eliminarSus")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult eliminarSus(int idUsuario, int tipoSuscripcion)
        {
            var list = _PresentationService.eliminarSus(idUsuario, tipoSuscripcion);
            return Ok(list);
        }

        [HttpPost("darLike")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult darLike(int idAnuncio, bool like)
        {
            var list = _PresentationService.darLike(idAnuncio, like);
            return Ok(list);
        }

        [HttpPost("crearVotacion")]
        [AllowAnonymous]
        [ValidarHeaders]
        public async Task<IActionResult> crearVotacion(RequestVotacion votacion)
        {
            var list = _PresentationService.crearVotacion(votacion);
            if (list != null)
            {
                NotificationMessage notificationMessage = new NotificationMessage();
                notificationMessage.Title = "Nueva votación en proceso";
                notificationMessage.Body = "Tu opinón nos importa, ven y vota ya!";
                await SendNotification(notificationMessage, votacion.IdCondominio, 2);
            }
            return Ok(list);
        }
        [HttpPost("cambiarEstadoVotacion")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult cambiarEstadoVotacion(int idVotacion, bool estado, int idCondominio, int idUsuario)
        {
            var list = _PresentationService.cambiarEstadoVotacion(idVotacion, estado, idCondominio, idUsuario);
            return Ok(list);
        }

        [HttpPost("votarEnVotacion")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult votarEnVotacion(int idOpcionVotacion, int idUsuario, int idCondominio)
        {
            var list = _PresentationService.votarEnVotacion(idOpcionVotacion, idUsuario, idCondominio);
            return Ok(list);
        }

        [HttpGet("getVotaciones")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getVotaciones(int condominio, int idUsuario)
        {
            var list = _PresentationService.getVotaciones(condominio, idUsuario);
            return Ok(list);
        }

        [HttpPost("createComentarioAnuncio")]
        [AllowAnonymous]
        [ValidarHeaders]
        public async Task<IActionResult> createComentarioAnuncio([FromBody] DbComentarioAnuncio comentarioAnuncio, [FromQuery] int idCondominio)
        {
            var list = _PresentationService.createComentarioAnuncio(comentarioAnuncio, idCondominio);
            if (list)
            {
                NotificationMessage notificationMessage = new NotificationMessage();
                notificationMessage.Title = "¡Alguien ha comentado tu publicación!";
                notificationMessage.Body = "";
                await SendNotification(notificationMessage, idCondominio, 3, comentarioAnuncio.IdAnuncio);
            }
            return Ok(list);
        }

        [HttpGet("getAnuncioPorId")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getAnuncioPorId(int idAnuncio)
        {
            var list = _PresentationService.getAnuncioPorId(idAnuncio);
            return Ok(list);
        }

        [HttpPost("editUsuarioPorId")]
        [AllowAnonymous]
        [ValidarHeaders]
        public async Task<IActionResult> editUsuarioPorId([FromBody] DbUsuarioCondominio usuarioCondominio)
        {
            var list = _PresentationService.editUsuarioPorId(usuarioCondominio);
            return Ok(list);
        }

        [HttpPost("getUsuarioPorId")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getUsuarioPorId([FromBody] PushSubscriptionModel subscription, int idUsuario, int idCondominio)
        {
            var list = _PresentationService.getUsuarioPorId(idUsuario, subscription, idCondominio);
            return Ok(list); ;
        }
        [HttpGet("getUsuarios")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getUsuarios(int idCondominio)
        {
            var list = _PresentationService.getUsuarios(idCondominio);
            return Ok(list);
        }

        [HttpPost("crearAviso")]
        [AllowAnonymous]
        public IActionResult crearAviso(DbAviso aviso, bool eliminar)
        {
            var list = _PresentationService.crearAviso(aviso, eliminar);
            return Ok(list);
        }

        [HttpGet("getAvisos")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getAvisos(int mes, int idCondominio, int anio)
        {
            var list = _PresentationService.getAvisos(mes, idCondominio, anio);
            return Ok(list);
        }
        [HttpPost("crearEmergencia")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult crearEmergencia(DbEmergencia emergencia, bool eliminar)
        {
            var list = _PresentationService.crearEmergencia(emergencia, eliminar);
            return Ok(list);
        }

        [HttpGet("getEmergencias")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getEmergencias(int idCondominio)
        {
            var list = _PresentationService.getEmergencias(idCondominio);
            return Ok(list);
        }

        [HttpPost("updateNormas")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult updateNormas([FromBody] RequestNormas normas, int idCondominio)
        {
            var html = normas.Html;
            var list = _PresentationService.updateNormas(html, idCondominio);
            return Ok(list);
        }

        [HttpPost("enviarNotifAvisos")]
        [AllowAnonymous]
        [ValidarHeaders]
        public async Task<IActionResult> enviarNotifAvisos(RequestNotifAvisos aviso)
        {
            var list = true;
            try
            {
                NotificationMessage notificationMessage = new NotificationMessage();
                notificationMessage.Title = "El usuario " + aviso.Usuario + " les recuerda: ";
                notificationMessage.Body = aviso.Mensaje;
                await SendNotification(notificationMessage, aviso.IdCondominio, 4, 0);
            }
            catch
            {
                list = false;
            }

            return Ok(list);
        }

        [HttpGet("getNotificaciones")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getNotificaciones(int idUsuario)
        {
            var list = _PresentationService.getNotificaciones(idUsuario);
            return Ok(list);
        }

        //[HttpGet("getAvisosHoy")]
        //[AllowAnonymous]
        //public IActionResult getAvisosHoy(int idCondominio)
        //{
        //    var communityId = Request.Headers["x-community-id"].ToString();
        //    var cors = Request.Headers["Access-Control-Allow-Origin"].ToString();
        //    if (cors.Length > 0 && communityId == PrivateKeyComunidad)
        //    {
        //        var list = _PresentationService.getAvisosHoy(idCondominio);
        //        return Ok(list);
        //    }
        //    return Ok(false);
        //}

        [HttpGet("getAnunciosPorUsuario")]
        [AllowAnonymous]
        [ValidarHeaders]
        public IActionResult getAnunciosPorUsuario(int idUsuario)
        {
            var list = _PresentationService.getAnunciosPorUsuario(idUsuario);
            return Ok(list);
        }

        //[HttpGet("cambiarEstadoAnuncio")]
        //[AllowAnonymous]
        //public IActionResult getAnunciosPorUsuario(int idUsuario)
        //{
        //    var communityId = Request.Headers["x-community-id"].ToString();
        //    var cors = Request.Headers["Access-Control-Allow-Origin"].ToString();
        //    if (cors.Length > 0 && communityId == PrivateKeyComunidad)
        //    {
        //        var list = _PresentationService.getAnunciosPorUsuario(idUsuario);
        //        return Ok(list);
        //    }
        //    return Ok(false);
        //}


        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] PushSubscriptionModel subscription)
        {
            List<PushSubscriptionModel> Subscriptions = new List<PushSubscriptionModel>();
            Subscriptions.Add(subscription);
            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationMessage message, int idCondominio, int tipoSuscripcion, int? idAnuncio = 0)
        {
            var vapidDetails = new VapidDetails(
                "mailto:vmaldonado@conexionresidencial.cl",
                PublicKey,
                PrivateKey
            );

            var webPushClient = new WebPushClient();
            var Subscriptions = new List<PushSubscription>();

            var list = _PresentationService.obtenerNotificaciones(idCondominio, idAnuncio ?? 0);

            foreach (var i in list) {
                if (i.TipoSuscripcion == tipoSuscripcion)
                {
                    PushSubscription _subscriptions = new PushSubscription();
                    _subscriptions.Auth = i.Auth;
                    _subscriptions.P256DH = i.P256DH;
                    _subscriptions.Endpoint = i.Endpoint;
                    Subscriptions.Add(_subscriptions);
                }
            }

            foreach (var sub in Subscriptions)
            {
                try
                {
                    var payload = JsonSerializer.Serialize(new
                    {
                        title = message.Title,
                        body = message.Body
                    });

                    await webPushClient.SendNotificationAsync(sub, payload, vapidDetails);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar notificación: {ex.Message}");
                }
            }

            return Ok("Notificaciones enviadas");
        }


        [HttpGet("publicKey")]
        public IActionResult GetPublicKey() => Ok(PublicKey);
    }

    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
