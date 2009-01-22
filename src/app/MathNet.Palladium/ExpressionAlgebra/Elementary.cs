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
    }
}
