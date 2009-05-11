//-----------------------------------------------------------------------
// <copyright file="AlgebraicFoldLambda.cs" company="Math.NET Project">
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
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra.Visitors
{
    public class AlgebraicFoldLamda<T> : AlgebraicFold<T>
    {
        Func<ParameterExpression, T> _foldLeafParam;
        Func<ConstantExpression, T> _foldLeafConst;
        Func<T, T, T> _combine;

        public AlgebraicFoldLamda(
            Func<T, T, T> combine,
            Func<ParameterExpression, T> foldParam,
            Func<ConstantExpression, T> foldConst)
        {
            _combine = combine;
            _foldLeafParam = foldParam;
            _foldLeafConst = foldConst;
        }

        public static Func<Expression, T> Create(
            Func<T, T, T> combine,
            Func<ParameterExpression, T> foldParam,
            Func<ConstantExpression, T> foldConst)
        {
            AlgebraicFoldLamda<T> fold = new AlgebraicFoldLamda<T>(combine, foldParam, foldConst);
            return fold.Visit;
        }

        protected override T Combine(T leftValue, T rightValue)
        {
            return _combine(leftValue, rightValue);
        }

        protected override T VisitConstant(ConstantExpression term)
        {
            return _foldLeafConst(term);
        }

        protected override T VisitParameter(ParameterExpression term)
        {
            return _foldLeafParam(term);
        }
    }
}
