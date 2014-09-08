using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using se.vlovgr.thesis.regression.core.Extensions;
using se.vlovgr.thesis.regression.core.Storage.Interfaces;

namespace se.vlovgr.thesis.regression.core.Storage
{
    public class VersionManager : IVersionManager
    {
        public string StoragePath { get; private set; }
        public IEnumerable<string> AssemblyPaths { get; private set; }

        public VersionManager(string storagePath, IEnumerable<string> assemblyPaths)
        {
            StoragePath = storagePath;
            AssemblyPaths = assemblyPaths;
        }

        public IEnumerable<Tuple<string, string>> GetPreviousAndCurrentVersions()
        {
            if (!IsPreviousVersionsAvailable())
                throw new InvalidOperationException("previous versions unavailable");

            var previousVersions = GetPreviousVersionPaths().ToList();
            return GetAssemblyFileNames().Select(fileName =>
                new Tuple<string, string>(
                    previousVersions.First(PathWithFileName(fileName)),
                    AssemblyPaths.First(PathWithFileName(fileName))));
        }

        public bool IsPreviousVersionsAvailable()
        {
            return GetNonExistingPreviousVersionPaths().None();
        }

        public void StoreCurrentVersions()
        {
            AssemblyPaths.ToList().ForEach(source =>
            {
                var destination = StoragePath + Path.GetFileName(source);
                File.Copy(source, destination, true);
            });
        }

        private IEnumerable<string> GetNonExistingPreviousVersionPaths()
        {
            return GetPreviousVersionPaths().Where(path => !File.Exists(path));
        }

        private IEnumerable<string> GetPreviousVersionPaths()
        {
            return GetAssemblyFileNames().Select(fileName => StoragePath + fileName);
        }

        private IEnumerable<string> GetAssemblyFileNames()
        {
            return AssemblyPaths.Select(Path.GetFileName);
        }

        private static Func<string, bool> PathWithFileName(string fileName)
        {
            return path => fileName.Equals(Path.GetFileName(path));
        }
    }
}