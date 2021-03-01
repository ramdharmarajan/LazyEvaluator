using System;
using System.Collections.Generic;

namespace LazyEvaluator.Core
{
    public class Evaluator<T> where T : struct
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

                seed = expression.Func.Invoke(seed, args);
            });

            return seed;
        }
    }
}