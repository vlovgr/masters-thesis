using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Comparers;
using se.vlovgr.thesis.regression.core.Diagrams.Interfaces;
using se.vlovgr.thesis.regression.core.Models.Changes;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;
using se.vlovgr.thesis.regression.core.Models.Methods;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Diagrams
{
    public sealed class AffectedClassDiagram : IAffectedClassDiagram
    {
        public IDictionary<ITestMethod, IList<IMethodInvocation>> Coverage { get; private set; }
        public ISet<IMethodChange> MethodChanges { get; private set; }
        public IClassDiagram ClassDiagram { get; private set; }

        public ISet<TypeDefinition> AffectedTypes { get; private set; }
        public IList<AffectedEdge> Edges { get; private set; }

        public AffectedClassDiagram(IDictionary<ITestMethod, IList<IMethodInvocation>> coverage,
            ISet<IMethodChange> methodChanges, IClassDiagram classDiagram)
        {
            Coverage = coverage;
            MethodChanges = methodChanges;
            ClassDiagram = classDiagram;

            GenerateDiagram();
        }

        public bool IsInAffectedClass(IMethod method)
        {
            return AffectedTypes.Any(c => c != null && c.FullName.Equals(method.TypeName));
        }

        public bool IsAffectedMethod(IMethod method)
        {
            return MethodChanges.Any(m =>
            {
                string methodName;
                if (m.Method.IsConstructor)
                {
                    var mFullName = m.Method.FullName;
                    methodName = mFullName.Substring(mFullName.LastIndexOf(':') + 1);
                    if (methodName.EndsWith("()"))
                        methodName = methodName.Substring(0, methodName.Length - 2);
                }
                else methodName = m.Method.Name;
                
                return m.Type == Change.Modified
                  && methodName.Equals(method.Name)
                  && m.Method.DeclaringType.FullName.Equals(method.TypeName);
            });
        }

        public bool IsAffectedInvocation(IMethodInvocation m)
        {
            return (IsInAffectedClass(m.From) && IsAffectedMethod(m.From))
                || (IsInAffectedClass(m.Target) && IsAffectedMethod(m.Target));
        }

        private void GenerateDiagram()
        {
            AffectedTypes = GetAffectedTypes(MethodChanges);
            Edges = new List<AffectedEdge>();

            AddBaseTypesForAffectedTypes();
            AddSubTypesOverridingMethodsInAffectedTypes();
            AddTypesUsingAnyAffectedType();
            AddAffectedMethodsForChangedStaticConstructors();
        }

        private void AddBaseTypesForAffectedTypes()
        {
            foreach (var affectedClass in AffectedTypes.ToList())
            {
                ClassDiagram.GetBaseTypes(affectedClass).ToList().ForEach(baseType =>
                {
                    AffectedTypes.Add(baseType);
                    Edges.Add(new AffectedEdge(affectedClass, baseType, Edge.Inheritance));
                });
            }
        }

        private void AddSubTypesOverridingMethodsInAffectedTypes()
        {
            foreach (var affectedClass in AffectedTypes.ToList())
            {
                ClassDiagram.GetSubTypesOverridingAnyMethodsIn(affectedClass).ToList().ForEach(type =>
                {
                    AffectedTypes.Add(type);
                    Edges.Add(new AffectedEdge(type, affectedClass, Edge.IndirectSubtype));
                });
            }
        }

        private void AddTypesUsingAnyAffectedType()
        {
            foreach (var invoke in Coverage.SelectMany(e => e.Value).Where(invoke => !(invoke.From is TestMethod)))
            {
                if (IsInAffectedClass(invoke.Target) && IsAffectedMethod(invoke.Target) && !IsInAffectedClass(invoke.From))
                {
                    var from = ClassDiagram.ResolveType(invoke.From.TypeName);
                    if (from != null)
                    {
                        var fromMethod = from.Methods.First(m => m.Name.Equals(invoke.From.Name));
                        var target = ClassDiagram.ResolveType(invoke.Target.TypeName);

                        AffectedTypes.Add(from);
                        Edges.Add(new AffectedEdge(from, target, Edge.Use));
                        MethodChanges.Add(new MethodChange(fromMethod, Change.Using));
                    }
                }
            }
        }

        private void AddAffectedMethodsForChangedStaticConstructors()
        {
            var affectedTypes = MethodChanges
                .Where(c => c.Method.IsStatic && c.Method.IsConstructor)
                .Select(change => change.Method.DeclaringType.FullName).ToList();

            foreach (var type in affectedTypes)
            {
                var resolvedType = ClassDiagram.ResolveType(type);
                foreach (var method in resolvedType.Methods)
                {
                    MethodChanges.Add(new MethodChange(method, Change.Modified));
                }
            }
        }

        private static ISet<TypeDefinition> GetAffectedTypes(IEnumerable<IMethodChange> methodChanges)
        {
            var affectedTypes = new HashSet<TypeDefinition>(new TypeEqualityComparer());
            methodChanges.ToList().ForEach(change =>
            {
                if (change.Type == Change.Modified || change.Type == Change.Deleted)
                    affectedTypes.Add(change.Method.DeclaringType);
            });

            return affectedTypes;
        }
    }
}