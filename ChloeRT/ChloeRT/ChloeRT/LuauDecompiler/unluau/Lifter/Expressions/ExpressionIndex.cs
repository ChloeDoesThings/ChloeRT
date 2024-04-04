// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unluau
{
    public class ExpressionIndex : Expression
    {
        public Expression Expression { get; set; }
        public Expression Index { get; set; }

        public ExpressionIndex(Expression expression, Expression index)
        {
            Expression = expression;
            Index = index;
        }

        public override void Write(Output output)
        {
            if (Index != null && Expression != null)
            {
                Expression.Write(output);

                output.Write("[");
                Index.Write(output);
                output.Write("]");
            }
            else
            {
                Console.WriteLine("UNLUAU ERROR: Index and/or Expression was null");
            }
        }
    }
}
