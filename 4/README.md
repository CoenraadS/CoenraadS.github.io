# .Net Unit Test - Mock Concrete Classes using Fody
*2023/11/07 - Coenraad Stijne*  

#### Summary:
When creating unit tests, object being tested either requires `virtual` members, or an `interface`.

Through the use of the [Fody](https://github.com/Fody/Fody) package, it is possible to manipulate the intermediate language (IL) during build, and make all concrete class members `virtual`, so that they can be mocked without extra developer effort.

⚠️ This will not work for `sealed` types. There is a [Unsealed.Fody](https://github.com/fodyarchived/Unsealed) project, but it is unmaintained.  

#### [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/4/src)

---

### Package Overview

| Package                                                      | Description                                                                    |
| ------------------------------------------------------------ | ------------------------------------------------------------------------------ |
| [Fody](https://github.com/Fody/Fody)                         | Extensible tool for weaving .net assemblies                                    |
| [Virtuosity](https://github.com/Fody/Virtuosity)             | Change all members to virtual.                                                 |
| [EmptyConstructor](https://github.com/Fody/EmptyConstructor) | Adds an empty constructor to classes even if you have a non-empty one defined. |

## Fody Configuration

To enable `Fody`, add the following to the *Test* `.csproj`.  
We configure it to only run in `Debug` configurations, so that `Release` builds remain unaffected.

```xml
<PropertyGroup>
  
  ...

  <WeaverConfiguration Condition="'$(Configuration)' == 'Debug'">
    <Weavers>
      <Virtuosity />
      <EmptyConstructor />
    </Weavers>
  </WeaverConfiguration>
</PropertyGroup>
```

## Example of a concrete class we want to test:

```csharp
public class Class1
{
    private readonly Random _random;

    public Class1(Random random)
    {
        _random = random;
    }

    public int GenerateRandomNumber() => _random.Next();
}
```

## Manipulated class that is included in the build:

```csharp
public class Class1
{
    private readonly Random _random;

    public Class1(Random random)
    {
        _random = random;
    }

    public Class1()
    {
        _random = random;
    }

    public virtual int GenerateRandomNumber() => _random.Next();
}
```

## `MSTest` + `Moq` example:

```csharp
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
```