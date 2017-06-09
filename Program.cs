using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;

namespace Ama.ObjectPools
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            config.Add(new MemoryDiagnoser());
            config.Add(new CsvExporter(
                CsvSeparator.CurrentCulture,
                new BenchmarkDotNet.Reports.SummaryStyle
                {
                    PrintUnitsInHeader = true,
                    PrintUnitsInContent = false,
                    TimeUnit = BenchmarkDotNet.Horology.TimeUnit.Millisecond,
                    SizeUnit = BenchmarkDotNet.Columns.SizeUnit.KB
                }
            ));
            config.Add(new MemoryDiagnoser());
            var summary = BenchmarkRunner.Run<MemoryTests>(config);
        }
    }

    [MemoryDiagnoser]
    public class MemoryTests
    {
        [Params(32, 64, 128)]
        public int Count {get;set;}

        [Params(64, 128, 256)]
        public int Size {get;set;}

        [Params(1, 2, 3)]
        public int ReuseCount {get;set;}

        [Benchmark(Description = "Allocate")]
        public void Allocate()
        {
            for (int i = 0; i < ReuseCount; i++)
            {
                using (var library = new SampleLibrary(Count, Size))
                {

                }
                GC.Collect();
            }
            Console.WriteLine($"Allocated {SampleObject.Allocated} objects. Params: {ReuseCount} * {Count} * {Size}B");
        }

        [Benchmark(Description = "Pool")]
        public void Pool()
        {
            for (int i = 0; i < ReuseCount; i++)
            {
                using (var library = new PooledLibrary(Count, Size))
                {

                }
            }
            Console.WriteLine($"Allocated {SampleObject.Allocated} objects. Params: {ReuseCount} * {Count} * {Size}B");
        }
    }
}
