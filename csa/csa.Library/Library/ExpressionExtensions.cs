using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace csa.Library

{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;

            var parameter = Expression.Parameter(typeof(T), "p");
            var combined = new ParameterReplacer(parameter).Visit(Expression.AndAlso(left.Body, right.Body));
            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null) return right;

            var parameter = Expression.Parameter(typeof(T), "p");
            var combined = new ParameterReplacer(parameter).Visit(Expression.OrElse(left.Body, right.Body));
            return Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
    }

    class ParameterReplacer : ExpressionVisitor
    {
        readonly ParameterExpression parameter;

        internal ParameterReplacer(ParameterExpression parameter)
        {
            this.parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return parameter;
        }
    }
}
