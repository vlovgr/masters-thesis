using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Diagrams;
using se.vlovgr.thesis.regression.core.Diagrams.Interfaces;
using se.vlovgr.thesis.regression.core.Differencers;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using se.vlovgr.thesis.regression.core.Storage.Interfaces;
using se.vlovgr.thesis.regression.core.Techniques.Interfaces;

namespace se.vlovgr.thesis.regression.core.Techniques
{
    public class SelectionTechnique : ISelectionTechnique<ITestMethod>
    {
        private readonly ICoverageData _coverageData;
        private readonly IVersionManager _versionManager;

        private readonly ISet<ITestMethod> _selectedTestCases;
        private readonly ISet<ITestMethod> _knownTestCases;

        public SelectionTechnique(ICoverageData coverageData, IVersionManager versionManager)
        {
            _coverageData = coverageData;
            _versionManager = versionManager;

            var selectedAndKnownTestCases = GetSelectedAndKnownTestCases();
            _selectedTestCases = selectedAndKnownTestCases.Item1;
            _knownTestCases = selectedAndKnownTestCases.Item2;
        }

        private Tuple<ISet<ITestMethod>, ISet<ITestMethod>> GetSelectedAndKnownTestCases()
        {
            if (!_versionManager.IsPreviousVersionsAvailable())
                return new Tuple<ISet<ITestMethod>, ISet<ITestMethod>>(new HashSet<ITestMethod>(), new HashSet<ITestMethod>());

            var storedCoverage = _coverageData.GetStored();
            var differences = new HashSet<IMethodChange>();
            var types = new List<TypeDefinition>();

            _versionManager.GetPreviousAndCurrentVersions().ToList().ForEach(tuple =>
            {
                string previous = tuple.Item1, current = tuple.Item2;
                differences.UnionWith(new ModuleDifferencer(previous, current).GetDifferences());
                types.AddRange(ModuleDefinition.ReadModule(previous).Types);
            });

            var classDiagram = new ClassDiagram(types);
            var acd = new AffectedClassDiagram(storedCoverage, differences, classDiagram);

            return new Tuple<ISet<ITestMethod>, ISet<ITestMethod>>(GetSelectedTestCases(acd), GetKnownTestCases(storedCoverage));
        }

        public ISet<ITestMethod> GetSelectedTestCases()
        {
            return _selectedTestCases;
        }

        public ISet<ITestMethod> GetKnownTestCases()
        {
            return _knownTestCases;
        }

        private static ISet<ITestMethod> GetSelectedTestCases(IAffectedClassDiagram acd)
        {
            var selected = new HashSet<ITestMethod>(acd.Coverage.Where(entry =>
                entry.Value.Any(acd.IsAffectedInvocation)).Select(e => e.Key));

            selected.UnionWith(acd.Coverage.Where(entry => !entry.Key.WasSuccessful).Select(entry => entry.Key));

            var prioritization = new PrioritizationTechnique(acd);
            selected.ToList().ForEach(test => test.Weight = prioritization.GetWeight(test));
            
            return selected;
        }

        private static ISet<ITestMethod> GetKnownTestCases(IEnumerable<KeyValuePair<ITestMethod, IList<IMethodInvocation>>> coverageData)
        {
            return new HashSet<ITestMethod>(coverageData.Select(e => e.Key));
        } 
    }
}