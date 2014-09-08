using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using se.vlovgr.thesis.regression.core.Models.Changes;
using se.vlovgr.thesis.regression.core.Models.Changes.Interfaces;

namespace se.vlovgr.thesis.regression.core.Extensions
{
    public static class SetExtension
    {
        public static void AddType(this ISet<IMethodChange> set, TypeDefinition type, Change change)
        {
            type.Methods.ToList().ForEach(method => set.AddMethod(method, change));
        }

        public static void AddMethod(this ISet<IMethodChange> set, MethodDefinition method, Change change)
        {
            set.Add(new MethodChange(method, change));
        }
    }
}