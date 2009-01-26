using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MathNet.Palladium.ExpressionAlgebra
{
    internal class TypeInference
    {
        static readonly string[] numericOrder = { "Int16", "Int32", "Int64", "Single", "Double" };
        List<Expression> _expressions;

        public TypeInference(Expression a, Expression b)
        {
            _expressions = new List<Expression>() { a, b };
        }

        public TypeInference(params Expression[] expressions)
        {
            _expressions = new List<Expression>(expressions);
        }

        public TypeInference(List<Expression> expressions)
        {
            _expressions = expressions;
        }

        public List<Expression> CastToMaxNumericList()
        {
            Type maxType = MaxNumeric(_expressions.Select(x => x.Type));
            return new List<Expression>(_expressions.Select(x => (x.Type == maxType) ? x : Expression.Convert(x, maxType)));
        }

        public IEnumerable<Expression> CastToMaxNumeric()
        {
            Type maxType = MaxNumeric(_expressions.Select(x => x.Type));
            return _expressions.Select(x => (x.Type == maxType) ? x : Expression.Convert(x, maxType));
        }

        public static Type MaxNumeric(Type a, Type b)
        {
            string nameA = a.Name;
            string nameB = b.Name;

            if(nameA == nameB)
            {
                return a;
            }

            int indexA = Array.IndexOf(numericOrder, nameA);
            int indexB = Array.IndexOf(numericOrder, nameB);

            if(indexA < 0)
            {
                return b;
            }

            if(indexB < 0)
            {
                return a;
            }

            return (indexA > indexB) ? a : b;
        }

        public static Type MaxNumeric(IEnumerable<Type> types)
        {
            Type max = typeof(Int32);

            foreach(Type type in types)
            {
                max = MaxNumeric(max, type);
            }

            return max;
        }
    }
}
