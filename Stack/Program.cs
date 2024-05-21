using System.Security.AccessControl;

namespace Stack
{
    class Program
    {
        static void PushToStack ( Stack<string> s )
        {
            s.Push("1001");
            s.Push("1002");
            s.Push("1003");
            s.Push("Hello 1004");
        }
        static void Main(string[] args)
        {
            Stack<string> s1 = new Stack<string>(3);

            try 
            {
                PushToStack(s1);
            }
            catch (Stack<string>.Exception ex) 
            {
                Console.WriteLine($"ex={ex}, top={ex.Top}");
            }

            Console.WriteLine($"s1={s1}");

            while (! s1.IsEmpty() )
            { 
                string value = s1.Top();
                Console.WriteLine($"value={value}");
                s1.Pop();
            }
        }
    }

    // Stack:LIFO data structure
    class Stack<T>
    {
        public class Exception : System.Exception
        {
            public int Top { get; }
            public Exception(int _top)
            {
                Top = _top;
            }
        }

        private T[] arr;

        // top: index of the top of the stack
        int top = -1;
        public Stack(int capacity)
        {
            arr = new T[capacity];
        }

        public void Push(T value)
        {
            if (top == arr.Length - 1)
            {
                throw new Exception(top);
            }
            top++;
            arr[top] = value;
        }
        public T Pop()
        {
            top--;
            return arr[top + 1];
        }
        public T Top()
        {
            return arr[top];
        }
        public bool IsEmpty()
        {
            return top <= -1;
        }
    }
}
