using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra.Visitors
{
    /// <summary>
    /// Alebraic Linq Expression Visitor Base
    /// </summary>
    public abstract class AlgebraicVisitor : ExpressionVisitor
    {
        protected AlgebraicVisitor()
        {
        }

        protected override Expression VisitUnary(UnaryExpression term)
        {
            switch(term.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    return VisitMinus((UnaryExpression)term);
                case ExpressionType.UnaryPlus:
                    return VisitPlus((UnaryExpression)term);
                default:
                    return base.VisitUnary(term);
            }
        }

        protected override Expression VisitBinary(BinaryExpression term)
        {
            switch(term.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return VisitAdd((BinaryExpression)term);
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return VisitSubtract((BinaryExpression)term);
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return VisitMultiply((BinaryExpression)term);
                case ExpressionType.Divide:
                    return VisitDivide((BinaryExpression)term);
                case ExpressionType.Power:
                    return VisitPower((BinaryExpression)term);
                default:
                    return base.VisitBinary(term);
            }
        }

        protected virtual Expression VisitAdd(BinaryExpression binaryExpression)
        {
            return base.VisitBinary(binaryExpression);
        }

        protected virtual Expression VisitPower(BinaryExpression binaryExpression)
        {
            return base.VisitBinary(binaryExpression);
        }

        protected virtual Expression VisitDivide(BinaryExpression binaryExpression)
        {
            return base.VisitBinary(binaryExpression);
        }

        protected virtual Expression VisitMultiply(BinaryExpression binaryExpression)
        {
            return base.VisitBinary(binaryExpression);
        }

        protected virtual Expression VisitSubtract(BinaryExpression binaryExpression)
        {
            return base.VisitBinary(binaryExpression);
        }

        protected virtual Expression VisitPlus(UnaryExpression unaryExpression)
        {
            return base.VisitUnary(unaryExpression);
        }

        protected virtual Expression VisitMinus(UnaryExpression unaryExpression)
        {
            return base.VisitUnary(unaryExpression);
        }
    }
}
