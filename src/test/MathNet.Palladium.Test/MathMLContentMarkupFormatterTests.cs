using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;

namespace MathNet.Palladium.Test
{
    using MathNet.ExpressionAlgebra.Output;
    using MathNet.Numerics;

    [TestFixture]
    public class MathMLContentMarkupFormatterTests
    {
        [Test]
        public void TestMMLCFormatterLinear()
        {
            Expression<Func<double, double>> lambda = x => 2 * x + 5;
            MathMLContentMarkupFormatter formatter = new MathMLContentMarkupFormatter();
            XElement xml = formatter.Format(lambda.Body);
            Assert.AreEqual("<apply><plus /><apply><times /><cn>2</cn><ci>x</ci></apply><cn>5</cn></apply>", xml.ToString(SaveOptions.DisableFormatting));
            Assert.AreEqual("<apply>\r\n  <plus />\r\n  <apply>\r\n    <times />\r\n    <cn>2</cn>\r\n    <ci>x</ci>\r\n  </apply>\r\n  <cn>5</cn>\r\n</apply>", xml.ToString());
        }

        [Test]
        public void TestMMLCFormatterTrigonometry()
        {
            Expression<Func<double, double>> lambda = x => Math.Sin(x) * Trig.HyperbolicCosecant(x);
            MathMLContentMarkupFormatter formatter = new MathMLContentMarkupFormatter();
            XElement xml = formatter.Format(lambda.Body);
            Assert.AreEqual("<apply><times /><apply><sin /><ci>x</ci></apply><apply><csch /><ci>x</ci></apply></apply>", xml.ToString(SaveOptions.DisableFormatting));
        }
    }
}
