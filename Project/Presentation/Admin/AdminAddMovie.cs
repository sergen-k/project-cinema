public static class AdminAddMovie
{
    private static MovieLogic moviesLogic = new MovieLogic();

    public static void Start()
    {
        Console.WriteLine("=== Add Movie ===");

        string title = AskForInput("Enter movie title:");
        string genre = AskGenre();
        string director = AskForInput("Enter movie director:");
        string description = AskForInput("Enter movie description:");
        long ageLimit = AskForAgeLimit();
        string duration = AskForDuration();

        bool success = moviesLogic.AddMovie(
            UserLogic.CurrentAccount,
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
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Failed to add movie. Please check the information.");
        }
    }


    private static string AskGenre()
    {
        string[] genres = ["Fantasy", "Comedy", "Drama", "Action", "Horror", "Sci-Fi"];

        List<OptionBase> options = new();
        options.Add(new InfoOption("===== SELECT A GENRE ====="));
        foreach (string genre in genres)
        {
            options.Add(new SelectOption(genre));
        }

        ConsoleMenu menu = new();
        menu.SetOptions(options);

        int chosen = menu.SelectOption();
        Console.Clear();

        // -1 = escape / return, same as the old empty-string fallback
        if (chosen == -1)
        {
            return "-";
        }

        // index 0 is the InfoOption header, so genres are shifted by 1
        return genres[chosen - 1];
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
            return 1;
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