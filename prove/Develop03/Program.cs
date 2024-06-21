using System;
using System.Collections.Generic;

namespace ScriptureHider
{
    public class Word
    {
        public string Text { get; set; }
        public bool IsHidden { get; set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }
    }

    public class ScriptureReference
    {
        public string Book { get; set; }
        public int StartVerse { get; set; }
        public int? EndVerse { get; set; }

        public ScriptureReference(string book, int startVerse)
        {
            Book = book;
            StartVerse = startVerse;
        }

        public ScriptureReference(string book, int startVerse, int endVerse)
        {
            Book = book;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            if (EndVerse.HasValue)
            {
                return $"{Book} {StartVerse}-{EndVerse.Value}";
            }
            else
            {
                return $"{Book} {StartVerse}";
            }
        }
    }

    public class Scripture
    {
        public ScriptureReference Reference { get; set; }
        public Word[] Words { get; set; }
        public string Explanation { get; set; }
        public string Context { get; set; }

        public Scripture(ScriptureReference reference, string text, string explanation, string context)
        {
            Reference = reference;
            Words = text.Split(' ').Select(w => new Word(w)).ToArray();
            Explanation = explanation;
            Context = context;
        }

        public void HideRandomWords()
        {
            var random = new Random();
            var wordIndex = random.Next(Words.Length);
            Words[wordIndex].IsHidden = true;
        }

        public override string ToString()
        {
            var words = Words.Select(w => w.IsHidden ? "_____" : w.Text);
            return $"{Reference}: {string.Join(" ", words)}";
        }
    }

    class Program
    {
        static List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture(
                new ScriptureReference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.",
                "This verse is a central message of Christianity, emphasizing God's love for humanity.",
                "John 3:1-21"
            ),
            new Scripture(
                new ScriptureReference("Philippians", 4, 13),
                "I can do all this through him who gives me strength.",
                "This verse is a declaration of faith and trust in God's power.",
                "Philippians 4:10-20"
            ),
            new Scripture(
                new ScriptureReference("Psalm", 23, 4),
                "Even though I walk through the darkest valley, I will fear no evil, for you are with me; your rod and your staff comfort me.",
                "This verse is a expression of trust in God's presence and guidance.",
                "Psalm 23:1-6"
            ),
            // Add more scriptures to the library here
        };

        static void Main(string[] args)
        {
            var random = new Random();
            while (true)
            {
                var scriptureIndex = random.Next(scriptureLibrary.Count);
                var scripture = scriptureLibrary[scriptureIndex];

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(scripture.ToString());
                    Console.WriteLine($"Explanation: {scripture.Explanation}");
                    Console.WriteLine($"Context: {scripture.Context}");
                    Console.Write("Press enter to hide words or type 'quit' to exit: ");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "quit")
                    {
                        return;
                    }
                    scripture.HideRandomWords();
                    if (scripture.Words.All(w => w.IsHidden))
                    {
                        break;
                    }
                }
            }
        }
    }
} 