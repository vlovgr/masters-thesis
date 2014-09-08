using System.Linq;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Comparers;
using se.vlovgr.thesis.regression.core.Models.Changes;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;

namespace se.vlovgr.thesis.regression.core.Differencers
{
    public sealed class MethodDifferencer
    {
        public MethodDefinition PreviousMethod { get; private set; }
        public MethodDefinition CurrentMethod { get; private set; }

        public MethodDifferencer(MethodDefinition previousMethod, MethodDefinition currentMethod)
        {
            PreviousMethod = previousMethod;
            CurrentMethod = currentMethod;
        }

        public IMethodChange GetDifference()
        {
            return !Equals(PreviousMethod, CurrentMethod) ?
                new MethodChange(PreviousMethod, Change.Modified)
                : null;
        }

        private static bool Equals(MethodDefinition previousMethod, MethodDefinition currentMethod)
        {
            if (previousMethod.Body == null)
                return currentMethod.Body == null;

            if (currentMethod.Body == null)
                return false;

            return previousMethod.Body.Instructions.SequenceEqual(
                currentMethod.Body.Instructions, new InstructionEqualityComparer());
        }
    }
}