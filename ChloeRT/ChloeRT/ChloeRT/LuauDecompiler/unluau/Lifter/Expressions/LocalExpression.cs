// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unluau
{
    public class LocalExpression : Expression
    {
        public Expression Expression { get; private set; }
        public Decleration Decleration { get; set; }

        public LocalExpression(Expression expression, Decleration decleration)
        {
            Expression = expression;
            Decleration = decleration;
        }

        public override void Write(Output output)
        {
            if (Expression != null && Decleration != null)
            {
                if (Decleration.Referenced != 1)
                    output.Write(Decleration.Name);
                else if (Expression != null)
                {
                    if (Expression is Closure)
                        output.Write("function");

                    Expression.Write(output);
                }
                else
                    output.Write(Decleration.Name);
            }
            else
            {
                Console.WriteLine("UNLUAU ERROR: Expression and/or Decleration was null");
                output.Write("???");
            }
        }

        public override string[] GetNames()
        {
            if (Expression is null && Decleration != null)
            {
                return new string[] { Decleration.Name };
            }
            else
            {
                if (Expression != null)
                {
                    return Expression.GetNames();
                }
                else
                {
                    Console.WriteLine("UNLUAU ERROR: Expression was null");
                    return null;
                }
            }
        }
    }
}
