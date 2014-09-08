using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Core;
using NUnit.Core.Extensibility;
using se.vlovgr.thesis.regression.core;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using TestMethod = se.vlovgr.thesis.regression.core.Models.Methods.TestMethod;

namespace se.vlovgr.thesis.project.core.test.Addin
{
    public sealed class RegressionDecorator : ITestDecorator
    {
        private readonly ISet<ITestMethod> _selectedTestCases = new HashSet<ITestMethod>();
        private readonly ISet<ITestMethod> _knownTestCases = new HashSet<ITestMethod>();

        public RegressionDecorator()
        {
            _selectedTestCases = Regression.Technique.GetSelectedTestCases();
            _knownTestCases = Regression.Technique.GetKnownTestCases();
        }

        public Test Decorate(Test test, MemberInfo m)
        {
            if (IsKnownTestCase(test, m) && !IsSelectedTestCase(test, m))
            {
                test.RunState = RunState.Skipped;
                test.IgnoreReason = "non-fault revealing test.";
            }

            return test;
        }

        private bool IsKnownTestCase(ITest test, MemberInfo m)
        {
            return IsTestCaseOf(_knownTestCases, test, m);
        }

        private bool IsSelectedTestCase(ITest test, MemberInfo m)
        {
            return IsTestCaseOf(_selectedTestCases, test, m);
        }

        private static bool IsTestCaseOf(IEnumerable<ITestMethod> testCases, ITest test, MemberInfo m)
        {
            return testCases.Contains(AsTestMethod(test, m));
        }

        private static ITestMethod AsTestMethod(ITest test, MemberInfo m)
        {
            return new TestMethod(m.Name, test.ClassName);
        }
    }
}