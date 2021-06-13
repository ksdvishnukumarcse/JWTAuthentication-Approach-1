using JWTAuthentication.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthentication.Infrastructure.InMemory
{
    /// <summary>
    /// DataContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class DataContext: DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }        
    }
}
