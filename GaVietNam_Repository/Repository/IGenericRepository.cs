using GaVietNam_Repository.Entity;
using System;
using System.Linq.Expressions;

namespace GaVietNam_Repository.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);

        TEntity GetByID(object id);
        void Insert(TEntity entity);
        /*Task<Order> GetOrderByPaymentAsync(long transactionId);*/
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        /*Task<Order> GetOrderByTransactionIdAsync(long transactionId);*/
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(long id);
        Task<User> GetByEmailAsync(string email);
        /*Task<Token> GetUserToken(long id);*/
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
        Task SaveChangesAsync();

        Task<TEntity> GetByIdWithInclude(long id, string includeProperties = "");

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}

