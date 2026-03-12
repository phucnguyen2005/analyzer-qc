using AnalyzerQC.Application;
using AnalyzerQC.Commons;
using AnalyzerQC.ValueObject;
using Microsoft.EntityFrameworkCore;


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
        modelBuilder.Entity<User>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(User.CreationTime).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(User.CreatorId).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(User.LastModificationTime).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(User.LastModifierId).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(User.DeleterId).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(User.DeletionTime).ToSnakeCase());
        modelBuilder.Entity<User>()
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(User.IsDeleted).ToSnakeCase());


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
        modelBuilder.Entity<Site>()
            .Property(s => s.WorkingTime)
            .HasColumnName(nameof(Site.WorkingTime).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.Frequency)
            .HasColumnName(nameof(Site.Frequency).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(s => s.NotificationType)
            .HasColumnName(nameof(Site.NotificationType).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Site>()
            .Property(p => p.WorkingDays)
            .HasColumnName(nameof(Site.WorkingDays).ToSnakeCase())
            .IsRequired()
            .HasConversion(
                workingDays => string.Join(',', workingDays),
                str => str.Split(',', StringSplitOptions.None)
                    .Select(Enum.Parse<WorkingDays>).ToList()
            );
        modelBuilder.Entity<Site>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Site.CreationTime).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Site.CreatorId).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(Site.LastModificationTime).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(Site.LastModifierId).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(Site.DeleterId).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(Site.DeletionTime).ToSnakeCase());
        modelBuilder.Entity<Site>()
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(Site.IsDeleted).ToSnakeCase());

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
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Analyzer.CreationTime).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Analyzer.CreatorId).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.LastModificationTime)
            .HasColumnName(nameof(Analyzer.LastModificationTime).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.LastModifierId)
            .HasColumnName(nameof(Analyzer.LastModifierId).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.DeleterId)
            .HasColumnName(nameof(Analyzer.DeleterId).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.DeletionTime)
            .HasColumnName(nameof(Analyzer.DeletionTime).ToSnakeCase());
        modelBuilder.Entity<Analyzer>()
            .Property(s => s.IsDeleted)
            .HasColumnName(nameof(Analyzer.IsDeleted).ToSnakeCase());

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
        modelBuilder.Entity<Model>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(AnalyzerQC.Model.CreationTime).ToSnakeCase());
        modelBuilder.Entity<Model>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(AnalyzerQC.Model.CreatorId).ToSnakeCase());


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
        modelBuilder.Entity<ModelGroup>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(ModelGroup.CreationTime).ToSnakeCase());
        modelBuilder.Entity<ModelGroup>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(ModelGroup.CreatorId).ToSnakeCase());
        modelBuilder.Entity<ModelGroup>()
            .HasMany(e => e.Reagents)
            .WithOne(e => e.ModelGroup)
            .HasForeignKey(e => e.ModelGroupId);

        modelBuilder.Entity<Reagent>()
            .ToTable(nameof(Reagent).ToSnakeCase());
        modelBuilder.Entity<Reagent>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Reagent>()
            .Property(e => e.Id)
            .HasColumnName(nameof(Reagent.Id).ToSnakeCase());
        modelBuilder.Entity<Reagent>()
            .Property(e => e.ReagentName)
            .HasColumnName(nameof(Reagent.ReagentName).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Reagent>()
            .Property(e => e.Description)
            .HasColumnName(nameof(Reagent.Description).ToSnakeCase());
        modelBuilder.Entity<Reagent>()
            .Property(e => e.Status)
            .HasColumnName(nameof(Reagent.Status).ToSnakeCase())
            .IsRequired();
        modelBuilder.Entity<Reagent>()
            .Property(e => e.Levels)
            .HasColumnName(nameof(Reagent.Levels).ToSnakeCase())
            .IsRequired()
            .HasConversion(
                levels => string.Join(' ', levels.Select(l => $"{l.LevelCode}:{l.LevelName}")),
                str => str.Split(' ', StringSplitOptions.None)
                    .Select(x => new Level(
                        x.Split(':', StringSplitOptions.None)[0],
                        x.Split(':', StringSplitOptions.None)[1]))
                    .ToList());
        modelBuilder.Entity<Reagent>()
            .Property(s => s.CreationTime)
            .HasColumnName(nameof(Reagent.CreationTime).ToSnakeCase());
        modelBuilder.Entity<Reagent>()
            .Property(s => s.CreatorId)
            .HasColumnName(nameof(Reagent.CreatorId).ToSnakeCase());
        modelBuilder.Entity<Reagent>()
            .Property(e => e.ModelGroupId)
            .HasColumnName(nameof(Reagent.ModelGroupId).ToSnakeCase());
    }
}