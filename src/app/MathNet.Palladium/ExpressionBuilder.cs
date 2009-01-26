//-----------------------------------------------------------------------
// <copyright file="ExpressionBuilder.cs" company="Math.NET Project">
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
using System.Reflection;

namespace MathNet.ExpressionAlgebra
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
