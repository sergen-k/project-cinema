public static class BrowseMovies
{

    private static readonly MovieLogic _movieLogic = new();
    private static readonly TimetableLogic _timetableLogic = new();
    private static readonly EventTimetableLogic _eventTimetableLogic = new();
    private static readonly EventLogic _eventLogic = new();

    private static List<MovieModel> _movies;
    private static List<EventTimetableModel> _eventTimetables;
    private static List<TimetableModel> _timetables;
    private static List<OptionBase> _options = new();

    public static void Start()
    {
        int answer;
        //_movies = _movieLogic.SelectNextTwoWeeks();
        _movies = _movieLogic.SelectAll();
        _eventTimetables = _eventTimetableLogic.SelectAll();
        _timetables = _timetableLogic.SelectAll();


        while(true)
        {
            Console.Clear();
            answer = Options();
            if (answer == -1)
            {
                return;
            }
            else
            {
                MovieModel selectedMovie = _movies[answer];
                if (selectedMovie.AgeLimit >= 18)
                {
                    Console.CursorVisible = true;
                    Console.Clear();
                    Console.WriteLine("This move is 18+");
                    Console.WriteLine("Are you sure you want to continue? (Y/N)");
                    Console.CursorVisible = false;
                    if (Console.ReadKey().Key != ConsoleKey.Y) {continue;}
                }

                InspectMovie.Start(selectedMovie, _eventLogic.MovieHasEvent(selectedMovie, _eventTimetables, _timetables));
            }
        }
    }

    private static string MovieInfoLine(MovieModel movie)
    {
        EventModel? eventModel = _eventLogic.MovieHasEvent(movie, _eventTimetables, _timetables);
        string movieString = $"[{(eventModel != null ? "X" : " ")}] {movie.Title}; {movie.Genre}; {MovieLogic.AgeToCategory(movie.AgeLimit)}";
        if (eventModel != null)
            movieString += $"\n   - {eventModel.Name} (Event)";

        return movieString;
    }

    private static int Options()
    {
        SearchableMenu menu = new();
        _options.Clear();
        foreach (var movie in _movies)
        {
            _options.Add(new ScrollOption(MovieInfoLine(movie)));
        }
        menu.SetOptions(_options);
        return menu.SelectOption();
    }
}