using System.Text;

public static class InspectMovie
{
    public static void Start(MovieModel movie)
    {
        ConsoleMenu menu = new(MovieInfo(movie).ToString());
        List<string> options = [];
        if (InspectMovieLogic.IsOfAge(movie)) {options.Add("Reserve");}
        options.Add("Return");
        string option = menu.SelectOption(options);
        if (option == "Reserve") {MakeReservation.Start(movie);}
    }

    private static StringBuilder MovieInfo(MovieModel movie)
    {
        StringBuilder movieInfo = new();

        movieInfo.AppendLine(movie.Title);
        movieInfo.AppendLine($"Genre: {movie.Genre}");
        movieInfo.AppendLine($"Directed by: {movie.Director}");
        movieInfo.AppendLine($"Must be at least {movie.AgeLimit} years old");
        movieInfo.AppendLine($"Duration: {movie.Duration} minutes");
        movieInfo.AppendLine();
        movieInfo.AppendLine(movie.Description);
        if (!InspectMovieLogic.IsLoggedIn())
        {
            movieInfo.AppendLine();
            movieInfo.AppendLine($"To reserve you need to be logged in.");
        }
        else if (!InspectMovieLogic.IsOfAge(movie))
        {
            movieInfo.AppendLine();
            movieInfo.AppendLine($"To reserve you need to be at least {movie.AgeLimit}!");
        }
        return movieInfo;
    }
}