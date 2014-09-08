using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Comparers;
using se.vlovgr.thesis.regression.core.Extensions;
using se.vlovgr.thesis.regression.core.Models.Changes;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;
using se.vlovgr.thesis.regression.core.Predicates;

namespace se.vlovgr.thesis.regression.core.Differencers
{
    public sealed class TypeDifferencer
    {
        public TypeDefinition PreviousType { get; private set; }
        public TypeDefinition CurrentType { get; private set; }

        public TypeDifferencer(TypeDefinition previousType, TypeDefinition currentType)
        {
            PreviousType = previousType;
            CurrentType = currentType;
        }

        public ISet<IMethodChange> GetDifferences()
        {
            var differences = new HashSet<IMethodChange>();
            foreach (var previousMethod in PreviousType.Methods)
            {
                var currentMethod = CurrentType.Methods.FirstOrDefault(Predicate.For(previousMethod));
                if (currentMethod != null)
                {
                    var methodDifference = GetDifference(previousMethod, currentMethod);
                    if (methodDifference != null)
                        differences.Add(methodDifference);
                }
                else differences.AddMethod(previousMethod, Change.Deleted);
            }

            GetAddedMethods().ToList().ForEach(m => differences.AddMethod(m, Change.Added));

            return differences;
        }

        private static IMethodChange GetDifference(MethodDefinition previousMethod, MethodDefinition currentMethod)
        {
            return new MethodDifferencer(previousMethod, currentMethod).GetDifference();
        }

        private IEnumerable<MethodDefinition> GetAddedMethods()
        {
            return CurrentType.Methods.Where(m => !PreviousType.Methods.Contains(m, new MethodEqualityComparer()));
        }
    }
}