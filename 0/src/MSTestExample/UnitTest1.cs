namespace MSTestExample;

[TestClass]
public class UnitTest1
{
    private int _counter;

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(5)]
    [DataRow(6)]
    [DataRow(7)]
    [DataRow(8)]
    [DataRow(9)]
    [DataRow(10)] // DataRow will run in parallel
    public void DataRow(int increment) => SleepAndAssert(increment);

    [TestMethod()]
    [DynamicData(nameof(Range))] // Dynamic data will run in parallel
    public void DynamicData(int increment) => SleepAndAssert(increment);

    private void SleepAndAssert(int increment)
    {
        Thread.Sleep(1000);
        _counter += increment;
        Assert.AreEqual(increment, _counter);
    }

    public static IEnumerable<object[]> Range => Enumerable.Range(0, 10).Select(e => new object[] { e });
}
