using System.Runtime.Serialization;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Models.Methods
{
    [DataContract]
    [KnownType(typeof(MethodInvocation))]
    public sealed class MethodInvocation : IMethodInvocation
    {
        [DataMember]
        public IMethod From { get; private set; }

        [DataMember]
        public IMethod Target { get; private set; }

        public MethodInvocation(IMethod from, IMethod target)
        {
            From = from;
            Target = target;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", From, Target);
        }
    }
}