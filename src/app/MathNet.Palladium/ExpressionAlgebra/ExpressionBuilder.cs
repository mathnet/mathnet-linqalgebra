using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MathNet.Palladium.ExpressionAlgebra
{
    internal static class ExpressionBuilder
    {
        public static Expression Reduce(this IEnumerable<Expression> terms, Func<Expression, Expression, Expression> map, Expression defaultIfEmpty)
        {
            Expression sum = null;
            foreach(Expression term in terms)
            {
                sum = (sum == null) ? term : map(sum, term);
            }

            return sum ?? defaultIfEmpty;
        }

        public static Expression ConstantDouble(double value)
        {
            return Expression.Constant(value, typeof(double));
        }

        public static Expression ConvertDouble(Expression expression)
        {
            if(expression.Type != typeof(double))
            {
                if(expression.NodeType == ExpressionType.Constant)
                {
                    ConstantExpression constExpr = (ConstantExpression)expression;
                    return ConstantDouble(Convert.ToDouble(constExpr.Value));
                }

                return Expression.Convert(expression, typeof(double));
            }

            return expression;
        }

        public static Expression CallDouble(MethodInfo method, Expression expression)
        {
            return Expression.Call(method, ConvertDouble(expression));
        }

        public static Expression CallDouble(MethodInfo method, Expression a, Expression b)
        {
            return Expression.Call(method, ConvertDouble(a), ConvertDouble(b));
        }
    }
}
