using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GenericApplication
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> next;
        public GenericList()
        {
            next = head = null;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (next == null)
            {
                head = next = n;
            }
            else
            {
                next.Next = n;
                next = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            for (Node<T> n = head; n != null; n = n.Next)
            {
                action(n.Data);
            }
        }
        public double Sum(Func<T, double> selector)
        {
            double sum = 0;
            for (Node<T> n = head; n != null; n = n.Next)
            {
                sum += selector(n.Data);
            }
            return sum;
        }
    }
    class Lklist
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            GenericList<int> lsti = new GenericList<int>();
            for (int x = 0; x < 10; x++)
            {
                lsti.Add(random.Next(1000));
            }
            lsti.ForEach(n => Console.Write(n + "\t"));
            double min = double.MaxValue;
            double max = double.MinValue;
            double sum = 0;
            lsti.ForEach(n => {
                min = (n < min) ? n : min;
                max = (n > max) ? n : max;
                sum += n;
            });
            Console.WriteLine();
            Console.WriteLine($"min={min},max={max},sum={sum}");
            sum = lsti.Sum(n => n);
            Console.WriteLine("sum=" + sum);
            GenericList<String> lsts = new GenericList<String>();
            lsts.Add("a");
            lsts.Add("123");
            int len = (int)lsts.Sum(s => s.Length);
            Console.WriteLine("len=" + len);
        }
    }
}