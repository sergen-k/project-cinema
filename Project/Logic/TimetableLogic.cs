public class TimetableLogic
{
    private readonly TimetableAccess _timeTableAccess = new();
    public TimetableModel Select(int id)
    {
        return _timeTableAccess.SelectAllById(id);
    }

    public List<TimetableModel> SelectByMovieId(MovieModel movie)
    {
        return _timeTableAccess.SelectByMovieId(movie);
    }
}