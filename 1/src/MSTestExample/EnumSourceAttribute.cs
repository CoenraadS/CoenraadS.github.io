using System.Reflection;

namespace MSTestExample;

[AttributeUsage(AttributeTargets.Method)]
public class EnumDataAttribute<T> : Attribute, ITestDataSource where T : struct, Enum
{
    public IEnumerable<object?[]> GetData(MethodInfo testMethod) => Enum.GetValues<T>().Select(e => new object[] { e });

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        return $"{methodInfo.Name}({data![0]})";
    }
}