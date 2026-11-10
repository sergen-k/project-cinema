public static class BrowseMovies
{
    private static List<MovieModel> Movies;

    public static void Start()
    {
        MovieModel answer;
        Movies = BrowseMovieLogic.SelectAll();
        while(true)
        {
            Console.Clear();
            answer = ChooseMovie();
            if (answer == null)
                break;
            else if (answer.AgeLimit >= 18)
            {
                Console.CursorVisible = true;
                Console.WriteLine("This move is 18+");
                Console.WriteLine("Are you sure you want to continue? (Y/N)");
                Console.CursorVisible = false;
                if (Console.ReadKey().Key != ConsoleKey.Y) {break;}
            }
            InspectMovie.Start(answer);
        }
    }

    private static string MovieInfoLine(MovieModel movie)
    { 
        return $"{movie.Title}; {movie.Genre}; {movie.AgeLimit}+";
    }

    private static MovieModel? ChooseMovie()
    {
        SearchableMenu<MovieModel> menu = new();
        foreach (var movie in Movies)
        {
            menu.AddScrollOption(new SelectOption<MovieModel>(MovieInfoLine(movie), movie));
        }
        menu.AddOption(new SelectOption<MovieModel>("Return", null));
        
        return menu.SelectOption();
    }
}