using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Documents.Manager.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Documents.Manager.Models.Models
{
    /// <summary>
    /// Documents manager db context.
    /// </summary>
    public class DocumentsManagerDbContext : DbContext
    {
        #region :: Properties ::

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public DbSet<Application> Application { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public DbSet<File> Files { get; set; }

        /// <summary>
        /// Gets or sets the file classification.
        /// </summary>
        /// <value>The file classifications.</value>
        public DbSet<FileClassification> FileClassification { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        /// <value>The file types.</value>
        public DbSet<FileType> FileType { get; set; }

        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>The process.</value>
        public DbSet<Process> Process { get; set; }

        /// <summary>
        /// Gets or sets the process type.
        /// </summary>
        /// <value>The process types.</value>
        public DbSet<ProcessType> ProcessType { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        /// <value>The urls.</value>
        public DbSet<Url> Url { get; set; }

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Models.DocumentsManagerDbContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public DocumentsManagerDbContext(DbContextOptions<DocumentsManagerDbContext> options) : base(options) { }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Adds the timestamps.
        /// </summary>
        private void AddTimestamps()
        {
            var now = DateTime.Now;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is IBaseEntity entity)
                {
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                    }
                    else if (changedEntity.State == EntityState.Modified ||
                             changedEntity.State == EntityState.Deleted)
                    {
                        Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                        entity.UpdatedAt = now;
                    }
                }
            }
        }

        /// <summary>
        /// Adjust the property IsDeleted of the entity according to its entity state.
        /// </summary>
        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues["IsDeleted"] = false;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }
        }

        #region  Overrided Methods

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <returns>The changes async.</returns>
        /// <param name="cancellationToken">Cancellation token.</param>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Apply the correct rules for every model.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<File>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<FileClassification>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<FileType>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Process>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ProcessType>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Url>()
                .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<ProcessType>()
                .HasMany(e => e.Processes)
                .WithOne(c => c.ProcessType)
                .HasForeignKey(c => c.ProcessTypeId);

            modelBuilder.Entity<File>()
                .HasMany(e => e.Urls)
                .WithOne(c => c.File)
                .HasForeignKey(c => c.FileId);

            modelBuilder.Entity<Application>()
                .HasMany(e => e.Files)
                .WithOne(c => c.Application)
                .HasForeignKey(c => c.ApplicationId);

            modelBuilder.Entity<FileType>()
                .HasMany(e => e.Files)
                .WithOne(l => l.FileType)
                .HasForeignKey(l => l.FileTypeId);

            modelBuilder.Entity<FileClassification>()
                .HasMany(e => e.Files)
                .WithOne(f => f.FileClassification)
                .HasForeignKey(f => f.FileClassificationId);

            modelBuilder.Entity<Process>()
                .HasMany(e => e.Files)
                .WithOne(s => s.Process)
                .HasForeignKey(s => s.ProcessId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.GetEntityTypes().ToList().ForEach(delegate (IMutableEntityType entity)
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names
                entity.GetProperties().ToList().ForEach(property
                    => property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase());

                // Replace keys
                entity.GetKeys().ToList().ForEach(key
                    => key.Relational().Name = key.Relational().Name.ToSnakeCase());

                entity.GetForeignKeys().ToList().ForEach(key
                    => key.Relational().Name = key.Relational().Name.ToSnakeCase());

                // Replace indexes
                entity.GetIndexes().ToList().ForEach(index
                    => index.Relational().Name = index.Relational().Name.ToSnakeCase());
            });
        }

        #endregion

        #endregion
    }
}
