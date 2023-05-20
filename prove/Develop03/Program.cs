using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world...");
        Console.Clear();
        scripture.Display();

        while (scripture.HasHiddenWords())
        {
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            scripture.HideRandomWord();
            Console.Clear();
            scripture.Display();
        }
    }
}

public class Scripture
{
    private List<Word> words;
    private string reference;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();

        string[] textWords = text.Split(' ');
        foreach (string word in textWords)
        {
            words.Add(new Word(word));
        }
    }

    public void Display()
    {
        Console.WriteLine(reference);
        foreach (Word word in words)
        {
            Console.Write(word.IsHidden ? " ____ " : " " + word.Text + " ");
        }
    }

    public bool HasHiddenWords()
    {
        foreach (Word word in words)
        {
            if (word.IsHidden == false)
                return true;
        }
        return false;
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        List<Word> visibleWords = GetVisibleWords();
        int index = random.Next(visibleWords.Count);
        visibleWords[index].Hide();
    }

    private List<Word> GetVisibleWords()
    {
        List<Word> visibleWords = new List<Word>();
        foreach (Word word in words)
        {
            if (!word.IsHidden)
                visibleWords.Add(word);
        }
        return visibleWords;
    }
}

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}
