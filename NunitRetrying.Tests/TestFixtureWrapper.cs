using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NunitRetrying.Tests
{
    public class TestFixtureWrapper<TFixture>
        where TFixture : new()
    {
        private readonly TFixture _fixture = new TFixture();

        public TestWrapper GetTest(Expression<Action<TFixture>> testSelector)
        {
            var testMethodName = ((MethodCallExpression) testSelector.Body).Method.Name;

            return GetTest(testMethodName);
        }

        private TestWrapper GetTest(string testName)
        {
            var testSuite = GetTestSuite();

            var selectedTest = testSuite.Tests.Single(test => test.Name == testName);

            return new TestWrapper(selectedTest, _fixture);
        }

        private TestSuite GetTestSuite()
        {
            var suiteBuilder = new DefaultSuiteBuilder();

            var testSuite = suiteBuilder.BuildFrom(new TypeWrapper(typeof(TFixture)));

            testSuite.Fixture = _fixture;

            return testSuite;
        }
    }
}