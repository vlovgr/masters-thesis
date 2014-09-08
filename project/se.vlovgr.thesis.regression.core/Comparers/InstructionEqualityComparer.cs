using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Cil;

namespace se.vlovgr.thesis.regression.core.Comparers
{
    public sealed class InstructionEqualityComparer : IEqualityComparer<Instruction>
    {
        private static readonly List<string> Ignored = new List<string> { "PostSharp.ImplementationDetails" };

        public bool Equals(Instruction x, Instruction y)
        {
            return IsIgnored(x) || AreEqual(x, y);
        }

        public int GetHashCode(Instruction i)
        {
            return i.ToString().GetHashCode();
        }

        private static bool IsIgnored(Instruction x)
        {
            var currentNextPrevious = new List<Instruction> { x, x.Next, x.Previous }.Where(i => i != null && i.Operand != null);
            return currentNextPrevious.Any(instr => Ignored.Any(ignored => instr.Operand.ToString().Contains(ignored)));
        }

        private static bool AreEqual(Instruction x, Instruction y)
        {
            return x.ToString().Equals(y.ToString());
        }
    }
}