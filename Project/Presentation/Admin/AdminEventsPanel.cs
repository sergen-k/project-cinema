public static class AdminEventsPanel
{
    private static List<OptionBase> _options = new();
 
    public static void Start()
    {
        while (true)
        {
            ConsoleMenu menu = new();
            GetOptions();
            menu.SetOptions(_options);
 
            int chosenOption = menu.SelectOption();
            Console.Clear();
            if (chosenOption == -1) { return; }
 
            switch (_options[chosenOption].Label)
            {
                case "Create Event":
                    CreateEvent();
                    break;
 
                case "View Events":
                    ViewEvents();
                    break;
            }
        }
    }
 
    private static void GetOptions()
    {
        _options.Clear();
 
        _options.Add(new InfoOption("===== EVENT MANAGEMENT ====="));
        _options.Add(new SelectOption("Create Event"));
        _options.Add(new SelectOption("View Events"));
    }
 
    private static void CreateEvent()
    {
        Console.WriteLine("===== CREATE EVENT =====");
 
        Console.Write("Event name: ");
        string name = Console.ReadLine();
 
        Console.Write("Description: ");
        string description = Console.ReadLine();
 
        Console.Write("Cost: ");
        if (!double.TryParse(Console.ReadLine(), out double cost))
        {
            Console.WriteLine("Invalid cost!");
            Console.ReadKey();
            return;
        }
 
        EventModel newEvent = new(0, name, description, cost);
        EventLogic eventLogic = new();
        eventLogic.CreateEvent(newEvent);
 
        Console.WriteLine("\nEvent successfully created!");
        Console.ReadKey();
    }
 
    // Select an event from the events table
    private static void ViewEvents()
    {
        EventLogic eventLogic = new();
        List<EventModel> events = eventLogic.SelectAll();
 
        if (events.Count == 0)
        {
            Console.WriteLine("No events found.");
            Console.ReadKey();
            return;
        }
 
        List<OptionBase> eventOptions = new();
        eventOptions.Add(new InfoOption("===== SELECT AN EVENT ====="));
        foreach (EventModel ev in events)
        {
            eventOptions.Add(new SelectOption($"{ev.Name} | {ev.Cost}"));
        }
 
        ConsoleMenu menu = new();
        menu.SetOptions(eventOptions);
        int chosen = menu.SelectOption();
 
        if (chosen == -1)
            return;
 
        // index 0 is the InfoOption header, so events are shifted by 1
        EventModel selectedEvent = eventLogic.SelectAllById((int)events[chosen - 1].Id);
 
        if (selectedEvent == null)
            return;
 
        Console.Clear();
 
        Console.WriteLine("===== SELECTED EVENT =====");
        Console.WriteLine($"Name: {selectedEvent.Name}");
        Console.WriteLine($"Description: {selectedEvent.Description}");
        Console.WriteLine($"Cost: {selectedEvent.Cost}");
 
        Console.WriteLine("\nPress any key to select a timetable...");
        Console.ReadKey();
 
        SelectMovie(selectedEvent);
    }
 
    private static void SelectMovie(EventModel selectedEvent)
    {
        TimetableLogic timetableLogic = new();
        MovieLogic movieLogic = new();
 
        List<TimetableModel> timetables = timetableLogic.SelectAll();
 
        if (timetables.Count == 0)
        {
            Console.WriteLine("No timetables found.");
            Console.ReadKey();
            return;
        }
 
        List<long> movieIds = timetables
            .Select(t => t.MovieId)
            .Distinct()
            .ToList();
 
        List<OptionBase> movieOptions = new();
        movieOptions.Add(new InfoOption("===== SELECT A MOVIE ====="));
        foreach (long movieId in movieIds)
        {
            MovieModel movie = movieLogic.Select((int)movieId);
            movieOptions.Add(new SelectOption($"Movie : {movie.Title}"));
        }
 
        ConsoleMenu movieMenu = new();
        movieMenu.SetOptions(movieOptions);
        int chosenMovie = movieMenu.SelectOption();
 
        if (chosenMovie == -1)
            return;
 
        // index 0 is the InfoOption header, so movies are shifted by 1
        MovieModel selectedMovie = movieLogic.Select((int)movieIds[chosenMovie - 1]);
 
        List<TimetableModel> movieTimetables =
            timetableLogic.SelectByMovieId(selectedMovie);
 
        Console.Clear();
 
        List<OptionBase> timetableOptions = new();
        timetableOptions.Add(new InfoOption("===== SELECT A TIMETABLE ====="));
        foreach (TimetableModel timetable in movieTimetables)
        {
            timetableOptions.Add(new SelectOption(
                $"Start: {timetable.HumanReadableStartTime} | Room: {timetable.RoomNumber}"
            ));
        }
 
        ConsoleMenu timetableMenu = new();
        timetableMenu.SetOptions(timetableOptions);
        int chosenTimetable = timetableMenu.SelectOption();
 
        if (chosenTimetable == -1)
            return;
 
        long timetableId = movieTimetables[chosenTimetable - 1].Id;
 
        Console.WriteLine("Timetable selected!");
        Console.ReadKey();
 
        EventTimetableModel eventTimetableModel = new(0, selectedEvent.Id, timetableId);
 
        EventTimetableLogic eventTimetableLogic = new();
        eventTimetableLogic.CreateEventTimetable(eventTimetableModel);
    }
}