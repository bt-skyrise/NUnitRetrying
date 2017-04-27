using System.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

namespace NunitRetrying.Tests
{
    public class RunnableTest
    {
        private readonly ITest _test;
        private readonly object _fixture;

        public RunnableTest(ITest test, object fixture)
        {
            _test = test;
            _fixture = fixture;
        }

        public ITestResult Run()
        {
            var workItem = WorkItem.CreateWorkItem(_test, TestFilter.Empty);

            workItem.InitializeContext(new TestExecutionContext
            {
                TestObject = _fixture,
                Dispatcher = new SimpleWorkItemDispatcher()
            });

            WaitUntilCompleted(workItem);

            return workItem.Result;
        }

        private static void WaitUntilCompleted(WorkItem workItem)
        {
            var autoResetEvent = new AutoResetEvent(false);

            workItem.Completed += (sender, args) => autoResetEvent.Set();

            workItem.Execute();

            autoResetEvent.WaitOne();
        }
    }
}