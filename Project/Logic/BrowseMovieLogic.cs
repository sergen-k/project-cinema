public class BrowseMovieLogic
{
    private static MovieAccess _movieAccess = new();
    public static List<MovieModel> SelectAll()
    {
        return _movieAccess.SelectAll();
    }
}