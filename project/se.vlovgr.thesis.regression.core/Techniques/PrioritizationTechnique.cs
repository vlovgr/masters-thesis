using System.Collections.Generic;
using System.Linq;
using se.vlovgr.thesis.regression.core.Diagrams.Interfaces;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using se.vlovgr.thesis.regression.core.Techniques.Interfaces;

namespace se.vlovgr.thesis.regression.core.Techniques
{
    public class PrioritizationTechnique : IPrioritizationTechnique<ITestMethod, double>
    {
        private readonly IAffectedClassDiagram _acd;

        public PrioritizationTechnique(IAffectedClassDiagram acd)
        {
            _acd = acd;
        }

        public double GetWeight(ITestMethod test)
        {
            var invocations = _acd.Coverage[test];

            double affectedMethods = new HashSet<IMethod>(invocations.Where(
                i => _acd.IsAffectedMethod(i.Target)).Select(i => i.Target)).Count();

            double affectedClasses = new HashSet<string>(invocations.Where(
                i => _acd.IsInAffectedClass(i.Target)).Select(i => i.Target.TypeName)).Count();

            double totalMethods = _acd.ClassDiagram.GetTotalMethodCount();
            return affectedMethods * (affectedClasses / totalMethods);
        }
    }
}