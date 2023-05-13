using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please enter your first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Please enter your last name: ");
        string lastName = Console.ReadLine();

        Console.WriteLine("Your name is {1}, {0}, {1}", firstName, lastName);
    }
}
