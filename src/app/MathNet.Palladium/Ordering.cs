//-----------------------------------------------------------------------
// <copyright file="Ordering.cs" company="Math.NET Project">
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
    public class Ordering : IComparer<Expression>
    {
        /// <summary>
        /// Compare two terms for ordering (i.e. "2*x", not "x*2").
        /// </summary>
        /// <remarks>
        /// Only the top expression node is analyzed, this method is therefore
        /// not intended (and can't be used) for comparing two expression trees for equality.
        /// </remarks>
        public static int Compare(Expression x, Expression y)
        {
            // ordering by type
            int comparison = Assess(x) - Assess(y);
            if(comparison != 0)
            {
                return comparison;
            }

            // if same type, ordering by name (if it has a name)
            string nameX = NameOfExpression(x);
            string nameY = NameOfExpression(y);

            return string.Compare(nameX, nameY, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns a simple ordering priority score based on the term type.
        /// </summary>
        static int Assess(Expression term)
        {
            if(term == null)
            {
                throw new ArgumentNullException("term");
            }

            ConstantExpression constExpr = term as ConstantExpression;
            if(constExpr != null)
            {
                if((constExpr.Type == typeof(int)) || (constExpr.Type == typeof(long)))
                {
                    return 1;
                }

                if((constExpr.Type == typeof(double)) || (constExpr.Type == typeof(float)))
                {
                    return 2;
                }

                return 3;
            }

            if(term is ParameterExpression)
            {
                return 10;
            }

            if(term is MethodCallExpression)
            {
                return 200;
            }

            BinaryExpression binaryExpr = term as BinaryExpression;
            if(binaryExpr != null)
            {
                switch(binaryExpr.NodeType)
                {
                    case ExpressionType.Multiply:
                        return 20;
                    case ExpressionType.Add:
                    case ExpressionType.Subtract:
                        return 21;
                    case ExpressionType.Divide:
                        return 22;
                }
            }

            return 100;
        }

        /// <summary>
        /// Tries to find and return the term's name, if it is named (i.e. a parameter or a method call).
        /// </summary>
        static string NameOfExpression(Expression term)
        {
            ParameterExpression paramExpr = term as ParameterExpression;
            if(paramExpr != null)
            {
                return paramExpr.Name;
            }

            MethodCallExpression methodExpr = term as MethodCallExpression;
            if(methodExpr != null)
            {
                return methodExpr.Method.Name;
            }

            return null;
        }

        int IComparer<Expression>.Compare(Expression x, Expression y)
        {
            return Compare(x, y);
        }
    }
}
