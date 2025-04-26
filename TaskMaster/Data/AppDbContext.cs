using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Configurer la clé composite pour Etiquetted
            modelBuilder.Entity<Etiquetted>()
                .HasKey(et => new { et.ID_Tache, et.ID_Etiquette });
        }



    }
}