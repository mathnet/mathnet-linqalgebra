//-----------------------------------------------------------------------
// <copyright file="PartialDerivative.cs" company="Math.NET Project">
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

namespace MathNet.ExpressionAlgebra
{
    using MathNet.ExpressionAlgebra.Visitors;

    /// <remarks>
    /// This class is stateful and not thread safe, therefore never share an instance
    /// between multiple threads (the ThreadStatic attribute might help).
    /// </remarks>
    public class PartialDerivative : AlgebraicVisitor
    {
        string _variableName;

        public Expression Differentiate(Expression term, string variableName)
        {
            _variableName = variableName;
            try
            {
                return Visit(term);
            }
            finally
            {
                _variableName = null;
            }
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if(p.Name == _variableName)
            {
                return Arithmeric.One();
            }

            return Arithmeric.Zero();
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            return Arithmeric.Zero();
        }

        protected override Expression VisitMultiply(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = Visit(binaryExpression.Left);
            Expression rightDerivative = Visit(binaryExpression.Right);
            return Arithmeric.Add(
                Arithmeric.Multiply(leftDerivative, binaryExpression.Right),
                Arithmeric.Multiply(rightDerivative, binaryExpression.Left));
        }

        protected override Expression VisitDivide(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = Visit(binaryExpression.Left);
            Expression rightDerivative = Visit(binaryExpression.Right);
            return Arithmeric.Subtract(
                Arithmeric.Divide(leftDerivative, binaryExpression.Right),
                Arithmeric.Divide(
                    Arithmeric.Multiply(binaryExpression.Left, rightDerivative),
                    Arithmeric.Multiply(binaryExpression.Right, binaryExpression.Right)));
        }

        protected override Expression VisitPower(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = Visit(binaryExpression.Left);
            Expression rightDerivative = Visit(binaryExpression.Right);
            return Arithmeric.Multiply(
                binaryExpression,
                Arithmeric.Add(
                    Arithmeric.Multiply(
                        rightDerivative,
                        Exponential.Ln(binaryExpression.Left)),
                    Arithmeric.Divide(
                        Arithmeric.Multiply(binaryExpression.Right, leftDerivative),
                        binaryExpression.Left)));
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch(m.Method.Name)
            {
                case "Sin":
                case "Sine":
                    {
                        Expression innerDerivative = Visit(m.Arguments[0]);
                        return Arithmeric.Multiply(
                            innerDerivative,
                            Trigonometry.Cosine(m.Arguments[0]));
                    }

                case "Cos":
                case "Cosine":
                    {
                        Expression innerDerivative = Visit(m.Arguments[0]);
                        return Arithmeric.Negate(
                            Arithmeric.Multiply(
                                innerDerivative,
                                Trigonometry.Sine(m.Arguments[0])));
                    }

                default:
                    throw new NotSupportedException(String.Format("Method Call to {0} is not supported.", m.ToString()));
            }
        }
    }
}
