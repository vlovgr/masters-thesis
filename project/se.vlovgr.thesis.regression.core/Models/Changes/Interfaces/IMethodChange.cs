using Mono.Cecil;

namespace se.vlovgr.thesis.regression.core.Models.Changes.Interfaces
{
    public interface IMethodChange
    {
        MethodDefinition Method { get; }
        Change Type { get; }
    }
}