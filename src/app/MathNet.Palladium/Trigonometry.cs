//-----------------------------------------------------------------------
// <copyright file="Trigonometry.cs" company="Math.NET Project">
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

namespace MathNet.ExpressionAlgebra
{
    using MathNet.Numerics;

    /// <summary>
    /// Describing a trigonometric function.
    /// </summary>
    public enum TrigonometryFunction
    {
        Sine,
        Cosine,
        Tangent,
        Cotangent,
        Secant,
        Cosecant,
        InverseSine,
        InverseCosine,
        InverseTangent,
        InverseCotangent,
        InverseSecant,
        InverseCosecant,
        HyperbolicSine,
        HyperbolicCosine,
        HyperbolicTangent,
        HyperbolicCotangent,
        HyperbolicSecant,
        HyperbolicCosecant,
        InverseHyperbolicSine,
        InverseHyperbolicCosine,
        InverseHyperbolicTangent,
        InverseHyperbolicCotangent,
        InverseHyperbolicSecant,
        InverseHyperbolicCosecant
    }

    /// <summary>
    /// Trigonometric Expression Builder
    /// </summary>
    public static class Trigonometry
    {
        static readonly Type _trigType = typeof(Trig);

        public static Expression Apply(Expression argument, TrigonometryFunction function)
        {
            return ExpressionBuilder.CallDouble(_trigType, function.ToString(), argument);
        }

        public static bool TryParse(MethodInfo method, out TrigonometryFunction function)
        {
            string name = method.Name;
            if(!Enum.IsDefined(typeof(TrigonometryFunction), name))
            {
                function = (TrigonometryFunction)0;
                return false;
            }

            function = (TrigonometryFunction)Enum.Parse(typeof(TrigonometryFunction), name);
            return true;
        }

        public static Expression Sine(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Sine"), radian);
        }

        public static Expression Cosine(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Cosine"), radian);
        }

        public static Expression Tangent(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Tangent"), radian);
        }

        public static Expression Cotangent(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Cotangent"), radian);
        }

        public static Expression Secant(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Secant"), radian);
        }

        public static Expression Cosecant(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("Cosecant"), radian);
        }

        public static Expression InverseSine(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseSine"), real);
        }

        public static Expression InverseCosine(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseCosine"), real);
        }

        public static Expression InverseTangent(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseTangent"), real);
        }

        public static Expression InverseTangentFromRational(Expression nominator, Expression denominator)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseTangentFromRational"), nominator, denominator);
        }

        public static Expression InverseCotangent(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseCotangent"), real);
        }

        public static Expression InverseSecant(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseSecant"), real);
        }

        public static Expression InverseCosecant(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseCosecant"), real);
        }

        public static Expression HyperbolicSine(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicSine"), radian);
        }

        public static Expression HyperbolicCosine(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicCosine"), radian);
        }

        public static Expression HyperbolicTangent(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicTangent"), radian);
        }

        public static Expression HyperbolicCotangent(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicCotangent"), radian);
        }

        public static Expression HyperbolicSecant(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicSecant"), radian);
        }

        public static Expression HyperbolicCosecant(Expression radian)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("HyperbolicCosecant"), radian);
        }

        public static Expression InverseHyperbolicSine(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicSine"), real);
        }

        public static Expression InverseHyperbolicCosine(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicCosine"), real);
        }

        public static Expression InverseHyperbolicTangent(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicTangent"), real);
        }

        public static Expression InverseHyperbolicCotangent(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicCotangent"), real);
        }

        public static Expression InverseHyperbolicSecant(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicSecant"), real);
        }

        public static Expression InverseHyperbolicCosecant(Expression real)
        {
            return ExpressionBuilder.CallDouble(_trigType.GetMethod("InverseHyperbolicCosecant"), real);
        }
    }
}
