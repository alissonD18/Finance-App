using Microsoft.EntityFrameworkCore;
using MVCSandBox.Context.Configurations;
using MVCSandBox.Models;

namespace MVCSandBox.Context
{
    public class MvcSandboxContext : DbContext
    {
        public DbSet<ResumoLancamento> ResumoLancamentos { get; set; }
        public DbSet<DetalheLancamento> DetalheLancamentos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }

        public string DbPath { get; }

        public MvcSandboxContext(DbContextOptions<MvcSandboxContext> options) : base(options)
        {
            DbPath = System.IO.Path.Join(@"C:\Users\aliss\OneDrive\Documentos\Estudos\Programs\C#\MVCSandBox\", "mvcSandbox.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ResumoLancamentoConfiguration().Configure(modelBuilder.Entity<ResumoLancamento>());
            new DetalheLancamentoConfiguration().Configure(modelBuilder.Entity<DetalheLancamento>());
            new DetalheLancamentoConfiguration().Configure(modelBuilder.Entity<DetalheLancamento>());
        }
    }
}
