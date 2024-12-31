using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EvoCommsWeb.Server.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
        : base(options)
    {
        Database.Migrate();
        var connection = Database.GetDbConnection();
        if (connection is SqliteConnection sqliteConnection) // For SQLite databases
        {
            Debug.WriteLine($"Database physical location: {sqliteConnection.DataSource}");
            logger.LogInformation($"Database physical location: {sqliteConnection.DataSource}");
        }
        else
        {
            logger.LogInformation($"Database connection string: {connection.ConnectionString}");
        }
    }

    // Add additional DbSets here if needed
}