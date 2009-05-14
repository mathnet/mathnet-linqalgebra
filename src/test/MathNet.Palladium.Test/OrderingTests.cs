using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;

namespace MathNet.Palladium.Test
{
    using MathNet.ExpressionAlgebra;
    using MathNet.Numerics;

    [TestFixture]
    public class OrderingTests
    {
        [Test]
        public void TestFrameworkStringCompare()
        {
            Assert.AreEqual(0, String.Compare("a", "a", StringComparison.Ordinal));
            Assert.AreEqual(-1, String.Compare("a", "b", StringComparison.Ordinal));
            Assert.AreEqual(1, String.Compare("b", "a", StringComparison.Ordinal));
            Assert.AreEqual(32, String.Compare("a", "A", StringComparison.Ordinal));
            Assert.AreEqual(-32, String.Compare("A", "a", StringComparison.Ordinal));
        }

        [Test]
        public void TestParameterOrdering()
        {
            ParameterExpression a = Expression.Parameter(typeof(int), "a");
            ParameterExpression b = Expression.Parameter(typeof(int), "b");
            ParameterExpression c = Expression.Parameter(typeof(int), "c");
            ParameterExpression B = Expression.Parameter(typeof(int), "B");
            ParameterExpression C = Expression.Parameter(typeof(int), "C");

            Assert.IsTrue(Ordering.Compare(a, b) < 0, "a < b");
            Assert.IsTrue(Ordering.Compare(b, a) > 0, "b > a");
            Assert.IsTrue(Ordering.Compare(a, c) < 0, "a < c");
            Assert.IsTrue(Ordering.Compare(a, a) == 0, "a = a");
            Assert.IsTrue(Ordering.Compare(c, c) == 0, "c = c");

            Assert.IsTrue(Ordering.Compare(B, C) < 0, "B < C");
            Assert.IsTrue(Ordering.Compare(C, B) > 0, "C > B");
            Assert.IsTrue(Ordering.Compare(C, C) == 0, "C = C");

            Assert.IsTrue(Ordering.Compare(b, B) > 0, "b > B");
            Assert.IsTrue(Ordering.Compare(c, C) > 0, "c > C");
            Assert.IsTrue(Ordering.Compare(c, B) > 0, "c > B");
        }

        [Test]
        public void TestTypeOrdering()
        {
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            ConstantExpression c = Expression.Constant(2);
            BinaryExpression sum = Expression.Add(x, c);
            BinaryExpression product = Expression.Multiply(x, c);
            Expression cos = Trigonometry.Cosine(x);
            Expression sin = Trigonometry.Sine(x);

            Assert.IsTrue(Ordering.Compare(c, x) < 0, "2 < x");
            Assert.IsTrue(Ordering.Compare(x, c) > 0, "x > 2");

            Assert.IsTrue(Ordering.Compare(c, sum) < 0, "2 < (2+x)");
            Assert.IsTrue(Ordering.Compare(sum, c) > 0, "(2+x) > 2");

            Assert.IsTrue(Ordering.Compare(x, sum) < 0, "x < (2+x)");
            Assert.IsTrue(Ordering.Compare(sum, x) > 0, "(2+x) > x");

            Assert.IsTrue(Ordering.Compare(x, sin) < 0, "x < sin(x)");
            Assert.IsTrue(Ordering.Compare(sin, x) > 0, "sin(x) > x");

            Assert.IsTrue(Ordering.Compare(product, sum) < 0, "2*x < (2+x)");
            Assert.IsTrue(Ordering.Compare(sum, product) > 0, "(2+x) > 2*x");

            Assert.IsTrue(Ordering.Compare(sum, sum) == 0, "(2+x) = (2+x)");
            Assert.IsTrue(Ordering.Compare(product, product) == 0, "2*x = 2*x");

            Assert.IsTrue(Ordering.Compare(product, sin) < 0, "2*x < sin(x)");
            Assert.IsTrue(Ordering.Compare(sin, product) > 0, "sin(x) > 2*x");

            Assert.IsTrue(Ordering.Compare(cos, sin) < 0, "cos(x) < sin(x)");
            Assert.IsTrue(Ordering.Compare(sin, cos) > 0, "sin(x) > cos(x)");
        }
    }
}
