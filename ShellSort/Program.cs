using System;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ShellSort
{
    public class Program
    {
        private static void FillRandom(int[] unsortedArray)
        {
            Random rand = new Random();
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = rand.Next(0, unsortedArray.Length);
            }
        }
        private static void FillAsc(int[] unsortedArray)
        {
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = i;
            }
        }
        private static void FillDesc(int[] unsortedArray)
        {
            for (int i = 0; i < unsortedArray.Length; i++)
            {
                unsortedArray[i] = unsortedArray.Length - i;
            }
        }
        public static bool IsSorted(int[] a)
        {
            bool sorted = true;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i - 1] > a[i])
                {
                    sorted = false;
                    break;
                }
            }
            return sorted;
        }
        
        [MemoryDiagnoser()]
        [Orderer(SummaryOrderPolicy.FastestToSlowest)]
        [RankColumn()]
        [RPlotExporter]
        [SimpleJob(RunStrategy.Throughput)]
        public class BenchmarkTest
        {
            [Params(1000000, 10000000, 100000000)]
            public int N = 1000000;
            
            public enum arrayType
            {
                Asc,
                Desc,
                Rand
            }
            [Params(arrayType.Rand,arrayType.Asc,arrayType.Desc)]
            public arrayType type = arrayType.Rand;
            
            [GlobalSetup]
            public void Setup()
            {
                unsortedArray = new int[N];
                switch (type)
                {
                    case arrayType.Rand:
                        FillRandom(unsortedArray);
                        break;
                    case arrayType.Asc:
                        FillAsc(unsortedArray);
                        break;
                    case arrayType.Desc:
                        FillDesc(unsortedArray);
                        break;
                }
            }
            
            public int[] unsortedArray;

            [Benchmark]
            public void ParallelTest()
            {
                ParallelSort.ShellSort(unsortedArray);
            }
            [Benchmark]
            public void SerialTest()
            {
                SerialSort.ShellSort(unsortedArray);
            }
        }

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkTest>();
        }
    }
}