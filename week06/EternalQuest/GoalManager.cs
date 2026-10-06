using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private long _score = 0;

    public void Start()
    {
        Console.WriteLine("Welcome to Eternal Quest!");

        try
        {
            bool running = true;
            while (running)
            {
                DisplayPlayerInfo();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Create New Goal");
                Console.WriteLine("  2. List Goals");
                Console.WriteLine("  3. Save Goals");
                Console.WriteLine("  4. Load Goals");
                Console.WriteLine("  5. Record Event");
                Console.WriteLine("  6. Quit");

                int choice = ReadInteger("Select a choice from the menu: ", 1, 6);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: CreateGoal(); break;
                    case 2: ListGoalDetails(); break;
                    case 3: SaveGoals(); break;
                    case 4: LoadGoals(); break;
                    case 5: RecordEvent(); break;
                    case 6:
                        Console.WriteLine("Goodbye! Keep working on your goals.");
                        running = false;
                        break;
                }
            }
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nInput ended. Any unsaved changes have not been saved.");
        }
    }

    public void DisplayPlayerInfo()
    {
        long level = GetLevel();
        long pointsToNextLevel = 1000 - (_score % 1000);
        Console.WriteLine($"\nYou have {_score} points.");
        Console.WriteLine($"Level {level} | {pointsToNextLevel} points to the next level.\n");
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create a goal first.");
            return;
        }

        Console.WriteLine("Your goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            // This calls the appropriate implementation for each goal type.
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        int type = ReadInteger("Which type of goal would you like to create? ", 1, 3);

        string name = ReadText("What is the name of your goal? ");
        string description = ReadText("What is a short description of it? ");
        int points = ReadInteger("How many points is each event worth? ", 1);
        Goal goal;

        switch (type)
        {
            case 1:
                goal = new SimpleGoal(name, description, points);
                break;
            case 2:
                goal = new EternalGoal(name, description, points);
                break;
            default:
                int target = ReadInteger("How many times must this goal be accomplished? ", 1);
                int bonus = ReadInteger("What is the bonus for finishing it? ", 0, int.MaxValue - points);
                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;
        }

        _goals.Add(goal);
        Console.WriteLine("Goal created successfully.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create a goal first.");
            return;
        }

        ListGoalDetails();
        int selection = ReadInteger("Which goal did you accomplish? ", 1, _goals.Count);
        Goal goal = _goals[selection - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("That goal is already complete. No additional points were awarded.");
            return;
        }

        // Reserve enough space for any valid event before changing its progress.
        if (_score > long.MaxValue - int.MaxValue)
        {
            Console.WriteLine("The score limit has been reached. No event was recorded.");
            return;
        }

        long previousLevel = GetLevel();
        int earnedPoints = goal.RecordEvent();
        _score += earnedPoints;

        Console.WriteLine($"Congratulations! You earned {earnedPoints} points.");
        if (goal.IsComplete())
            Console.WriteLine($"You completed: {goal.GetShortName()}!");

        if (GetLevel() > previousLevel)
            Console.WriteLine($"LEVEL UP! You are now level {GetLevel()}!");

        Console.WriteLine($"Your total score is {_score} points.");
    }

    public void SaveGoals()
    {
        string filename = ReadText("Enter the filename to save to (for example, goals.txt): ");
        if (File.Exists(filename) && !Confirm("This file exists. Overwrite it? (y/n): "))
            return;

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(_score);
                foreach (Goal goal in _goals)
                    writer.WriteLine(goal.GetStringRepresentation());
            }
            Console.WriteLine($"Goals and score saved to {filename}.");
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
            ex is ArgumentException || ex is NotSupportedException || ex is SecurityException)
        {
            Console.WriteLine($"Unable to save the file: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        string filename = ReadText("Enter the filename to load from: ");

        try
        {
            string[] lines = File.ReadAllLines(filename);
            long loadedScore;
            if (lines.Length == 0 || !long.TryParse(lines[0], out loadedScore) || loadedScore < 0)
                throw new FormatException("The file does not contain a valid score.");

            // Validate the whole file before replacing the current session.
            List<Goal> loadedGoals = new List<Goal>();
            for (int i = 1; i < lines.Length; i++)
                loadedGoals.Add(CreateGoalFromString(lines[i]));

            if ((_goals.Count > 0 || _score > 0) &&
                !Confirm("Loading will replace your current goals and score. Continue? (y/n): "))
                return;

            _goals = loadedGoals;
            _score = loadedScore;
            Console.WriteLine($"Loaded {_goals.Count} goals and {_score} points.");
        }
        catch (EndOfStreamException)
        {
            throw;
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
            ex is FormatException || ex is OverflowException || ex is ArgumentException ||
            ex is NotSupportedException || ex is SecurityException)
        {
            Console.WriteLine($"Unable to load the file: {ex.Message}");
            Console.WriteLine("Your current goals and score were not changed.");
        }
    }

    private static Goal CreateGoalFromString(string line)
    {
        int separator = line.IndexOf(':');
        if (separator < 0)
            throw new FormatException("A goal record is missing its type.");

        string type = line.Substring(0, separator);
        string[] parts = line.Substring(separator + 1).Split(',');
        if (parts.Length < 3)
            throw new FormatException("A goal record is missing required fields.");

        string name = Uri.UnescapeDataString(parts[0]);
        string description = Uri.UnescapeDataString(parts[1]);
        int points = int.Parse(parts[2]);

        switch (type)
        {
            case "SimpleGoal":
                if (parts.Length == 4)
                    return new SimpleGoal(name, description, points, bool.Parse(parts[3]));
                break;
            case "EternalGoal":
                if (parts.Length == 3)
                    return new EternalGoal(name, description, points);
                break;
            case "ChecklistGoal":
                if (parts.Length == 6)
                    return new ChecklistGoal(name, description, points,
                        int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                break;
        }

        throw new FormatException("A goal record has an unknown type or an incorrect number of fields.");
    }

    private long GetLevel()
    {
        return (_score / 1000) + 1;
    }

    private static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? throw new EndOfStreamException();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Please enter a value.");
        }
    }

    private static int ReadInteger(string prompt, int minimum, int maximum = int.MaxValue)
    {
        while (true)
        {
            string input = ReadText(prompt);
            int value;
            if (int.TryParse(input, out value) && value >= minimum && value <= maximum)
                return value;

            Console.WriteLine($"Please enter a whole number from {minimum} to {maximum}.");
        }
    }

    private static bool Confirm(string prompt)
    {
        while (true)
        {
            string answer = ReadText(prompt).ToLowerInvariant();
            if (answer == "y" || answer == "yes") return true;
            if (answer == "n" || answer == "no") return false;
            Console.WriteLine("Please enter y or n.");
        }
    }
}
