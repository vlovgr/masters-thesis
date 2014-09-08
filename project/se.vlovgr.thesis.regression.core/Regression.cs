using System.Collections.Generic;
using System.Configuration;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;
using se.vlovgr.thesis.regression.core.Storage;
using se.vlovgr.thesis.regression.core.Storage.Interfaces;
using se.vlovgr.thesis.regression.core.Techniques;
using se.vlovgr.thesis.regression.core.Techniques.Interfaces;

namespace se.vlovgr.thesis.regression.core
{
    public static class Regression
    {
        private static class Settings
        {
            public static string GetStoragePath()
            {
                var storagePath = ConfigurationManager.AppSettings["RegressionStoragePath"];
                if (!storagePath.EndsWith(@"\"))
                    storagePath = storagePath + @"\";

                return storagePath.ToLower();
            }

            public static string GetCoverageDataFileName()
            {
                return GetStoragePath() + "coverage.json";
            }

            public static IEnumerable<string> GetAssemblyPaths()
            {
                return ConfigurationManager.AppSettings["RegressionAssemblyPaths"].ToLower().Split(';');
            }
        }

        public static readonly ICoverageData CoverageData = new CoverageData(Settings.GetCoverageDataFileName());

        public static readonly IVersionManager VersionManager = new VersionManager(Settings.GetStoragePath(), Settings.GetAssemblyPaths());

        public static readonly ISelectionTechnique<ITestMethod> Technique = new SelectionTechnique(CoverageData, VersionManager);
    }
}