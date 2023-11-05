using Common;

namespace xUnitExample;

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