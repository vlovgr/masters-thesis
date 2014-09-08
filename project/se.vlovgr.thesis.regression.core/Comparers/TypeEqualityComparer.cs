using System.Collections.Generic;
using Mono.Cecil;

namespace se.vlovgr.thesis.regression.core.Comparers
{
    public sealed class TypeEqualityComparer : IEqualityComparer<TypeDefinition>
    {
        public bool Equals(TypeDefinition x, TypeDefinition y)
        {
            if (x == null)
                return y == null;

            return x.FullName.Equals(y.FullName);
        }

        public int GetHashCode(TypeDefinition t)
        {
            return t.FullName.GetHashCode();
        }
    }
}