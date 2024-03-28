using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.DAL.Generic_Repos
{
    /// <summary>
    /// Generic repository implementation for interacting with the database.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    /// <typeparam name="T">The entity type.</typeparam>
    public class Repository<TContext, T> : IRepository<TContext, T> where T : class where TContext : DbContext
    {
        #region Fields

        private readonly TContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TContext, T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(TContext context)
        {
            _context = context;
        }

        #endregion


        #region Modification Operations

        /// <inheritdoc/>
        public async void Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        #endregion

        #region Retrieval Operations

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> where)
        {
            return await _context.Set<T>().Where(where).ToListAsync();
        }

        #endregion

        #region Save Changes

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion
    }
}
