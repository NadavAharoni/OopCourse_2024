namespace Memory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 1, 2, 3 };
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            ExpandArray(a);

            Console.WriteLine();
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }

        static void ExpandArray(int[] array)
        {
            int[] oldArray = array;
            int arrayLength = array.Length;
            array = new int[arrayLength * 2];
            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = oldArray[i];
                array[i * 2] = oldArray[i];
            }
        }
    }
}
