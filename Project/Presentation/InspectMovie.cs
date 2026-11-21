using System.Text;

public static class InspectMovie
{
    public static void Start(MovieModel movie)
    {
        ConsoleMenu menu = new();
        menu.AddOption(new InfoOption(MovieInfo(movie)));
        List<string> options = [];

        if (MovieLogic.IsOfAge(movie))
        {
            options.Add("Reserve");
        }

        menu.PopulateOptions(options);

        while (true)
        {
            string option = menu.SelectOption();
            if (option == "Reserve")
            {
                MakeReservation.Start(movie);
            }
            else
            {
                break;
            }
        }
    }

    private static string MovieInfo(MovieModel movie)
    {
        StringBuilder movieInfo = new();

        movieInfo.AppendLine(movie.Title);
        movieInfo.AppendLine($"Genre: {movie.Genre}");
        movieInfo.AppendLine($"Directed by: {movie.Director}");
        movieInfo.AppendLine($"Must be at least {movie.AgeLimit} years old");
        movieInfo.AppendLine($"Duration: {movie.Duration} minutes");
        movieInfo.AppendLine();
        movieInfo.AppendLine(movie.Description);
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
}