``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19041.1348 (2004/May2020Update/20H1)
Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=5.0.402
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  Job-VEAHKA : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

RunStrategy=Throughput  

```
|       Method | elemsCount |         Mean |      Error |     StdDev | Rank |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|------------- |----------- |-------------:|-----------:|-----------:|-----:|--------:|-------:|-------:|----------:|
|   SerialTest |      10000 |     7.350 μs |  0.0177 μs |  0.0165 μs |    1 |  0.0839 |      - |      - |     280 B |
|   SerialTest |   10000000 |     7.364 μs |  0.0123 μs |  0.0109 μs |    1 |  0.0839 |      - |      - |     280 B |
|   SerialTest |  100000000 |     7.449 μs |  0.0273 μs |  0.0242 μs |    1 |  0.0839 |      - |      - |     280 B |
|   SerialTest |    1000000 |     7.473 μs |  0.0592 μs |  0.0494 μs |    1 |  0.0839 |      - |      - |     280 B |
| ParallelTest |      10000 | 1,870.686 μs | 16.1190 μs | 14.2891 μs |    2 | 68.3594 | 7.8125 | 7.8125 |  12,432 B |
| ParallelTest |  100000000 | 1,878.184 μs | 18.5696 μs | 17.3701 μs |    2 | 68.3594 | 7.8125 | 7.8125 |  12,432 B |
| ParallelTest |   10000000 | 1,886.122 μs | 14.5638 μs | 12.9105 μs |    2 | 68.3594 | 7.8125 | 7.8125 |  12,432 B |
| ParallelTest |    1000000 | 1,891.223 μs | 19.2790 μs | 17.0903 μs |    2 | 68.3594 | 7.8125 | 7.8125 |  12,432 B |
