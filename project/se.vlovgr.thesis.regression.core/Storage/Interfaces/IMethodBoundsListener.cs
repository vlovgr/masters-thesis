using System.Reflection;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Storage.Interfaces
{
    public interface IMethodBoundsListener
    {
        void OnMethodEntered(MethodBase m);
        void OnMethodExited(MethodBase m);

        void OnTestMethodEntered(MethodBase m);
        void OnTestMethodExited(MethodBase m);

        void OnTestStarted(ITestMethod m);
        void OnTestFinished(bool successful);
    }
}