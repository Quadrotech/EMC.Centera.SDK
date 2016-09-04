using System;

namespace EMC.Centera.SDK
{
    public interface IFPAttribute
    {
        String Name { get; }
        String Value { get; }
        string ToString();
    }
}