using System.Text;

public static class InspectMovie
{
    private static EventTimetableLogic _eventTimetableLogic = new();
    private static List<OptionBase> _options = new();
    public static void Start(MovieModel movie, EventModel? eventModel)
    {

        while (true)
        {
            int option = Options(movie, eventModel);
            if (option == -1)
                break;
            else
            {
                MakeReservation.Start(movie, _eventTimetableLogic.SelectAllByMovieId(movie.Id));
            }
        }
    }

    private static string MovieInfo(MovieModel movie, EventModel? eventModel)
    {
        StringBuilder movieInfo = new();

        movieInfo.AppendLine(movie.Title);
        movieInfo.AppendLine($"Genre: {movie.Genre}");
        movieInfo.AppendLine($"Directed by: {movie.Director}");
        movieInfo.AppendLine($"Must be at least {movie.AgeLimit} years old");
        movieInfo.AppendLine($"Duration: {movie.Duration} minutes");
        movieInfo.AppendLine();
        movieInfo.AppendLine(movie.Description);

        if (eventModel != null)
        {
            movieInfo.AppendLine();
            movieInfo.AppendLine("There is a special event upcoming in this movie's screenings!");
            movieInfo.AppendLine(eventModel.Name);
            movieInfo.AppendLine($"- {eventModel.Description}");
            movieInfo.AppendLine();
        }


        if (!UserLogic.IsLoggedIn())
        {
            movieInfo.AppendLine();
            movieInfo.AppendLine($"To reserve you need to be logged in.");
        }
        else if (!MovieLogic.IsOfAge(movie))
        {
            movieInfo.AppendLine();
            movieInfo.AppendLine($"To reserve you need to be at least {movie.AgeLimit}!");
        }

        return movieInfo.ToString();
    }

    private static int Options(MovieModel movie, EventModel? eventModel)
    {
        _options.Clear();
        ConsoleMenu menu = new();
        
        _options.Add(new InfoOption(MovieInfo(movie, eventModel)));

        if (MovieLogic.IsOfAge(movie))
        {
            _options.Add(new SelectOption("Reserve"));
        }
        menu.SetOptions(_options);
        return menu.SelectOption();
    }
}