using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.Numerics;
    using MathNet.Palladium.ExpressionAlgebra;

    [TestFixture]
    public class ExpressionAlgebraElementaryTests
    {
        [Test]
        public void TestNumeratorDenominatorOfTernaryProduct()
        {
            Expression<Func<double, double>> lambda = x => 2 * x * Trig.Cosecant(x);

            Expression nominator = Elementary.Numerator(lambda.Body);
            Assert.AreEqual(ExpressionType.Multiply, nominator.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), nominator);
            Assert.AreEqual("((2 * x) * Cosecant(x))", nominator.ToString());

            Expression denominator = Elementary.Denominator(lambda.Body);
            Assert.AreEqual(ExpressionType.Constant, denominator.NodeType);
            Assert.IsInstanceOfType(typeof(ConstantExpression), denominator);
            Assert.AreEqual("1", denominator.ToString());
        }

        [Test]
        public void TestNumeratorDenominatorOfBinaryQuotient()
        {
            Expression<Func<double, double>> lambda = x => Math.Exp(x) / (2 * x);

            Expression nominator = Elementary.Numerator(lambda.Body);
            Assert.AreEqual(ExpressionType.Call, nominator.NodeType);
            Assert.IsInstanceOfType(typeof(MethodCallExpression), nominator);
            Assert.AreEqual("Exp(x)", nominator.ToString());

            Expression denominator = Elementary.Denominator(lambda.Body);
            Assert.AreEqual(ExpressionType.Multiply, denominator.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), denominator);
            Assert.AreEqual("(2 * x)", denominator.ToString());
        }

        [Test]
        public void TestNumeratorDenominatorOfTernaryQuotient()
        {
            Expression<Func<double, double>> lambda = x => x / 2 / Math.PI;

            Expression nominator = Elementary.Numerator(lambda.Body);
            Assert.AreEqual(ExpressionType.Divide, nominator.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), nominator);
            Assert.AreEqual("(x / 2)", nominator.ToString());

            Expression denominator = Elementary.Denominator(lambda.Body);
            Assert.AreEqual(ExpressionType.Constant, denominator.NodeType);
            Assert.IsInstanceOfType(typeof(ConstantExpression), denominator);
            Assert.AreEqual(Math.PI.ToString(), denominator.ToString());
        }
    }
}
