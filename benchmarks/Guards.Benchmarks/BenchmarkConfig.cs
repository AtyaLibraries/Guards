using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Loggers;

namespace Guards.Benchmarks;

public sealed class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddColumnProvider(DefaultColumnProviders.Instance);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddExporter(MarkdownExporter.GitHub);
        AddExporter(JsonExporter.Full);
        AddLogger(ConsoleLogger.Default);
        WithOptions(ConfigOptions.DisableLogFile);
    }
}
