using System;

class Program
{
    static void Main(string[] args)
    {
        // Generate a random number between 1 and 100
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        // Ask the user to guess the number
        Console.Write("Guess the magic number between 1 and 100: ");
        int guess = int.Parse(Console.ReadLine());

        // Keep looping until the user guesses correctly
        while (guess != magicNumber)
        {
            // Provide a hint to the user about whether their guess was too high or too low
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else
            {
                Console.WriteLine("Lower");
            }

            // Ask the user to guess again
            Console.Write("Guess again: ");
            guess = int.Parse(Console.ReadLine());
        }

        // The user guessed correctly!
        Console.WriteLine("You guessed it! The magic number was " + magicNumber);
    }
}
