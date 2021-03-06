﻿using System.Runtime.Serialization;
using se.vlovgr.thesis.regression.core.Models.Methods.Interfaces;

namespace se.vlovgr.thesis.regression.core.Models.Methods
{
    [DataContract]
    [KnownType(typeof(Method))]
    public sealed class Method : IMethod
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string TypeName { get; private set; }

        public Method(string name, string typeName)
        {
            Name = name;
            TypeName = typeName;
        }

        private bool Equals(Method m)
        {
            return string.Equals(Name, m.Name)
                && string.Equals(TypeName, m.TypeName);
        }

        public override bool Equals(object m)
        {
            if (ReferenceEquals(null, m)) return false;
            if (ReferenceEquals(this, m)) return true;
            return m.GetType() == GetType() && Equals((Method)m);
        }

        public override int GetHashCode()
        {
            return (Name.GetHashCode() * 397) ^ TypeName.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}#{1}", TypeName, Name);
        }
    }
}