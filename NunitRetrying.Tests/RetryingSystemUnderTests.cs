using System;
using NUnit.Framework;
using NUnitRetrying;

namespace NunitRetrying.Tests
{
    [Explicit("These tests should only run programmatically by other tests or for debugging purposes.")]
    public class RetryingSystemUnderTests
    {
        private int _timesToFail = 2;

        [Test]
        [Retrying(Times = 2)]
        public void fails_assertion_two_times_and_retries_two_times()
        {
            MaybeFailAssertion();
        }

        [Test]
        [Retrying(Times = 1)]
        public void fails_assertion_two_times_and_retries_one_time()
        {
            MaybeFailAssertion();
        }

        private void MaybeFailAssertion()
        {
            if (_timesToFail > 0)
            {
                _timesToFail--;

                Assert.Fail("welp!");
            }
        }

        [Test]
        [Retrying(Times = 2)]
        public void throws_exception_two_times_and_retries_two_times()
        {
            MaybeThrowException();
        }

        [Test]
        [Retrying(Times = 1)]
        public void throws_exception_two_times_and_retries_one_time()
        {
            MaybeThrowException();
        }

        private void MaybeThrowException()
        {
            if (_timesToFail > 0)
            {
                _timesToFail--;

                throw new Exception("oops!");
            }
        }
    }
}
