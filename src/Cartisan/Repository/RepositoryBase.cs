//using System;
//using System.Linq;
//using System.Linq.Expressions;
//using Cartisan.Domain;
//
//namespace Cartisan.Repository {
//    public abstract class RepositoryBase<TEntity, TId>: Disposable, IRepository<TEntity, TId> 
//        where TEntity: class, IAggregateRoot, IEntity<TId> where TId: IComparable {
//        private readonly IUnitOfWork _unitOfWork;
//
//        protected RepositoryBase(IUnitOfWork unitOfWork) {
//            this._unitOfWork = unitOfWork;
//        }
//
//        public abstract TEntity Load(TId id);
//        public abstract TEntity Get(TId id);
//        public abstract IQueryable<TEntity> GetAll();
//        public abstract IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
//
//        public virtual void Add(TEntity entity) {
//            this._unitOfWork.RegisterNew(entity);
//        }
//
//        public virtual void Save(TEntity entity) {
//            this._unitOfWork.RegisterAmended(entity);
//        }
//
//        public virtual void Remove(TEntity entity) {
//            this._unitOfWork.RegisterRemoved(entity);
//        }
//
//        public virtual void Remove(Expression<Func<TEntity, bool>> predicate) {
//            this.FindBy(predicate).ToList().ForEach(entity => Remove(entity));
//        }
//    }
//}