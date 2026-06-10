using AnalyzerQC.Application;
using AnalyzerQC.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace AnalyzerQC.Infrastructure.Database;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseMySQL("jdbc:mysql://localhost:3306");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<ModelGroup> ModelGroups { get; set; }
    public DbSet<Analyzer> Analyzers { get; set; }
    public DbSet<Reagent> Reagents { get; set; }
    public DbSet<AssayLimit> AssayLimits { get; set; }
    public DbSet<Lot> Lots { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<AssayLimitParameter> AssayLimitParameters { get; set; }
    public DbSet<QcUpload> QcUploads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}