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
        void Insert(TEntity Entity);
        void Delete(object id);
        void Delete(TEntity EntityToDelete);
        void Update(TEntity EntityToUpdate);

        bool Exists(Expression<Func<TEntity, bool>> filter);
    }
}

