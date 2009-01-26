//-----------------------------------------------------------------------
// <copyright file="Arithmetic.cs" company="Math.NET Project">
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

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra
{
    /// <summary>
    /// Arithmetic Expression Builder
    /// </summary>
    public static class Arithmeric
    {
        public static Expression Zero()
        {
            return ExpressionBuilder.ConstantDouble(0d);
        }

        public static Expression One()
        {
            return ExpressionBuilder.ConstantDouble(1d);
        }

        public static Expression MinusOne()
        {
            return ExpressionBuilder.ConstantDouble(-1d);
        }

        public static Expression Add(params Expression[] terms)
        {
            TypeInference type = new TypeInference(terms);
            return type.CastToMaxNumeric()
                .Where(x => !Elementary.IsConstantZero(x))
                .Reduce(Expression.Add, Zero());
        }

        public static Expression Subtract(Expression a, Expression b)
        {
            if(Elementary.IsConstantZero(a))
            {
                return Negate(b);
            }

            if(Elementary.IsConstantZero(b))
            {
                return a;
            }

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Subtract(expressions[0], expressions[1]);
        }

        public static Expression Negate(Expression term)
        {
            if(Elementary.IsConstantZero(term))
            {
                return Arithmeric.Zero();
            }

            if(term.NodeType == ExpressionType.Negate)
            {
                UnaryExpression unary = (UnaryExpression)term;
                return unary.Operand;
            }

            return Expression.Negate(term);
        }

        public static Expression Multiply(params Expression[] terms)
        {
            TypeInference type = new TypeInference(terms);
            List<Expression> factors = type.CastToMaxNumeric()
                .Where(x => !Elementary.IsConstantOne(x))
                .ToList();

            if(factors.Exists(Elementary.IsConstantZero))
            {
                return Zero();
            }

            return factors.Reduce(Expression.Multiply, One());
        }

        public static Expression Divide(Expression a, Expression b)
        {
            if(Elementary.IsConstantZero(a))
            {
                return Arithmeric.Zero();
            }

            if(Elementary.IsConstantOne(b))
            {
                return a;
            }

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Divide(expressions[0], expressions[1]);
        }

        public static Expression Invert(Expression term)
        {
            if(term.NodeType == ExpressionType.Divide)
            {
                BinaryExpression binary = (BinaryExpression)term;
                return Divide(binary.Right, binary.Left);
            }

            return Divide(Expression.Constant(1), term);
        }

        public static Expression Power(Expression a, Expression b)
        {
            // TODO: automatic simplification
            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Power(expressions[0], expressions[1]);
        }
    }
}
