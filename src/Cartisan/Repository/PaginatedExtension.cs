using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cartisan.Repository {
    public static class PaginatedExtension {
        public static Paginated<T> ToPaginated<T>(this IQueryable<T> query, int pageIndex, int pageSize) {
            var totalCount = query.Count();
            var collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new Paginated<T>(collection,pageIndex, pageSize, totalCount );
        }

        public static Paginated<T> ToPaginated<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }

        public static Paginated<T> ToPaginated<TKey, T>(this IQueryable<T> query, int pageIndex,
            int pageSize, Expression<Func<T, TKey>> orderBySelector, IComparer<TKey> comparer, bool isDescending = false) {
            query = isDescending ? query.OrderByDescending(orderBySelector, comparer) : query.OrderBy(orderBySelector);
            return Paginate(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 对查询进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static Paginated<T> Paginate<T>(IQueryable<T> query, int pageIndex, int pageSize) {
            // TODO: 断言需要处理参数异常
//            if (pageIndex <= 0) {
//                throw new ArgumentException("pageIndex必须大于等于零。", "pageIndex");
//            }
//            if (pageSize <= 0) {
//                throw new ArgumentException("pageSize必须大于等于零。", "pageSize");
//            }
            AssertionConcern.True(pageIndex > 0, "页码必须大于零。");
            AssertionConcern.True(pageSize > 0, "每页显示记录数必须大于零。");
            int totalCount = query.Count();

            IQueryable<T> collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new Paginated<T>(collection, pageIndex, pageSize, totalCount);
        }
    }
}