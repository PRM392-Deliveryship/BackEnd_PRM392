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

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            var query = context.Set<TEntity>().Where(filterExpression);
            var queryableType = query.GetType().GetProperty("ElementType");
            // use queryableType here
            return await query.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync(); // Save changes asynchronously
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync(); // Save changes asynchronously
            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Set<User>().SingleOrDefaultAsync(u => u.Email == email);
        }



        /*public async Task<Token> GetUserToken(long userId)
        {
            return await context.Set<Token>().Where(t => t.UserId == userId && !t.IsExpired && !t.Revoked).FirstOrDefaultAsync();
        }
        public async Task<Order> GetOrderByTransactionIdAsync(long transactionId)
        {
            return await context.Set<Order>().FirstOrDefaultAsync(o => o.TransactionId == transactionId);
        }
        public async Task<Order> GetOrderByPaymentAsync(long transactionId)
        {
            return await context.Set<Order>().FirstOrDefaultAsync(o => o.PaymentCode == transactionId.ToString());
        }*/


        public virtual async Task<IEnumerable<TEntity>> GetAsync(
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

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdWithInclude(long id, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(entity => EF.Property<long>(entity, "Id") == id);

        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public bool Exists(Expression<Func<TEntity, bool>> filter)
        {
            return dbSet.Any(filter);
        }
    }
}