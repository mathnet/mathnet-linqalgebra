using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra
{
    public static class Arithmeric
    {
        public static Expression Zero()
        {
            return ExpressionBuilder.ConstantDouble(0d);
        }

        public static Expression One()
        {
            return ExpressionBuilder.ConstantDouble(1d);
        }

        public static Expression MinusOne()
        {
            return ExpressionBuilder.ConstantDouble(-1d);
        }

        public static Expression Add(params Expression[] terms)
        {
            TypeInference type = new TypeInference(terms);
            return type.CastToMaxNumeric()
                .Where(x => !Elementary.IsConstantZero(x))
                .Reduce(Expression.Add, Zero());
        }

        public static Expression Subtract(Expression a, Expression b)
        {
            if(Elementary.IsConstantZero(a))
            {
                return Negate(b);
            }

            if(Elementary.IsConstantZero(b))
            {
                return a;
            }

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Subtract(expressions[0], expressions[1]);
        }

        public static Expression Negate(Expression term)
        {
            if(Elementary.IsConstantZero(term))
            {
                return Arithmeric.Zero();
            }

            if(term.NodeType == ExpressionType.Negate)
            {
                UnaryExpression unary = (UnaryExpression)term;
                return unary.Operand;
            }

            return Expression.Negate(term);
        }

        public static Expression Multiply(params Expression[] terms)
        {

            TypeInference type = new TypeInference(terms);
            List<Expression> factors = type.CastToMaxNumeric()
                .Where(x => !Elementary.IsConstantOne(x))
                .ToList();

            if(factors.Exists(Elementary.IsConstantZero))
            {
                return Zero();
            }

            return factors.Reduce(Expression.Multiply, One());
        }

        public static Expression Divide(Expression a, Expression b)
        {
            if(Elementary.IsConstantZero(a))
            {
                return Arithmeric.Zero();
            }

            if(Elementary.IsConstantOne(b))
            {
                return a;
            }

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Divide(expressions[0], expressions[1]);
        }

        public static Expression Invert(Expression term)
        {
            // TODO: automatic simplification

            return Divide(Expression.Constant(1), term);
        }

        public static Expression Power(Expression a, Expression b)
        {
            // TODO: automatic simplification

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Power(expressions[0], expressions[1]);
        }
    }
}
