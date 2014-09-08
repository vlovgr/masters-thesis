using System.Collections.Generic;
using Mono.Cecil;

namespace se.vlovgr.thesis.regression.core.Diagrams.Interfaces
{
    public interface IClassDiagram
    {
        IEnumerable<TypeDefinition> Types { get; }
        int GetTotalMethodCount();
        IEnumerable<TypeDefinition> GetBaseTypes(TypeDefinition type);
        IEnumerable<TypeDefinition> GetSubTypes(TypeDefinition type);
        IEnumerable<TypeDefinition> GetSubTypesOverridingAnyMethodsIn(TypeDefinition type);
        TypeDefinition ResolveType(string typeFullName);
    }
}