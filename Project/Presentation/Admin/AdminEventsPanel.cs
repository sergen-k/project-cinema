
public static class AdminEventsPanel
{
    public static void Start()
    {
        while (true)
        {
            ConsoleMenu menu = new();

            menu.AddOption(new InfoOption("===== EVENT MANAGEMENT ====="));
            menu.PopulateOptions(GetOptions());

            string chosenOption = menu.SelectOption();

            Console.Clear();

            switch (chosenOption)
            {
                case "Create Event":
                    CreateEvent();
                    break;

                case "View Events":
                    ViewEvents();
                    break;

              
                case "Return":
                    return;

                case null:
                    return;
            }
        }
    }

    private static List<string> GetOptions()
    {
        List<string> options = [];

        options.Add("Create Event");
        options.Add("View Events");
       

        return options;
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

        EventModel newEvent = new(
            0,
            name,
            description,
            cost
        );
        EventLogic eventLogic = new();
        eventLogic.CreateEvent(newEvent);

        Console.WriteLine("\nEvent successfully created!");
        Console.ReadKey();
    }

   // select an event from the events table 
    private static void ViewEvents()
    { 
        EventLogic eventLogic1 = new();
        List<EventModel> events = eventLogic1.GetAllEvents();

        if (events.Count == 0)
        {
            Console.WriteLine("No events found.");
            Console.ReadKey();
            return;
        }

        ConsoleMenu<string> menu = new();

        menu.AddOption(new InfoOption("===== SELECT AN EVENT ====="));

        foreach (EventModel ev in events)
        {
            menu.AddOption(new SelectOption<string>(
                $"{ev.Name} | {ev.Cost}",
                ev.Id.ToString()
            ));
        }

        string selectedId = menu.SelectOption();

        if (selectedId == null)
            return;

        if (!long.TryParse(selectedId, out long eventId))
            return;
        EventLogic eventLogic = new();
        EventModel selectedEvent = eventLogic.GetEventById((int)eventId);

        if (selectedEvent == null)
            return;

        Console.Clear();

        Console.WriteLine("===== SELECTED EVENT =====");
        Console.WriteLine($"Name: {selectedEvent.Name}");
        Console.WriteLine($"Description: {selectedEvent.Description}");
        Console.WriteLine($"Cost:{selectedEvent.Cost}");

        Console.WriteLine("\nPress any key to select a timetable...");
        Console.ReadKey();


        SelectMovie(selectedEvent);

    }

private static void SelectMovie(EventModel selectedEvent)
{
    TimetableLogic timetableLogic = new();

    List<TimetableModel> timetables =
        timetableLogic.SelectAll();

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

    ConsoleMenu<string> menu = new();

    menu.AddOption(new InfoOption("===== SELECT A MOVIE ====="));

    foreach (long movieId in movieIds) 
    {   
        MovieLogic movieLogic = new();
        MovieModel movie = movieLogic.Select((int)movieId);
        menu.AddOption(new SelectOption<string>(
            $"Movie : {movie.Title}",
            movieId.ToString()
        ));
    }

    string selectedMovieId = menu.SelectOption();

    if (selectedMovieId == null)
        return;
 if (!long.TryParse(selectedMovieId, out long selectedMovieid))
    return;
 
 MovieLogic movielogic = new();
 MovieModel selectedMovie =  movielogic.Select((int)selectedMovieid);

 List<TimetableModel> movieTimetables =
    timetableLogic.SelectByMovieId(selectedMovie);

    Console.Clear();

    ConsoleMenu<string> timetableMenu = new();

    timetableMenu.AddOption(
    new InfoOption("= SELECT A TIMETABLE =")
);
   foreach (TimetableModel timetable in movieTimetables)
{
    timetableMenu.AddOption(new SelectOption<string>(
        $"Start: {timetable.HumanReadableStartTime} | Room: {timetable.RoomNumber}",
        timetable.Id.ToString()
    ));
}

string selectedTimetableId = timetableMenu.SelectOption();

if (selectedTimetableId == null)
    return;
Console.WriteLine("Timetable selected!");
Console.ReadKey();

if (selectedTimetableId == null)
    return;

if (!long.TryParse(selectedTimetableId, out long timetableId))
    return;


EventTimetableModel eventTimetableModel = new(0,selectedEvent.Id,timetableId);

EventTimetableLogic eventTimetableLogic = new();
eventTimetableLogic.CreateEventTimetable(eventTimetableModel);



}}