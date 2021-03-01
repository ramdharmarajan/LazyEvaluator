using System;
using FluentAssertions;
using LazyEvaluator.Core;
using NUnit.Framework;

namespace LazyEvaluator
{
    public class EvaluatorShould
    {
        [Test]
        public void ThrowArgumentNullException_WhenNullFuncIsPassed()
        {
            var evaluator = new Evaluator<int>();

            Assert.Throws<ArgumentNullException>(() => evaluator.Add(null, 1));
        }
        
        [Test]
        public void ThrowDivideByZeroException_WhenDivideByZeroOperationIsAttempted()
        {
            var evaluator = new Evaluator<int>();

            evaluator.Add((val, _) => val / 0);

            Assert.Throws<DivideByZeroException>(() => evaluator.Evaluate(8));
        }
        
        [Test]
        public void DoesNotThrowArgumentNullException_WhenNullArgsIsPassed()
        {
            var evaluator = new Evaluator<int>();
            
            Assert.DoesNotThrow(() => evaluator.Add((val, _) => val / 4));
        }
            
        [TestCase(8, 22)]
        [TestCase(10, 23)]
        [TestCase(-11, 13)]
        [TestCase(0, 18)]
        [TestCase(100, 68)]
        public void CalculateTheCorrectOutput_WhenValidIntegerArgumentsArePassed(int seed, int output)
        {
            var evaluator = new Evaluator<int>();
            
            evaluator.Add((val, _) => val / 2);
            evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
            evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
            evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

            evaluator.Evaluate(seed).Should().Be(output);
        }
        
        [TestCase(8.545, 22.2725)]
        [TestCase(10.123, 23.0615)]
        [TestCase(0.0, 18)]
        [TestCase(100.987, 68.4935)]
        public void CalculateTheCorrectOutput_WhenValidDoubleArgumentsArePassed(double seed, double output)
        {
            var evaluator = new Evaluator<double>();
            
            evaluator.Add((val, _) => val / 2);
            evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
            evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
            evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

            evaluator.Evaluate(seed).Should().Be(output);
        }
    }
}