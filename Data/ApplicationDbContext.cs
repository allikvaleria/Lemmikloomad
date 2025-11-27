using System.Collections.Generic;
using Lemmikloomad.Models;
using Microsoft.EntityFrameworkCore;

namespace Lemmikloomad.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Kliinik> Kliinikud { get; set; }
        public DbSet<Lemmikloom> Lemmikloomad { get; set; }
        public DbSet<Omanik> Omanikud { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }

}
