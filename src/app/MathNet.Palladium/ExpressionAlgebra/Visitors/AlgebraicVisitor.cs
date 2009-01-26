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

        protected virtual Expression VisitAdd(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Add(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitSubtract(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Subtract(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitPlus(UnaryExpression term)
        {
            return base.VisitUnary(term);
        }

        protected virtual Expression VisitMinus(UnaryExpression term)
        {
            return base.VisitUnary(term);
        }

        protected virtual Expression VisitMultiply(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Multiply(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitDivide(BinaryExpression term)
        {
            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Divide(newLeft, newRight);
            }

            return term;
        }

        protected virtual Expression VisitPower(BinaryExpression term)
        {

            Expression newLeft = Visit(term.Left);
            Expression newRight = Visit(term.Right);
            if(newLeft != term.Left || newRight != term.Right)
            {
                return Arithmeric.Power(newLeft, newRight);
            }

            return term;
        }
    }
}
