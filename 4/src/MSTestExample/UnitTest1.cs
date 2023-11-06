using ClassLibrary1;
using Moq;

namespace MSTestMethodExample;

[TestClass]
public class TestMethods
{
    [TestMethod]
    public void ConcreteClassMockTest()
    {
        var mock = new Mock<Class1>();

        mock.Setup(e => e.GenerateRandomNumber()).Returns(1);

        var result = mock.Object.GenerateRandomNumber();

        Assert.AreEqual(1, result);
    }
}