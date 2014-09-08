namespace se.vlovgr.thesis.regression.core.Techniques.Interfaces
{
    public interface IPrioritizationTechnique<in TIn, out TOut>
    {
        TOut GetWeight(TIn test);
    }
}