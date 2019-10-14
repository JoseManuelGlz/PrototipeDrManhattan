using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Documents.Manager.Models.Models;

namespace Documents.Manager.Factories
{
    /// <summary>
    /// Connection factory.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ConnectionFactory : IDisposable
    {
        #region :: Properties ::

        /// <summary>
        /// The name of the database.
        /// </summary>
        private readonly string _databaseName;

        /// <summary>
        /// The context.
        /// </summary>
        private DocumentsManagerDbContext _context;

        #endregion

        #region :: Constructor ::

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> class.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        public ConnectionFactory(string databaseName)
        {
            _databaseName = databaseName;
        }

        #endregion

        #region :: Methods ::

        /// <summary>
        /// Creates the context for in memory.
        /// </summary>
        /// <returns>The context for in memory.</returns>
        public DocumentsManagerDbContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<DocumentsManagerDbContext>().UseInMemoryDatabase(_databaseName).Options;
            _context = new DocumentsManagerDbContext(option);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            return _context;
        }

        #region  IDisposable Methods 

        /// <summary>
        /// Releases all resource used by the <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> object.
        /// </summary>
        /// <remarks>Call Dispose when you are finished using the
        /// <see cref="T:Documents.Manager.Factories.ConnectionFactory"/>. The Disponse method
        /// leaves the <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> in an unusable state.
        /// After calling Disponse, you must release all references to the
        /// <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> so the garbage collector can reclaim
        /// the memory that the <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Realeases all resource used by the <see cref="T:Documents.Manager.Factories.ConnectionFactory"/> object.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        #endregion

        #endregion
    }
}
