using System;
namespace std
{
    public class P4
    {
        public static int[,] Input(int m,int n)
        {
            int[,] result = new int[m, n];
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    result[i,j] = int.Parse(Console.ReadLine());
                }
            }
            return result;
        }
        public static bool T(int m, int n, int[,] sq)
        {
            if(sq == null)
            {
                return false;
            }
            if(m == 1)
            {
                return true;
            }
            if (n == 1)
            {
                return true;
            }
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(i + 1 < m && j + 1 < n && sq[i,j] != sq[i + 1, j + 1])
                    {
                        return false;
                    }
                }
            }
            return true;
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
            Console.WriteLine("Please input another integar:");
            int n = 0;
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error.");
                return;
            }
            Console.Write("Please input ");
            Console.Write(m * n);
            Console.WriteLine(" integar(s):");
            int[,] sq = new int[m, n];
            try
            {
                sq = Input(m, n);
            }
            catch
            {
                Console.WriteLine("Error.");
                return;
            }
            bool res = T(m, n, sq);
            if (res)
            {
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }
    }
}