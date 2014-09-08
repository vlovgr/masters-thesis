namespace se.vlovgr.thesis.regression.core.Models.Methods.Interfaces
{
    public interface ITestMethod : IMethod
    {
        double Weight { get; set; }
        bool WasSuccessful { get; set; }
    }
}