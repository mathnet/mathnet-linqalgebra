using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MathNet.Palladium.ExpressionAlgebra
{
    public static class Arithmeric
    {
        public static Expression Zero()
        {
            return Expression.Constant(0d, typeof(double));
        }

        public static Expression One()
        {
            return Expression.Constant(1d, typeof(double));
        }

        public static Expression MinusOne()
        {
            return Expression.Constant(-1d, typeof(double));
        }

        public static Expression Add(params Expression[] terms)
        {
            // TODO: automatic simplification

            TypeInference type = new TypeInference(terms);
            return type.CastToMaxNumeric()
                .Where(x => (x.NodeType != ExpressionType.Constant) || (Convert.ToDouble(((ConstantExpression)x).Value) != 0d))
                .Reduce(Expression.Add, Zero());
        }

        public static Expression Subtract(Expression a, Expression b)
        {
            // TODO: automatic simplification

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Subtract(expressions[0], expressions[1]);
        }

        public static Expression Negate(Expression a)
        {
            // TODO: automatic simplification

            return Expression.Negate(a);
        }

        public static Expression Multiply(params Expression[] terms)
        {
            // TODO: automatic simplification

            TypeInference type = new TypeInference(terms);
            List<Expression> factors = type.CastToMaxNumeric()
                .Where(x => (x.NodeType != ExpressionType.Constant) || (Convert.ToDouble(((ConstantExpression)x).Value) != 1d))
                .ToList();

            if(factors.Exists(x => (x.NodeType == ExpressionType.Constant) && (Convert.ToDouble(((ConstantExpression)x).Value) == 0d)))
            {
                return Zero();
            }

            return factors.Reduce(Expression.Multiply, One());
        }

        public static Expression Divide(Expression a, Expression b)
        {
            // TODO: automatic simplification

            TypeInference type = new TypeInference(a, b);
            List<Expression> expressions = type.CastToMaxNumericList();
            return Expression.Divide(expressions[0], expressions[1]);
        }

        public static Expression Invert(Expression a)
        {
            // TODO: automatic simplification

            return Divide(Expression.Constant(1), a);
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
