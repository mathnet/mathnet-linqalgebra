//-----------------------------------------------------------------------
// <copyright file="Elementary.cs" company="Math.NET Project">
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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra
{
    using MathNet.Numerics;

    public static class Elementary
    {
        public static bool IsConstantAlmost(Expression term, double expected)
        {
            if(term.NodeType == ExpressionType.Convert)
            {
                // TODO: do we need 'while' instead of 'if' here?
                UnaryExpression conv = (UnaryExpression)term;
                term = conv.Operand;
            }

            if(term.NodeType != ExpressionType.Constant)
            {
                return false;
            }

            ConstantExpression constExpr = (ConstantExpression)term;
            return Number.AlmostEqual(Convert.ToDouble(constExpr.Value), expected);
        }

        public static bool IsConstantOne(Expression term)
        {
            return IsConstantAlmost(term, 1d);
        }

        public static bool IsConstantZero(Expression term)
        {
            return IsConstantAlmost(term, 0d);
        }

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

        public static bool DependsOn(Expression term, string parameterName)
        {
            return Visitors.AlgebraicFoldLamda<bool>.Create(
                (left, right) => left || right,
                parameter => parameter.Name.Equals(parameterName),
                constant => false)(term);
        }
    }
}
