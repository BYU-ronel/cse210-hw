using System;

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

        public Scripture(ScriptureReference reference, string text)
        {
            Reference = reference;
            Words = text.Split(' ').Select(w => new Word(w)).ToArray();
        }

        public void HideRandomWords()
        {
            var random = new Random();
            var wordIndex = random.Next(Words.Length);
            Words[wordIndex].IsHidden = true;
        }

        public override string ToString()
        {
            var words = Words.Select(w => w.IsHidden? "_____" : w.Text);
            return $"{Reference}: {string.Join(" ", words)}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var scripture = new Scripture(
                new ScriptureReference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."
            );

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.ToString());
                Console.Write("Press enter to hide words or type 'quit' to exit: ");
                var input = Console.ReadLine();
                if (input.ToLower() == "quit")
                {
                    break;
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