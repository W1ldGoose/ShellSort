namespace ShellSort
{
    public static class SerialSort
    {
        //метод для обмена элементов
        static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }

        public static void ShellSort(ref int[] array)
        {
            //расстояние между элементами, которые сравниваются
            var step = array.Length / 2;
            while (step >= 1)
            {
                for (var i = step; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= step) && (array[j - step] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - step]);
                        j = j - step;
                    }
                }
                step = step / 2;
            }
        }
    }
}