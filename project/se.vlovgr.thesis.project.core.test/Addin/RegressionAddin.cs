using NUnit.Core.Extensibility;

namespace se.vlovgr.thesis.project.core.test.Addin
{
    [NUnitAddin(Name = "Regression Technique", Type = ExtensionType.Core,
        Description = "Selective execution of regression test cases.")]
    public sealed class RegressionAddin : IAddin
    {
        public bool Install(IExtensionHost host)
        {
            var decorators = host.GetExtensionPoint("TestDecorators");
            if (decorators == null)
                return false;

            var listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            decorators.Install(new RegressionDecorator());
            listeners.Install(new RegressionListener());

            return true;
        }
    }
}