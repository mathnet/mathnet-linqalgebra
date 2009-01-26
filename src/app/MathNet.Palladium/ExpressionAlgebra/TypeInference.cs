//-----------------------------------------------------------------------
// <copyright file="TypeInference.cs" company="Math.NET Project">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra
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
            Type max = typeof(int);

            foreach(Type type in types)
            {
                max = MaxNumeric(max, type);
            }

            return max;
        }
    }
}
