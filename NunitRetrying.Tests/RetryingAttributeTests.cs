using FluentAssertions;
using NunitRetrying.Tests.RunningTests;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NunitRetrying.Tests
{
    public class RetryingAttributeTests
    {
        [Test]
        public void test_succeeds_when_single_retry_has_no_assertion_failures()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.fails_assertion_two_times_and_retries_two_times());

            result.ResultState.Should().Be(ResultState.Success);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void test_result_is_failure_when_last_retry_fails_assertion()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.fails_assertion_two_times_and_retries_one_time());

            result.ResultState.Should().Be(ResultState.Failure);
            result.Output.Should().Contain("Test retried 1 time/s.");
        }

        [Test]
        public void test_succeeds_when_single_retry_throws_no_unhandled_exceptions()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throws_exception_two_times_and_retries_two_times());

            result.ResultState.Should().Be(ResultState.Success);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void test_result_is_error_when_last_retry_throws_exception()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throws_exception_two_times_and_retries_one_time());

            result.ResultState.Should().Be(ResultState.Error);
            result.Output.Should().Contain("Test retried 1 time/s.");
        }
    }
}