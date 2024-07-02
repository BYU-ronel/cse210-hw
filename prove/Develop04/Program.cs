using System;
using System.Collections.Generic;

public abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;

    public Activity()
    {
        // Constructor
    }

    public void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting activity: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Duration: {_duration} seconds");
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine($"Ending activity: {_name}");
    }

    public void ShowSpinner(int seconds)
    {
        // Implementation for a spinner
    }

    public void ShowCountDown(int seconds)
    {
        // Implementation for a countdown
    }

    public abstract void Run();
}

public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing";
        _description = "Deep breathing exercise";
        _duration = 60;
    }

    public override void Run()
    {
        Console.WriteLine("Inhale deeply through your nose...");
        Console.WriteLine("Hold your breath for a few seconds...");
        Console.WriteLine("Exhale slowly through your mouth...");
    }
}

public class ListingActivity : Activity
{
    private int _count;
    private List<string> _prompts;

    public ListingActivity()
    {
        _name = "Listing";
        _description = "List your thoughts or feelings";
        _duration = 120;
        _count = 0;
        _prompts = new List<string> {
            "What are you grateful for?",
            "What are you proud of?",
            "What are you looking forward to?",
        };
    }

    public void GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        Console.WriteLine(_prompts[index]);
    }

    public List<string> GetListFromUser()
    {
        List<string> list = new List<string>();
        Console.WriteLine("Enter items for the list (type 'done' to finish):");
        string input;
        do
        {
            input = Console.ReadLine();
            if (input != "done")
            {
                list.Add(input);
            }
        } while (input != "done");
        return list;
    }

    public override void Run()
    {
        GetRandomPrompt();
        List<string> userResponses = GetListFromUser();
        Console.WriteLine("Your list:");
        foreach (string response in userResponses)
        {
            Console.WriteLine(response);
        }
    }
}

public class ReflectingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;

    public ReflectingActivity()
    {
        _name = "Reflecting";
        _description = "Reflect on your day and thoughts";
        _duration = 180;
        _prompts = new List<string> {
            "What was the highlight of your day?",
            "What are you learning?",
            "What are you grateful for?",
        };
        _questions = new List<string> {
            "What are you thinking about?",
            "What are you feeling?",
            "What are you working on?",
        };
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }

    public string GetRandomQuestion()
    {
        Random random = new Random();
        int index = random.Next(_questions.Count);
        return _questions[index];
    }

    public void DisplayPrompt()
    {
        Console.WriteLine(GetRandomPrompt());
    }

    public void DisplayQuestions()
    {
        Console.WriteLine("Think about these questions:");
        foreach (string question in _questions)
        {
            Console.WriteLine(question);
        }
    }

    public override void Run()
    {
        DisplayPrompt();
        DisplayQuestions();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Activity activity;

        // Choose an activity to run
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing");
        Console.WriteLine("2. Listing");
        Console.WriteLine("3. Reflecting");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                activity = new BreathingActivity();
                break;
            case 2:
                activity = new ListingActivity();
                break;
            case 3:
                activity = new ReflectingActivity();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        activity.DisplayStartingMessage();
        activity.Run();
        activity.DisplayEndingMessage();
    }
}
