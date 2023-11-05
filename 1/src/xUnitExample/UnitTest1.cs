namespace xUnitExample;

public class UnitTest1
{
    [Theory]
    [EnumData<Season>]
    public void EnumIterationTest(Season season) => Console.WriteLine(season);
}