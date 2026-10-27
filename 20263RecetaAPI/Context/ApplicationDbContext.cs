
using _20263RecetaAPI.Models;
using _20263RecetaAPI.Models._20263RecetaAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
//cerebro del orm 
namespace _20263RecetaAPI.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//heredamos de identity y le pasamos aplicationuser
    {//tengo que hacer el constructor y sobreescribir un metedo y es el nmodelcreating
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);//heredando sobrescribiendo el metodo yvamos a ejecutar esto 

            // relacin receta recetaingrediente
            builder.Entity<RecetaIngrediente>()
                .HasOne(ri => ri.Receta)
                .WithMany(r => r.RecetaIngredientes)
                .HasForeignKey(ri => ri.IdReceta)
                .OnDelete(DeleteBehavior.Cascade);

            //relacion ingrediente receta ingrediente
            builder.Entity<RecetaIngrediente>()
                .HasOne(ri => ri.Ingrediente)
                .WithMany(i => i.RecetaIngredientes)
                .HasForeignKey(ri => ri.IdIngrediente)
                .OnDelete(DeleteBehavior.Restrict);

            // relacion receta applicationuser
            builder.Entity<Receta>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Recetas)
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            //------------------------------Category------------------------------
            //nuestro modelo category estamos pasadno sus propiedades
            builder.Entity<Category>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd(); //autoincremental
                e.Property(x => x.Name).HasMaxLength(60).IsRequired();//maximo 60 cara teres
                e.Property(x => x.Description).HasMaxLength(250);//maximo 250 cacarcteres 
                e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("GETDATE()");//fecha actual
                e.HasIndex(x => x.Name).IsUnique();//indice campo nombre no se puede repetir

            });
            builder.Entity<Comentario>()
                .HasOne(c => c.Receta)
                .WithMany(r => r.Comentarios)
                .HasForeignKey(c => c.IdReceta)
                .OnDelete(DeleteBehavior.Restrict);

            //relcacion con application user
            //si se borra el usuario se borra sus comentarios
            builder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        //enum mi base de datos se llama category y en mi modelo se llama categories y se mapea, modelos en singular 
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<RecetaIngrediente> RecetaIngredientes{ get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

    }

}
