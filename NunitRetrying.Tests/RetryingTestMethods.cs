using NUnit.Framework;
using NUnitRetrying;

namespace NunitRetrying.Tests
{
    [Explicit("These tests should only run programmatically or for debugging purposes.")]
    public class RetryingTestMethods
    {
        public int TimesToFail = 2;

        [Test]
        [Retrying(Times = 2)]
        public void will_succeed_on_a_third_run()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;
                Assert.Fail("welp!");
            }
        }

        [Test]
        [Retrying(Times = 1)]
        public void will_fail_on_a_second_run()
        {
            if (TimesToFail > 0)
            {
                TimesToFail--;
                Assert.Fail("welp!");
            }
        }
    }
}
