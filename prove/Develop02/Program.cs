using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry {
    public string prompt;
    public string response;
    public DateTime date;

    public JournalEntry(string prompt, string response) {
        this.prompt = prompt;
        this.response = response;
        this.date = DateTime.Now;
    }

    public override string ToString() {
        return $"{date.ToShortDateString()}: {prompt}\n{response}\n";
    }
}

class Journal {
    private List<JournalEntry> entries;

    public Journal() {
        entries = new List<JournalEntry>();
    }

    public void AddEntry(JournalEntry entry) {
        entries.Add(entry);
    }

    public void DisplayEntries() {
        Console.WriteLine("Journal Entries:");
        Console.WriteLine("-----------------");
        foreach (JournalEntry entry in entries) {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename) {
    using (StreamWriter writer = new StreamWriter(filename)) {
        writer.WriteLine("Prompt,Response,Date"); // Add headers
        foreach (JournalEntry entry in entries) {
            writer.WriteLine($"\"{entry.prompt}\",\"{entry.response}\",\"{entry.date}\""); // Use quotes and escape quotes
        }
    }
    Console.WriteLine($"Journal saved to {filename}.");
}

public void LoadFromFile(string filename) {
    entries.Clear();
    using (StreamReader reader = new StreamReader(filename)) {
        string line = reader.ReadLine();
        if (line != null && line == "Prompt,Response,Date") { // Check headers
            while ((line = reader.ReadLine()) != null) {
                string[] parts = line.Split(',');
                if (parts.Length == 3) {
                    string prompt = parts[0].Trim('\"'); // Remove quotes
                    string response = parts[1].Trim('\"'); // Remove quotes
                    DateTime date;
                    if (DateTime.TryParse(parts[2].Trim('\"'), out date)) { // Remove quotes and parse date
                        JournalEntry entry = new JournalEntry(prompt, response);
                        entry.date = date;
                        entries.Add(entry);
                    }
                }
            }
        }
    }
    Console.WriteLine($"Journal loaded from {filename}.");
}
}
class PromptGenerator {
    private List<string> prompts;
    private Random random;

    public PromptGenerator() {
        prompts = new List<string>() {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What new thing did I learn today?",
            "What was the most challenging thing I faced today?",
            "What am I grateful for today?",
            "What would I like to improve on tomorrow?",
            "What made me laugh today?"
        };
        random = new Random();
    }

    public string GetPrompt() {
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}

class Program {
    static void Main(string[] args) {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        while (true) {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");

            string input = Console.ReadLine();
            int choice;
            if (int.TryParse(input, out choice)) {
                switch (choice) {
                    case 1:
                        string prompt = promptGenerator.GetPrompt();
                        Console.WriteLine(prompt);
                        string response = Console.ReadLine();
                        JournalEntry entry = new JournalEntry(prompt, response);
                        journal.AddEntry(entry);
                        break;
                    case 2:
                        journal.DisplayEntries();
                        break;
                    case 3:
                        Console.WriteLine("Enter filename:");
                        string filename = Console.ReadLine();
                        journal.SaveToFile(filename);
                    break;
                case 4:
                    Console.WriteLine("Enter filename:");
                    filename = Console.ReadLine();
                    journal.LoadFromFile(filename);
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        else {
            Console.WriteLine("Invalid input.");
        }
    }
}
}
