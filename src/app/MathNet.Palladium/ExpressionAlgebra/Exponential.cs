using System;
using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra
{
    public static class Exponential
    {
        static readonly Type _mathType = typeof(Math);

        public static Expression NaturalLogarithm(Expression term)
        {
            return Expression.Call(_mathType.GetMethod("Log"), ExpressionBuilder.ConvertDouble(term));
        }

        public static Expression Exponential(Expression term)
        {
            return Expression.Call(_mathType.GetMethod("Exp"), ExpressionBuilder.ConvertDouble(term));
        }
    }
}
