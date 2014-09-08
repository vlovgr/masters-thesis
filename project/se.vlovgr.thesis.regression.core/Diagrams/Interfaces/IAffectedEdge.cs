using Mono.Cecil;

namespace se.vlovgr.thesis.regression.core.Diagrams.Interfaces
{
    public interface IAffectedEdge
    {
        TypeDefinition From { get; }
        TypeDefinition Target { get; }
        Edge Type { get; }
    }
}