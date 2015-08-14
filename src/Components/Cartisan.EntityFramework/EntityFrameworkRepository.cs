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
    public class EfRepository<TAggregateRoot>: IRepository<TAggregateRoot> where TAggregateRoot: class, IAggregateRoot, new() {
        private readonly ContextBase _context;

        public EfRepository(ContextBase context) {
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

        public async Task<object> DeleteAsync(object key) {
            AssertionConcern.NotNull(key, "");

            await DeleteAsync(Get(key));

            return null;
        }

        public async Task<object> DeleteAsync(TAggregateRoot entity) {
            AssertionConcern.NotNull(entity, "");

            _context.SetAsDeleted(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<object> DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate) {
            AssertionConcern.NotNull(predicate, "");

            Query(predicate).ForEach(entity => _context.SetAsDeleted(entity));
            await _context.SaveChangesAsync();

            return null;
        }

        public TAggregateRoot Get(object key) {
            AssertionConcern.NotNull(key, "");

            return DbSet.Find(key);
        }

        //        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params string[] includePaths)  {
        //            AssertionConcern.NotNull(predicate, "");
        //
        //            return GetSetWithIncludePaths(includePaths).SingleOrDefault(predicate);
        //        }

        public TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            AssertionConcern.NotNull(predicate, "");

            return GetSetWithIncludeProperties(includeProperties).SingleOrDefault(predicate);
        }

        //        public IQueryable<TEntity> All(params string[] includePaths)  {
        //            return Query(null, includePaths);
        //        }

        public IQueryable<TAggregateRoot> All(params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            return Query(null, includeProperties);
        }

        //        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] includePaths)  {
        //            var query = GetSetWithIncludePaths(includePaths);
        //            if (predicate!=null) {
        //                return query.Where(predicate);
        //            }
        //            return query;
        //        }

        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            var query = GetSetWithIncludeProperties(includeProperties);
            if(predicate != null) {
                return query.Where(predicate);
            }
            return query;
        }

        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector) {
            throw new NotImplementedException();
            //return Paginate(pageIndex, pageSize, keySelector, null);
        }

        //        public Paginated<TEntity> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector, Expression<Func<TEntity, bool>> predicate,
        //            params string[] includePaths) {
        //            throw new NotImplementedException();
        //        }

        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector, Expression<Func<TAggregateRoot, bool>> predicate,
            params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            var query = Query(predicate, includeProperties);
            query = query.OrderBy(keySelector);

            return query.ToPaginated(pageIndex, pageSize);
        }

        //        private IQueryable<TEntity> GetSetWithIncludePaths(IEnumerable<string> includePaths) {
        //            IQueryable<TEntity> query = _context.Set<TEntity>();
        //
        //            foreach (string path in includePaths ?? Enumerable.Empty<string>()) {
        //                query = query.Include(path);
        //            }
        //
        //            return query;
        //        }

        private IQueryable<TAggregateRoot> GetSetWithIncludeProperties(Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            IQueryable<TAggregateRoot> query = DbSet.AsQueryable();

            foreach(Expression<Func<TAggregateRoot, object>> expression in includeProperties ?? Enumerable.Empty<Expression<Func<TAggregateRoot, object>>>()) {
                query = query.Include(expression);
            }

            return query;
        }

        public TAggregateRoot Get(ISpecification<TAggregateRoot> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            throw new NotImplementedException();
        }

        public bool Exists(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TAggregateRoot, bool>> predicate) {
            throw new NotImplementedException();
        }

        public long Count(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<TAggregateRoot, bool>> predicate) {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> All() {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> predicate) {
            throw new NotImplementedException();
        }

        public IQueryable<TAggregateRoot> Query(ISpecification<TAggregateRoot> predicate) {
            throw new NotImplementedException();
        }

        public Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector, ISpecification<TAggregateRoot> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties) {
            throw new NotImplementedException();
        }
    }
}