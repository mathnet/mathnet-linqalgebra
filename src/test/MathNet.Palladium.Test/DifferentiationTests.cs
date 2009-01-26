using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.Palladium.ExpressionAlgebra;

    [TestFixture]
    public class DifferentiationTests
    {
        [Test]
        public void TestDeriveConstant()
        {
            Expression<Func<double, double>> lambda = x => 5d;

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Constant, derivative.NodeType);
            Assert.AreEqual("0", derivative.ToString());
        }

        [Test]
        public void TestDeriveVariable()
        {
            Expression<Func<double, double>> lambda = x => x;

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Constant, derivative.NodeType);
            Assert.AreEqual("1", derivative.ToString());
        }

        [Test]
        public void TestDeriveLinear()
        {
            Expression<Func<double, double>> lambda = x => 2*x + 5;

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Constant, derivative.NodeType);
            Assert.AreEqual("2", derivative.ToString());
        }

        [Test]
        public void TestDeriveSine()
        {
            Expression<Func<double, double>> lambda = x => Math.Sin(x);

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Call, derivative.NodeType);
            Assert.AreEqual("Cosine(x)", derivative.ToString());
        }

        [Test]
        public void TestDeriveCosineProduct()
        {
            Expression<Func<double, double, double>> lambda = (x, y) => Math.Cos(x) * Math.Sin(y);

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Multiply, derivative.NodeType);
            Assert.AreEqual("(-Sine(x) * Sin(y))", derivative.ToString());

            /*
            NOTE: The reason why the first is named "Sine" while the second uses "Sin" is
            that Math.NET Palladium always uses the Math.NET Iridium trigonometric functions
            (MathNet.Numerics.Trig.Sine(x)). However, the second instance (System.Math.Sin)
            was not touched by Palladium at all and therefore is still using Math.Sin.
            */
        }

        public void TestDeriveSineInv()
        {
            Expression<Func<double, double>> lambda = x => Math.Sin(1d/x);

            PartialDerivative pd = new PartialDerivative();
            Expression derivative = pd.Differentiate(lambda.Body, "x");
            Assert.AreEqual(ExpressionType.Multiply, derivative.NodeType);
            Assert.AreEqual("(-(1 / (x * x)) * Cosine(1 / x))", derivative.ToString());
        }
    }
}
