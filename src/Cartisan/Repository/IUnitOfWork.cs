//using Cartisan.Domain;
//
//namespace Cartisan.Repository {
//    public interface IUnitOfWork {
//        /// <summary>
//        /// 注册修改的实体及使用的仓储
//        /// </summary>
//        /// <param name="entity"></param>
//        void RegisterAmended(IAggregateRoot entity);
//
//        /// <summary>
//        /// 注册新增的实体及使用的仓储
//        /// </summary>
//        /// <param name="entity"></param>
//        void RegisterNew(IAggregateRoot entity);
//
//        /// <summary>
//        /// 注册删除的实体及使用的仓储
//        /// </summary>
//        /// <param name="entity"></param>
//        void RegisterRemoved(IAggregateRoot entity);
//        /// <summary>
//        /// 提交
//        /// </summary>
//        void Commit();
//    }
//}