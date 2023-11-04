namespace NUnitExample;

public class Tests
{
    private int _counter;

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(10)] // TestCase will run in parallel
    public void TestCase(int increment) => SleepAndAssert(increment);

    [Test]
    [TestCaseSource(nameof(Range))] // TestCaseSource data will run in parallel
    public void TestCaseSource(int increment) => SleepAndAssert(increment);

    private void SleepAndAssert(int increment)
    {
        Thread.Sleep(1000);
        _counter += increment;
        Assert.That(_counter, Is.EqualTo(increment));
    }

    public static IEnumerable<object[]> Range => Enumerable.Range(0, 10).Select(e => new object[] { e });
}