# Guards

`Guards` is the repository for the `Atya.Foundation.Guards` NuGet package.

| | |
| --- | --- |
| Repository | [https://github.com/AtyaLibraries/Guards](https://github.com/AtyaLibraries/Guards) |
| NuGet | `Atya.Foundation.Guards` |
| License | MIT |

This package provides lightweight guard clauses for validating arguments and
protecting invariants in constructors, factories, and other low-level APIs.

## Included guards

- `Guard.AgainstNull`
- `Guard.AgainstNullOrEmpty`
- `Guard.AgainstNullOrWhiteSpace`
- `Guard.AgainstDefault`
- `Guard.AgainstEmpty`
- `Guard.AgainstOutOfRange`
- `Guard.AgainstNegative`
- `Guard.AgainstZeroOrNegative`

## Layout

```text
.
|-- src/Guards/
|-- tests/Guards.UnitTests/
|-- samples/Guards.Samples.Console/
|-- benchmarks/Guards.Benchmarks/
`-- .github/
```

## Build, Test, Pack

```bash
dotnet restore ./Guards.sln
dotnet build ./Guards.sln --configuration Release --no-restore
dotnet test ./tests/Guards.UnitTests/Guards.UnitTests.csproj --configuration Release --no-build
dotnet pack ./src/Guards/Guards.csproj --configuration Release --no-build
```

Artifacts land in `artifacts/packages/`.
