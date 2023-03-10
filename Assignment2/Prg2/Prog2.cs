using System;
namespace std
{
    public class P2
    {
		public static void Input(int[] r)
		{
			for (int i = 0; i < r.Length; i++)
			{
				r[i] = int.Parse(Console.ReadLine());
			}
		}
		public static int Max(int[] r)
		{
			int mx = r[0];
			for (int i = 1; i < r.Length; i++)
			{
				if (r[i] > mx)
				{
					mx = r[i];
				}
			}
			return mx;
		}
		public static int Min(int[] r)
		{
			int mn = r[0];
			for (int i = 1; i < r.Length; i++)
			{
				if (r[i] < mn)
				{
					mn = r[i];
				}
			}
			return mn;
		}
		public static int Sum(int[] r)
		{
			int sm = 0;
			for (int i = 0; i < r.Length; i++)
			{
				sm += r[i];
			}
			return sm;
		}
		public static int Ave(int[] r)
		{
			int sm = Sum(r);
			int av = sm / r.Length;
			return av;
		}
		public static void Main(String[] args)
		{
			int sz;
			Console.Write("Please set the size:");
			try
			{
				sz = int.Parse(Console.ReadLine());
			}
			catch
			{
				Console.WriteLine("Error.");
				return;
			}
			int[]
			r = new int[sz];
			Console.Write("Please input ");
			Console.Write(sz);
			Console.WriteLine(" integar(s):");
			try
			{
				Input(r);
			}
			catch
			{
				Console.WriteLine("Error.");
				return;
			}
			Console.Write("The maximum is ");
			Console.Write(Max(r));
			Console.WriteLine(".");
			Console.Write("The minimum is ");
			Console.Write(Min(r));
			Console.WriteLine(".");
			Console.Write("The average is ");
			Console.Write(Ave(r));
			Console.WriteLine(".");
			Console.Write("The sum is ");
			Console.Write(Sum(r));
			Console.WriteLine(".");
		}
	}
}