using System.Reflection;
using Xunit.Sdk;

namespace xUnitExample;

[AttributeUsage(AttributeTargets.Method)]
public class EnumDataAttribute<T> : DataAttribute where T : struct, Enum
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) => Enum.GetValues<T>().Select(e => new object[] { e });
}
