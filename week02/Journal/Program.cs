using System;

class Program
{
    static void Main(string[] args)
    {
        /*
         * EXCEEDING REQUIREMENTS:
         *
         * To exceed the core requirements, I added mood tracking.
         * When users create a journal entry, they can record their mood
         * along with their response. The mood is displayed with the entry
         * and is also saved to and loaded from the journal file.
         *
         * I also added:
         * - More than five journal prompts.
         * - Error handling for invalid menu selections.
         * - A check to make sure a file exists before loading it.
         * - Friendly confirmation messages after writing, saving, and loading.
         */

        Journal journal = new Journal();

        List<string> prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is one thing I learned today?",
            "What am I most grateful for today?",
            "What is one thing I accomplished today?"
        };

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            string input = Console.ReadLine();

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid number.");
                Console.WriteLine();
                continue;
            }

            if (choice == 1)
            {
                Random random = new Random();

                int index = random.Next(prompts.Count);
                string prompt = prompts[index];

                Console.WriteLine();
                Console.WriteLine(prompt);
                Console.Write("> ");

                string response = Console.ReadLine();

                Console.Write("How would you describe your mood today? ");
                string mood = Console.ReadLine();

                Entry entry = new Entry();

                entry._date = DateTime.Now.ToShortDateString();
                entry._promptText = prompt;
                entry._entryText = response;
                entry._mood = mood;

                journal.AddEntry(entry);

                Console.WriteLine();
                Console.WriteLine("Your journal entry has been added.");
                Console.WriteLine();
            }

            else if (choice == 2)
            {
                Console.WriteLine();

                if (journal._entries.Count == 0)
                {
                    Console.WriteLine("There are currently no journal entries.");
                }
                else
                {
                    journal.DisplayAll();
                }

                Console.WriteLine();
            }

            else if (choice == 3)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();

                if (File.Exists(filename))
                {
                    journal.LoadFromFile(filename);

                    Console.WriteLine();
                    Console.WriteLine("Journal loaded successfully.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("The file could not be found.");
                }

                Console.WriteLine();
            }

            else if (choice == 4)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();

                journal.SaveToFile(filename);

                Console.WriteLine();
                Console.WriteLine("Journal saved successfully.");
                Console.WriteLine();
            }

            else if (choice == 5)
            {
                Console.WriteLine();
                Console.WriteLine("Thank you for using the Journal Program!");
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Please choose a number from 1 to 5.");
                Console.WriteLine();
            }
        }
    }
}