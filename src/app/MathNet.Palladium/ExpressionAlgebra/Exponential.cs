using System;
using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra
{
    public static class Exponential
    {
        static readonly Type _mathType = typeof(Math);

        /// <summary>
        /// Natural Logarithm
        /// </summary>
        public static Expression Ln(Expression term)
        {
            return ExpressionBuilder.CallDouble(_mathType.GetMethod("Log"), term);
        }

        /// <summary>
        /// Exponential
        /// </summary>
        public static Expression Exp(Expression term)
        {
            return ExpressionBuilder.CallDouble(_mathType.GetMethod("Exp"), term);
        }
    }
}
