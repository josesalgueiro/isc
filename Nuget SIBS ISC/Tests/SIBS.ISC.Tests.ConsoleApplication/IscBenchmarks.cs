namespace SIBS.ISC.Tests.ConsoleApplication
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Text;
	using System.Threading.Tasks;
	using BenchmarkDotNet.Attributes;
	using BenchmarkDotNet.Diagnostics.dotMemory;
	using BenchmarkDotNet.Diagnostics.Windows.Configs;
	using BenchmarkDotNet.Order;

	[MemoryDiagnoser]
	[DotMemoryDiagnoser]
	[NativeMemoryProfiler]
	[RankColumn]
	[SimpleJob(iterationCount: 20)]
	public class IscBenchmarks
	{
		private static readonly IscSplitter _IscSplitter = new IscSplitter();

		[Benchmark]
		public void Run()
		{
			_IscSplitter.RunSingle();
		}
	}
}
