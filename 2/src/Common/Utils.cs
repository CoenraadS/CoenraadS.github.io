using System.Reflection;

namespace Common;

public static class Utils
{
    public static List<List<object?>> GetPossibleValuesForEachParameter(MethodInfo methodInfo)
    {
        List<List<object?>> values = new();

        foreach (var parameter in methodInfo.GetParameters())
        {
            var attribute = parameter.GetCustomAttribute<ValuesAttribute>();

            if (attribute == null || attribute.Values.Length == 0)
            {
                if (parameter.ParameterType.IsEnum)
                {
                    values.Add(Enum.GetValues(parameter.ParameterType).Cast<object?>().ToList());
                    continue;
                }

                if (parameter.ParameterType == typeof(bool))
                {
                    values.Add(new List<object?> { true, false });
                    continue;
                }

                if (attribute == null)
                {
                    throw new InvalidOperationException($"{parameter.Name} should have a [Values(...)] attribute set");
                }
                else
                {
                    throw new InvalidOperationException($"[Values] {parameter.ParameterType} {parameter.Name} is only valid for Enum or Boolean types. Consider using the attribute constructor [Values(...)].");
                }
            }

            values.Add(attribute.Values.ToList());
        }

        return values;
    }

    public static IEnumerable<object?[]> ZipLongestFillWithNull(List<List<object?>> values)
    {
        var longest = values.Max(e => e.Count);

        foreach (var list in values)
        {
            if (list.Count < longest)
            {
                var diff = longest - list.Count;
                list.AddRange(Enumerable.Repeat<object?>(null, diff));
            }
        }

        for (int i = 0; i < longest; i++)
        {
            yield return values.Select(e => e[i]).ToArray();
        }
    }

    public static IEnumerable<object?[]> CreateCombinations(List<List<object?>> values)
    {
        var indices = new int[values.Count];

        while (true)
        {
            // Create new arguments
            var arg = new object?[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                arg[i] = values[i][indices[i]];
            }

            yield return arg!;

            // Increment indices
            for (int i = indices.Length - 1; i >= 0; i--)
            {
                indices[i]++;
                if (indices[i] >= values[i].Count)
                {
                    indices[i] = 0;

                    if (i == 0)
                        yield break;
                }
                else
                    break;
            }
        }
    }
}
