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
    public sealed class ModuleDifferencer
    {
        public string PreviousModulePath { get; private set; }
        public string CurrentModulePath { get; private set; }

        public ModuleDifferencer(string previousModulePath, string currentModulePath)
        {
            PreviousModulePath = previousModulePath;
            CurrentModulePath = currentModulePath;
        }

        public ISet<IMethodChange> GetDifferences()
        {
            return GetDifferences(
                ModuleDefinition.ReadModule(PreviousModulePath),
                ModuleDefinition.ReadModule(CurrentModulePath)
            );
        }

        private static ISet<IMethodChange> GetDifferences(ModuleDefinition previousModule, ModuleDefinition currentModule)
        {
            var differences = new HashSet<IMethodChange>();
            foreach (var previousType in previousModule.Types)
            {
                var currentType = currentModule.Types.FirstOrDefault(Predicate.For(previousType));
                if (currentType != null)
                {
                    var typeDifferencer = new TypeDifferencer(previousType, currentType);
                    differences.UnionWith(typeDifferencer.GetDifferences());
                }
                else differences.AddType(previousType, Change.Deleted);
            }

            var addedTypes = GetAddedTypes(previousModule, currentModule);
            addedTypes.ToList().ForEach(t => differences.AddType(t, Change.Added));

            return differences;
        }

        private static IEnumerable<TypeDefinition> GetAddedTypes(ModuleDefinition previousModule, ModuleDefinition currentModule)
        {
            return currentModule.Types.Where(t => !previousModule.Types.Contains(t, new TypeEqualityComparer()));
        }
    }
}