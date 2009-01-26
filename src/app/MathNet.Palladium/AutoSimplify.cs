//-----------------------------------------------------------------------
// <copyright file="AutoSimplify.cs" company="Math.NET Project">
//    Copyright (c) 2002-2009, Christoph Rüegg.
//    All Right Reserved.
// </copyright>
// <author>
//    Christoph Rüegg, http://christoph.ruegg.name
// </author>
// <product>
//    Math.NET Palladium, part of the Math.NET Project.
//    http://mathnet.opensourcedotnet.info
// </product>
// <license type="opensource" name="LGPL" version="2 or later">
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published 
//    by the Free Software Foundation; either version 2 of the License, or
//    any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public 
//    License along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// </license>
//-----------------------------------------------------------------------

using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra
{
    /// <summary>
    /// Automatic Algebraic Expression Simplification.
    /// </summary>
    public static class AutoSimplify
    {
        /// <summary>
        /// Automatic factor simplification
        /// </summary>
        /// <remarks>
        /// Ensures that the following rules are met (fixes term if needed):
        /// <list type="bullet">
        /// <item>a * b: a is not a product, neither a nor b is a quotient.</item>
        /// <item>a / b: neither a nor b are quotients</item>
        /// </list>
        /// </remarks>
        public static Expression SimplifyFactors(Expression term)
        {
            switch(term.NodeType)
            {
                case ExpressionType.Divide:
                    return SimplifyQuotient((BinaryExpression)term);
                case ExpressionType.Multiply:
                    return SimplifyProduct((BinaryExpression)term);
                default:
                    return term;
            }
        }

        /// <summary>
        /// Ensures that for (a / b), neither a nor b are quotients.
        /// </summary>
        static Expression SimplifyQuotient(BinaryExpression quotient)
        {
            Expression nominator = SimplifyFactors(quotient.Left);
            Expression denominator = SimplifyFactors(quotient.Right);

            return SimpleQuotient(nominator, denominator, quotient);
        }

        static Expression SimpleQuotient(Expression nominator, Expression denominator, BinaryExpression quotient)
        {
            // ensure "nominator" is no quotient
            if(nominator.NodeType == ExpressionType.Divide)
            {
                BinaryExpression nominatorQuotient = (BinaryExpression)nominator;
                nominator = nominatorQuotient.Left;
                denominator = SimpleProduct(denominator, nominatorQuotient.Right, null);
            }

            // ensure "denominator" is no quotient
            if(denominator.NodeType == ExpressionType.Divide)
            {
                BinaryExpression denominatorQuotient = (BinaryExpression)denominator;
                nominator = SimpleProduct(nominator, denominatorQuotient.Right, null);
                denominator = denominatorQuotient.Left;
            }

            if((quotient == null) || (nominator != quotient.Left) || (denominator != quotient.Right))
            {
                return Expression.Divide(nominator, denominator);
            }

            return quotient;
        }

        /// <summary>
        /// Ensures that for (a * b), neither a nor b is a quotient, and b is no product.
        /// </summary>
        static Expression SimplifyProduct(BinaryExpression product)
        {
            Expression left = SimplifyFactors(product.Left);
            Expression right = SimplifyFactors(product.Right);

            return SimpleProduct(left, right, product);
        }

        static Expression SimpleProduct(Expression left, Expression right, BinaryExpression product)
        {
            // ensure "left" is no quotient
            if(left.NodeType == ExpressionType.Divide)
            {
                BinaryExpression leftQuotient = (BinaryExpression)left;
                Expression nominator = SimpleProduct(right, leftQuotient.Left, null);
                return SimpleQuotient(nominator, leftQuotient.Right, null);
            }

            // ensure "right" is no quotient
            if(right.NodeType == ExpressionType.Divide)
            {
                BinaryExpression rightQuotient = (BinaryExpression)right;
                Expression nominator = SimpleProduct(left, rightQuotient.Left, null);
                return SimpleQuotient(nominator, rightQuotient.Right, null);
            }

            // ensure "right" is no product
            while(right.NodeType == ExpressionType.Multiply)
            {
                BinaryExpression rightProduct = (BinaryExpression)right;
                left = Expression.Multiply(left, rightProduct.Right);
                right = rightProduct.Left;
            }

            if((product == null) || (left != product.Left) || (right != product.Right))
            {
                return Expression.Multiply(left, right);
            }

            return product;
        }
    }
}
