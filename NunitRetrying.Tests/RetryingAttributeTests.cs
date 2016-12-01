using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NunitRetrying.Tests
{
    public class RetryingAttributeTests
    {
        [Test]
        public void test_succeeds_when_single_retry_succeeds()
        {
            var test = TestFixture<RetryingTestMethods>.GetTest(fixture => fixture.will_succeed_on_a_third_run());

            var result = test.Run();

            result.ResultState.Should().Be(ResultState.Success);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void test_fails_when_last_retry_fails()
        {
            var test = TestFixture<RetryingTestMethods>.GetTest(fixture => fixture.will_fail_on_a_second_run());

            var result = test.Run();

            result.ResultState.Should().Be(ResultState.Failure);
            result.Output.Should().Contain("Test retried 1 time/s.");
        }
    }
}