using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthHub_WebAPI.DAL.Generic_Repos
{
    /// <summary>
    /// Generic repository interface for interacting with the database.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<TContext, T> where T : class where TContext : DbContext
    {
        #region Retrieval Operations

        /// <summary>
        /// Retrieves all entities of type T from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of entities.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Retrieves entities of type T from the database based on a filter condition.
        /// </summary>
        /// <param name="where">The filter condition expressed as a lambda expression.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the filtered collection of entities.</returns>
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> where);

        #endregion

        #region Modification Operations

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        void Add(T entity);

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(T entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(T entity);

        #endregion


        #region Save Changes

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();

        #endregion
    }
}
