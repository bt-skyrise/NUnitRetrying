using System;
using System.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

namespace NunitRetrying.Tests
{
    public class RunnableTest
    {
        private readonly ITest _test;
        private readonly object _testObject;

        public RunnableTest(ITest test, object testObject)
        {
            _test = test;
            _testObject = testObject;
        }

        public ITestResult Run()
        {
            var workItem = WorkItem.CreateWorkItem(_test, TestFilter.Empty);

            workItem.InitializeContext(new TestExecutionContext
            {
                TestObject = _testObject,
                Dispatcher = new SuperSimpleDispatcher()
            });

            workItem.Execute();

            while (workItem.State != WorkItemState.Complete)
            {
                Thread.Sleep(1);
            }

            return workItem.Result;
        }
    }

    /// <summary>
    /// SuperSimpleDispatcher merely executes the work item.
    /// It is needed particularly when running suites, since
    /// the child items require a dispatcher in the context.
    /// </summary>
    class SuperSimpleDispatcher : IWorkItemDispatcher
    {
        public void Dispatch(WorkItem work)
        {
            work.Execute();
        }

        public void CancelRun(bool force)
        {
            throw new NotImplementedException();
        }
    }
}