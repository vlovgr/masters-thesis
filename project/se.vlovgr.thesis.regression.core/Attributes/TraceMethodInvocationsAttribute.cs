using System;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace se.vlovgr.thesis.regression.core.Attributes
{
    [MulticastAttributeUsage(MulticastTargets.InstanceConstructor | MulticastTargets.Method | MulticastTargets.StaticConstructor, TargetMemberAttributes = MulticastAttributes.AnyScope)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Serializable]
    public class TraceMethodInvocationsAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Regression.CoverageData.OnMethodEntered(args.Method);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Regression.CoverageData.OnMethodExited(args.Method);
        }
    }
}