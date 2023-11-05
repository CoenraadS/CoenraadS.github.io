namespace NUnitExample;

public class Tests
{
    [Test]
    public void EnumIterationTest([Values] Season season) => Console.WriteLine(season);
}