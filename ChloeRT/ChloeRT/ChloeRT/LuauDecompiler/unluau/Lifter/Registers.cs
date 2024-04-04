﻿// Copyright (c) Valence. All Rights Reserved.
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unluau.Decleration;

namespace Unluau
{
    public class Registers
    {
        public int Count { get; private set; } = 0;
        public int Top { get; private set; }
        public bool ReassignOccupiedRegisters { get; set; } = false;

        private IDictionary<int, Decleration> declerations;
        private IDictionary<int, Expression> expressions;
        private Namer namer;
        private DecompilerOptions options;

        public Registers(Function function, IDictionary<int, Decleration> declerations, IDictionary<int, Expression> expressions, DecompilerOptions options)
        {
            Count = function.MaxStackSize;

            this.declerations = declerations;
            this.expressions = expressions;
            this.options = options;

            namer = new Namer(this);
        }

        public Registers(Function function, DecompilerOptions options)
            : this(function, new Dictionary<int, Decleration>(), new Dictionary<int, Expression>(), options)
        { }

        public Registers(Registers registers)
        {
            Count = registers.Count;

            declerations = new Dictionary<int,Decleration>(registers.declerations);
            expressions = new Dictionary<int, Expression>(registers.expressions);
            options = registers.options;

            namer = new Namer(this);
        }

        public void LoadRegister(int register, Expression expression, Block block, int pc, DeclerationType type = DeclerationType.Local)
        {
            LocalExpression local = LoadTempRegister(register, expression, block, type);

            var assignment = new LocalAssignment(local);

            block.AddStatement(assignment, pc);
        }

        // Literally just loads a register but doesn't create a local assignment for it. Mainly used for GETVARARGS
        public LocalExpression LoadTempRegister(int register, Expression expression, Block block, DeclerationType type)
        {
            Decleration decleration = namer.CreateDecleration(register, expression, block, type, !options.VariableNameGuessing);

            LocalExpression local = new LocalExpression(expression, decleration);

            SetDecleration(register, decleration);
            SetExpression(register, local);

            Top = register;

            return local;
        }

        public void FreeRegisters(Block block)
        {
            int localsCount = 0;
            int upvaluesCount = 0;
            int closuresCount = 0;

            // Eliminate all dead local assignments
            // TODO: Fix this horrendous code 
            for (int i = 0; i < block.Statements.Count; i++)
            {
                if (block.Statements[i] is LocalAssignment)
                {
                    LocalAssignment assignment = block.Statements[i] as LocalAssignment;

                    if (assignment.TryGetVariable(out LocalExpression variable) && variable.Decleration != null)
                    {
                        if (variable.Decleration.Referenced == 1)
                        {
                            block.Statements.RemoveAt(i);
                            i--;
                        }
                        else if (declerations.ContainsKey(variable.Decleration.Register))
                        {
                            // Update the ID of the variable
                            switch (variable.Decleration.Type)
                            {
                                case DeclerationType.Local:
                                    declerations[variable.Decleration.Register].Id = localsCount++;
                                    break;
                                case DeclerationType.Upvalue:
                                    declerations[variable.Decleration.Register].Id = upvaluesCount++;
                                    break;
                                case DeclerationType.Closure:
                                    declerations[variable.Decleration.Register].Id = closuresCount++;
                                    break;
                            }
                        }
                    }
                    else if (assignment.TryGetVariables(out ExpressionList expressions))
                    {
                        foreach (Expression expression in expressions.Expressions)
                        {
                            if (expression is LocalExpression local && declerations.ContainsKey(local.Decleration.Register) && expression != null)
                            {
                                // Update the ID of the variable
                                switch (local.Decleration.Type)
                                {
                                    case DeclerationType.Local:
                                        declerations[local.Decleration.Register].Id = localsCount++;
                                        break;
                                    case DeclerationType.Upvalue:
                                        declerations[local.Decleration.Register].Id = upvaluesCount++;
                                        break;
                                    case DeclerationType.Closure:
                                        declerations[local.Decleration.Register].Id = closuresCount++;
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            namer.PurifyVariableNames();

            declerations.Clear();
            expressions.Clear();
        }

        public void FreeRegister(int register, Block block)
        {
            Decleration decleration;

            if ((decleration = GetDecleration(register, false)) != null)
            {
                /*if (decleration.Referenced == 1)
                    block.Statements.RemoveAt(decleration.Location);*/
                // Remove decleration and expression
                declerations.Remove(register);
                expressions.Remove(register);
            }

            /*            // Remove decleration and expression
                        declerations.Remove(register);
                        expressions.Remove(register);*/
        }

        public void MoveRegister(int fromRegister, int toRegister)
        {
            Decleration decleration = GetDecleration(fromRegister);

            SetDecleration(toRegister, decleration);
            SetExpression(toRegister, expressions[fromRegister]);
        }

        public IList<Decleration> GetDeclerations()
        {
            return declerations.Values.ToList();
        }

        public IDictionary<int, Decleration> GetDeclerationDict()
        {
            return declerations;
        }

        public Decleration GetDecleration(int register, bool reference = true)
        {
            if (declerations.ContainsKey(register))
            {
                var decleration = declerations[register];

                if (reference && decleration != null)
                {
                    decleration.Referenced++;

                    SetDecleration(register, decleration);

                    return decleration;
                }
                else
                {
                    Console.WriteLine("UNLUAU ERROR: decleration was null");
                }
            }

            return null;
        }

        public bool IsEmpty(int register) => !(declerations.ContainsKey(register) && expressions.ContainsKey(register));

        public void SetDecleration(int register, Decleration decleration)
        {
            if (declerations.ContainsKey(register))
            {
                declerations.Remove(register);
            }
            
            declerations.Add(register, decleration);
        }

        public Expression GetExpression(int register, bool reference = true)
        {
            Decleration decleration = GetDecleration(register, reference);
            
            if (expressions.ContainsKey(register))
            {
                if (decleration != null)
                    ((LocalExpression)expressions[register]).Decleration = decleration;
                return expressions[register];
            }

            return null;
        }

        public Expression GetExpressionValue(int register)
        {
            if ((LocalExpression)GetExpression(register) != null)
            {
                return ((LocalExpression)GetExpression(register, false)).Expression;
            }

            return null;
        }

        public Expression GetRefExpressionValue(int register)
        {
            if ((LocalExpression)GetExpression(register) != null)
            {
                return ((LocalExpression)GetExpression(register)).Expression;
            }

            return null;
        }

        public void SetExpression(int register, Expression expression)
        {
            if (expressions.ContainsKey(register))
                expressions[register] = expression;
            else
                expressions.Add(register, expression);
        }
    }
}
