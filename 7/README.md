# .NET Unit Test Generation Comparison - VS2022
*2023/11/07 - Coenraad Stijne*  

#### Summary

The inbuilt test generator in Visual Studio will only generate stubs.  
A more useful test generator, is one that is aware of the dependencies and provides auto mocking options.  

`Unitverse` and `Unit Test Boilerplate Generator` both support a broad combination of the most popular Test Frameworks and Mocking Libraries. Also both are highly configurable, and support exporting their configurations, so they can be committed to the repository.

### Extension Overview

| Package                                                                                                                        |
| ------------------------------------------------------------------------------------------------------------------------------ |
| [Unit Test Boilerplate Generator](https://marketplace.visualstudio.com/items?itemName=RandomEngy.UnitTestBoilerplateGenerator) |
| [Unitverse C# Unit Test Generator](https://marketplace.visualstudio.com/items?itemName=MattWhitfield.UnitverseVS2022)          |

## Example File

To test the output of both extensions, I used the following setup:

```csharp
public interface IFooService
{
    public string Foo();
}

public interface IBarService
{
    public string Foo();
}

public class FooBarService
{
    private readonly IFooService _fooService;
    private readonly IBarService _barService;

    public FooBarService(IFooService fooService, IBarService barService)
    {
        _fooService = fooService;
        _barService = barService;
    }

    public string FooBar() => _fooService.Foo() + _barService.Foo();
}
```

## Unit Test Boiler Plate generator

`MSTest` + `Moq` example

```csharp
namespace MSTestExample
{
    [TestClass]
    public class FooBarServiceTests
    {
        private MockRepository mockRepository;

        private Mock<IFooService> mockFooService;
        private Mock<IBarService> mockBarService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockFooService = this.mockRepository.Create<IFooService>();
            this.mockBarService = this.mockRepository.Create<IBarService>();
        }

        private FooBarService CreateService()
        {
            return new FooBarService(
                this.mockFooService.Object,
                this.mockBarService.Object);
        }

        [TestMethod]
        public void FooBar_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = service.FooBar();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
```

## Unitverse

`MSTest` + `Moq` example

```csharp
[TestClass]
public class FooBarServiceTests
{
    private FooBarService _sut;
    private Mock<IFooService> _fooService;
    private Mock<IBarService> _barService;

    [TestInitialize]
    public void SetUp()
    {
        _fooService = new Mock<IFooService>();
        _barService = new Mock<IBarService>();
        _sut = new FooBarService(_fooService.Object, _barService.Object);
    }

    [TestMethod]
    public void CanCallFooBar()
    {
        // Arrange
        _fooService.Setup(mock => mock.Foo()).Returns("TestValue861335204");
        _barService.Setup(mock => mock.Foo()).Returns("TestValue1216217236");

        // Act
        var result = _sut.FooBar();

        // Assert
        _fooService.Verify(mock => mock.Foo());
        _barService.Verify(mock => mock.Foo());

        Assert.Fail("Create or modify test");
    }
}
```