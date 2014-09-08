using System;
using System.Collections.Generic;

namespace se.vlovgr.thesis.regression.core.Storage.Interfaces
{
    public interface IVersionManager
    {
        string StoragePath { get; }
        IEnumerable<string> AssemblyPaths { get; }
        IEnumerable<Tuple<string, string>> GetPreviousAndCurrentVersions();
        bool IsPreviousVersionsAvailable();
        void StoreCurrentVersions();
    }
}