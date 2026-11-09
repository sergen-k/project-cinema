public static class BrowseMovies
{
    private static int _scrollPosition;
    private static int _scrollStart;
    private static int _scrollEnd;

    private static List<MovieModel> Movies;

    public static void Start()
    {
        _scrollPosition = 0;
        string answer;
        MovieModel movieToSee = null;
        do
        {
            Console.Clear();
            Movies = BrowseMovieLogic.SelectAll();
            // if (Movies.Count == 0)
            // {
            //     Console.WriteLine("Currently no movies available");
            // }
            
            answer = ChooseMovie();
            switch(answer)
            {
                case "↓ Scroll Down ↓":
                    if (_scrollPosition < Movies.Count / 10)
                        _scrollPosition++;
                    break;
                case "↑ Scroll Up ↑":
                    if (_scrollPosition > 0)
                        _scrollPosition--;
                    break;
                case "Return":
                    break;
                default:
                    movieToSee = Movies.Find(m => m.Title == answer);
                    Console.Clear();
                    if (movieToSee.AgeLimit >= 18)
                    {
                        Console.CursorVisible = true;
                        Console.WriteLine("This move is 18+");
                        Console.WriteLine("Are you sure you want to continue? (Y/N)");
                        Console.CursorVisible = false;
                        if (Console.ReadKey().Key != ConsoleKey.Y) {break;}
                    }
                    InspectMovie.Start(movieToSee);
                    break;
            }
        } while (answer != "Return");
    }

    private static List<string> MovieOptionsList()
    {
        List<string> movieOptions = [];
        _scrollStart = _scrollPosition + 10 * _scrollPosition;
        _scrollEnd = _scrollStart+10 > Movies.Count ? Movies.Count : _scrollStart + 10;
        for(int i = _scrollStart; i < _scrollEnd; i++)
        {
            movieOptions.Add(MovieInfoLine(Movies[i]));
        }
        movieOptions.Add(null);
        if (_scrollEnd == Movies.Count) {movieOptions.Add("↑ Scroll Up ↑");}
        if (_scrollStart == 0) {movieOptions.Add("↓ Scroll Down ↓");}
        movieOptions.Add("Return");
        return movieOptions;

    }

    private static string MovieInfoLine(MovieModel movie)
    { 
        return $"{movie.Title}; {movie.Genre}; {movie.AgeLimit}+";
    }

    private static string ChooseMovie()
    {
        ConsoleMenu menu = new( $"Page ({_scrollPosition+1}/{(int)Math.Ceiling((double)Movies.Count/10)})");
        string option = menu.SelectOption(MovieOptionsList());
        if (option.Contains(';'))
        {
            option = option.Split(';')[0];
        }
        return option;
    }
}