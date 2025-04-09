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
        // Constructeur : passe les options de DbContext à la base de classe
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Déclare les tables/entités de la base de données
        public DbSet<TaskModel> Tasks { get; set; }
    }
}