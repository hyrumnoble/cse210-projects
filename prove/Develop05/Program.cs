using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Goal
{
    public string Name { get; }
    public int Value { get; protected set; }
    public bool IsComplete { get; protected set; }
    protected List<string> earnedBadges;

    public Goal(string name)
    {
        Name = name;
        Value = 0;
        IsComplete = false;
        earnedBadges = new List<string>();
    }

    public bool HasEarnedBadge(string badge)
    {
        return earnedBadges.Contains(badge);
    }

    public void AddBadge(string badge)
    {
        earnedBadges.Add(badge);
    }

    public abstract void MarkComplete();

    public abstract string GetGoalStatus();
}

public class SimpleGoal : Goal
{
    private int points;

    public SimpleGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override void MarkComplete()
    {
        if (!IsComplete)
        {
            Value += points;
            IsComplete = true;
            Console.WriteLine($"Goal '{Name}' marked as complete. You earned {points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already marked as complete.");
        }
    }

    public override string GetGoalStatus()
    {
        return IsComplete ? "[X]" : "[ ]";
    }
}

public class EternalGoal : Goal
{
    private int points;

    public EternalGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override void MarkComplete()
    {
        Value += points;
        Console.WriteLine($"Goal '{Name}' recorded. You earned {points} points.");
    }

    public override string GetGoalStatus()
    {
        return "[ ]";
    }
}

public class ChecklistGoal : Goal
{
    private int points;
    private int completionTarget;
    private int completionCount;

    public int CompletionCount { get { return completionCount; } }

    public ChecklistGoal(string name, int points, int completionTarget) : base(name)
    {
        this.points = points;
        this.completionTarget = completionTarget;
        this.completionCount = 0;
    }

    public override void MarkComplete()
    {
        completionCount++;
        Value += points;
        Console.WriteLine($"Goal '{Name}' recorded. You earned {points} points.");

        if (completionCount >= completionTarget && !IsComplete)
        {
            IsComplete = true;
            Value += 500; // Bonus points for completing the goal
            Console.WriteLine($"Congratulations! Goal '{Name}' completed {completionTarget} times. You earned a bonus of 500 points.");
        }
    }

    public override string GetGoalStatus()
    {
        return $"Completed {completionCount}/{completionTarget} times";
    }
}

public class Badge
{
    public string Name { get; }
    public string Description { get; }

    public Badge(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

public class EternalQuestProgram
{
    private List<Goal> goals;
    private int score;
    private List<Badge> earnedBadges;

    public EternalQuestProgram()
    {
        goals = new List<Goal>();
        score = 0;
        earnedBadges = new List<Badge>();
    }

    public void Run()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nEternal Quest Program Menu:");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Mark a goal as complete");
            Console.WriteLine("3. Display goals");
            Console.WriteLine("4. Display score");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    MarkGoalComplete();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("\nGoal Types:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("Enter the goal type: ");
        string input = Console.ReadLine();

        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine();

        switch (input)
        {
            case "1":
                Console.Write("Enter the points for completing the goal: ");
                int points = int.Parse(Console.ReadLine());
                goals.Add(new SimpleGoal(name, points));
                Console.WriteLine($"Simple goal '{name}' created successfully.");
                break;
            case "2":
                Console.Write("Enter the points for recording the goal: ");
                points = int.Parse(Console.ReadLine());
                goals.Add(new EternalGoal(name, points));
                Console.WriteLine($"Eternal goal '{name}' created successfully.");
                break;
            case "3":
                Console.Write("Enter the points for completing each instance of the goal: ");
                points = int.Parse(Console.ReadLine());
                Console.Write("Enter the completion target: ");
                int target = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, points, target));
                Console.WriteLine($"Checklist goal '{name}' created successfully.");
                break;
            default:
                Console.WriteLine("Invalid goal type. Goal creation failed.");
                break;
        }
    }

    private void MarkGoalComplete()
    {
        Console.WriteLine("\nSelect a goal to mark as complete:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} {goals[i].GetGoalStatus()}");
        }

        Console.Write("Enter the goal number: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            goals[index].MarkComplete();
            score += goals[index].Value;

            CheckForEarnedBadges();
        }
        else
        {
            Console.WriteLine("Invalid goal number. Marking goal as complete failed.");
        }
    }

    private void DisplayGoals()
    {
        Console.WriteLine("\nGoals:");

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals created.");
        }
        else
        {
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].GetGoalStatus()} {goals[i].Name}");
            }
        }
    }

    private void DisplayScore()
    {
        Console.WriteLine($"\nScore: {score}");

        if (earnedBadges.Count > 0)
        {
            Console.WriteLine("Earned Badges:");
            foreach (var badge in earnedBadges)
            {
                Console.WriteLine($"- {badge.Name}: {badge.Description}");
            }
        }
    }

    private bool HasEarnedBadge(string badgeName)
    {
        return earnedBadges.Any(badge => badge.Name == badgeName);
    }

    private void AddBadge(Badge badge)
    {
        earnedBadges.Add(badge);
        Console.WriteLine($"Congratulations! You earned the '{badge.Name}' badge: {badge.Description}");
    }

    private void CheckForEarnedBadges()
    {
        foreach (var goal in goals)
        {
            if (goal is SimpleGoal simpleGoal && simpleGoal.Value >= 1000 && !HasEarnedBadge("Marathoner"))
            {
                AddBadge(new Badge("Marathoner", "Completed a marathon goal"));
            }
            else if (goal is ChecklistGoal checklistGoal && checklistGoal.CompletionCount >= 10 && !HasEarnedBadge("Temple Master"))
            {
                AddBadge(new Badge("Temple Master", "Completed the temple goal 10 times"));
            }
            else if (goal is EternalGoal eternalGoal && eternalGoal.Value >= 5000 && !HasEarnedBadge("Eternal Scholar"))
            {
                AddBadge(new Badge("Eternal Scholar", "Accumulated 5000 points from eternal goals"));
            }
            else if (goal is SimpleGoal && goals.OfType<SimpleGoal>().All(g => g.IsComplete) && !HasEarnedBadge("Goal Crusher"))
            {
                AddBadge(new Badge("Goal Crusher", "Completed all simple goals"));
            }
        }
    }
}

public class Program
{
    public static void Main()
    {
        EternalQuestProgram program = new EternalQuestProgram();
        program.Run();
    }
}
