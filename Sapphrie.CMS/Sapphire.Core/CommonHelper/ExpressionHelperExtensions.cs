using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 表达式帮助器扩展类。
    /// </summary>
    public static class ExpressionHelperExtensions
    {
        ///// <summary>
        ///// 获取模型主键表达式。
        ///// </summary>
        ///// <typeparam name="TModel">TModel。</typeparam>
        ///// <returns>模型主键表达式。</returns>
        //public static Expression<Func<TModel, int>> GetPrimaryKeyExpression<TModel>()
        //{
        //    return GetPropertyValueExpression<TModel, int>(ModelHelper.GetPrimaryKeyInfo(typeof(TModel)));
        //}

        /// <summary>
        /// 获取属性值表达式。
        /// </summary>
        /// <typeparam name="TModel">TModel。</typeparam>
        /// <typeparam name="TValue">TValue。</typeparam>
        /// <param name="propertyInfo">属性信息实例对象。</param>
        /// <returns>属性值表达式。</returns>
        public static Expression<Func<TModel, TValue>> GetPropertyValueExpression<TModel, TValue>(PropertyInfo propertyInfo)
        {
            var modelParameterExpression = Expression.Parameter(typeof(TModel));
            var propertyExpression = Expression.Property(modelParameterExpression, propertyInfo.Name);
            return Expression.Lambda<Func<TModel, TValue>>(propertyExpression, modelParameterExpression);
        }

        /// <summary>
        /// 获取属性赋值表达式。
        /// </summary>
        /// <typeparam name="TModel">模型类型。</typeparam>
        /// <typeparam name="TValue">属性类型。</typeparam>
        /// <param name="propertyExpression">属性表达式。</param>
        /// <returns>属性赋值表达式。</returns>
        public static Expression<Action<TModel, TValue>> GetPropertyAssignExpression<TModel, TValue>(Expression<Func<TModel, TValue>> propertyExpression)
        {
            var memberExpression = propertyExpression.GetMemberExpression();
            Check.NotNull(memberExpression, "memberExpression");
            var parameterExpression = memberExpression.Expression as ParameterExpression;
            var valueParameterExpression = Expression.Parameter(typeof(TValue));

            return Expression.Lambda<Action<TModel, TValue>>(
                Expression.Assign(memberExpression, valueParameterExpression),
                parameterExpression,
                valueParameterExpression);
        }

        /// <summary>
        /// 获取表达式的body并转为MemberExpression类型对象。
        /// </summary>
        /// <typeparam name="TDelete">TDelete。</typeparam>
        /// <param name="expression">表达式实例对象。</param>
        /// <returns>MemberExpression类型对象。</returns>
        public static MemberExpression GetMemberExpression<TDelete>(this Expression<TDelete> expression)
        {
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                return expression.Body as MemberExpression;
            }

            if (expression.Body is UnaryExpression)
            {
                if ((expression.Body as UnaryExpression).Operand is MemberExpression)
                {
                    return (expression.Body as UnaryExpression).Operand as MemberExpression;
                }
            }

            return null;
        }
    }
}