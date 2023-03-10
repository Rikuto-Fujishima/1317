using System;
namespace std
{
    public class Prog1
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Input a number:");
            double n1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input another number:");
            double n2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Input an operator:");
            String op = Console.ReadLine();
            if (op == "+")
            {
                Console.WriteLine(n1 + n2);
            }
            else if(op == "-")
            {
                Console.WriteLine(n1 - n2);
            }
            else if (op == "*")
            {
                Console.WriteLine(n1 * n2);
            }
            else if (op == "/")
            {
                Console.WriteLine(n1 / n2);
            }
            else
            {
                Console.WriteLine("Error.");
            }
        }
    }
}