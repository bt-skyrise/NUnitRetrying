using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace NUnitRetrying
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RetryingAttribute : NUnitAttribute, IWrapSetUpTearDown
    {
        public int Times { get; set; }

        public RetryingAttribute(int times = 1)
        {
            Times = times;
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new RetryingCommand(command, Times);
        }

        public class RetryingCommand : DelegatingTestCommand
        {
            private readonly int _times;

            public RetryingCommand(TestCommand innerCommand, int times)
                : base(innerCommand)
            {
                _times = times;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                var retriesLeft = _times;

                context.CurrentResult = innerCommand.Execute(context);

                while (TestFailed(context.CurrentResult) && retriesLeft > 0)
                {
                    // clear result for retry
                    context.CurrentResult = context.CurrentTest.MakeTestResult();
                    context.CurrentResult = innerCommand.Execute(context);

                    retriesLeft--;
                }

                var performedRetries = _times - retriesLeft;
                
                if (performedRetries > 0)
                {
                    context.OutWriter.WriteLine();
                    context.OutWriter.WriteLine($"Test retried {performedRetries} time/s.");
                }

                return context.CurrentResult;
            }

            private static bool TestFailed(TestResult testResult)
            {
                return testResult.ResultState.Equals(ResultState.Failure); // todo: shouldn't we also check error?
            }
        }
    }
}
