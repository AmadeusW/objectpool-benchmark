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
        [Params(32, 64, 128)]
        public int Count {get;set;}

        [Params(64, 128, 256)]
        public int Size {get;set;}

        [Params(1, 2, 3)]
        public int ReuseCount {get;set;}

        [Benchmark(Description = "Allocate new objects")]
        public void AllocateNew()
        {
            for (int i = 0; i < ReuseCount; i++)
            {
                var library = new SampleLibrary(Count, Size);
                library = null;
            }
        }
/*
        [Benchmark(Description = "Allocate or reuse objects")]
        void AllocateOrReuse()
        {
            var library = new SampleLibrary(Count, Size);
            library = null;
            var library2 = new SampleLibrary(Count, Size);
            library2 = null;
        }
        */
    }
}
