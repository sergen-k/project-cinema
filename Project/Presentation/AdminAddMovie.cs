public static class AdminAddMovie
{
    private static MoviesLogic moviesLogic = new MoviesLogic();

    public static void Start()
    {
        Console.WriteLine("=== Add Movie ===");

        string title = AskForInput("Enter movie title:");
        string genre = AskForInput("Enter movie genre:");
        string director = AskForInput("Enter movie director:");
        string description = AskForInput("Enter movie description:");
        long ageLimit = AskForAgeLimit();
        string duration = AskForDuration();

        bool success = moviesLogic.AddMovie(
            AccountsLogic.CurrentAccount,
            title,
            genre,
            director,
            description,
            ageLimit,
            duration
        );

        if (success)
        {
            Console.WriteLine("Movie added successfully!");
        }
        else
        {
            Console.WriteLine("Failed to add movie. Please check the information.");
        }
    }

    private static string AskForInput(string message)
    {
        Console.WriteLine(message);

        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Input cannot be empty. Please try again:");
            input = Console.ReadLine();
        }

        return input;
    }

    private static long AskForAgeLimit()
    {
        Console.WriteLine("Does this movie have an age restriction?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");

        string? choice = Console.ReadLine();

        while (choice != "1" && choice != "2")
        {
            Console.WriteLine("Invalid choice. Enter 1 or 2:");
            choice = Console.ReadLine();
        }

        if (choice == "2")
        {
            return 0;
        }

        Console.WriteLine("Enter minimum age:");

        long age;

        while (!long.TryParse(Console.ReadLine(), out age) || age <= 0)
        {
            Console.WriteLine("Invalid age. Please enter a positive number:");
        }

        return age;
    }

    private static string AskForDuration()
    {
        Console.WriteLine("Enter movie duration in minutes:");

        string? duration = Console.ReadLine();
        double minutes;

        while (!double.TryParse(duration, out minutes) || minutes <= 0)
        {
            Console.WriteLine("Invalid duration. Please enter a positive number:");
            duration = Console.ReadLine();
        }

        return duration;
    }
}