using Common;
using MSTestExample;

namespace MSTestMethodExample;

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