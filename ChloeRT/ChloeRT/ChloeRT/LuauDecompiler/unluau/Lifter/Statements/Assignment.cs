﻿// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unluau
{
    public class Assignment : Statement
    {
        public Expression Variable { get; private set; }
        public Expression Value { get; private set; }

        public Assignment(Expression variable, Expression value)
        {
            Variable = variable;
            Value = value;
        }

        public override void Write(Output output)
        {
            if (Value != null)
            {
                if (Value is LocalExpression && ((LocalExpression)Value).Expression is Closure)
                {
                    output.Write("function ");
                    Variable.Write(output);

                    ((LocalExpression)Value).Expression.Write(output);
                    output.WriteLine();
                }
                else
                {
                    Variable.Write(output);
                    output.Write(" = ");
                    Value.Write(output);
                }
            }
            else
            {
                Console.WriteLine("UNLUAU ERROR: Value was null");
            }
        }
    }
}
