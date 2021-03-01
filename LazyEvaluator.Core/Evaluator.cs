using System;
using System.Collections.Generic;

namespace LazyEvaluator.Core
{
    public class Evaluator<T> where T : struct
    {
        private readonly List<(Func<T, T[], T>, T[])> _expressions = new();
        
        public void Add(Func<T, T[], T> func, params T[] additionalArgs)
        {
            _expressions.Add((func, additionalArgs));
        }

        public T Evaluate(T seed)
        {
            _expressions.ForEach(expression =>
            {
                var args = expression.Item2;

                seed = expression.Item1.Invoke(seed, args);
            });

            return seed;
        }
    }
}