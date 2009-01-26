using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MathNet.Palladium.ExpressionAlgebra
{
    public static class Elementary
    {
        public static Expression Numerator(Expression quotient)
        {
            if(quotient.NodeType != ExpressionType.Divide)
            {
                return quotient;
            }

            BinaryExpression binary = quotient as BinaryExpression;
            if(binary == null)
            {
                ////return quotient;
                throw new Exception(String.Format("Expected a BinaryExpression, but was {0} instead.", quotient.Type.Name));
            }

            return binary.Left;
        }

        public static Expression Denominator(Expression quotient)
        {
            if(quotient.NodeType != ExpressionType.Divide)
            {
                return Expression.Constant(1);
            }

            BinaryExpression binary = quotient as BinaryExpression;
            if(binary == null)
            {
                ////return Expression.Constant(1);
                throw new Exception(String.Format("Expected a BinaryExpression, but was {0} instead.", quotient.Type.Name));
            }

            return binary.Right;
        }

        /// <summary>
        /// Collect all factors (including inverse factors in case of a quotient) of the provided term.
        /// This algorithm does not apply any factorization.
        /// </summary>
        public static List<Expression> Factors(Expression product)
        {
            List<Expression> list = new List<Expression>();
            CollectFactorsRecursive(product, list, false);
            return list;
        }

        static void CollectFactorsRecursive(Expression term, List<Expression> factors, bool denominator)
        {
            if(term.NodeType == ExpressionType.Multiply)
            {
                BinaryExpression binary = (BinaryExpression)term;
                CollectFactorsRecursive(binary.Left, factors, denominator);
                CollectFactorsRecursive(binary.Right, factors, denominator);
                return;
            }

            if(term.NodeType == ExpressionType.Divide)
            {
                BinaryExpression binary = (BinaryExpression)term;
                CollectFactorsRecursive(binary.Left, factors, denominator);
                CollectFactorsRecursive(binary.Right, factors, !denominator);
                return;
            }

            if(denominator)
            {
                term = Expression.Divide(Expression.Constant(1d), term);
            }

            factors.Add(term);
        }
    }
}
