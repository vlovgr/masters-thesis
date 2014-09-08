namespace se.vlovgr.thesis.regression.core.Models.Methods.Interfaces
{
    public interface IMethodInvocation
    {
        IMethod From { get; }
        IMethod Target { get; }
    }
}