namespace EMC.Centera.SDK
{
    public interface IFPAttribute
    {
        string Name { get; }
        string Value { get; }
        string ToString();
    }
}