class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // Instead of using only one scripture, this program contains a small
        // library of scriptures. Each time the program starts, it randomly
        // selects one scripture for the user to memorize.
        //
        // The program also improves the random word-hiding process by only
        // selecting words that have not already been hidden.

        List<Scripture> scriptures = new List<Scripture>();

        Reference reference1 = new Reference("John", 3, 16);
        scriptures.Add(new Scripture(
            reference1,
            "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life."
        ));

        Reference reference2 = new Reference("Proverbs", 3, 5, 6);
        scriptures.Add(new Scripture(
            reference2,
            "Trust in the Lord with all thine heart and lean not unto thine own understanding; in all thy ways acknowledge him and he shall direct thy paths."
        ));

        Reference reference3 = new Reference("Philippians", 4, 13);
        scriptures.Add(new Scripture(
            reference3,
            "I can do all things through Christ which strengtheneth me."
        ));

        Random random = new Random();

        int scriptureIndex = random.Next(scriptures.Count);

        Scripture scripture = scriptures[scriptureIndex];

        string input = "";

        while (input.ToLower() != "quit" && !scripture.IsCompletelyHidden())
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press enter to continue or type 'quit' to finish:");

            input = Console.ReadLine();

            if (input.ToLower() != "quit")
            {
                scripture.HideRandomWords(3);
            }
        }

        if (scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
        }
    }
}