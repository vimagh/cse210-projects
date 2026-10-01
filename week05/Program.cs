using System;

class Program
{
    static void Main(string[] args)
    {
        /*
         * EXCEEDING REQUIREMENTS:
         *
         * I added a session activity counter.
         * The program keeps track of how many mindfulness activities
         * the user completes during the current session.
         *
         * When the user chooses to quit, the program displays the total
         * number of activities completed.
         */

        string choice = "";

        int completedActivities = 0;

        while (choice != "4")
        {
            Console.Clear();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");

            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity breathingActivity = new BreathingActivity();

                breathingActivity.Run();

                completedActivities++;
            }

            else if (choice == "2")
            {
                ReflectingActivity reflectingActivity = new ReflectingActivity();

                reflectingActivity.Run();

                completedActivities++;
            }

            else if (choice == "3")
            {
                ListingActivity listingActivity = new ListingActivity();

                listingActivity.Run();

                completedActivities++;
            }

            else if (choice == "4")
            {
                Console.WriteLine();
                Console.WriteLine($"You completed {completedActivities} mindfulness activities during this session.");
                Console.WriteLine("Thank you for using the Mindfulness Program!");
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid menu option.");

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}