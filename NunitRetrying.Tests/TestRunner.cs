using System;
using System.Linq.Expressions;
using NUnit.Framework.Interfaces;

namespace NunitRetrying.Tests
{
    public static class TestRunner<TFixture>
        where TFixture : new()
    {
        public static ITestResult Run(Expression<Action<TFixture>> testSelector)
        {
            var fixture = new TestFixtureWrapper<TFixture>();

            var test = fixture.GetTest(testSelector);

            return test.Run();
        }
    }
}