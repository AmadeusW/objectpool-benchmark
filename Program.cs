using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;

namespace A6z.ObjectPools
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            config.Set(new BenchmarkDotNet.Reports.SummaryStyle()
            {
                PrintUnitsInHeader = true,
                PrintUnitsInContent = false,
                TimeUnit = BenchmarkDotNet.Horology.TimeUnit.Millisecond,
                SizeUnit = BenchmarkDotNet.Columns.SizeUnit.KB
            });
            config.Add(new MemoryDiagnoser());
            var summary = BenchmarkRunner.Run<MemoryTests>(config);
        }
    }

    [MemoryDiagnoser]
    public class MemoryTests
    {
        [Benchmark(Description = "Allocate 100B")]
        public byte[] Allocate()
        {
            return new byte[100];
        }
    }
}
