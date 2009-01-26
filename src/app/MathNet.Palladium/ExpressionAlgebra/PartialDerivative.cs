using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MathNet.Palladium.ExpressionAlgebra
{
    using MathNet.Palladium.ExpressionAlgebra.Visitors;

    /// <remarks>
    /// This class is stateful and not thread safe, therefore never share an instance
    /// between multiple threads (the ThreadStatic attribute might help).
    /// </remarks>
    public class PartialDerivative : AlgebraicVisitor
    {
        string _variableName;

        public Expression Differentiate(Expression term, string variableName)
        {
            _variableName = variableName;
            try
            {
                return Visit(term);
            }
            finally
            {
                _variableName = null;
            }
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if(p.Name == _variableName)
            {
                return Arithmeric.One();
            }

            return Arithmeric.Zero();
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            return Arithmeric.Zero();
        }

        protected override Expression VisitMultiply(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = base.Visit(binaryExpression.Left);
            Expression rightDerivative = base.Visit(binaryExpression.Right);
            return Arithmeric.Add(
                Arithmeric.Multiply(leftDerivative, binaryExpression.Right),
                Arithmeric.Multiply(rightDerivative, binaryExpression.Left));
        }

        protected override Expression VisitDivide(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = base.Visit(binaryExpression.Left);
            Expression rightDerivative = base.Visit(binaryExpression.Right);
            return Arithmeric.Subtract(
                Arithmeric.Divide(leftDerivative, binaryExpression.Right),
                Arithmeric.Divide(
                    Arithmeric.Multiply(binaryExpression.Left, rightDerivative),
                    Arithmeric.Multiply(binaryExpression.Right, binaryExpression.Right)));
        }

        protected override Expression VisitPower(BinaryExpression binaryExpression)
        {
            Expression leftDerivative = base.Visit(binaryExpression.Left);
            Expression rightDerivative = base.Visit(binaryExpression.Right);
            return Arithmeric.Multiply(
                binaryExpression,
                Arithmeric.Add(
                    Arithmeric.Multiply(
                        rightDerivative,
                        Expression.Call(
                            typeof(Math),
                            "Ln",
                            null,
                            binaryExpression.Left)),
                    Arithmeric.Divide(
                        Arithmeric.Multiply(binaryExpression.Right, leftDerivative),
                        binaryExpression.Left)));
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            switch(m.Method.Name)
            {
                case "Sin":
                case "Sine":
                    {
                        Expression innerDerivative = base.Visit(m.Arguments[0]);
                        return Arithmeric.Multiply(
                            innerDerivative,
                            Trigonometry.Cosine(m.Arguments[0]));
                    }
                default:
                    throw new NotSupportedException(String.Format("Method Call to {0} is not supported.", m.ToString()));
            }

        }
    }
}
