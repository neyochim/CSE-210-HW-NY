using System;
using System.Collections.Generic;
using System.IO;

public class Prompt
{
    // List of prompts to be used in the program
    private readonly List<string> _promptsList;

    // Random object to select a random prompt
    private readonly Random _randomSelector;

    // Constructor to initialize the fields
    public Prompt()
    {
        _promptsList = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
        _randomSelector = new Random();
    }

    // Method to select a random prompt
    public string SelectRandomPrompt()
    {
        int promptIndex = _randomSelector.Next(0, _promptsList.Count);
        return _promptsList[promptIndex];
    }
}

public class JournalEntry
{
    // Properties to store the date and time, prompt, and entry
    public string EntryDateTime { get; private set; }
    public string EntryPrompt { get; private set; }
    public string Entry { get; private set; }

    // Method to get the random prompt and set it to the entry
    public void SetPrompt(Prompt prompt)
    {
        EntryPrompt = prompt.SelectRandomPrompt();
    }

    // Method to get the date and time
    public void SetDateTime()
    {
        Console.WriteLine("Enter the date and time of the entry: ");
        EntryDateTime = Console.ReadLine();
    }

    // Method to get the entry from the user
    public void SetEntry()
    {
        Console.WriteLine("Today's Prompt:");
        Console.WriteLine(EntryPrompt);
        Console.WriteLine("Enter your entry:");
        Console.WriteLine("");
        Entry = Console.ReadLine();
    }

    // Method to display the entry
    public void DisplayEntry()
    {
        Console.WriteLine("");
        Console.WriteLine("==================================================================================");
        Console.WriteLine(EntryDateTime);
        Console.WriteLine("");
        Console.WriteLine($"Prompt: {EntryPrompt}");
        Console.WriteLine("");
        Console.WriteLine($"Entry: {Entry}");
        Console.WriteLine("==================================================================================");
    }

    // Method to convert the entry to a string for saving to a file
    public override string ToString()
    {
        return $"{EntryDateTime}|{EntryPrompt}|{Entry}";
    }

    // Method to create an entry from a string (for loading from a file)
    public static JournalEntry FromString(string entryString)
    {
        var parts = entryString.Split('|');
        return new JournalEntry
        {
            EntryDateTime = parts[0],
            EntryPrompt = parts[1],
            Entry = parts[2]
        };
    }
}

public class Journal
{
    // List to hold the journal entries
    private readonly List<JournalEntry> _journalEntries;

    // Constructor to initialize the list
    public Journal()
    {
        _journalEntries = new List<JournalEntry>();
    }

    // Method to store the journal entry
    public void StoreJournalEntry(JournalEntry entry)
    {
        _journalEntries.Add(entry);
    }

    // Method to display journal entries
    public void DisplayJournalEntries()
    {
        Console.WriteLine("");
        Console.WriteLine("Journal Entries:");
        Console.WriteLine("");
        foreach (JournalEntry entry in _journalEntries)
        {
            entry.DisplayEntry();
        }
    }

    // Method to save the journal to a file
    public void SaveJournalToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (JournalEntry entry in _journalEntries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
    }

    // Method to load the journal from a file
    public void LoadJournalFromFile(string filename)
    {
        _journalEntries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                _journalEntries.Add(JournalEntry.FromString(line));
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Journal journal = new Journal();
        Prompt prompt = new Prompt();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    JournalEntry entry = new JournalEntry();
                    entry.SetPrompt(prompt);
                    entry.SetDateTime();
                    entry.SetEntry();
                    journal.StoreJournalEntry(entry);
                    break;
                case "2":
                    journal.DisplayJournalEntries();
                    break;
                case "3":
                    Console.Write("Enter the filename to save the journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournalToFile(saveFilename);
                    break;
                case "4":
                    Console.Write("Enter the filename to load the journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournalFromFile(loadFilename);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}