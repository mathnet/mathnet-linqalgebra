using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.ExpressionAlgebra;

    [TestFixture]
    public class ExpressionAlgebraSimplifyTests
    {
        [Test]
        public void TestSimplifyTernaryQuotient()
        {
            Expression<Func<double, double>> lambda = x => x / 2 / Math.PI;
            Assert.AreEqual(String.Format("((x / 2) / {0})", Math.PI), lambda.Body.ToString());

            Expression simplified = AutoSimplify.SimplifyFactors(lambda.Body);
            Assert.AreEqual(ExpressionType.Divide, simplified.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), simplified);
            Assert.AreEqual(String.Format("(x / ({0} * 2))", Math.PI), simplified.ToString());
        }

        [Test]
        public void TestSimplifyCrazyFactors()
        {
            Expression<Func<double, double, double, double, double>> lambda = (a, b, c, d) => (a / b / (c * a)) * (c * d / a) / d;
            Assert.AreEqual("((((a / b) / (c * a)) * ((c * d) / a)) / d)", lambda.Body.ToString());

            Expression simplified = AutoSimplify.SimplifyFactors(lambda.Body);
            Assert.AreEqual(ExpressionType.Divide, simplified.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), simplified);
            Assert.AreEqual("(((a * d) * c) / ((((d * a) * b) * a) * c))", simplified.ToString());
        }
    }
}
