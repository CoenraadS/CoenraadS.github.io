using Common;
using System.Reflection;
using Xunit.Sdk;

namespace xUnitExample;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SequentialAttribute : DataAttribute
{
    public override IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        var values = Utils.GetPossibleValuesForEachParameter(methodInfo);
        return Utils.ZipLongestFillWithNull(values);
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