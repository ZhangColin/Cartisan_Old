using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cartisan.Utility {
    public static class ObjectExtensions {
        public static string GetPropertyName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            var property = GetProperty(model, expression);

            if (property != null) {
                return property.Name;
            }

            return null;
        }

        private static PropertyInfo GetProperty<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            return GetMember(model, expression) as PropertyInfo;
        }

        private static MemberInfo GetMember<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression) {
            return ((MemberExpression)expression.Body).Member;
        }
    }
}