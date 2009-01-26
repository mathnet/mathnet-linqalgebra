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
