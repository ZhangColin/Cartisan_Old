using System;
using System.Linq.Expressions;

namespace Cartisan.Specification {
    /// <summary>
    /// 规则接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T> {
        /// <summary>
        /// 测试对象是否符合规则
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool IsSatisfiedBy(T obj);

        /// <summary>
        /// 规则：And
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> And(ISpecification<T> other);

        /// <summary>
        /// 规则：Or
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> Or(ISpecification<T> other);

        /// <summary>
        /// 规则：AndNot
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        ISpecification<T> AndNot(ISpecification<T> other);

        /// <summary>
        /// 规则：Not
        /// </summary>
        /// <returns></returns>
        ISpecification<T> Not();

        /// <summary>
        /// 生成表达式
        /// </summary>
        /// <returns></returns>
        Expression<Func<T, bool>> GetExpression();
    }
}