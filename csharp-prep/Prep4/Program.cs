using System;
using System.Collections.Generic;

namespace NumberListProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            Console.WriteLine("Enter a list of numbers, type 0 when finished.");

            while (true)
            {
                Console.Write("Enter number: ");
                int num = int.Parse(Console.ReadLine());

                if (num == 0)
                {
                    break;
                }

                numbers.Add(num);
            }

            int sum = 0;
            int max = numbers[0];

            foreach (int num in numbers)
            {
                sum += num;

                if (num > max)
                {
                    max = num;
                }
            }

            double avg = (double)sum / numbers.Count;

            Console.WriteLine("The sum is: " + sum);
            Console.WriteLine("The average is: " + avg);
            Console.WriteLine("The largest number is: " + max);
        }
    }
}
