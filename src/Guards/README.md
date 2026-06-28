# Atya.Foundation.Guards

*Lightweight guard clauses for validating arguments and protecting invariants in modern .NET code.*

[![NuGet Version](https://img.shields.io/nuget/v/Atya.Foundation.Guards?style=for-the-badge&logo=nuget&logoColor=white&label=NuGet&color=512BD4)](https://www.nuget.org/packages/Atya.Foundation.Guards)
[![Downloads](https://img.shields.io/nuget/dt/Atya.Foundation.Guards?style=for-the-badge&logo=nuget&logoColor=white&label=Downloads&color=512BD4)](https://www.nuget.org/packages/Atya.Foundation.Guards)
![.NET 10.0](https://img.shields.io/badge/.NET%2010.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
[![License: MIT](https://img.shields.io/badge/License-MIT-512BD4?style=for-the-badge)](https://github.com/AtyaLibraries/Guards/blob/development/LICENSE)

## 📖 Overview

`Atya.Foundation.Guards` provides a compact set of guard clauses for validating method arguments, constructor parameters, and low-level invariants. It helps fail fast with standard .NET exceptions while preserving the original argument name through caller argument expressions. The package is intended for foundation libraries and application code that need clear validation without a large dependency surface.

## ✨ Features

- **Null validation** — validate reference types and nullable value types with `ArgumentNullException`.
- **String guards** — reject null, empty, and whitespace-only strings.
- **Collection guards** — validate read-only collections and enumerables without unnecessary enumeration when counts are available.
- **Numeric guards** — enforce inclusive ranges, non-negative values, and values greater than zero.
- **Invariant-friendly API** — return the validated value so guards can be used directly in assignments.

## 📦 Installation

**.NET CLI**
```bash
dotnet add package Atya.Foundation.Guards
```

**Package Manager**
```powershell
Install-Package Atya.Foundation.Guards
```

**PackageReference**
```xml
<PackageReference Include="Atya.Foundation.Guards" Version="1.0.0" />
```

## 🚀 Quick Start

```csharp
using Atya.Foundation.Guards;
using System;

string customerName = Guard.AgainstNullOrWhiteSpace("Ada Lovelace");
int quantity = Guard.AgainstZeroOrNegative(25);
Guid customerId = Guard.AgainstEmpty(Guid.NewGuid());

Console.WriteLine($"{customerName}: {customerId} ({quantity})");
```

## 📚 Usage

### Validate Constructor Input

```csharp
using Atya.Foundation.Guards;
using System;

public sealed class OrderRequest(Guid customerId, string customerName, int quantity)
{
    public Guid CustomerId { get; } = Guard.AgainstEmpty(customerId);

    public string CustomerName { get; } = Guard.AgainstNullOrWhiteSpace(customerName);

    public int Quantity { get; } = Guard.AgainstZeroOrNegative(quantity);
}
```

### Validate Ranges

```csharp
using Atya.Foundation.Guards;

int retryCount = Guard.AgainstOutOfRange(3, minimum: 1, maximum: 10);
decimal discount = Guard.AgainstNegative(0.15m);
```

### Validate Collections

```csharp
using Atya.Foundation.Guards;
using System;
using System.Collections.Generic;
using System.Linq;

IReadOnlyCollection<Guid> candidateItemIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
IReadOnlyCollection<Guid> itemIds = Guard.AgainstNullOrEmpty(candidateItemIds);

IEnumerable<int> candidatePageNumbers = Enumerable.Range(1, 3);
IEnumerable<int> pageNumbers = Guard.AgainstNullOrEmpty(candidatePageNumbers);
```

## 🧩 API Overview

| Type | Description |
|------|-------------|
| `Guard` | Static entry point for all guard clauses. |
| `Guard.AgainstNull` | Validates reference types and nullable value types. |
| `Guard.AgainstNullOrEmpty` | Validates strings, read-only collections, and enumerables. |
| `Guard.AgainstNullOrWhiteSpace` | Validates strings that must contain non-whitespace text. |
| `Guard.AgainstDefault` | Rejects the default value for value types. |
| `Guard.AgainstEmpty` | Rejects `Guid.Empty`. |
| `Guard.AgainstOutOfRange` | Validates `int`, `long`, and `decimal` values against an inclusive range. |
| `Guard.AgainstNegative` | Rejects negative `int`, `long`, and `decimal` values. |
| `Guard.AgainstZeroOrNegative` | Rejects zero or negative `int`, `long`, and `decimal` values. |

## 🗂️ Project Structure

```text
📦 Guards
├── 📂 src
│   └── 📄 Guards.csproj                  # the published package
├── 🧪 tests
│   └── 📄 Guards.UnitTests.csproj
├── 🎯 samples
│   └── 📄 Guards.Samples.Console.csproj
├── ⚡ benchmarks
│   └── 📄 Guards.Benchmarks.csproj
├── ⚙️ .github/workflows
│   └── 📄 ci.yml
├── 📄 Guards.sln
├── 📄 README.md
└── 📄 LICENSE
```

| Icon | Meaning |
|------|---------|
| 📦 | Solution / repo root (`.sln`) |
| 📂 `src` | Library source code (the published package) |
| 🧪 `tests` | Unit & integration tests |
| 🎯 `samples` / `examples` | Runnable usage samples |
| ⚡ `benchmarks` | Performance benchmarks |
| 📚 `docs` | Documentation |
| ⚙️ `.github` | CI/CD & repo config |
| 📄 | Notable single file |

## 🎯 Compatibility

Targets `net10.0`. The package is platform-neutral and uses standard .NET exception types.

## 🧪 Testing

```bash
dotnet test
```

## ⚡ Benchmarks

Benchmarks are available under `benchmarks/Guards.Benchmarks` and can be run in Release configuration.

```bash
dotnet run --project benchmarks/Guards.Benchmarks/Guards.Benchmarks.csproj --configuration Release
```

## 🤝 Contributing

Contributions are welcome! Please open an issue or pull request on [GitHub](https://github.com/AtyaLibraries/Guards).

## 📄 License

Released under the **MIT** license. See [LICENSE](https://github.com/AtyaLibraries/Guards/blob/development/LICENSE) for details.

---

## 🏛️ About Atya Libraries

`Atya.Foundation.Guards` is part of **[Atya Libraries](https://github.com/AtyaLibraries)** — a family of focused, modern .NET libraries published under the reserved **`Atya.*`** prefix on NuGet. Every package shares the same principles: a small, clear public API, full test coverage, and consistent documentation.

🔎 Browse the full collection on [GitHub](https://github.com/AtyaLibraries) and [NuGet](https://www.nuget.org/profiles/ArsenAsulyan).

Made with 💜 .NET · © 2026 Atya Libraries
