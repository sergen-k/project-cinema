public class EventLogic
{
    private EventAccess _access = new();

    public List<EventModel> SelectAll()
    {
        return _access.SelectAll();
    }
    public EventModel? SelectAllById(long id)
    {
        return _access.SelectAllById((int)id);
    }

    public EventModel? MovieHasEvent(MovieModel movie, List<EventTimetableModel> eventTimetables, List<TimetableModel> timetables)
    {
        EventTimetableModel? eventTimetable = eventTimetables
        .FirstOrDefault(e => timetables.First(t => t.Id == e.TimetableId).MovieId == movie.Id);

        if (eventTimetable != null)
        {
            return SelectAllById(eventTimetable.EventId);
        }
        else
        {
            return null;
        }
    }
}