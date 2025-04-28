using Microsoft.EntityFrameworkCore;
using TaskMaster.Models;

namespace TaskMaster.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Etiquette> Etiquettes { get; set; }
        public DbSet<Etiquetted> Etiquetteds { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipe>()
                .HasKey(e => new { e.ID_Utilisateur, e.ID_Projet });

            modelBuilder.Entity<Equipe>()
                .HasOne(e => e.Utilisateur)
                .WithMany(u => u.Projets)
                .HasForeignKey(e => e.ID_Utilisateur);

            modelBuilder.Entity<Equipe>()
                .HasOne(e => e.Projet)
                .WithMany(p => p.Membres)
                .HasForeignKey(e => e.ID_Projet);

            // Table de jointure Etiquetted (relie Etiquette et Tache)
            modelBuilder.Entity<Etiquetted>()
                .HasKey(et => new { et.ID_Tache, et.ID_Etiquette });

            modelBuilder.Entity<Etiquetted>()
                .HasOne(et => et.Etiquette)
                .WithMany(e => e.Etiquetteds)  // Relation entre Etiquette et Etiquetted
                .HasForeignKey(et => et.ID_Etiquette);

            modelBuilder.Entity<Etiquetted>()
                .HasOne(et => et.Tache)
                .WithMany(t => t.Etiquetteds)  // Relation entre Tache et Etiquetted
                .HasForeignKey(et => et.ID_Tache);

            // Propriétés de navigation de Tache
            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Projet)
                .WithMany(p => p.Taches)
                .HasForeignKey(t => t.ID_Projet);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.Responsable)
                .WithMany(u => u.TachesResponsable)
                .HasForeignKey(t => t.ID_Responsable);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.CreePar)
                .WithMany(u => u.TachesCree)
                .HasForeignKey(t => t.ID_CreePar);

            modelBuilder.Entity<Tache>()
                .HasOne(t => t.TacheParent)
                .WithMany(t => t.SousTaches)
                .HasForeignKey(t => t.ID_TacheParent)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
