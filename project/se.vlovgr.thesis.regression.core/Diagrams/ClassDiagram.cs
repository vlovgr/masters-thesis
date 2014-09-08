using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using se.vlovgr.thesis.regression.core.Comparers;
using se.vlovgr.thesis.regression.core.Diagrams.Interfaces;

namespace se.vlovgr.thesis.regression.core.Diagrams
{
    public sealed class ClassDiagram : IClassDiagram
    {
        public IEnumerable<TypeDefinition> Types { get; private set; }

        public ClassDiagram(IEnumerable<TypeDefinition> types)
        {
            Types = types;
        }

        public int GetTotalMethodCount()
        {
            return Types.Select(type => type.Methods.Count).Sum();
        }

        public IEnumerable<TypeDefinition> GetBaseTypes(TypeDefinition type)
        {
            var baseTypes = new List<TypeDefinition>();
            if (type.BaseType == null)
                return baseTypes;

            var currentType = type.BaseType.Resolve();
            if (!IsInDiagram(currentType))
                return baseTypes;

            do
            {
                baseTypes.Add(currentType);
                if (currentType.BaseType == null)
                    break;

                currentType = currentType.BaseType.Resolve();
            } while (IsInDiagram(currentType));

            return baseTypes;
        }

        public IEnumerable<TypeDefinition> GetSubTypes(TypeDefinition type)
        {
            var comparer = new TypeEqualityComparer();
            return Types.Where(t => !comparer.Equals(t, type)
                && GetBaseTypes(t).Contains(type, comparer));
        }

        public IEnumerable<TypeDefinition> GetSubTypesOverridingAnyMethodsIn(TypeDefinition type)
        {
            return GetSubTypes(type).Where(ClassOverridesAnyMethod());
        }

        public TypeDefinition ResolveType(string typeFullName)
        {
            return Types.FirstOrDefault(type => type.FullName.Equals(typeFullName));
        }

        private bool IsInDiagram(TypeDefinition type)
        {
            return Types.Contains(type, new TypeEqualityComparer());
        }

        private static Func<TypeDefinition, bool> ClassOverridesAnyMethod()
        {
            return subtype => subtype.Methods.Any(method => !method.DeclaringType.Equals(method.GetBaseMethod().DeclaringType));
        }
    }
}