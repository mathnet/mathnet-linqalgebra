using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.Numerics;

    [TestFixture]
    public class LinqExpressionTests
    {
        [Test]
        public void LamdaExpressionOperatorDecompositionTest()
        {
            Expression<Func<double, double, double>> add = (x, y) => x + y;

            Assert.AreEqual(ExpressionType.Lambda, add.NodeType);
            Assert.IsInstanceOfType(typeof(LambdaExpression), add);
            Assert.AreEqual("(x, y) => (x + y)", add.ToString());

            Expression addBody = add.Body;
            Assert.AreEqual(ExpressionType.Add, addBody.NodeType);
            Assert.IsInstanceOfType(typeof(BinaryExpression), addBody);
            Assert.AreEqual("(x + y)", addBody.ToString());

            BinaryExpression addBodyBinary = (BinaryExpression)addBody;
            Assert.IsNull(addBodyBinary.Method);
        }

        [Test]
        public void LambdaExpressionFunctionDecompositionTest()
        {
            Expression<Func<double, double>> gamma = x => Fn.Gamma(x);

            Assert.AreEqual(ExpressionType.Lambda, gamma.NodeType);
            Assert.IsInstanceOfType(typeof(LambdaExpression), gamma);
            Assert.AreEqual("x => Gamma(x)", gamma.ToString());

            Expression gammaBody = gamma.Body;
            Assert.AreEqual(ExpressionType.Call, gammaBody.NodeType);
            Assert.IsInstanceOfType(typeof(MethodCallExpression), gammaBody);
            Assert.AreEqual("Gamma(x)", gammaBody.ToString());

            MethodCallExpression gammaBodyCall = (MethodCallExpression)gammaBody;
            Assert.IsNull(gammaBodyCall.Object);
            Assert.IsNotNull(gammaBodyCall.Method);
            Assert.AreEqual(typeof(Fn).GetMethod("Gamma"), gammaBodyCall.Method);
        }
    }
}
