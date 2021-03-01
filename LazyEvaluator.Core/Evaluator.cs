using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LazyEvaluator.Core
{
    public class Evaluator<T> where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    {
        private readonly List<ExpressionArgsMapper<T>> _expressions = new();
        
        public void Add(Func<T, T[], T> func, params T[] additionalArgs)
        {
            if (func == null)
            {
                throw new ArgumentNullException($"argument {nameof(func)} is null");
            }
            
            _expressions.Add(new ExpressionArgsMapper<T>(func, additionalArgs));
        }

        public T Evaluate(T seed)
        {
            _expressions.ForEach(expression =>
            {
                var args = expression.Args;

                try
                {
                    seed = expression.Func.Invoke(seed, args);
                }
                catch (DivideByZeroException divideByZeroException)
                {
                    Trace.WriteLine($"Divide by Zero Exception occured during evaluation: {divideByZeroException.Message}");
                    throw;
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                    throw;
                }
            });

            return seed;
        }
    }
}