using FluentAssertions;
using LazyEvaluator.Core;
using NUnit.Framework;

namespace LazyEvaluator
{
    public class EvaluatorShould
    {
        [Test]
        public void CalculateTheCorrectOutput()
        {
            Evaluator<int> evaluator = new Evaluator<int>();
            
            evaluator.Add((val, additionalVals) => val / 2);
            evaluator.Add((val, additionalVals) => val + additionalVals[0], 5);
            evaluator.Add((val, additionalVals) => val + 1 + additionalVals[0], 20);
            evaluator.Add((val, additionalVals) => val - additionalVals[0] - additionalVals[1], 5, 3);

            evaluator.Evaluate(8).Should().Be(22);
        }
    }
}