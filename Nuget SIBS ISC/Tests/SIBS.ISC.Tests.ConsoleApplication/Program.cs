// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Running;
using SIBS.ISC.Tests.ConsoleApplication;

//BenchmarkRunner.Run<IscBenchmarks>();

IscSplitter _iscSplitter = new IscSplitter();
//_iscSplitter.RunSingle();
_iscSplitter.Run();

