using Common;
using System.Reflection;
using Xunit.Sdk;

namespace xUnitExample;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CombinatorialAttribute : DataAttribute
{
    public override IEnumerable<object?[]> GetData(MethodInfo methodInfo)
    {
        var values = Utils.GetPossibleValuesForEachParameter(methodInfo);
        return Utils.CreateCombinations(values);
    }
}