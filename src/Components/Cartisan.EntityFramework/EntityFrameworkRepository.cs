using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cartisan.Domain;
using Cartisan.Extensions;
using Cartisan.Repository;
using Cartisan.Specification;

namespace Cartisan.EntityFramework {
    public class EntityFrameworkRepository<TAggregateRoot>: IRepository<TAggregateRoot> where TAggregateRoot: class, IAggregateRoot {
        private readonly ContextBase _context;


        public EntityFrameworkRepository(ContextBase context) {
            AssertionConcern.NotNull(context, "没有数据库上下文，仓储无法工作。");
            _context = context;
        }

        private IDbSet<TAggregateRoot> _dbSet;

        private IDbSet<TAggregateRoot> DbSet {
            get { return _dbSet ?? (_dbSet = _context.Set<TAggregateRoot>()); }
        }

        public void Dispose() {
            if(_context == null) {
                return;
            }
            _context.Dispose();
        }

        public void Add(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");

            try {
                _context.SetAsAdded(entity);
                _context.SaveChanges();
            }
            catch(DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            
        }

        public void Add(IEnumerable<TAggregateRoot> entities) {
            AssertionConcern.NotNull(entities, "");

            try {
                entities.ForEach(entity => this._context.SetAsAdded(entity));
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<object> AddAsync(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");

            _context.SetAsAdded(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<object> AddAsync(IEnumerable<TAggregateRoot> entities) {
            AssertionConcern.NotNull(entities, "");

            entities.ForEach(entity => this._context.SetAsAdded(entity));

            await _context.SaveChangesAsync();

            return entities;
        }

        public void Save(TAggregateRoot entity) {
            try {
                AssertionConcern.NotNull(entity, "");
                _context.SetAsModified(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Save(IEnumerable<TAggregateRoot> entities) {
            try {
                AssertionConcern.NotNull(entities, "");
                entities.ForEach(entity => this._context.SetAsModified(entity));
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<object> SaveAsync(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");
            _context.SetAsModified(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<object> SaveAsync(IEnumerable<TAggregateRoot> entities) {
            AssertionConcern.NotNull(entities, "");
            entities.ForEach(entity => this._context.SetAsModified(entity));
            await _context.SaveChangesAsync();

            return entities;
        }

        public void Remove(object key) {
            AssertionConcern.NotNull(key, "");

            Remove(Get(key));
        }

        public void Remove(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");

            try {
                _context.SetAsDeleted(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            
        }

        public void Remove(Expression<Func<TAggregateRoot, bool>> predicate) {
            AssertionConcern.NotNull(predicate, "");

            try {
                Query(predicate).ForEach(entity => _context.SetAsDeleted(entity));
                
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<object> RemoveAsync(object key) {
            AssertionConcern.NotNull(key, "");

            await RemoveAsync(Get(key));

            return null;
        }

        public async Task<object> RemoveAsync(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");

            _context.SetAsDeleted(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<object> RemoveAsync(Expression<Func<TAggregateRoot, bool>> predicate) {
            AssertionConcern.NotNull(predicate, "");

            Query(predicate).ForEach(entity => _context.SetAsDeleted(entity));
            await _context.SaveChangesAsync();

            return null;
        }

        public TAggregateRoot Get(object key) {
            AssertionConcern.NotNull(key, "");

            return DbSet.Find(key);
        }

        public async Task<TAggregateRoot> GetAsync(object key) {
            AssertionConcern.NotNull(key, "");
            return await Task.Run(() => DbSet.Find(key));
        }


        public TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            AssertionConcern.NotNull(predicate, "");

            return GetSetWithIncludeProperties(includeProperties).SingleOrDefault(predicate);
        }

        public async Task<TAggregateRoot> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public TAggregateRoot Get(ISpecification<TAggregateRoot> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            throw new NotImplementedException();
        }


//        public IQueryable<TAggregateRoot> All(params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
//            return Query(null, includeProperties);
//        }

        public IQueryable<TAggregateRoot> All() {
            return DbSet.AsQueryable();
        }

        public ICollection<TAggregateRoot> GetAll() {
            return DbSet.ToList();
        }

        public async Task<ICollection<TAggregateRoot>> GetAllAsync() {
            return await DbSet.ToListAsync();
        }

//        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
//            var query = GetSetWithIncludeProperties(includeProperties);
//            if(predicate != null) {
//                return query.Where(predicate);
//            }
//            return query;
//        }

        public ICollection<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> predicate) {
            return DbSet.Where(predicate).ToList();
        }

        public async Task<ICollection<TAggregateRoot>> QueryAsync(Expression<Func<TAggregateRoot, bool>> predicate) {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public IQueryable<TAggregateRoot> Query(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector) {
            throw new NotImplementedException();
            //return Paginate(pageIndex, pageSize, keySelector, null);
        }


        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector, Expression<Func<TAggregateRoot, bool>> predicate,
            params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            throw new NotImplementedException();
            //            var query = Query(predicate, includeProperties);
            //            query = query.OrderBy(keySelector);
            //
            //            return query.ToPaginated(pageIndex, pageSize);
        }


        private IQueryable<TAggregateRoot> GetSetWithIncludeProperties(Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            IQueryable<TAggregateRoot> query = DbSet.AsQueryable();

            foreach(Expression<Func<TAggregateRoot, object>> expression in includeProperties ?? Enumerable.Empty<Expression<Func<TAggregateRoot, object>>>()) {
                query = query.Include(expression);
            }

            return query;
        }

        public bool Exists(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TAggregateRoot, bool>> predicate) {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> predicate) {
            throw new NotImplementedException();
        }

        public long Count(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<TAggregateRoot, bool>> predicate) {
            return DbSet.Where(predicate).Count();
        }

        public async Task<long> CountAsync(Expression<Func<TAggregateRoot, bool>> predicate) {
            return await DbSet.Where(predicate).CountAsync();
        }

        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector, ISpecification<TAggregateRoot> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            throw new NotImplementedException();
        }
    }
}