using System;
using System.Linq;
using System.Linq.Expressions;
using Cartisan.Extensions;

namespace Cartisan.Repository {
    public static class QueryableExtension {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryable,
            params OrderExpression<TEntity>[] orderByExpressions) {
            if (orderByExpressions == null || !orderByExpressions.Any()) {
                return queryable;
            }

            IOrderedQueryable<TEntity> orderedQueryable;
            OrderExpression<TEntity> firstOrderExpression = orderByExpressions.First();

            orderedQueryable = firstOrderExpression.SortOrder == SortOrder.Ascending ?
                queryable.OrderBy(firstOrderExpression.OrderByExpression) :
                queryable.OrderByDescending(firstOrderExpression.OrderByExpression);

            orderByExpressions.Skip(1).ForEach(orderExpression => {
                orderedQueryable = orderExpression.SortOrder == SortOrder.Ascending ?
                    orderedQueryable.ThenBy(orderExpression.OrderByExpression) :
                    orderedQueryable.ThenByDescending(orderExpression.OrderByExpression);
            });

            return orderedQueryable;
        }

        public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> queryable,
            params Expression<Func<TEntity, object>>[] includeProperties) {
            return queryable;
        }
    }
}