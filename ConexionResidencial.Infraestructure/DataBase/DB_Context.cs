using Microsoft.EntityFrameworkCore;
using ConexionResidencial.Core.Entities;
using ConexionResidencial.Core.Entities.DB;
using ConexionResidencial.Core.Entities.DB.Condominio;

namespace ConexionResidencial.Infraestructure.DataBase
{
    public partial class DB_Context : DbContext
    {
        public DB_Context(DbContextOptions<DB_Context> options)
           : base(options)
        {
        }
        //Condominio
        public virtual DbSet<DbCondominio> DbCondominio { get; set; }
        public virtual DbSet<DbAnuncio> DbAnuncio { get; set; }
        public virtual DbSet<DbUsuarioCondominio> DbUsuarioCondominio { get; set; }
        public virtual DbSet<DbSuscripcion> DbSuscripcion { get; set; }
        public virtual DbSet<DbLike> DbLike { get; set; }
        public virtual DbSet<DbSeleccionVotacion> DbSeleccionVotacion { get; set; }
        public virtual DbSet<DbOpcionVotacion> DbOpcionVotacion { get; set; }
        public virtual DbSet<DbVotacion> DbVotacion { get; set; }
        public virtual DbSet<DbComentarioAnuncio> DbComentarioAnuncio { get; set; }
        public virtual DbSet<DbAviso> DbAviso { get; set; }
        public virtual DbSet<DbEmergencia> DbEmergencia { get; set; }
        public virtual DbSet<DbNotificaciones> DbNotificaciones { get; set; }
        public virtual DbSet<DbCondominioUsuario> DbCondominioUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            //Condominio
            modelBuilder.Entity<DbCondominio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("CONDOMINIO");

                entity.Property(e => e.Nombre).HasColumnName("NOMBRE");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Logo).HasColumnName("LOGO");
                entity.Property(e => e.Guid).HasColumnName("GUID");
                entity.Property(e => e.Normas).HasColumnName("NORMAS");
            });
            modelBuilder.Entity<DbAnuncio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("ANUNCIO");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
                entity.Property(e => e.Cabecera).HasColumnName("CABECERA");
                entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
                entity.Property(e => e.Organizador).HasColumnName("ORGANIZADOR");
                entity.Property(e => e.FechaDesde).HasColumnName("FECHA_DESDE");
                entity.Property(e => e.FechaHasta).HasColumnName("FECHA_HASTA");
                entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
                entity.Property(e => e.Amedida).HasColumnName("A_MEDIDA");
                entity.Property(e => e.IdTipo).HasColumnName("ID_TIPO");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            });
            modelBuilder.Entity<DbUsuarioCondominio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("USUARIO_CONDOMINIO");

                entity.Property(e => e.Nombre).HasColumnName("NOMBRE");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
                entity.Property(e => e.Clave).HasColumnName("CLAVE");
                entity.Property(e => e.Rol).HasColumnName("ROL");
                entity.Property(e => e.Activo).HasColumnName("ACTIVO");
                entity.Property(e => e.Imagen).HasColumnName("IMAGEN");
                entity.Property(e => e.Direccion).HasColumnName("DIRECCION");
                entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
                entity.Property(e => e.FechaCaducidad).HasColumnName("FECHA_CADUCIDAD");
            });
            modelBuilder.Entity<DbSuscripcion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("SUSCRIPCION");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
                entity.Property(e => e.Endpoint).HasColumnName("ENDPOINT");
                entity.Property(e => e.P256DH).HasColumnName("P256DH");
                entity.Property(e => e.Auth).HasColumnName("AUTH");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.TipoSuscripcion).HasColumnName("TIPO_SUSCRIPCION");
            });

            modelBuilder.Entity<DbLike>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("LIKE");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdAnuncio).HasColumnName("ID_ANUNCIO");
                entity.Property(e => e.Likes).HasColumnName("LIKES");
            });

            modelBuilder.Entity<DbVotacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("VOTACION");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Cabecera).HasColumnName("CABECERA");
                entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Activo).HasColumnName("ACTIVO");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
            });
            modelBuilder.Entity<DbOpcionVotacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("OPCION_VOTACION");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
                entity.Property(e => e.IdVotacion).HasColumnName("ID_VOTACION");
            });
            modelBuilder.Entity<DbSeleccionVotacion>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("SELECCION_VOTACION");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdOpcion).HasColumnName("ID_OPCION");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            });
            modelBuilder.Entity<DbComentarioAnuncio>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("COMENTARIO_ANUNCIO");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdAnuncio).HasColumnName("ID_ANUNCIO");
                entity.Property(e => e.Mensaje).HasColumnName("MENSAJE");
                entity.Property(e => e.Fecha).HasColumnName("FECHA");
                entity.Property(e => e.NombreUsuario).HasColumnName("NOMBRE_USUARIO");
            });

            modelBuilder.Entity<DbAviso>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("AVISO");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Fecha).HasColumnName("FECHA");
                entity.Property(e => e.Mensaje).HasColumnName("MENSAJE"); 
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
            });
            modelBuilder.Entity<DbEmergencia>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("EMERGENCIA");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Descripcion).HasColumnName("DESCRIPCION");
                entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
                entity.Property(e => e.Direccion).HasColumnName("DIRECCION");
            });
            modelBuilder.Entity<DbNotificaciones>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("NOTIFICACIONES");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Fecha).HasColumnName("FECHA");
                entity.Property(e => e.Mensaje).HasColumnName("MENSAJE");
                entity.Property(e => e.Usuario).HasColumnName("USUARIO");
                entity.Property(e => e.TipoNotificacion).HasColumnName("TIPO_NOTIFICACION");
                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
            });
            modelBuilder.Entity<DbCondominioUsuario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("CONDOMINIO_USUARIO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.IdCondominio).HasColumnName("ID_CONDOMINIO");
                entity.Property(e => e.FechaCaducidad).HasColumnName("FECHA_EXPIRACION");
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
