namespace xUnitExample;

public class UnitTest1
{
    private int _counter;

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)] // InlineData will NOT run in parallel
    public void InlineData(int increment) => SleepAndAssert(increment);

    [Theory]
    [MemberData(nameof(Range))] // MemberData will NOT run in parallel
    public void MemberData(int increment) => SleepAndAssert(increment);

    private void SleepAndAssert(int increment)
    {
        Thread.Sleep(1000);
        _counter += increment;
        Assert.Equal(increment, _counter);
    }

    public static IEnumerable<object[]> Range => Enumerable.Range(0, 10).Select(e => new object[] { e });
}