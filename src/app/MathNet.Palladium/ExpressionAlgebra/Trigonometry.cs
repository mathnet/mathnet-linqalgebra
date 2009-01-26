using System;
using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra
{
    using MathNet.Numerics;

    public static class Trigonometry
    {
        static readonly Type _trigType = typeof(Trig);

        public static Expression Sine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Sine"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression Cosine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cosine"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression Tangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Tangent"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression Cotangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cotangent"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression Secant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Secant"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression Cosecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cosecant"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression InverseSine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseSine"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseCosine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCosine"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseTangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseTangent"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseTangentFromRational(Expression nominator, Expression denominator)
        {
            return Expression.Call(_trigType.GetMethod("InverseTangentFromRational"), ExpressionBuilder.CastToDouble(nominator), ExpressionBuilder.CastToDouble(denominator));
        }

        public static Expression InverseCotangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCotangent"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseSecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseSecant"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseCosecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCosecant"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression HyperbolicSine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicSine"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression HyperbolicCosine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCosine"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression HyperbolicTangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicTangent"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression HyperbolicCotangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCotangent"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression HyperbolicSecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicSecant"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression HyperbolicCosecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCosecant"), ExpressionBuilder.CastToDouble(radian));
        }

        public static Expression InverseHyperbolicSine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicSine"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseHyperbolicCosine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCosine"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseHyperbolicTangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicTangent"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseHyperbolicCotangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCotangent"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseHyperbolicSecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicSecant"), ExpressionBuilder.CastToDouble(real));
        }

        public static Expression InverseHyperbolicCosecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCosecant"), ExpressionBuilder.CastToDouble(real));
        }
    }
}
