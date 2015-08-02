using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cartisan.Repository {
//    /// <summary>
//    /// 仓储
//    /// </summary>
//    /// <typeparam name="TEntity">实体类型</typeparam>
//    /// <typeparam name="TId">主键类型</typeparam>
//    public interface IRepository<TEntity, TId>: IDisposable
//        where TEntity: class, IEntity<TId>, IAggregateRoot
//        where TId: IComparable {
//        /// <summary>
//        /// 通过标识获取一个实体，只有在使用实体时，才会访问数据库
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        TEntity Load(TId id);
//
//        /// <summary>
//        /// 通过标识获取一个实体
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        TEntity Get(TId id);
//
//        /// <summary>
//        /// 获取所有实体
//        /// </summary>
//        /// <returns></returns>
//        IQueryable<TEntity> GetAll();
//
//        /// <summary>
//        /// 根据条件查询实体
//        /// </summary>
//        /// <param name="predicate"></param>
//        /// <returns></returns>
//        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
//
//        /// <summary>
//        /// 添加实体
//        /// </summary>
//        /// <param name="entity"></param>
//        void Add(TEntity entity);
//
//        /// <summary>
//        /// 保存实体
//        /// </summary>
//        /// <param name="entity"></param>
//        void Save(TEntity entity);
//
//        /// <summary>
//        /// 移除实体
//        /// </summary>
//        /// <param name="entity"></param>
//        void Remove(TEntity entity);
//
//        /// <summary>
//        /// 根据条件移除实体
//        /// </summary>
//        /// <param name="predicate"></param>
//        void Remove(Expression<Func<TEntity, bool>> predicate);
//
//    }
//
//    public interface IRepository<TEntity>: IRepository<TEntity, long>
//        where TEntity: class, IEntity<long>, IAggregateRoot {
//    }

    public interface IRepository<TEntity>: IDisposable where TEntity: class, new() {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);

        Task<object> AddAsync(TEntity entity);
        Task<object> AddAsync(IEnumerable<TEntity> entities);

        void Save(TEntity entity);
        void Save(IEnumerable<TEntity> entities);

        Task<object> SaveAsync(TEntity entity);
        Task<object> SaveAsync(IEnumerable<TEntity> entities);

        void Delete(object key);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task<object> DeleteAsync(object key);
        Task<object> DeleteAsync(TEntity entity);
        Task<object> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(object key);
        //        TEntity Get(Expression<Func<TEntity, bool>> predicate, params string[] includePaths);
        TEntity Get(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        //        IQueryable<TEntity> All(params string[] includePaths);
        IQueryable<TEntity> All(params Expression<Func<TEntity, object>>[] includeProperties);
        //        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, params string[] includePaths);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Paginated<TEntity> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector);
        //        Paginated<TEntity> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector,
        //            Expression<Func<TEntity, bool>> predicate, params string[] includePaths);
        Paginated<TEntity> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TEntity, TKey>> keySelector,
            Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}


