using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cartisan.Domain;
using Cartisan.Specification;

namespace Cartisan.Repository {
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TAggregateRoot">聚合类型</typeparam>
    public interface IRepository<TAggregateRoot>: IDisposable where TAggregateRoot: class, IAggregateRoot, new() {
        /// <summary>
        /// 添加聚合
        /// </summary>
        /// <param name="entity"></param>
        void Add(TAggregateRoot entity);
        void Add(IEnumerable<TAggregateRoot> entities);

        Task<object> AddAsync(TAggregateRoot entity);
        Task<object> AddAsync(IEnumerable<TAggregateRoot> entities);

        /// <summary>
        /// 保存聚合
        /// </summary>
        /// <param name="entity"></param>
        void Save(TAggregateRoot entity);
        void Save(IEnumerable<TAggregateRoot> entities);

        Task<object> SaveAsync(TAggregateRoot entity);
        Task<object> SaveAsync(IEnumerable<TAggregateRoot> entities);

        void Remove(object key);
        /// <summary>
        /// 移除聚合
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TAggregateRoot entity);
        /// <summary>
        /// 根据条件移除聚合
        /// </summary>
        /// <param name="predicate"></param>
        void Remove(Expression<Func<TAggregateRoot, bool>> predicate);

        Task<object> RemoveAsync(object key);
        Task<object> RemoveAsync(TAggregateRoot entity);
        Task<object> RemoveAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        /// <summary>
        /// 通过标识获取一个实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TAggregateRoot Get(object key);
        Task<TAggregateRoot> GetAsync(object key);
        //        TEntity Get(Expression<Func<TEntity, bool>> predicate, params string[] includePaths);
        TAggregateRoot Get(Expression<Func<TAggregateRoot, bool>> predicate,
            params Expression<Func<TAggregateRoot, object>>[] includeProperties);
        Task<TAggregateRoot> GetAsync(Expression<Func<TAggregateRoot, bool>> predicate,
            params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        TAggregateRoot Get(ISpecification<TAggregateRoot> predicate,
            params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        bool Exists(ISpecification<TAggregateRoot> predicate);
        Task<bool> ExistsAsync(ISpecification<TAggregateRoot> predicate);
        bool Exists(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        long Count(ISpecification<TAggregateRoot> predicate);
        Task<long> CountAsync(ISpecification<TAggregateRoot> predicate);
        long Count(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<long> CountAsync(Expression<Func<TAggregateRoot, bool>> predicate);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        IQueryable<TAggregateRoot> All();
        ICollection<TAggregateRoot> GetAll();
        Task<ICollection<TAggregateRoot>> GetAllAsync();

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ICollection<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> predicate);
        Task<ICollection<TAggregateRoot>> QueryAsync(Expression<Func<TAggregateRoot, bool>> predicate);
        IQueryable<TAggregateRoot> Query(ISpecification<TAggregateRoot> predicate);

        Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector);
        Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector,
            Expression<Func<TAggregateRoot, bool>> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties);

        Paginated<TAggregateRoot> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<TAggregateRoot, TKey>> keySelector,
            ISpecification<TAggregateRoot> predicate, params Expression<Func<TAggregateRoot, object>>[] includeProperties);
    }
}


