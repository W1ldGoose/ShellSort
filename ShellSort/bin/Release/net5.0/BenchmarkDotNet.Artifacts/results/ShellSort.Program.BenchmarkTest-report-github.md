``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19041.1348 (2004/May2020Update/20H1)
Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET SDK=5.0.402
  [Host]     : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT
  Job-ATNWLT : .NET 5.0.11 (5.0.1121.47308), X64 RyuJIT

RunStrategy=Throughput  

```
|       Method |         N | type |        Mean |     Error |    StdDev | Rank |     Gen 0 |     Gen 1 |     Gen 2 |       Allocated |
|------------- |---------- |----- |------------:|----------:|----------:|-----:|----------:|----------:|----------:|----------------:|
| ParallelTest |   1000000 | Desc |    18.29 ms |  0.358 ms |  0.608 ms |    1 | 1375.0000 | 1343.7500 | 1343.7500 |    19,017,326 B |
| ParallelTest |   1000000 |  Asc |    18.40 ms |  0.358 ms |  0.452 ms |    1 | 1312.5000 | 1281.2500 | 1281.2500 |    19,018,854 B |
| ParallelTest |   1000000 | Rand |    20.56 ms |  0.367 ms |  0.603 ms |    2 | 1187.5000 | 1156.2500 | 1156.2500 |    19,015,524 B |
|   SerialTest |   1000000 | Desc |    21.59 ms |  0.204 ms |  0.181 ms |    3 |         - |         - |         - |               - |
|   SerialTest |   1000000 | Rand |    21.75 ms |  0.112 ms |  0.105 ms |    3 |         - |         - |         - |               - |
|   SerialTest |   1000000 |  Asc |    21.78 ms |  0.211 ms |  0.187 ms |    3 |         - |         - |         - |               - |
| ParallelTest |  10000000 | Desc |   182.72 ms |  3.086 ms |  2.577 ms |    4 |  666.6667 |  666.6667 |  666.6667 |   190,018,152 B |
| ParallelTest |  10000000 |  Asc |   183.06 ms |  2.259 ms |  2.113 ms |    4 |  666.6667 |  666.6667 |  666.6667 |   190,012,525 B |
|   SerialTest |  10000000 |  Asc |   265.27 ms |  0.545 ms |  0.455 ms |    5 |         - |         - |         - |           668 B |
| ParallelTest |  10000000 | Rand |   266.23 ms |  5.132 ms |  7.681 ms |    5 | 1000.0000 | 1000.0000 | 1000.0000 |   190,017,756 B |
|   SerialTest |  10000000 | Rand |   266.24 ms |  1.456 ms |  1.291 ms |    5 |         - |         - |         - |               - |
|   SerialTest |  10000000 | Desc |   268.30 ms |  3.632 ms |  3.397 ms |    5 |         - |         - |         - |               - |
| ParallelTest | 100000000 | Desc | 2,217.21 ms |  7.047 ms |  6.592 ms |    6 |         - |         - |         - | 1,900,009,728 B |
| ParallelTest | 100000000 |  Asc | 2,217.66 ms | 17.183 ms | 14.349 ms |    6 |         - |         - |         - | 1,900,011,072 B |
| ParallelTest | 100000000 | Rand | 2,755.57 ms | 28.136 ms | 26.318 ms |    7 |         - |         - |         - | 1,900,011,072 B |
|   SerialTest | 100000000 | Desc | 2,995.36 ms |  8.207 ms |  7.677 ms |    8 |         - |         - |         - |               - |
|   SerialTest | 100000000 |  Asc | 3,019.02 ms |  9.212 ms |  8.617 ms |    8 |         - |         - |         - |         1,240 B |
|   SerialTest | 100000000 | Rand | 3,061.95 ms | 53.694 ms | 50.226 ms |    8 |         - |         - |         - |         1,240 B |
