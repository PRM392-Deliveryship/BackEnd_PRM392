using GaVietNam_Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GaVietNam_Repository.Repository
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal GaVietNamContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(GaVietNamContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        // Updated Get method with pagination
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Implementing pagination
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                // Ensure the pageIndex and pageSize are valid
                int validPageIndex = pageIndex.Value > 0 ? pageIndex.Value - 1 : 0;
                int validPageSize = pageSize.Value > 0 ? pageSize.Value : 10; // Assuming a default pageSize of 10 if an invalid value is passed

                query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);
            }

            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity Entity)
        {
            dbSet.Add(Entity);
            context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity EntityToDelete = dbSet.Find(id);
            Delete(EntityToDelete);
        }

        public virtual void Delete(TEntity EntityToDelete)
        {
            if (context.Entry(EntityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(EntityToDelete);
            }
            dbSet.Remove(EntityToDelete);
        }

        public virtual void Update(TEntity EntityToUpdate)
        {
            dbSet.Attach(EntityToUpdate);
            context.Entry(EntityToUpdate).State = EntityState.Modified;
        }


        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }
    }
}