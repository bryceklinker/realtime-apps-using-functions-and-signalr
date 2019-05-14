using System;
using System.Linq.Expressions;

namespace Realtime.Presenter.Mobile
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<T>(this Expression<Func<T>> expression)
        {
            if (expression.Body is MemberExpression body) 
                return body.Member.Name;
            
            var unaryExpression = (UnaryExpression)expression.Body;
            body = unaryExpression.Operand as MemberExpression;
            return body.Member.Name;
        }
    }
}