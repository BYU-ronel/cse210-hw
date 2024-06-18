using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JournalProgram
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Mood { get; set; }
        public string Weather { get; set; }
    }

    public class Journal
    {
        public List<Entry> Entries { get; set; }

        public Journal()
        {
            Entries = new List<Entry>();
        }

        public void AddEntry(Entry entry)
        {
            Entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (var entry in Entries)
            {
                Console.WriteLine($"Date: {entry.Date}, Prompt: {entry.Prompt}, Response: {entry.Response}");
            }
        }

        public void SaveToFile(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.WriteLine("Date,Prompt,Response,Mood,Weather");
                foreach (var entry in Entries)
                {
                    writer.WriteLine($"{entry.Date.ToString("yyyy-MM-dd")},{entry.Prompt},{entry.Response.Replace(",", ";")},{entry.Mood},{entry.Weather}");
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            Entries.Clear();
            using (var reader = new StreamReader(filename))
            {
                reader.ReadLine(); // skip header
                string line;
                while ((line = reader.ReadLine())!= null)
                {
                    var columns = line.Split(',');
                    var entry = new Entry
                    {
                        Date = DateTime.Parse(columns[0]),
                        Prompt = columns[1],
                        Response = columns[2].Replace(";", ","),
                        Mood = columns[3],
                        Weather = columns[4]
                    };
                    Entries.Add(entry);
                }
            }
        }
    }

    public class PromptGenerator
    {
        private List<string> _prompts;

        public PromptGenerator()
        {
            _prompts = new List<string>
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?",
                "What was the most challenging part of my day?",
                "How did I grow today?",
                "What am I grateful for today?",
                "What did I learn today?",
                "What was the kindest thing I did today?"
            };
        }

        public string GeneratePrompt()
        {
            var random = new Random();
            return _prompts[random.Next(_prompts.Count)];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            var promptGenerator = new PromptGenerator();

            while (true)
            {
                Console.WriteLine("\nJournal Program");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Quit");

                Console.Write("What Would you like to do? ");
                var choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var prompt = promptGenerator.GeneratePrompt();
                        Console.Write($"Response to '{prompt}': ");
                        var response = Console.ReadLine();
                        var entry = new Entry
                        {
                            Prompt = prompt,
                            Response = response,
                            Date = DateTime.Now
                        };
                        journal.AddEntry(entry);
                        break;
                    case 2:
                        journal.DisplayEntries();
                        break;
                    case 3:
                        Console.Write("Enter the filename to save: ");
                        var filename = Console.ReadLine();
                        journal.SaveToFile(filename);
                        break;
                    case 4:
                        Console.Write("Enter the filename: ");
                        filename = Console.ReadLine();
                        journal.LoadFromFile(filename);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}