using System;
namespace std
{
    public class P1
    {
        public static int[] Prime(int m)
        {
            int n = 1;
            int[] r = new int[m];
            r[0] = 0;
            for (int i = 2; i <= m;)
            {
                if (m % i == 0)
                {
                    m /= i;
                    r[n] = i;
                    n++;
                    r[0]++;
                }
                else
                {
                    i++;
                }
            }
            return r;
        }
        public static void Display(int[] r)
        {
            for (int i = 1; i < r[0] + 1; i++)
            {
                Console.WriteLine(r[i]);
            }
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Please input an integar:");
            int m = 0;
            try
            {
                m = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error.");
                return;
            }
            if (m <= 1)
            {
                Console.WriteLine("Error.");
            }
            else
            {
                int[] r = Prime(m);
                Display(r);
            }
        }
    }
}