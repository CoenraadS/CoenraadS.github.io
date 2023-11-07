# .NET Unit Test - Internal Interfaces
*2023/11/07 - Coenraad Stijne*  

Often during Unit Testing, internal interfaces are required to be visible to the test project.

The following can be added to the `.csproj`, or `Directory.Build.props`

```xml
<ItemGroup>
  <InternalsVisibleTo Include="$(ProjectName).TestProject" />
  <InternalsVisibleTo Include="DynamicProxyGenAssembly2"  />
</ItemGroup>
```