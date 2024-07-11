using System;
using System.Collections.Generic;

namespace GoalTracker
{
    public abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public bool IsComplete { get; set; }

        public Goal(string name, int points)
        {
            Name = name;
            Points = points;
            IsComplete = false;
        }

        public abstract void RecordEvent();
    }

    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points) { }

        public override void RecordEvent()
        {
            IsComplete = true;
            Console.WriteLine($"You have completed the goal '{Name}' and earned {Points} points!");
        }
    }

    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points) { }

        public override void RecordEvent()
        {
            Console.WriteLine($"You have recorded the goal '{Name}' and earned {Points} points!");
        }
    }

    public class ChecklistGoal : Goal
    {
        public int TargetCount { get; set; }
        public int CurrentCount { get; set; }

        public ChecklistGoal(string name, int points, int targetCount) : base(name, points)
        {
            TargetCount = targetCount;
            CurrentCount = 0;
        }

        public override void RecordEvent()
        {
            CurrentCount++;
            if (CurrentCount < TargetCount)
            {
                Console.WriteLine($"You have recorded the goal '{Name}' and earned {Points} points! ({CurrentCount}/{TargetCount})");
            }
            else
            {
                IsComplete = true;
                Console.WriteLine($"You have completed the goal '{Name}' and earned {Points * TargetCount} points!");
            }
        }
    }

    public class Program
    {
        private List<Goal> Goals { get; set; }
        private int Score { get; set; }

        public Program()
        {
            Goals = new List<Goal>();
            Score = 0;
        }

        public void CreateGoal()
        {
            Console.Write("Enter goal name: ");
            string name = Console.ReadLine();
            Console.Write("Enter goal points: ");
            int points = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose goal type:");
            Console.WriteLine("1. Simple goal");
            Console.WriteLine("2. Eternal goal");
            Console.WriteLine("3. Checklist goal");
            int choice = int.Parse(Console.ReadLine());

            Goal goal;
            switch (choice)
            {
                case 1:
                    goal = new SimpleGoal(name, points);
                    break;
                case 2:
                    goal = new EternalGoal(name, points);
                    break;
                case 3:
                    Console.Write("Enter target count: ");
                    int targetCount = int.Parse(Console.ReadLine());
                    goal = new ChecklistGoal(name, points, targetCount);
                    break;
                default:
                    throw new Exception("Invalid choice");
            }

            Goals.Add(goal);
            Console.WriteLine($"Goal '{name}' created!");
        }

        public void RecordEvent()
        {
            Console.WriteLine("Choose a goal to record:");
            for (int i = 0; i < Goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Goals[i].Name} {(Goals[i].IsComplete? "[X]" : "[ ]")}");
            }
            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < Goals.Count)
            {
                Goals[choice].RecordEvent();
                Score += Goals[choice].Points;
                Console.WriteLine($"Your current score is {Score} points.");
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }

        public void DisplayGoals()
        {
            Console.WriteLine("Your goals:");
            for (int i = 0; i < Goals.Count; i++)
            {
                Goal goal = Goals[i];
                string status = goal.IsComplete? "[X]" : "[ ]";
                if (goal is ChecklistGoal checklistGoal)
                {
                    status += $" ({checklistGoal.CurrentCount}/{checklistGoal.TargetCount})";
                }
                Console.WriteLine($"{i + 1}. {goal.Name} {status}");
            }
        }

        public void Save()
        {
            // Implement saving to a file or database
            Console.WriteLine("Goals saved!");
        }

        public void Load()
        {
            // Implement loading from a file or database
            Console.WriteLine("Goals loaded!");
        }

        public static void Main(string[] args)
        {
            Program program = new Program();

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create a goal");
                Console.WriteLine("2. Record an event");
                Console.WriteLine("3. Display goals");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                       program.CreateGoal();
                       break;
                    case 2:
                        program.RecordEvent();
                        break;
                    case 3:
                        program.DisplayGoals();
                        break;
                    case 4:
                        program.Save();
                        break;
                    case 5:
                        program.Load();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}