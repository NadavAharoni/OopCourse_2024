using System.Net.Sockets;
using System.Numerics;

namespace Memory
{
    struct IntStruct
    {
        public int Value, Value1, Value2, Value3;
        public int Value4, Value5, Value6, Value7;
        public int Value8, Value9, Value10, Value11;
    }

    class IntClass
    {
        public int Value, Value1, Value2, Value3;
        public int Value4, Value5, Value6, Value7;
        public int Value8, Value9, Value10, Value11;
    }

    class MyClass
    {
        public int Value;

        public void MoveValue(MyClass other)
        {
            // if (this == other)
            //     return;
            Value += other.Value;
            other.Value = 0;
        }
    }

    class Shape 
    { 
        public virtual void Draw() { Console.WriteLine("!"); }
    }
    class Circle : Shape
    {
        public override void Draw() { Console.WriteLine($"Circle: {radius}");  }

        public double radius=5;
    }

    class Group : Shape
    {
        public MyArray<Shape> shapes = new MyArray<Shape>();
        public override void Draw()
        {
            for (int i = 0; i < shapes.array.Length; i++)
                shapes.array[i].Draw(); 
        }
    }

    class MyArray<T>
    {
        public T[] array = new T[7];
        public int _last = -1;
        public void Add(T item)
        { array[++_last] = item; }
    }

    class Page
    {
        private static Page ThePage = new Page();
        public static Page getPage1()
        {
            return ThePage;
        }
        public static Page getPage2()
        {
            return new Page();
        }

        private Page() { }
    }

    class MyException : Exception
    {
        public int x = 0;
    }

    class Program
    {
        public static void f(int x)
        {
            if (x % 2 == 0)
            {
                Console.WriteLine($"x={x}");
                f(x + 1);
            }
            else
            {
                MyException e= new MyException();
                e.x = x;
                throw e;
            }
                
        }
        static void Main(string[] args)
        {
            try
            {
                f(4);
            }
            catch (MyException e) 
            {
                Console.WriteLine($"e.x={e.x}");
            }
        }
        static void Main7(string[] args)
        {
            Shape[] shapes = new Shape[3];
            shapes[0] = new Shape();
            shapes[0].Draw();
            // Circle circle = (Circle)shapes[0];

            
            // MyArray<Circle> circleArray1 = new MyArray<Circle>();
            // MyArray<Shape> shapeArray = circleArray1;

            //MyArray<Circle> Array1 = new MyArray<Circle>();
            //MyArray<Shape> shapeArray = new MyArray<Shape>(); ;
            //Array1.Add(new Shape());
            //shapeArray.Add(new Shape());


            MyArray<Shape> shapeArray = new MyArray<Shape>(); ;
            shapeArray.Add(new Shape());
            Group group = new Group();
            group.shapes.Add(group);       // 4
            shapeArray.Add(group);         // 5
            group.Draw();                  // 6

        }

        static void MainMeasureMemory(string[] args)
        {
            GcTest<IntStruct>(1000); // 33448: 
            GcTest<IntStruct>(2000); // 64048: 32 bytes
            GcTest<IntClass>(1000); //  8048: 8 bytes for each 
            GcTest<IntClass>(2000); // 16048: 8 bytes for each 
        }
        static void MainMoveValue(string[] args)
        {
            MyClass a = new MyClass();
            a.Value = 2;
            // MyClass b = new MyClass();
            // b.Value = 3;
            a.MoveValue(a);
            Console.WriteLine($"a.Value={a.Value}");
            // Console.WriteLine($"a.Value={a.Value},b.Value={b.Value}");
        }

        delegate double op_func(double x, double y);
        static void Main4(string[] args)
        {
            op_func op1, op2;
            op1 = (x, y) => x + y;
            op2 = (x, y) => x - y;

            Console.WriteLine(op1(2, 3));
            Console.WriteLine(op2(2, 3));
        }
        static void Main3(string[] args)
        {
            int[] b = new int[10];
            b[0] = 3;
            MyMath.increment(b, 0);
            Console.WriteLine($"b[0]={b[0]}");

        }

        static void Main8(string[] args)
        {
            int i = 12;
            ref int j = ref i;
            j++;
            i++;
            Console.WriteLine($"1: i={i}");
            int k = j;
            j++;
            Console.WriteLine($"k={k}, i={i}");
            k = 20;
            j = ref k;
            Console.WriteLine($"after j=k: k={k}, i={i}, j={j}");
            j++;
            k++;
            Console.WriteLine($"after j++: k={k}, i={i}, j={j}");
        }
        static void Main1(string[] args)
        {
            Stack<string> stk1 = new Stack<string>(3);
            Console.WriteLine(stk1);
            Stack<string> stk2 = new Stack<string>(6);
            Console.WriteLine(stk2);

            Console.WriteLine("moving stk2 -> stk1");
            stk1.MoveItemsFrom(stk2);
            Console.WriteLine(stk1);
            Console.WriteLine(stk2);

            stk2.MoveItemsFrom(stk2);
            Console.WriteLine($"stk1={stk1}");


            Stack<string>.MoveItems(stk1, stk2);

            Stack<int> si = new Stack<int>(7);
            // the following line does not compile
            // si = stk1;

            int k = 8;
            MyMath.increment(ref k);
            Console.WriteLine(k);

            int[] b = new int[10];
            b[0] = 3;
            MyMath.increment(b,0);
            Console.WriteLine($"b[0]={b[0]}");

            IntStruct s1 = new IntStruct();
            s1.Value = 3;
            MyMath.increment(s1);
            Console.WriteLine($"s1.Value={s1.Value}");

            IntClass c1 = new IntClass();
            c1.Value = 3;
            MyMath.increment(c1);
            Console.WriteLine($"c1.Value={c1.Value}");

            int[] a = { 1, 2, 3 };
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            ExpandArray(ref a);

            Console.WriteLine("after ExpandArray");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }

            a = new int[10];

            /*int[] aa  = */ GcTest<IntStruct>(1000);
            //for( int i=0; i<1; i+=50)
            //{
            //    Console.WriteLine(aa[i]);
            //}
        }

        static void ExpandArray(ref int[] array)
        {
            array[0] = 100;

            int[] oldArray = array;

            array = new int[oldArray.Length * 2];
            for (int i = 0; i < oldArray.Length; i++)
            {
                array[i] = oldArray[i];
                array[i + oldArray.Length] = oldArray[i];
            }

            Console.WriteLine("in ExpandArray: oldArray:");
            for (int i = 0; i < oldArray.Length; i++)
            {
                Console.WriteLine(oldArray[i]);
            }
        }

        static void /*int[]*/ GcTest<T>(int size)
        {
            Random random = new Random();
            // long mem_before = GC.GetTotalMemory(false);
            long mem_before = GC.GetAllocatedBytesForCurrentThread();
            Console.WriteLine("Total Memory: {0}", mem_before);
            T[] a = new T[size];
            long mem_after = GC.GetAllocatedBytesForCurrentThread();
            Console.WriteLine("Total Memory: {0}", mem_after);
            Console.WriteLine("Consumed: {0}", mem_after - mem_before);
        }
    }
    class MyMath
    {
        public static void increment(ref int i)
        {
            i++;
        }
        public static void increment(int[] a, int index)
        {
            a[index]++;
        }
        public static void increment(IntStruct s)
        {
            s.Value++;
        }
        public static void increment(IntClass c)
        {
            c.Value++;
        }
    }

    class Stack<T>
    {
        private T[] _arr;

        // top will always be the index of the top element 
        // top will be -1 if the stack is empty 
        private int _top = -1;

        public Stack(int capacity)
        {
            _arr = new T[capacity];
        }

        public void MoveItemsFrom(Stack<T> other)
        {
            if (this == other)
            {
                return;
            }
            _arr = new T[other._arr.Length];
            _top = other._top;
            for (int i = 0; i < other._arr.Length; i++)
            {
                _arr[i] = other._arr[i];
            }
            other._arr = new T[0];
            other._top = -1;
        }

        public override string ToString()
        {
            if (_arr == null || _arr.Length == 0)
                return "<empty stack>";
            string ret = "";
            for (int i = 0; i < _arr.Length; i++)
            {
                ret += _arr[i] == null ? "null" : _arr[i].ToString();
                ret += ", ";

            }
            return ret;
        }

        public static void MoveItems(Stack<T> stack1, Stack<T> stack2)
        {
            stack1.MoveItemsFrom(stack2);
        }
    }

}
