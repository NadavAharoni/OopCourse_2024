using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

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

        public static Fraction AddFraction(Fraction x, Fraction y)
        {
            return x + y;
        }

        static void TestFractions()
        {
            Fraction f1 = new Fraction(2, 4);
            Console.WriteLine($"f1={f1}");

            Fraction f2 = new Fraction(1, 2);

            Console.WriteLine($"f1+f2={f1 + f2}");

            Fraction f3 = new Fraction(100, 50);
            f3.Simplify();
            Console.WriteLine($"f3={f3}");

            if (f3 == new Fraction(4, 2))
            {
                Console.WriteLine("f3 == Fraction(4, 2)");
            }

            Console.WriteLine($"f1={f1}");
            if (f1 == f2)
            {
                Console.WriteLine($"{f1}=={f2}");
            }
            else
            {
                Console.WriteLine($"{f1}!={f2}");
            }


        }
        static void Main(string[] args)
        {
            List<Fraction> row_values = new List<Fraction>();
            for(int i=1; i<=12; i++)
            {
                row_values.Add(new Fraction(i, 12));
            }
            List<Fraction> col_values = new List<Fraction>();
            for (int i = 1; i < 7; i++)
            {
                col_values.Add(new Fraction(i, 12));
            }

            OperationTable<Fraction> t1 = new OperationTable<Fraction>(row_values, col_values, (x,y) => x+y );
            t1.Print();
        }
    }

    class OperationTable<T>
    {
        // the followng line defines a _type_ op_func
        public delegate T OpFunc(T x, T y);

        // the following line defines a variable of type op_func
        public OpFunc op;

        protected T[,]? values = null;

        protected List<T>? row_values = null;
        protected List<T>? col_values = null;

        public OperationTable(List<T> _row_values, List<T> _col_values, OpFunc _op)
        {
            op = _op;
            // --> exercise: comlete this function
        }

        public void Print()
        {
            Console.WriteLine($"==== table ======");

            if ((row_values is null) || (col_values is null) || (values is null))
            {
                return;
            }

            int rows = values.GetLength(0);
            int cols = values.GetLength(1);

            Console.Write($"      : ");
            Console.Write($"{col_values[0],5}");
            for (int c=1; c<cols; c++)
            {
                Console.Write($" | {col_values[c],5}");
            }
            Console.WriteLine();
            for (int c = 0; c < cols; c++)
            {
                Console.Write($"---------");
            }
            Console.WriteLine();

            for (int r = 0; r < rows; r++)
            {
                Console.Write($"{row_values[r],5} : ");
                Console.Write($"{values[r, 0],5}");
                for (int c = 1; c < cols; c++)
                {
                    Console.Write($" | {values[r, c],5}");
                }
                Console.WriteLine();
            }
        }
    }
}
