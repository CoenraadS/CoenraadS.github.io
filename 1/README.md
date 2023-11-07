# .NET Unit Test Framework Comparison - Enum Iteration
*2023/11/05 - Coenraad Stijne*  

*Note: A follow up post describes how to reimplement `NUnit's` `[ValuesAttribute]` in `MSTest` and `xUnit`.*  
*See [.NET Unit Test Framework Comparison - Combinatorial Parameters](../2/README.md).*

---

#### Summary:  
- `NUnit` is the only library that support `Enum` iteration natively.  
- For `MSTest` and `xUnit`, we can create our own custom attributes.

#### [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/1/src)

---

### Framework Overview

| Framework | Version | 
| --------- | ------- | 
| MSTest    | 3.1.1   | 
| NUnit     | 4.5.0   | 
| xUnit     | 2.6.1   | 

## MSTest

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class EnumDataAttribute<T> : Attribute, ITestDataSource where T : struct, Enum
{
    public IEnumerable<object?[]> GetData(MethodInfo testMethod) => Enum.GetValues<T>().Select(e => new object[] { e });

    public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
    {
        return $"{methodInfo.Name}({data![0]})";
    }
}

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [EnumData<Season>]
    public void EnumIterationTest(Season season) => Console.WriteLine(season);
}
```

## NUnit

```csharp
public class Tests
{
    [Test]
    public void EnumIterationTest([Values] Season season) => Console.WriteLine(season);
}
```

## xUnit

```csharp
[AttributeUsage(AttributeTargets.Method)]
public class EnumDataAttribute<T> : DataAttribute where T : struct, Enum
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) => Enum.GetValues<T>().Select(e => new object[] { e });
}

public class UnitTest1
{
    [Theory]
    [EnumData<Season>]
    public void EnumIterationTest(Season season) => Console.WriteLine(season);
}
```