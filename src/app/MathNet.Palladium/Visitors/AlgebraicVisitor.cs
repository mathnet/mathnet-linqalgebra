//-----------------------------------------------------------------------
// <copyright file="ExpressionVisitor.cs" company="Math.NET Project">
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
using System.Reflection;

namespace MathNet.ExpressionAlgebra.Visitors
{
    /// <summary>
    /// Linq Expression Generic Algebraic Visitor Base
    /// </summary>
    public abstract class AlgebraicVisitor<T>
    {
        protected AlgebraicVisitor()
        {
        }

        protected virtual T Visit(Expression term)
        {
            if(term == null)
            {
                return default(T);
            }

            switch(term.NodeType)
            {
                case ExpressionType.Constant:
                    return VisitConstant((ConstantExpression)term);

                case ExpressionType.Parameter:
                    return VisitParameter((ParameterExpression)term);

                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return VisitMinus((UnaryExpression)term);

                case ExpressionType.UnaryPlus:
                    return VisitPlus((UnaryExpression)term);

                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return VisitAdd((BinaryExpression)term);

                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return VisitSubtract((BinaryExpression)term);

                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return VisitMultiply((BinaryExpression)term);

                case ExpressionType.Divide:
                    return VisitDivide((BinaryExpression)term);

                case ExpressionType.Power:
                    return VisitPower((BinaryExpression)term);

                case ExpressionType.Call:
                    return VisitMethodCallCore((MethodCallExpression)term);

                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    return VisitConvert((UnaryExpression)term);

                default:
                    return VisitUnknown(term);
            }
        }

        protected abstract T VisitConstant(ConstantExpression c);

        protected abstract T VisitParameter(ParameterExpression p);

        protected abstract T VisitAdd(BinaryExpression binaryExpression);

        protected abstract T VisitSubtract(BinaryExpression binaryExpression);

        protected abstract T VisitPlus(UnaryExpression unaryExpression);

        protected abstract T VisitMinus(UnaryExpression unaryExpression);

        protected abstract T VisitMultiply(BinaryExpression binaryExpression);

        protected abstract T VisitDivide(BinaryExpression binaryExpression);

        protected abstract T VisitPower(BinaryExpression binaryExpression);

        protected abstract T VisitTrigonometry(MethodCallExpression methodCall, TrigonometryFunction function);

        protected abstract T VisitExponential(MethodCallExpression methodCall, ExponentialFunction function);

        protected virtual T VisitUnknown(Expression expression)
        {
            throw new NotSupportedException(string.Format("Unhandled expression type: '{0}'", expression.NodeType));
        }

        protected virtual T VisitUnknownFunction(MethodCallExpression expression)
        {
            throw new NotSupportedException(string.Format("Unhandled expression method call: '{0}'", expression.Method));
        }

        protected virtual T VisitConvert(UnaryExpression unaryExpression)
        {
            return Visit(unaryExpression.Operand);
        }

        private T VisitMethodCallCore(MethodCallExpression methodCall)
        {
            MethodInfo method = methodCall.Method;

            TrigonometryFunction trigFunction;
            if(Trigonometry.TryParse(method, out trigFunction))
            {
                return VisitTrigonometry(methodCall, trigFunction);
            }

            ExponentialFunction expFunction;
            if(Exponential.TryParse(method, out expFunction))
            {
                return VisitExponential(methodCall, expFunction);
            }

            return VisitUnknownFunction(methodCall);
        }
    }
}
