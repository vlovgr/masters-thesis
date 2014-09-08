using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;

namespace se.vlovgr.thesis.regression.core.Models.Changes
{
    public sealed class MethodChange : IMethodChange
    {
        public MethodDefinition Method { get; private set; }
        public Change Type { get; private set; }

        public MethodChange(MethodDefinition method, Change type)
        {
            Method = method;
            Type = type;
        }

        private bool Equals(MethodChange other)
        {
            return Equals(Method.FullName, other.Method.FullName);
        }

        public override bool Equals(object m)
        {
            if (ReferenceEquals(null, m)) return false;
            if (ReferenceEquals(this, m)) return true;
            return m.GetType() == GetType() && Equals((MethodChange)m);
        }

        public override int GetHashCode()
        {
            return Method.FullName.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Type, Method.FullName);
        }
    }
}