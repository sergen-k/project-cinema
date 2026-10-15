public static class BrowseMovies
{
    private static int _scrollPosition;
    private static List<MovieModel> Movies;

    public static void Start()
    {
        _scrollPosition = 0;
        string answer;
        MovieModel movieToSee = null;
        do
        {
            Movies = BrowseMovieLogic.SelectAll();
            MovieList();
            Console.WriteLine();
            answer = Options();
            switch(answer)
            {
                case "+":
                    if (_scrollPosition < Movies.Count / 10)
                        _scrollPosition++;
                    break;
                case "-":
                    if (_scrollPosition > 0)
                        _scrollPosition--;
                    break;
                case "q":
                    break;
                default:
                    movieToSee = Movies.Find(m => answer == m.Title.ToLower());
                    InspectMovie.Start(movieToSee);
                    break;
            }
        } while (answer != "q" && movieToSee == null);
    }

    private static void MovieList()
    {
        int scrollStart = _scrollPosition + 10 * _scrollPosition;
        int scrollEnd = (scrollStart+10 > Movies.Count ? Movies.Count : scrollStart + 10)-1;
        for(int i = scrollStart; i <= scrollEnd; i++)
        {
            Console.WriteLine(MovieInfoLine(Movies[i]));
        }
    }

    private static string MovieInfoLine(MovieModel movie)
    { 
        return $"{movie.Title}; {movie.Genre}; {movie.AgeLimit}+";
    }

    private static string Options()
    {
        string answer;
        do
        {
            Console.WriteLine("+ Scroll up");
            Console.WriteLine("- Scroll down");
            Console.WriteLine("Q. Return to the main menu");
            Console.WriteLine("Fill in a movie's title to inspect it.");
            answer = Console.ReadLine()!.ToLower();
        } while (!(answer == "+") || (answer == "-") || (answer == "q") || Movies.Contains(Movies.Find(m => m.Title.ToLower() == answer)));
        return answer;
    }
}