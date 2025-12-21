using Microsoft.EntityFrameworkCore;

namespace AnalyzerQC.WebApi.Database;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseMySQL("jdbc:mysql://localhost:3306");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Analyzer> Analyzers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(User.MaxUserNameLength)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);
        
        
        modelBuilder.Entity<Site>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Site>()
            .HasIndex(e => e.SiteCode)
            .IsUnique();
        modelBuilder.Entity<Site>()
            .HasIndex(e => e.SiteName)
            .IsUnique();
        modelBuilder.Entity<Site>()
            .Property(s => s.Address)
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.TimeZone)
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.IsActive)
            .IsRequired();
        modelBuilder.Entity<Site>()
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.AssignedSite)
            .HasForeignKey(e => e.SiteId);
        
        
        modelBuilder.Entity<Analyzer>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.SerialNumber)
            .IsRequired();
        modelBuilder.Entity<Analyzer>()
            .Property(e => e.Status)
            .IsRequired();
        
        
        modelBuilder.Entity<Model>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Model>()
            .HasIndex(e => e.ModelCode)
            .IsUnique();
        modelBuilder.Entity<Model>()
            .HasIndex(e => e.ModelName)
            .IsUnique();
        modelBuilder.Entity<Model>()
            .HasMany(e => e.Analyzers)
            .WithOne(e => e.Model)
            .HasForeignKey(e => e.ModelId);
        
        
        modelBuilder.Entity<ModelGroup>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<ModelGroup>()
            .HasIndex(e => e.ModelGroupName)
            .IsUnique();
        modelBuilder.Entity<ModelGroup>()
            .HasMany(e => e.Models)
            .WithOne(e => e.ModelGroup)
            .HasForeignKey(e => e.ModelGroupId);
        
    }
    
    
    
}