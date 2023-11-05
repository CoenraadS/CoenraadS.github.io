using Common;
using ValuesAttribute = NUnit.Framework.ValuesAttribute;

namespace NUnitExample;

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