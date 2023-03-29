using Microsoft.EntityFrameworkCore;
using Moreno.RPA_AeC.Domain.Entities;
using Moreno.RPA_AeC.Infra.Data.Mappings;

namespace Moreno.RPA_AeC.Infra.Data.Context.Entity;

[ExcludeFromCodeCoverage]
public class RPA_AeCDbContext : DbContext
{
    public RPA_AeCDbContext()
    {

    }

    public RPA_AeCDbContext(DbContextOptions<RPA_AeCDbContext> options) : base(options)
    {

    }

    public DbSet<Pesquisa> Pesquisa { get; set; }
    public DbSet<ResultadoPesquisa> ResultadoPesquisa { get; set; }
    public DbSet<LogPesquisa> LogPesquisa { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PesquisaMap());
        modelBuilder.ApplyConfiguration(new ResultadoPesquisaMap());
        modelBuilder.ApplyConfiguration(new LogPesquisaMap());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            if (entry.State == EntityState.Modified)
                entry.Property("DataCadastro").IsModified = false;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
