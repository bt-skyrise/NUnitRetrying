using NUnit.Framework;

namespace NUnitRetrying
{
    public class SampleTest
    {
        public static int TimesToFail = 2;

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
    }
}
