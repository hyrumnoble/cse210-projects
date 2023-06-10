using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected int duration;

    public MindfulnessActivity(int duration)
    {
        this.duration = duration;
        ActivityLog.IncrementActivityCount(GetType().Name);
    }

    public abstract void Start();

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/");
            Thread.Sleep(250);
            Console.Write("\b-");
            Thread.Sleep(250);
            Console.Write("\b\\");
            Thread.Sleep(250);
            Console.Write("\b|");
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }
    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
        }
    }
}


public class MeditationActivity : MindfulnessActivity
{
    private static int meditationCount = 0;

    public MeditationActivity(int duration) : base(duration)
    {
        meditationCount++;
    }

    public override void Start()
    {
        Console.WriteLine("Meditation Activity");
        Console.WriteLine("This activity will help you relax and find inner peace through meditation.");
        Console.WriteLine("Duration: {0} seconds", duration);
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        Console.WriteLine("Start focusing on your breath, read the quote and meditate on the quote.");

        string[] motivationalQuotes = {
        "Focus on the present moment and let go of worries.",
        "Inhale peace, exhale stress.",
        "You are stronger than you think.",
        "Calm mind, peaceful soul.",
        "Breathe in positivity, breathe out negativity.",
        "Every breath is an opportunity to start anew."
    };

    // Select a random quote from the array
    Random random = new Random();
    int index = random.Next(motivationalQuotes.Length);
    string quote = motivationalQuotes[index];

    Console.WriteLine("Motivational Quote: \"{0}\"", quote);

        Countdown(duration); // Use the Countdown method instead of custom loop

        Console.WriteLine("Good job! You have completed the Meditation Activity for {0} seconds.", duration);
        Thread.Sleep(3000);
    }

    public static int GetMeditationCount()
    {
        return meditationCount;
    }
}


public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Breathing Activity");
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
        Console.WriteLine("Duration: {0} seconds", duration);
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        for (int i = 0; i < duration; i += 2)
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(2);
            Console.WriteLine("Breathe out...");
            ShowSpinner(2);
        }

        Console.WriteLine("Good job! You have completed the Breathing Activity for {0} seconds.", duration);
        Thread.Sleep(3000);
    }
}


public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Reflection Activity");
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Console.WriteLine("Duration: {0} seconds", duration);
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        Random random = new Random();

        for (int i = 0; i < duration; i += 5)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            ShowSpinner(2);

            foreach (string question in questions)
            {
                Console.WriteLine(question);
                ShowSpinner(3);
            }
        }

        Console.WriteLine("Good job! You have completed the Reflection Activity for {0} seconds.", duration);
        Thread.Sleep(3000);
    }
}


public class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Listing Activity");
        Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
        Console.WriteLine("Duration: {0} seconds", duration);
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);

        Random random = new Random();

        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        ShowSpinner(2);

        Thread.Sleep(3000);

        Console.WriteLine("Start listing items:");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
                itemCount++;
        }

        Console.WriteLine("You listed {0} items.", itemCount);
        Console.WriteLine("Good job! You have completed the Listing Activity for {0} seconds.", duration);
        Thread.Sleep(3000);
    }
}

public class ActivityLog
{
    private static Dictionary<string, int> activityCounts = new Dictionary<string, int>();

    public static void IncrementActivityCount(string activityName)
    {
        if (activityCounts.ContainsKey(activityName))
        {
            activityCounts[activityName]++;
        }
        else
        {
            activityCounts[activityName] = 1;
        }
    }

    public static void SaveLogToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var activityCount in activityCounts)
            {
                writer.WriteLine("{0}: {1}", activityCount.Key, activityCount.Value);
            }
        }
    }

    public static void LoadLogFromFile(string filename)
    {
        activityCounts.Clear();

        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string activityName = parts[0].Trim();
                    int count;
                    if (int.TryParse(parts[1], out count))
                    {
                        activityCounts[activityName] = count;
                    }
                }
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Meditation Activity");
            Console.WriteLine("5. Save Log");
            Console.WriteLine("6. Load Log");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    // Create Breathing Activity
                    Console.Write("Enter duration for Breathing Activity (in seconds): ");
                    int breathingDuration = int.Parse(Console.ReadLine());
                    BreathingActivity breathingActivity = new BreathingActivity(breathingDuration);
                    breathingActivity.Start();
                    break;

                case "2":
                    // Create Reflection Activity
                    Console.Write("Enter duration for Reflection Activity (in seconds): ");
                    int reflectionDuration = int.Parse(Console.ReadLine());
                    ReflectionActivity reflectionActivity = new ReflectionActivity(reflectionDuration);
                    reflectionActivity.Start();
                    break;

                case "3":
                    // Create Listing Activity
                    Console.Write("Enter duration for Listing Activity (in seconds): ");
                    int listingDuration = int.Parse(Console.ReadLine());
                    ListingActivity listingActivity = new ListingActivity(listingDuration);
                    listingActivity.Start();
                    break;

                case "4":
                    Console.Write("Enter duration for Meditation Activity (in seconds): ");
                    int meditationDuration = int.Parse(Console.ReadLine());
                    MeditationActivity meditationActivity = new MeditationActivity(meditationDuration);
                    meditationActivity.Start();
                    break;

                case "5":
                    Console.Write("Enter the filename to save the log: ");
                    string saveFilename = Console.ReadLine();
                    ActivityLog.SaveLogToFile(saveFilename);
                    Console.WriteLine("Log saved successfully.");
                    break;

                case "6":
                    Console.Write("Enter the filename to load the log: ");
                    string loadFilename = Console.ReadLine();
                    ActivityLog.LoadLogFromFile(loadFilename);
                    Console.WriteLine("Log loaded successfully.");
                    break;

                case "7":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}

