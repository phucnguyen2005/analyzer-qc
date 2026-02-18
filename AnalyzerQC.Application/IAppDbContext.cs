using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.Application;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<ModelGroup> ModelGroups { get; set; }
    public DbSet<Analyzer> Analyzers { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}