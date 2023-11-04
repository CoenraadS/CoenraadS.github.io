# .NET Unit Test Framework Comparison - Parallelism Granularity
*2023/11/05 - Coenraad Stijne*  

#### Summary:  
`MSTest` and `NUnit` support greater levels of *Parallelism Granularity* then `xUnit`.  
When `NUnit` is configured to use `Method`,  the documentation recommends changing the `Test Class` instantiation behavior from `Single Instance` to `Instance Per Test` for thread safety.

#### Personal Learnings:

`MSTest` and `NUnit` both support `ctor/dispose` pattern to `setup/teardown` tests, similar to `xUnit`.

#### [Code Examples](https://github.com/CoenraadS/CoenraadS.github.io/tree/main/0/src)

---

### Framework Overview

| Framework | Version | Default Parallelism | Maximum Parallelism |
| --------- | ------- | ------------------- | ------------------- |
| MSTest    | 3.1.1   | None                | Method              |
| NUnit     | 4.5.0   | None                | Method              |
| xUnit     | 2.6.1   | Collection (class)  | Collection (class)  |

#### Note: When using `Method` level of parallelism, it is the developer's responsibility to ensure the tests are thread safe.

## Configuring MSTest

[ParallelizeAttribute Documentation](https://devblogs.microsoft.com/devops/mstest-v2-in-assembly-parallel-test-execution/)

The `Parallelize` attribute can be set to configure the default strategy.

```csharp
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
```

This will parallelize all methods, including `DataRow` and `DynamicData`.

### ✅ Thread Safety:

`MSTest` creates a new instance per test by default. Unless using `static`, fields/properties will be thread safe.

## Configuring NUnit

[ParallelizableAttribute Documentation](https://docs.nunit.org/articles/nunit/writing-tests/attributes/parallelizable.html)  
[FixtureLifeCycle Documentation](https://docs.nunit.org/articles/nunit/writing-tests/attributes/fixturelifecycle.html)

The `Parallelizable` attribute can be set to configure the default strategy.  
`NUnit` also recommends to set the `FixtureLifeCycle` attribute

```csharp
[assembly: Parallelizable(ParallelScope.Children)]
[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
```

This will parallelize all methods, including `TestCase` and `TestCaseSource`.

### ⚠️ Thread Safety:

The `FixtureLifeCycle` attribute was added so that `NUnit` will create a new instance of the test class for each test method executed.
This is to ensure that fields/properties of the class will not be accessed by multiple tests at the same time.

### Important Notes about [FixtureLifeCycle](https://docs.nunit.org/articles/nunit/writing-tests/attributes/fixturelifecycle.html):

- When using `LifeCycle.InstancePerTestCase`, the `OneTimeSetUp` and `OneTimeTearDown` methods must be `static`, and each are only called once. This is required so that the `setup` or `teardown` methods do not access instance fields or properties that are reset for every test.

- When using `LifeCycle.InstancePerTestCase`, a class's `constructor` will be called before every test is executed and `IDisposable` test fixtures will be disposed after the test is finished.

- `SetUp` and `TearDown` methods are called before and after every test.

- The `Order` attribute is respected.


## Configuring xUnit

- `xUnit` does not support method level parallelism out of the box
- It is being considered for `v3`, see the [Roadmap](https://github.com/xunit/xunit/issues/2133)
- There is a community solution, see [https://github.com/xunit/xunit/issues/1986#issuecomment-1028299919](https://github.com/xunit/xunit/issues/1986#issuecomment-1028299919)
- Since it is not officially supported, it will not be covered in this blog
