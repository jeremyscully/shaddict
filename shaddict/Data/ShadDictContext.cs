using Microsoft.EntityFrameworkCore;
using Shaddict.Models;

namespace Shaddict.Data
{
    /// <summary>
    /// Database context for the Shaddict application
    /// </summary>
    public class ShadDictContext : DbContext
    {
        /// <summary>
        /// Constructor for the ShadDictContext
        /// </summary>
        /// <param name="options">The options for this context</param>
        public ShadDictContext(DbContextOptions<ShadDictContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet for business entities
        /// </summary>
        public DbSet<Entity> Entities { get; set; }

        /// <summary>
        /// DbSet for database tables
        /// </summary>
        public DbSet<DatabaseTable> DatabaseTables { get; set; }

        /// <summary>
        /// DbSet for table columns
        /// </summary>
        public DbSet<TableColumn> TableColumns { get; set; }

        /// <summary>
        /// Configure the model
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity relationships
            modelBuilder.Entity<Entity>()
                .HasMany(e => e.Tables)
                .WithOne(t => t.Entity)
                .HasForeignKey(t => t.EntityId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<DatabaseTable>()
                .HasMany(t => t.Columns)
                .WithOne(c => c.Table)
                .HasForeignKey(c => c.TableId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
