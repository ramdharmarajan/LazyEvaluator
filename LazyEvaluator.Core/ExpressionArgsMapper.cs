using System;

namespace LazyEvaluator.Core
{
    public class ExpressionArgsMapper<T> where T : struct
    {
        public ExpressionArgsMapper(Func<T, T[], T> func, T[] args)
        {
            Func = func;
            Args = args;
        }
        
        public Func<T, T[], T> Func { get; }
        
        public T[] Args { get; }
    }
}