using EvoCommsWeb.Server.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvoCommsWeb.Server.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        //Database.Migrate();
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeTemplate> EmployeeTemplates { get; set; }
    public DbSet<Terminal> Terminals { get; set; }
    public DbSet<Clocking> Clockings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enforce unique constraint on SerialNumber in Terminal
        modelBuilder.Entity<Terminal>()
            .HasIndex(t => t.SerialNumber)
            .IsUnique();

        // Configure relationships if needed (optional if using data annotations)
        modelBuilder.Entity<Clocking>()
            .HasOne(c => c.Employee)
            .WithMany(e => e.Clockings)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Clocking>()
            .HasOne(c => c.Terminal)
            .WithMany(t => t.Clockings)
            .HasForeignKey(c => c.TerminalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmployeeTemplate>()
            .HasOne(et => et.ReceivedFrom)
            .WithMany(t => t.EmployeeTemplates)
            .HasForeignKey(et => et.ReceivedFromId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}