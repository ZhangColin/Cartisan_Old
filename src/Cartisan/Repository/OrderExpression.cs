using System;
using System.Linq.Expressions;

namespace Cartisan.Repository {
    /// <summary>
    /// 排序表达式
    /// </summary>
    public class OrderExpression<TEntity> {
        public Expression<Func<TEntity, dynamic>> OrderByExpression { get; set; }
        public SortOrder SortOrder { get; set; }

        public OrderExpression(Expression<Func<TEntity, dynamic>> orderByExpression,
            SortOrder sortOrder = SortOrder.Ascending) {
            this.OrderByExpression = orderByExpression;
            this.SortOrder = sortOrder;
        }
    }
}