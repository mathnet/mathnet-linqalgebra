//-----------------------------------------------------------------------
// <copyright file="AlgebraicManipulator.cs" company="Math.NET Project">
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

using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System;

namespace MathNet.ExpressionAlgebra.Visitors
{
    /// <summary>
    /// Alebraic Linq Expression Manipulator Base
    /// </summary>
    public abstract class AlgebraicManipulator : ExpressionManipulator
    {
        protected AlgebraicManipulator()
        {
        }

        protected override Expression VisitUnary(UnaryExpression term)
        {
            switch(term.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return VisitMinus((UnaryExpression)term);
                case ExpressionType.UnaryPlus:
                    return VisitPlus((UnaryExpression)term);
                default:
                    return base.VisitUnary(term);
            }
        }

        protected override Expression VisitBinary(BinaryExpression term)
        {
            switch(term.NodeType)
            {
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
                default:
                    return base.VisitBinary(term);
            }
        }

        protected virtual Expression VisitAdd(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Add(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitSubtract(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Subtract(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitPlus(UnaryExpression term)
        {
            return base.VisitUnary(term);
        }

        protected virtual Expression VisitMinus(UnaryExpression term)
        {
            return base.VisitUnary(term);
        }

        protected virtual Expression VisitMultiply(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Multiply(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitDivide(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Divide(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitPower(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Power(newLeft, newRight);
            }

            return term;
        }

        protected override Expression VisitMethodCall(MethodCallExpression term)
        {
            MethodInfo method = term.Method;

            TrigonometryFunction trigFunction;
            if(Trigonometry.TryParse(method, out trigFunction))
            {
                return VisitTrigonometry(term, trigFunction);
            }

            ExponentialFunction expFunction;
            if(Exponential.TryParse(method, out expFunction))
            {
                return VisitExponential(term, expFunction);
            }

            return VisitOtherMethodCall(term);
        }

        protected virtual Expression VisitOtherMethodCall(MethodCallExpression term)
        {
            return base.VisitMethodCall(term);
        }

        protected virtual Expression VisitTrigonometry(MethodCallExpression term, TrigonometryFunction function)
        {
            ReadOnlyCollection<Expression> newArguments = VisitExpressionList(term.Arguments);
            if(newArguments != term.Arguments)
            {
                if(newArguments.Count != 1)
                {
                    throw new InvalidOperationException("Single Argument Expected.");
                }

                return Trigonometry.Apply(function, newArguments[0]);
            }

            return term;
        }

        protected virtual Expression VisitExponential(MethodCallExpression term, ExponentialFunction function)
        {
            ReadOnlyCollection<Expression> newArguments = VisitExpressionList(term.Arguments);
            if(newArguments != term.Arguments)
            {
                if(newArguments.Count != 1)
                {
                    throw new InvalidOperationException("Single Argument Expected.");
                }

                return Exponential.Apply(function, newArguments[0]);
            }

            return term;
        }
    }
}
