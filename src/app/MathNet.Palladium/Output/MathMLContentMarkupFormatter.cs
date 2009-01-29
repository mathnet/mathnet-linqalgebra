//-----------------------------------------------------------------------
// <copyright file="MathMLContentMarkupFormatter.cs" company="Math.NET Project">
//    Copyright (c) 2002-2009, Christoph Rüegg.
//    All Right Reserved.
// </copyright>
// <author>
//    Christoph Rüegg, http://christoph.ruegg.name
// </author>
// <product>
//    Math.NET Palladium, part of the Math.NET Project.
//    http://mathnet.opensourcedotnet.info
// </product>
// <license type="opensource" name="LGPL" version="2 or later">
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published 
//    by the Free Software Foundation; either version 2 of the License, or
//    any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public 
//    License along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// </license>
//-----------------------------------------------------------------------

using System.Linq.Expressions;
using System.Xml.Linq;

namespace MathNet.ExpressionAlgebra.Output
{
    using MathNet.ExpressionAlgebra.Visitors;

    public class MathMLContentMarkupFormatter : AlgebraicVisitor<XElement>
    {
        enum MathMLTrigonometryFunction
        {
            sin = TrigonometryFunction.Sine,
            cos = TrigonometryFunction.Cosine,
            tan = TrigonometryFunction.Tangent,
            cot = TrigonometryFunction.Cotangent,
            sec = TrigonometryFunction.Secant,
            csc = TrigonometryFunction.Cosecant,
            arcsin = TrigonometryFunction.InverseSine,
            arccos = TrigonometryFunction.InverseCosine,
            arctan = TrigonometryFunction.InverseTangent,
            arccot = TrigonometryFunction.InverseCotangent,
            arcsec = TrigonometryFunction.InverseSecant,
            arccsc = TrigonometryFunction.InverseCosecant,
            sinh = TrigonometryFunction.HyperbolicSine,
            cosh = TrigonometryFunction.HyperbolicCosine,
            tanh = TrigonometryFunction.HyperbolicTangent,
            coth = TrigonometryFunction.HyperbolicCotangent,
            sech = TrigonometryFunction.HyperbolicSecant,
            csch = TrigonometryFunction.HyperbolicCosecant,
            arcsinh = TrigonometryFunction.InverseHyperbolicSine,
            arccosh = TrigonometryFunction.InverseHyperbolicCosine,
            arctanh = TrigonometryFunction.InverseHyperbolicTangent,
            arccoth = TrigonometryFunction.InverseHyperbolicCotangent,
            arcsech = TrigonometryFunction.InverseHyperbolicSecant,
            arccsch = TrigonometryFunction.InverseHyperbolicCosecant
        }

        enum MathMLExponentialFunction
        {
            exp = ExponentialFunction.Exp,
            ln = ExponentialFunction.Ln
        }

        public XElement Format(Expression term)
        {
            return Visit(term);
        }

        protected override XElement VisitConstant(ConstantExpression term)
        {
            return new XElement("cn", term.Value.ToString());
        }

        protected override XElement VisitParameter(ParameterExpression term)
        {
            return new XElement("ci", term.Name);
        }

        protected override XElement VisitAdd(BinaryExpression term)
        {
            // NOTE: n-ary support
            return new XElement("apply",
                new XElement("plus"),
                Visit(term.Left),
                Visit(term.Right));
        }

        protected override XElement VisitSubtract(BinaryExpression term)
        {
            return new XElement("apply",
                new XElement("minus"),
                Visit(term.Left),
                Visit(term.Right));
        }

        protected override XElement VisitPlus(UnaryExpression term)
        {
            return Visit(term.Operand);
        }

        protected override XElement VisitMinus(UnaryExpression term)
        {
            return new XElement("apply",
                new XElement("minus"),
                Visit(term.Operand));
        }

        protected override XElement VisitMultiply(BinaryExpression term)
        {
            // NOTE: n-ary support
            return new XElement("apply",
                new XElement("times"),
                Visit(term.Left),
                Visit(term.Right));
        }

        protected override XElement VisitDivide(BinaryExpression term)
        {
            return new XElement("apply",
                new XElement("divide"),
                Visit(term.Left),
                Visit(term.Right));
        }

        protected override XElement VisitPower(BinaryExpression term)
        {
            return new XElement("apply",
                new XElement("power"),
                Visit(term.Left),
                Visit(term.Right));
        }

        protected override XElement VisitTrigonometry(MethodCallExpression term, TrigonometryFunction function)
        {
            return new XElement("apply",
                new XElement(((MathMLTrigonometryFunction)function).ToString()),
                Visit(term.Arguments[0]));
        }

        protected override XElement VisitExponential(MethodCallExpression term, ExponentialFunction function)
        {
            return new XElement("apply",
                new XElement(((MathMLExponentialFunction)function).ToString()),
                Visit(term.Arguments[0]));
        }
    }
}
