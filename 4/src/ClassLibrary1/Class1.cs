namespace ClassLibrary1;

public class Class1
{
    private readonly Random _random;

    public Class1(Random random)
    {
        _random = random;
    }

    public int GenerateRandomNumber() => _random.Next();
}
