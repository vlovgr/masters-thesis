using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace se.vlovgr.thesis.regression.core.Attributes
{
    [MulticastAttributeUsage(MulticastTargets.InstanceConstructor | MulticastTargets.Method | MulticastTargets.StaticConstructor, TargetMemberAttributes = MulticastAttributes.AnyScope)]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Serializable]
    public class TraceTestInvocationsAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Regression.CoverageData.OnTestMethodEntered(args.Method);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Regression.CoverageData.OnTestMethodExited(args.Method);
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            return !method.DeclaringType.FullName.StartsWith("se.vlovgr.thesis.project.core.test.Addin.");
        }
    }
}