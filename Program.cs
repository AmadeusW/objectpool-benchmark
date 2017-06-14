using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Engines;
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

    [SimpleJob(RunStrategy.Monitoring, launchCount: 3, warmupCount: 0, targetCount: 1)]
    public class MemoryTests
    {
        [Params(256, 1024, 4096, 16384)]
        public int Count {get;set;}

        [Params(64, 256, 1024)]
        public int Size {get;set;}

        [Params(1, 2, 4)]
        public int ReuseCount {get;set;}

        [Params(0, 2, 4)]
        public static int CreationCost {get;set;}

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
             if (SampleObject.Allocated != ReuseCount * Count) {
                 throw new InvalidOperationException($"Allocate {ReuseCount}*{Count} expected {ReuseCount*Count} objects but saw {SampleObject.Allocated}.");
             }
        }

        [Benchmark(Description = "Pool")]
        public void Pool()
        {
            for (int i = 0; i < ReuseCount; i++)
            {
                using (var library = new PooledLibrary(Count, Size))
                {

                }
                GC.Collect();
            }
            if (SampleObject.Allocated != Count) {
                 throw new InvalidOperationException($"Pool {ReuseCount}*{Count} expected {Count} objects but saw {SampleObject.Allocated}.");
            }
        }
    }
}
