using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.ExpressionAlgebra;
    using MathNet.Numerics;

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

        [Test]
        public void TestFactorsOfSum()
        {
            Expression<Func<double, double>> lambda = x => 2 + x;

            List<Expression> factors = Elementary.Factors(lambda.Body);
            Assert.AreEqual(1, factors.Count);
            Assert.AreEqual(lambda.Body, factors[0]);
        }

        [Test]
        public void TestFactorsOfProduct()
        {
            Expression<Func<double, double>> lambda = x => 2 * x * Math.Sin(x);

            List<Expression> factors = Elementary.Factors(lambda.Body);
            Assert.AreEqual(3, factors.Count);
            Assert.AreEqual("2", factors[0].ToString());
            Assert.AreEqual("x", factors[1].ToString());
            Assert.AreEqual("Sin(x)", factors[2].ToString());
        }

        [Test]
        public void TestFactorsOfProductWithQuotients()
        {
            Expression<Func<double, double>> lambda = x => 2 * (x / Math.Sin(x)) * (1 / (x * Math.Exp(x) * (1 + x)));

            List<Expression> factors = Elementary.Factors(lambda.Body);
            Assert.AreEqual(7, factors.Count);
            Assert.AreEqual("2", factors[0].ToString());
            Assert.AreEqual("x", factors[1].ToString());
            Assert.AreEqual("(1 / Sin(x))", factors[2].ToString());
            Assert.AreEqual("1", factors[3].ToString());
            Assert.AreEqual("(1 / x)", factors[4].ToString());
            Assert.AreEqual("(1 / Exp(x))", factors[5].ToString());
            Assert.AreEqual("(1 / (1 + x))", factors[6].ToString());
        }

        [Test]
        public void TestFactorsOfNestedQuotients()
        {
            Expression<Func<double, double, double, double, double>> lambda = (a, b, c, d) => (a / b / (c * a)) * (c * d / a) / d;

            List<Expression> factors = Elementary.Factors(lambda.Body);
            Assert.AreEqual(8, factors.Count);
            Assert.AreEqual("a", factors[0].ToString());
            Assert.AreEqual("(1 / b)", factors[1].ToString());
            Assert.AreEqual("(1 / c)", factors[2].ToString());
            Assert.AreEqual("(1 / a)", factors[3].ToString());
            Assert.AreEqual("c", factors[4].ToString());
            Assert.AreEqual("d", factors[5].ToString());
            Assert.AreEqual("(1 / a)", factors[6].ToString());
            Assert.AreEqual("(1 / d)", factors[7].ToString());
        }
    }
}
