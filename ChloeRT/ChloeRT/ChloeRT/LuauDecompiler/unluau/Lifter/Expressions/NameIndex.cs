// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unluau
{
    public class NameIndex : Expression
    {
        public Expression Expression { get; set; }
        public string Name { get; set; }
        public bool IsSelf { get; set; }

        public NameIndex(Expression expression, string name, bool isSelf = false)
        {
            Expression = expression;
            Name = name;
            IsSelf = isSelf;
        }

        public override void Write(Output output)
        {
            if (Expression != null)
            {
                Expression.Write(output);
                output.Write((IsSelf ? ":" : ".") + Name);
            }
            else
            {
                Console.WriteLine("UNLUAU ERROR: Expression was null");
            }
        }

        public override string[] GetNames()
        {
            if (Expression != null) {
                string[] names = Expression.GetNames();

            if (names is null)
            {
                return new string[] { Name };
            }

            return names.Append(Name).ToArray();
        }
            else
            {
                Console.WriteLine("UNLUAU ERROR: Expression was null");
                return null;
            }
    }
    }
}
