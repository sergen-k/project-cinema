
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
        options.Add("Edit Event");
        options.Add("Delete Event");

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

        EventLogic.CreateEvent(newEvent);

        Console.WriteLine("\nEvent successfully created!");
        Console.ReadKey();
    }

private static void ViewEvents()
{
    Console.WriteLine("===== ALL EVENTS =====\n");

    List<EventModel> events = EventLogic.GetAllEvents();

    if (events.Count == 0)
    {
        Console.WriteLine("No events found.");
    }
    else
    {
        foreach (EventModel ev in events)
        {
            Console.WriteLine($"ID: {ev.Id}");
            Console.WriteLine($"Name: {ev.Name}");
            Console.WriteLine($"Description: {ev.Description}");
            Console.WriteLine($"Cost: {ev.Cost:C}");
            Console.WriteLine("-----------------------------");
        }
    }

    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}
    

  