public static class AdminManageScreenings
{
    private static TimetableLogic timetableLogic = new TimetableLogic();
    private static MovieLogic movieLogic = new MovieLogic();

    public static void Start()
    {
        Console.WriteLine("=== Manage Screenings ===");
        Console.WriteLine("1. Schedule a new screening");
        Console.WriteLine("2. Edit an existing screening");
        Console.WriteLine("3. Cancel a screening");

        string choice = AskForInput("Choose an option (1-3):");

        switch (choice)
        {
            case "1":
                ScheduleScreening();
                break;
            case "2":
                EditScreening();
                break;
            case "3":
                CancelScreening();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private static void ScheduleScreening()
    {
        int movieId = (int)AskForLong("Enter the movie ID to schedule:");
        MovieModel movie = movieLogic.Select(movieId);

        if (movie == null)
        {
            Console.WriteLine("No movie found with that ID.");
            return;
        }

        int roomNumber = (int)AskForLong("Enter the screening room number:");
        DateTime startTime = AskForStartTime();
        DateTime endTime = startTime.AddMinutes(movie.Duration);

        bool hasConflict = timetableLogic.HasSchedulingConflict(roomNumber, startTime, endTime);

        if (hasConflict)
        {
            Console.WriteLine("This room is already occupied during that time. Please choose a different time or room.");
            return;
        }

        TimetableModel newScreening = new TimetableModel(0, 0, 0, movieId, roomNumber);
        newScreening.HumanReadableStartTime = startTime;
        newScreening.HumanReadableEndtime = endTime;

        timetableLogic.ScheduleScreening(newScreening);
        Console.WriteLine("Screening scheduled successfully!");
    }

    private static void EditScreening()
    {
        int timetableId = (int)AskForLong("Enter the ID of the screening to edit:");
        TimetableModel screening = timetableLogic.Select(timetableId);

        if (screening == null)
        {
            Console.WriteLine("No screening found with that ID.");
            return;
        }

        int roomNumber = (int)AskForLong("Enter the screening room number:");
        DateTime startTime = AskForStartTime();

        MovieModel movie = movieLogic.Select((int)screening.MovieId);
        DateTime endTime = startTime.AddMinutes(movie.Duration);

        bool hasConflict = timetableLogic.HasSchedulingConflict(roomNumber, startTime, endTime, screening.Id);

        if (hasConflict)
        {
            Console.WriteLine("This room is already occupied during that time. Please choose a different time or room.");
            return;
        }

        screening.RoomNumber = roomNumber;
        screening.HumanReadableStartTime = startTime;
        screening.HumanReadableEndtime = endTime;

        timetableLogic.EditScreening(screening);
        Console.WriteLine("Screening updated successfully!");
    }

    private static void CancelScreening()
    {
        int timetableId = (int)AskForLong("Enter the ID of the screening to cancel:");
        TimetableModel screening = timetableLogic.Select(timetableId);

        if (screening == null)
        {
            Console.WriteLine("No screening found with that ID.");
            return;
        }

        timetableLogic.CancelScreening(screening);
        Console.WriteLine("Screening cancelled successfully!");
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

    private static long AskForLong(string message)
    {
        Console.WriteLine(message);
        long value;

        while (!long.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.WriteLine("Invalid number. Please try again:");
        }

        return value;
    }

    private static DateTime AskForStartTime()
    {
        Console.WriteLine("Enter the screening start date and time (dd-MM-yyyy HH:mm):");
        DateTime startTime;

        while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out startTime))
        {
            Console.WriteLine("Invalid date/time format. Please use dd-MM-yyyy HH:mm:");
        }

        return startTime;
    }
}