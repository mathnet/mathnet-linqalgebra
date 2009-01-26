//-----------------------------------------------------------------------
// <copyright file="Exponential.cs" company="Math.NET Project">
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
using System.Linq.Expressions;

namespace MathNet.ExpressionAlgebra
{
    public static class Exponential
    {
        static readonly Type _mathType = typeof(Math);

        /// <summary>
        /// Natural Logarithm
        /// </summary>
        public static Expression Ln(Expression term)
        {
            return ExpressionBuilder.CallDouble(_mathType.GetMethod("Log"), term);
        }

        /// <summary>
        /// Exponential
        /// </summary>
        public static Expression Exp(Expression term)
        {
            return ExpressionBuilder.CallDouble(_mathType.GetMethod("Exp"), term);
        }
    }
}
