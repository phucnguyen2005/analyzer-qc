using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.WebApi.Database;

public class AppDbContext : DbContext
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable(nameof(User).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasColumnName(nameof(User.Username).ToSnakeCase())
            .HasMaxLength(User.MaxUserNameLength)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<User>()
            .Property(s => s.Id)
            .HasColumnName(nameof(User.Id).ToSnakeCase())
            .IsRequired();


        modelBuilder.Entity<Site>()
            .ToTable(nameof(Site).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Site>()
            .Property(s => s.Id)
            .HasColumnName(nameof(Site.Id).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .HasIndex(e => e.SiteCode)
            .IsUnique();
        modelBuilder.Entity<Site>()
            .Property(s => s.SiteCode)
            .HasColumnName(nameof(Site.SiteCode).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(e => e.SiteName)
            .HasColumnName(nameof(Site.SiteName).ToSnakeCase())
            .HasMaxLength(Site.MaxSiteNameLength)
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.Address)
            .HasColumnName(nameof(Site.Address).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.TimeZone)
            .HasColumnName(nameof(Site.TimeZone).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.IsActive)
            .HasColumnName(nameof(Site.IsActive).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.AssignedSite)
            .HasForeignKey(e => e.SiteId);


        modelBuilder.Entity<Analyzer>()
            .ToTable(nameof(Analyzer).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.Id)
            .HasColumnName(nameof(Analyzer.Id).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.SerialNumber)
            .HasColumnName(nameof(Analyzer.SerialNumber).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.Status)
            .HasColumnName(nameof(Analyzer.Status).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.SiteId)
            .HasColumnName(nameof(Analyzer.SiteId).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.ModelId)
            .HasColumnName(nameof(Analyzer.ModelId).ToSnakeCase());


        modelBuilder.Entity<Model>()
            .ToTable(nameof(AnalyzerQC.Model).ToSnakeCase());
        modelBuilder.Entity<Model>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Model>()
            .Property(e => e.Id)
            .HasColumnName(nameof(AnalyzerQC.Model.Id).ToSnakeCase());
        modelBuilder.Entity<Model>()
            .HasIndex(e => e.ModelCode)
            .IsUnique();
        modelBuilder.Entity<Model>()
            .Property(e => e.ModelCode)
            .HasColumnName(nameof(AnalyzerQC.Model.ModelCode).ToSnakeCase());
        modelBuilder.Entity<Model>()
            .Property(e => e.ModelName)
            .HasColumnName(nameof(AnalyzerQC.Model.ModelName).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Model>()
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.Model)
            .HasForeignKey(e => e.ModelId);
        modelBuilder.Entity<Model>()
            .Property(e => e.ModelGroupId)
            .HasColumnName(nameof(AnalyzerQC.Model.ModelGroupId).ToSnakeCase());

        
        modelBuilder.Entity<ModelGroup>()
            .ToTable(nameof(ModelGroup).ToSnakeCase());
        modelBuilder.Entity<ModelGroup>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<ModelGroup>()
            .Property(e => e.Id)
            .HasColumnName(nameof(ModelGroup.Id).ToSnakeCase());
        modelBuilder.Entity<ModelGroup>()
            .Property(e => e.ModelGroupName)
            .HasColumnName(nameof(ModelGroup.ModelGroupName).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<ModelGroup>()
            .HasIndex(e => e.ModelGroupCode)
            .IsUnique();
        modelBuilder.Entity<ModelGroup>()
            .Property(e => e.ModelGroupCode)
            .HasColumnName(nameof(ModelGroup.ModelGroupCode).ToSnakeCase());
        modelBuilder.Entity<ModelGroup>()
            .HasMany(e => e.Models)
            .WithOne(e => e.ModelGroup)
            .HasForeignKey(e => e.ModelGroupId);
    }
}