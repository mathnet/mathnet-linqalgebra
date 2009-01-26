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
            return Expression.Call(_trigType.GetMethod("Sine"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression Cosine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cosine"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression Tangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Tangent"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression Cotangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cotangent"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression Secant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Secant"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression Cosecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("Cosecant"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression InverseSine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseSine"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseCosine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCosine"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseTangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseTangent"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseTangentFromRational(Expression nominator, Expression denominator)
        {
            return Expression.Call(_trigType.GetMethod("InverseTangentFromRational"), ExpressionBuilder.ConvertDouble(nominator), ExpressionBuilder.ConvertDouble(denominator));
        }

        public static Expression InverseCotangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCotangent"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseSecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseSecant"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseCosecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseCosecant"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression HyperbolicSine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicSine"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression HyperbolicCosine(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCosine"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression HyperbolicTangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicTangent"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression HyperbolicCotangent(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCotangent"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression HyperbolicSecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicSecant"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression HyperbolicCosecant(Expression radian)
        {
            return Expression.Call(_trigType.GetMethod("HyperbolicCosecant"), ExpressionBuilder.ConvertDouble(radian));
        }

        public static Expression InverseHyperbolicSine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicSine"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseHyperbolicCosine(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCosine"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseHyperbolicTangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicTangent"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseHyperbolicCotangent(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCotangent"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseHyperbolicSecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicSecant"), ExpressionBuilder.ConvertDouble(real));
        }

        public static Expression InverseHyperbolicCosecant(Expression real)
        {
            return Expression.Call(_trigType.GetMethod("InverseHyperbolicCosecant"), ExpressionBuilder.ConvertDouble(real));
        }
    }
}
