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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    }
}