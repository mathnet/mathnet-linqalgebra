using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
            Assert.AreEqual("Cos(x)", derivative.ToString());
        }
    }
}
