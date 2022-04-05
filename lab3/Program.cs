using System;

namespace lab3
{

    class Stack<T>
    {
        private T[] arr = new T[10];
        private int _last = -1;
        public void Push(T item)
        {
            arr[++_last] = item;
        }
        public T Pop()
        {
            return arr[_last--];
        }
    }

    class Student
    {
        private string _firstName;
        public int Exam { get; set; }

        public void Push(Stack<string> stack)
        {
            stack.Push(_firstName);
        }

        public T GetReward<T>(T reward)
        {
            if (Exam > 50)
            {
                return reward;
            }
            else
            {
                return default;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(5);
            stack.Push(9);

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());

            Student student = new Student() { Exam = 60 };
            string v = student.GetReward("Gratulacje");
            Console.WriteLine(v);
            ValueTuple<string, decimal, int> product = ValueTuple.Create("laptop", 1200m, 2);
            Console.WriteLine(product);
            (string, decimal, int) laptop = ("laptop", 1200m, 2);
            Console.WriteLine(product == laptop);
            (string name, decimal price, int quantity) tuple = ("laptop", 3000m, 4);
            laptop = tuple;
            Console.WriteLine(laptop);
        }
    }
}