using NUnit.Core;
using se.vlovgr.thesis.regression.core;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using TestMethod = se.vlovgr.thesis.regression.core.Models.Methods.TestMethod;

namespace se.vlovgr.thesis.project.core.test.Addin
{
    public sealed class RegressionListener : EventAdapter
    {
        public override void TestStarted(TestName testName)
        {
            Regression.CoverageData.OnTestStarted(AsTestMethod(testName));
        }

        public override void TestFinished(TestResult result)
        {
            var successful = !(result.IsError || result.IsFailure);
            Regression.CoverageData.OnTestFinished(successful);
        }

        public override void RunFinished(TestResult result)
        {
            Regression.CoverageData.Store();
            Regression.VersionManager.StoreCurrentVersions();
        }

        private static ITestMethod AsTestMethod(TestName test)
        {
            var typeName = test.FullName.Substring(0, test.FullName.Length - test.Name.Length - 1);
            return new TestMethod(test.Name, typeName);
        }
    }
}