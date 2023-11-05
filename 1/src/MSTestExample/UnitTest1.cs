namespace MSTestExample;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [EnumData<Season>]
    public void EnumIterationTest(Season season) => Console.WriteLine(season);
}
