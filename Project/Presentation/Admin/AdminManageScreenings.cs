public static class AdminManageScreenings
{
    private static TimetableLogic timetableLogic = new TimetableLogic();
    private static MovieLogic movieLogic = new MovieLogic();
    private static List<OptionBase> _options = new();

    public static void Start()
    {
        while (true)
        {
            ConsoleMenu menu = new();
            _options.Clear();
            _options.Add(new SelectOption("Schedule a new screening"));
            _options.Add(new SelectOption("Edit an existing screening"));
            _options.Add(new SelectOption("Cancel a screening"));
            menu.SetOptions(_options);
            int chosenOption = menu.SelectOption();

            if (chosenOption == -1)
            {
                return;
            }

            Console.Clear();

            switch (_options[chosenOption].Label)
            {
                case "Schedule a new screening":
                    ScheduleScreening();
                    break;
                case "Edit an existing screening":
                    EditScreening();
                    break;
                case "Cancel a screening":
                    CancelScreening();
                    break;
            }
        }
    }

    private static void ScheduleScreening()
    {
        MovieModel? movie = ChooseMovie();
        if (movie == null)
        {
            return;
        }
        int movieId = (int)movie.Id;

        long? roomNumberOrNull = AskForLong("Enter the screening room number (or Q to go back):");
        if (roomNumberOrNull == null)
        {
            return;
        }
        int roomNumber = (int)roomNumberOrNull;

        DateTime? startTimeOrNull = AskForStartTime();
        if (startTimeOrNull == null)
        {
            return;
        }
        DateTime startTime = (DateTime)startTimeOrNull;
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
        TimetableModel? screening = ChooseScreening();
        if (screening == null)
        {
            return;
        }

        long? roomNumberOrNull = AskForLong("Enter the screening room number (or Q to go back):");
        if (roomNumberOrNull == null)
        {
            return;
        }
        int roomNumber = (int)roomNumberOrNull;

        DateTime? startTimeOrNull = AskForStartTime();
        if (startTimeOrNull == null)
        {
            return;
        }
        DateTime startTime = (DateTime)startTimeOrNull;

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
        TimetableModel? screening = ChooseScreening();
        if (screening == null)
        {
            return;
        }

        timetableLogic.CancelScreening(screening);
        Console.WriteLine("Screening cancelled successfully!");
    }



    private static long? AskForLong(string message)
    {
        Console.WriteLine(message);

        while (true)
        {
            string? input = Console.ReadLine();

            if (input != null && input.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (long.TryParse(input, out long value) && value > 0)
            {
                return value;
            }

            Console.WriteLine("Invalid number. Please try again (or Q to go back):");
        }
    }

    private static DateTime? AskForStartTime()
    {
        Console.WriteLine("Enter the screening start date and time (dd-MM-yyyy HH:mm) (or Q to go back):");

        while (true)
        {
            string? input = Console.ReadLine();

            if (input != null && input.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (!DateTime.TryParseExact(input, "dd-MM-yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime startTime))
            {
                Console.WriteLine("Invalid date/time format. Please use dd-MM-yyyy HH:mm (or Q to go back):");
                continue;
            }

            if (!timetableLogic.IsWithinAllowedSchedulingWindow(startTime))
            {
                Console.WriteLine("Screening must be scheduled between now and 2 weeks from now. Please try again (or Q to go back):");
                continue;
            }

            return startTime;
        }
    }
    private static TimetableModel? ChooseScreening()
    {
        List<TimetableModel> screenings = timetableLogic.SelectAll();

        SearchableMenu menu = new();
        _options.Clear();
        foreach (var screening in screenings)
        {
            _options.Add(new ScrollOption(ScreeningInfoLine(screening)));
        }
        menu.SetOptions(_options);
        int option = menu.SelectOption();
        if (option == -1) {return null;}
        else {return screenings[option];}
    }

    private static string ScreeningInfoLine(TimetableModel screening)
    {
        MovieModel movie = movieLogic.Select((int)screening.MovieId);
        string movieTitle = movie != null ? movie.Title : "Unknown movie";
        return $"{movieTitle}: Room {screening.RoomNumber}, {screening.HumanReadableStartTime:dd-MM-yyyy HH:mm}";
    }
    private static MovieModel? ChooseMovie()
    {
        List<MovieModel> movies = movieLogic.SelectAll();

        SearchableMenu menu = new();
        _options.Clear();
        foreach (var movie in movies)
        {
            _options.Add(new ScrollOption(MovieInfoLine(movie)));
        }
        menu.SetOptions(_options);
        int option = menu.SelectOption();
        if (option == -1) {return null;}
        else {return movies[option];}
    }

    private static string MovieInfoLine(MovieModel movie)
    {
        return $"{movie.Title}; {movie.Genre}; {movie.AgeLimit}+";
    }
}