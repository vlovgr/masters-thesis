using System.Collections.Generic;
using Mono.Cecil;

namespace se.vlovgr.thesis.regression.core.Comparers
{
    public sealed class MethodEqualityComparer : IEqualityComparer<MethodDefinition>
    {
        public bool Equals(MethodDefinition x, MethodDefinition y)
        {
            return x.FullName.Equals(y.FullName);
        }

        public int GetHashCode(MethodDefinition m)
        {
            return m.FullName.GetHashCode();
        }
    }
}