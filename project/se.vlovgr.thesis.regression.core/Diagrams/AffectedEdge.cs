using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Diagrams.Interfaces;

namespace se.vlovgr.thesis.regression.core.Diagrams
{
    public sealed class AffectedEdge : IAffectedEdge
    {
        public TypeDefinition From { get; private set; }
        public TypeDefinition Target { get; private set; }
        public Edge Type { get; private set; }

        public AffectedEdge(TypeDefinition from, TypeDefinition target, Edge type)
        {
            From = from;
            Target = target;
            Type = type;
        }

        private bool Equals(AffectedEdge other)
        {
            return Equals(From, other.From) && Equals(Target, other.Target) && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((AffectedEdge)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = (From != null ? From.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Target != null ? Target.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (int)Type;
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1} ({2})", From, Target, Type);
        }
    }
}