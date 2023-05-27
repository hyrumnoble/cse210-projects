using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        Scripture scripture = new Scripture("Moroni 10:3-5", @"Behold, I would exhort you that when ye shall read these things, if it be wisdom in God that ye should read them, that ye would remember how merciful the Lord hath been unto the children of men, from the creation of Adam even down until the time that ye shall receive these things, and ponder it in your hearts.

And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost.

And by the power of the Holy Ghost ye may know the truth of all things.");
        
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

        string[] verseTexts = Regex.Split(text, @"\d+\s");
        int verseNumber = 1;

        foreach (string verseText in verseTexts)
        {
            string[] textWords = verseText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in textWords)
            {
                if (!string.IsNullOrWhiteSpace(word))
                    words.Add(new Word(word, verseNumber));
            }

            verseNumber++;
        }
    }

    public void Display()
    {
        Console.WriteLine(reference);
        int currentVerseNumber = -1;

        foreach (Word word in words)
        {
            if (currentVerseNumber != word.VerseNumber)
            {
                Console.WriteLine("\nVerse " + word.VerseNumber);
                currentVerseNumber = word.VerseNumber;
            }

            Console.Write(word.IsHidden ? " ____ " : " " + word.Text + " ");
        }
    }

    public bool HasHiddenWords()
    {
        foreach (Word word in words)
        {
            if (!word.IsHidden)
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
    public int VerseNumber { get; private set; }

    public Word(string text, int verseNumber)
    {
        Text = text;
        IsHidden = false;
        VerseNumber = verseNumber;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}
