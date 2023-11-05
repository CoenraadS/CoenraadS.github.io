namespace Common;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValuesAttribute : Attribute
{
    public object?[] Values { get; }

    public ValuesAttribute(params object?[] values)
    {
        Values = values;
    }
}
