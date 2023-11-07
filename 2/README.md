# .NET Unit Test Framework Comparison - Combinatorial Parameters
*2023/11/05 - Coenraad Stijne*  

#### Summary:  
- `NUnit` supports *Combinatorial Parameters* natively, via the [ValuesAttribute](https://docs.nunit.org/articles/nunit/writing-tests/attributes/values.html).  
- `xUnit` can support *Combinatorial Parameters* via a third party package: [Xunit.Combinatorial](https://github.com/AArnott/Xunit.Combinatorial).  
- `MSTest` does not have native or third party support for *Combinatorial Parameters*.  
    
This post will reimplement `NUnit's`  attributes `[Values]`, `[Combinatorial]` and `[Sequential]` for `MSTest` and `xUnit`.  

It is reusing a lot of code from [Xriuk's answer on StackOverflow](https://stackoverflow.com/a/75531690/4503491).

This post only shows the end result, for implementation see the [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/2/src)

---

### Framework Overview

| Framework | Version | 
| --------- | ------- | 
| MSTest    | 3.1.1   | 
| NUnit     | 4.5.0   | 
| xUnit     | 2.6.1   | 

## MSTest

This shows the end result, view the [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/2/src) for implementation.

```csharp
[TestClass]
public class TestMethods
{
    [TestMethod, Combinatorial]
    public void EnumIterationTestMethod(Season season) => Console.WriteLine(season);

    [TestMethod, Combinatorial]
    public void BoolIterationTestMethod(bool boolean) => Console.WriteLine(boolean);

    [TestMethod, Combinatorial]
    public void CombinatoralValuesIterationTestMethod(Season season, bool boolean) => Console.WriteLine($"{season} {boolean}");

    [TestMethod, Sequential]
    public void SequentialCombinatoralIterationTestMethod(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");

    [TestMethod, Combinatorial]
    public void CombinatoralIterationTestMethod(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");
}
```

## NUnit

NUnit supports Combinatorial values natively.

```csharp
public class Tests
{
    [Test]
    public void EnumIterationTest([Values] Season season) => Console.WriteLine(season);

    [Test]
    public void BoolIterationTest([Values] bool boolean) => Console.WriteLine(boolean);

    [Test]
    public void CombinatoralValuesIterationTest([Values] Season season, [Values] bool boolean) => Console.WriteLine($"{season} {boolean}");

    [Test, Sequential]
    public void SequentialCombinatoralIterationTest(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");

    [Test, Combinatorial]
    public void CombinatoralIterationTest(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");
}
```

## xUnit

This shows the end result, view the [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/2/src) for implementation.

```csharp
public class UnitTest1
{
    [Theory, Combinatorial]
    public void EnumIterationTest(Season season) => Console.WriteLine(season);

    [Theory, Combinatorial]
    public void BoolIterationTest(bool boolean) => Console.WriteLine(boolean);

    [Theory, Combinatorial]
    public void CombinatoralValuesIterationTest(Season season, bool boolean) => Console.WriteLine($"{season} {boolean}");

    [Theory, Sequential]
    public void SequentialCombinatoralIterationTest(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");

    [Theory, Combinatorial]
    public void CombinatoralIterationTest(
    [Values(1, 2, 3)] int param1,
    [Values("A", "B")] string param2) => Console.WriteLine($"{param1} {param2 ?? "null"}");
}
```