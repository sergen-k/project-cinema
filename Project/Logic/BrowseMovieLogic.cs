public class BrowseMovieLogic
{
    private static MovieAccess _movieAccess = new();
    public static List<MovieModel> SelectNextTwoWeeks()
    {
        return _movieAccess.SelectNextTwoWeeks();
    }

    
}