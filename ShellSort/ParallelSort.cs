using System;
using System.Linq;
using System.Threading;
using BenchmarkDotNet.Attributes;


namespace ShellSort
{
    public static class ParallelSort
    {
        [Params(3, 4)] public static int N = 3;

        private static int blocksCount = (int) Math.Pow(2, N);

        private static int threadsCount = blocksCount / 2;
        private static int blocksStep = blocksCount / threadsCount;

        private static int[][] blocks = new int[blocksCount][];
     
        private static Thread[] threads = new Thread[threadsCount];
        private static int[] unsortedArray;


        // метод локальной сортировки, каждый поток сортирует по 2 блока для N = 3
        private static void LocalSort(object threadIndex)
        {
            int index = (int) threadIndex;
            //  blocks[index].Sort();
            //blocks[index + 1].Sort();
            Array.Sort(blocks[index]);
            Array.Sort(blocks[index + 1]);
        }

        private static void MergeSplit(object data)
        {
            int[] tmp = (int[]) data;
            int firstBlockInd = tmp[0];
            int secondBlockInd = tmp[1];
            int[] mergedArray = new int[blocks[firstBlockInd].Length + blocks[secondBlockInd].Length];

            Array.Copy(blocks[firstBlockInd], mergedArray, blocks[firstBlockInd].Length);
            Array.Copy(blocks[secondBlockInd], 0, mergedArray, blocks[firstBlockInd].Length,
                blocks[secondBlockInd].Length);
            Array.Sort(mergedArray);
            Array.Copy(mergedArray, blocks[firstBlockInd], blocks[firstBlockInd].Length);
            Array.Copy(mergedArray, blocks[firstBlockInd].Length, blocks[secondBlockInd], 0,
                blocks[secondBlockInd].Length);
        }
        
        public static void ShellSort(int[] array)
        {
            unsortedArray = array;

            // Этап 0: заполнение блоков
            for (int i = 0; i < threadsCount; i++)
            {
                threads[i] = new Thread(FillBlocks);
            }

            for (int i = 0; i < threadsCount; i++)
            {
                threads[i].Start(i);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Этап 1: локальная сортировка блоков
            for (int i = 0; i < threadsCount; i++)
            {
                threads[i] = new Thread(LocalSort);
                threads[i].Start(i);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Этап 2: merge-split
            for (int i = 0; i < N; i++)
            {
                int x = 0;
                for (int j = 0; j < threadsCount; j++)
                {
                    while ((x & (1 << N - i - 1)) != 0)
                    {
                        x++;
                    }

                    threads[j] = new Thread(MergeSplit);
                    threads[j].Start(new int[] {x, x | (1 << (N - i - 1))});
                    x++;
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }
            }   

            // Этап 3: чет-нечетная сортировка до прекращения изменений
            bool isChanged = true;
            int xor = 0;
            int[] checkArr = new int[blocksCount / 2];
            for (int i = 0; i < blocksCount / 2; i++)
            {
                checkArr[i] = IsChanged(i * 2);
            }

            while (isChanged)
            {
                for (int j = 0; j < threadsCount - xor; j++)
                {
                    threads[j] = new Thread(MergeSplit);
                    threads[j].Start(new int[] {j * 2 + xor, j * 2 + xor + 1});
                }

                xor ^= 1;
                foreach (var thread in threads)
                {
                    thread.Join();
                }

                isChanged = false;
                for (int i = 0; i < blocksCount / 2; i++)
                {
                    if (checkArr[i] != IsChanged(i * 2))
                    {
                        checkArr[i] = IsChanged(i * 2);
                        isChanged = true;
                    }
                }
            }

            // array = blocks.SelectMany(x => x).ToArray();
            int i1 = 0;
            for (int i = 0; i < blocksCount; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    array[i1] = blocks[i][j];
                    i1++;
                }
            }
            
          //  Console.ReadLine();
        }

        private static int IsChanged(int index)
        {
            int tmp = 0;
            foreach (var i in blocks[index])
            {
                tmp <<= i;
            }
            return tmp;
        }

        private static void FillBlocks(object threadIndex)
        {
            int index = (int) threadIndex;
            int startIndex = index * blocksStep;
            int finishIndex = (index + 1) * blocksStep;
            
            for (int i = startIndex; i < finishIndex; i++)
            {
                int firstElemIndex = i * (unsortedArray.Length / blocksCount);
                int lastElemIndex = (i + 1) * (unsortedArray.Length / blocksCount) +
                                    (i == blocksCount - 1 ? unsortedArray.Length % blocksCount : 0);
                blocks[i] = new int[lastElemIndex - firstElemIndex];
                for (int j = firstElemIndex, k = 0; j < lastElemIndex && k < blocks[i].Length; j++, k++)
                {
                    blocks[i][k] = (unsortedArray[j]);
                }
            }
        }
    }
}