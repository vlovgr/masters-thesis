using System.Collections.Generic;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Storage.Interfaces
{
    public interface ICoverageData : IMethodBoundsListener
    {
        string BackingFileName { get; }
        IDictionary<ITestMethod, IList<IMethodInvocation>> GetStored();
        IDictionary<ITestMethod, IList<IMethodInvocation>> Get();
        void Store();
    }
}