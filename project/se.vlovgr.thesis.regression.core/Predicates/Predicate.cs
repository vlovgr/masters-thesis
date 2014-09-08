using System;
using System.Collections.Generic;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Comparers;

namespace se.vlovgr.thesis.regression.core.Predicates
{
    public static class Predicate
    {
        public static Func<TypeDefinition, bool> For(TypeDefinition y, IEqualityComparer<TypeDefinition> comparer)
        {
            return x => comparer.Equals(x, y);
        }

        public static Func<TypeDefinition, bool> For(TypeDefinition y)
        {
            return For(y, new TypeEqualityComparer());
        }

        public static Func<MethodDefinition, bool> For(MethodDefinition y, IEqualityComparer<MethodDefinition> comparer)
        {
            return x => comparer.Equals(x, y);
        }

        public static Func<MethodDefinition, bool> For(MethodDefinition y)
        {
            return For(y, new MethodEqualityComparer());
        }
    }
}