using Common;
using System.Reflection;

namespace MSTestExample;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CombinatorialAttribute : Attribute, ITestDataSource
{
    public IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        var values = Utils.GetPossibleValuesForEachParameter(methodInfo);
        return Utils.CreateCombinations(values);
    }

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        if (data != null)
        {
            return $"{methodInfo.Name} ({string.Join(", ", data.Select(e => e ?? "null"))})";
        }

        return null;
    }
}