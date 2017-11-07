using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 属性信息选择器。
    /// </summary>
    public static class PropertySelector
    {
        /// <summary>
        /// 获取属性。
        /// </summary>
        /// <typeparam name="T">泛型类型。</typeparam>
        /// <param name="type">类型。</param>
        /// <param name="selector">属性选择器。</param>
        /// <returns>返回属性信息。</returns>
        public static PropertyInfo GetProperty<T>(this Type type, Expression<Func<T, object>> selector)
        {
            if (selector.NodeType != ExpressionType.Lambda)
            {
                throw new ArgumentException("选择器必须为Lambda表达式。", "selector");
            }

            var lambda = (LambdaExpression)selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression == null)
            {
                throw new ArgumentException("选择器必须是成员访问表达式。", "selector");
            }

            if (memberExpression.Member.DeclaringType == null)
            {
                throw new InvalidOperationException("属性未声明类型。");
            }

            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        private static MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return (MemberExpression)expression;
            }

            if (expression.NodeType == ExpressionType.Convert)
            {
                var operand = ((UnaryExpression)expression).Operand;
                return ExtractMemberExpression(operand);
            }

            return null;
        }
    }
}