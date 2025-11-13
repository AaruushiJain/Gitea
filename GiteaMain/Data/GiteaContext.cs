using Microsoft.EntityFrameworkCore;
using Gitea_1.GiteaMain.Models;

namespace Gitea_1.GiteaMain.Data
{
    public class GiteaContext : DbContext
    {
        public GiteaContext(DbContextOptions<GiteaContext> options) : base(options)
        {
        }

        public DbSet<Repo> Repositories { get; set; }
        public DbSet<GitObject> GitObjects { get; set; }
        public DbSet<FileRecord> FileRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Repo>().ToTable("Repositories");
            modelBuilder.Entity<GitObject>().ToTable("GitObjects");
            modelBuilder.Entity<FileRecord>().ToTable("FileRecords");
        }
    }
}
