//-----------------------------------------------------------------------
// <copyright file="AlgebraicFold.cs" company="Math.NET Project">
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

using System.Linq;
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra.Visitors
{
    /// <summary>
    /// Alebraic Linq Expression Folding/Reduction Base
    /// </summary>
    public abstract class AlgebraicFold<T> : AlgebraicVisitor<T>
    {
        protected abstract T Combine(T leftValue, T rightValue);

        protected virtual T Combine(params T[] values)
        {
            if((values == null) || (values.Length == 0))
            {
                return default(T);
            }

            if(values.Length == 1)
            {
                return values[0];
            }

            T fold = Combine(values[0], values[1]);
            for(int i = 2; i < values.Length; i++)
            {
                fold = Combine(fold, values[i]);
            }

            return fold;
        }

        protected T VisitUnary(UnaryExpression term)
        {
            return Visit(term.Operand);
        }

        protected T VisitBinary(BinaryExpression term)
        {
            return Combine(
                Visit(term.Left),
                Visit(term.Right));
        }

        protected T VisitMethodCall(MethodCallExpression term)
        {
            var visitQuery = term.Arguments.Select(a => Visit(a));
            return Combine(visitQuery.ToArray());
        }

        protected override T VisitAdd(BinaryExpression term)
        {
            return VisitBinary(term);
        }

        protected override T VisitSubtract(BinaryExpression term)
        {
            return VisitBinary(term);
        }

        protected override T VisitPlus(UnaryExpression term)
        {
            return VisitUnary(term);
        }

        protected override T VisitMinus(UnaryExpression term)
        {
            return VisitUnary(term);
        }

        protected override T VisitMultiply(BinaryExpression term)
        {
            return VisitBinary(term);
        }

        protected override T VisitDivide(BinaryExpression term)
        {
            return VisitBinary(term);
        }

        protected override T VisitPower(BinaryExpression term)
        {
            return VisitBinary(term);
        }

        protected override T VisitTrigonometry(MethodCallExpression term, TrigonometryFunction function)
        {
            return VisitMethodCall(term);
        }

        protected override T VisitExponential(MethodCallExpression term, ExponentialFunction function)
        {
            return VisitMethodCall(term);
        }
    }
}
