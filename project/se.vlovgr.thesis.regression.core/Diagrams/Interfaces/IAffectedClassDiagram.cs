using System.Collections.Generic;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Diagrams.Interfaces
{
    public interface IAffectedClassDiagram
    {
        IDictionary<ITestMethod, IList<IMethodInvocation>> Coverage { get; }
        ISet<IMethodChange> MethodChanges { get; }
        IClassDiagram ClassDiagram { get; }

        ISet<TypeDefinition> AffectedTypes { get; }
        IList<AffectedEdge> Edges { get; }

        bool IsInAffectedClass(IMethod method);
        bool IsAffectedMethod(IMethod method);
        bool IsAffectedInvocation(IMethodInvocation m);
    }
}