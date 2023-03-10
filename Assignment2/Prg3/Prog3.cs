using System;
namespace std
{
    class P3
    {
        public static bool[] Prime(int n)
        {
            bool[] prm = new bool[n + 1];
            for (int i = 0; i <= n; i++)
            {
                prm[i] = true;
            }
            prm[0] = false;
            prm[1] = false;
            double nn = Math.Sqrt(n);
            int m = (int)nn + 1;
            for(int i = 2; i <= m; i++)
            {
                for(int j = 2; j <= n / i; j++)
                {
                    prm[i * j] = false;
                }
            }
            return prm;
        }
        public static void Display(bool[] prm)
        {
            for(int n = 0; n < prm.Length; n++)
            {
                if(prm[n] == true)
                {
                    Console.WriteLine(n);
                }
            }
        }
        public static void Main(String[] args)
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
                return;
            }
            else
            {
                bool[] b = Prime(m);
                Display(b);
            }
        }
    }
}