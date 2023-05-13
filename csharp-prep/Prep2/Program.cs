using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter your grade percentage: ");
        float gradePercentage = float.Parse(Console.ReadLine());

        string letter;

        if (gradePercentage >= 90)
        {
            letter = "A";
            Console.WriteLine("Your grade is an A.");
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
            Console.WriteLine("Your grade is a B.");
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
            Console.WriteLine("Your grade is a C.");
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
            Console.WriteLine("Your grade is a D.");
        }
        else
        {
            letter = "F";
            Console.WriteLine("Your grade is an F.");
        }

        if (letter == "A" || letter == "B" || letter == "C")
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Don't worry, you can always try again next time.");
        }

        Console.WriteLine("Your letter grade is: " + letter);
    }
}
