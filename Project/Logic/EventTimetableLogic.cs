public class EventTimetableLogic
{
    private EventTimetableAccess _access = new();

    public List<EventTimetableModel> SelectAll()
    {
        return _access.SelectAll();
    }

    public List<EventTimetableModel> SelectAllByMovieId(long movieId)
    {
        return _access.SelectAllByMovieId(movieId);
    }
}