using System;
using NUnit.Framework;
using NUnitRetrying;

namespace NunitRetrying.Tests
{
    [Explicit("These tests should only run programmatically by other tests or for debugging purposes.")]
    public class RetryingTestMethods
    {
        public int TimesToFail = 2;

        [Test]
        [Retrying(Times = 2)]
        public void fails_assertion_two_times_and_retries_two_times()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;

                Assert.Fail("welp!");
            }
        }

        [Test]
        [Retrying(Times = 2)]
        public void throws_exception_two_times_and_retries_two_times()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;

                throw new Exception("oops!");
            }
        }

        [Test]
        [Retrying(Times = 1)]
        public void fails_assertion_two_times_and_retries_one_time()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;

                Assert.Fail("welp!");
            }
        }

        [Test]
        [Retrying(Times = 1)]
        public void throws_exception_two_times_and_retries_one_time()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;

                throw new Exception("oops!");
            }
        }
    }
}
