using System.Collections.Generic;

namespace se.vlovgr.thesis.regression.core.Techniques.Interfaces
{
    public interface ISelectionTechnique<T>
    {
        ISet<T> GetSelectedTestCases();
        ISet<T> GetKnownTestCases();
    }
}