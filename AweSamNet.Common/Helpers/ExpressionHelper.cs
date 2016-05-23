using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AweSamNet.Common.Helpers
{
    public static class ExpressionHelper
    {
        public static object GetValue(this object o, string path)
        {
            var propertyNames = path.Split('.');
            var value = o.GetType().GetProperty(propertyNames[0]).GetValue(o, null);

            if (propertyNames.Length == 1 || value == null) return value;

            return GetValue(value, path.Replace(propertyNames[0] + ".", ""));
        }

        //http://stackoverflow.com/a/9601914/1509728
        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda, TValue value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                    return;
                }
            }

            throw new NotSupportedException("Exressions other than property expressions are not supported.");
        }

        public static TValue GetValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda)
        {
            return (TValue)GetExpressionValue(target, memberLamda.Body);
        }

        private static object GetExpressionValue<T>(T target, Expression expression)
        {

            if (expression is MethodCallExpression)
            {
                var methodCallExpression = expression as MethodCallExpression;

                var methodInfo = methodCallExpression.Method;
                var allParameters = from element in methodCallExpression.Arguments
                    select GetExpressionValue(target, element);

                if(methodInfo.IsStatic) return methodInfo.Invoke(null, allParameters.ToArray());
                if (methodCallExpression.Object == null) return methodInfo.Invoke(target, allParameters.ToArray());

                var methodTarget = GetExpressionValue(target, methodCallExpression.Object);
                return methodInfo.Invoke(methodTarget, allParameters.ToArray());
            }
            else if (expression is MemberExpression)
            {
                var memberExpression = expression as MemberExpression;

                // expression is ConstantExpression or FieldExpression then just return the value
                if (memberExpression.Expression is ConstantExpression)
                {
                    return (((ConstantExpression)memberExpression.Expression).Value)
                        .GetType()
                        .GetField(memberExpression.Member.Name)
                        .GetValue(((ConstantExpression)memberExpression.Expression).Value);
                }

                if (memberExpression.Expression is ParameterExpression)
                {
                    return GetMemberValue(memberExpression, target);
                }

                var parentObject = GetExpressionValue(target, memberExpression.Expression);
                return parentObject == null ? null : GetMemberValue(memberExpression, parentObject);
            }
            else if (expression is UnaryExpression)
            {
                return GetExpressionValue(target, (expression as UnaryExpression).Operand);
            }
            else if (expression is ConstantExpression)
            {
                return (expression as ConstantExpression).Value;
            }

            throw new NotSupportedException(
                String.Format("The expression {0} is not supported on {1}, with an Expression type of {2}."
                    , target
                    , target.GetType()
                    , expression.GetType()));
        }

        /// <summary>
        /// Tries to return the value of a memberExpression for a given target.  If unsuccessful, a NotCupportedException is thrown.
        /// </summary>
        private static object GetMemberValue(MemberExpression memberExpression, object target)
        {
            //if this is the top-most expression, then return the value
            switch (memberExpression.Member.MemberType)
            {
                case MemberTypes.Field:
                    if (memberExpression.Member is FieldInfo)
                    {
                        return (memberExpression.Member as FieldInfo).GetValue(target);
                    }
                    break;
                case MemberTypes.Property:
                    if (memberExpression.Member is PropertyInfo)
                    {
                        return (memberExpression.Member as PropertyInfo).GetValue(target);
                    }
                    break;
            }
            throw new NotSupportedException(
                String.Format("The expression {0} is not supported on {1}, with a MemberType of {2}."
                    , memberExpression
                    , target.GetType()
                    , memberExpression.Member.MemberType));
        }

        public static Expression<Func<TFirstParam, TResult>> Combine<TFirstParam, TIntermediate, TResult>(
            this Expression<Func<TFirstParam, TIntermediate>> first,
            Expression<Func<TIntermediate, TResult>> second)
        {
            var param = Expression.Parameter(typeof (TFirstParam), "param");

            var newFirst = first.Body.Replace(first.Parameters[0], param);
            var newSecond = second.Body.Replace(second.Parameters[0], newFirst);

            return Expression.Lambda<Func<TFirstParam, TResult>>(newSecond, param);
        }

        public static Expression Replace(this Expression expression,
            Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        internal class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression from, to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                this.@from = @from;
                this.to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == @from ? to : base.Visit(node);
            }
        }
    }
}
