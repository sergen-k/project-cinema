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
            if (Movies.Count == 0)
            {
                Console.WriteLine("Currently no movies available");
            }

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
                    movieToSee = Movies[int.Parse(answer)-1];
                    Console.Clear();
                    InspectMovie.Start(movieToSee);
                    break;
            }
        } while (answer != "q" && movieToSee == null);
    }

    private static void MovieList()
    {
        _scrollStart = _scrollPosition + 10 * _scrollPosition;
        _scrollEnd = _scrollStart+10 > Movies.Count ? Movies.Count : _scrollStart + 10;
        for(int i = _scrollStart; i < _scrollEnd; i++)
        {
            Console.WriteLine(MovieInfoLine(Movies[i], i+1));
        }
    }

    private static string MovieInfoLine(MovieModel movie, int iteration)
    { 
        return $"{iteration}. {movie.Title}; {movie.Genre}; {movie.AgeLimit}+";
    }

    private static string Options()
    {
        string answer;
        int movieAnswer;
        do
        {
            Console.WriteLine("+ Scroll up");
            Console.WriteLine("- Scroll down");
            Console.WriteLine("Q. Return to the main menu");
            Console.WriteLine("Fill in a movie's title to inspect it.");
            answer = Console.ReadLine()!.ToLower();
            int.TryParse(answer, out movieAnswer);
        } while (!(answer == "+" || answer == "-" || answer == "q" || movieAnswer-1 >= _scrollStart && movieAnswer-1 < _scrollEnd));
        
        return (movieAnswer-1 >= _scrollStart && movieAnswer-1 <= _scrollEnd) ? $"{movieAnswer}" : answer;
    }
}