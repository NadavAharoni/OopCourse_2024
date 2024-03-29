namespace OpTable
{
    internal class Program
    {
        public static double Add(double x, double y)
        { 
            return x + y;
        }

        public static double Multiply(double x, double y)
        {
            return x * y;
        }


        static void Main(string[] args)
        {
            OperationTable t1 = new OperationTable(5, 6, Add);
            t1.Print();

            Console.WriteLine();
            OperationTable.op_func op = Multiply;
            OperationTable t2 = new OperationTable(5, 6, op);
            t2.Print();
        }
    }

    class OperationTable
    {
        // the followng line defines a _type_ op_func
        public delegate double op_func(double x, double y);

        // the following line defines a variable of type op_func
        private op_func op;

        private double[,] values;
        
        public OperationTable(int rows, int cols, op_func _op)
        {
            values = new double[rows, cols];
            op = _op;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    values[r, c] = op(r, c);
                }
            }
        }

        public void Print()
        {
            int rows = values.GetLength(0);
            int cols = values.GetLength(1);
            for (int r = 0; r < rows; r++)
            {
                Console.Write($"{values[r, 0]}");
                for (int c = 1; c < cols; c++)
                {
                    Console.Write($" | {values[r, c]}");
                }
                Console.WriteLine();
            }
        }
    }
}
