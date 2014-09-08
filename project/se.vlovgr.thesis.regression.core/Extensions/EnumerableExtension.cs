using System.Collections.Generic;
using System.Linq;

namespace se.vlovgr.thesis.regression.core.Extensions
{
    public static class EnumerableExtension
    {
        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }
    }
}