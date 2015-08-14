using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cartisan.Expressions {
    /// <summary>
    /// Lambda表达式工具
    /// </summary>
    public static class LambdaUitl {
        /// <summary>
        /// 生成指定属性的Lambda表达式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static LambdaExpression GetLambdaExpression(Type type, string propertyName) {
            ParameterExpression param = Expression.Parameter(type);
            PropertyInfo property = type.GetProperty(propertyName);
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            var le = Expression.Lambda(propertyAccessExpression, param);
            return le;
        }
    }
}