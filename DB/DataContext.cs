using System.Configuration;
using HomeWork.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.DB;

public sealed class DataContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public DataContext()
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    public override int SaveChanges()
    {
        var modifiedEntities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified)
            .ToList();

        foreach (var entity in modifiedEntities)
        {
            // обновляем поле UpdatedAt при обновлении сущности
            entity.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
        }

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Client>()
            .Property(e => e.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Passport>()
            .Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Passport>()
            .Property(e => e.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Account>()
            .Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Account>()
            .Property(e => e.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}
