// EXCEEDING REQUIREMENTS:
// I added a leveling system. The player starts at level 1 and gains a level
// for every 1,000 points. The menu shows the current level and points needed
// for the next level, and recording an event announces any level increase.
// The level is calculated from the score, so saving/loading restores it.
// I also added input validation, confirmation before overwriting a save or
// replacing the current session, and protection against invalid save files.

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}