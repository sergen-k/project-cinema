public  class EventLogic
{
    private  EventAccess _eventAccess = new();

    public  void CreateEvent(EventModel model)
    {
        _eventAccess.Write(model);
    }

    public  void UpdateEvent(EventModel model)
    {
        _eventAccess.Update(model);
    }

    public  void DeleteEvent(EventModel model)
    {
        _eventAccess.Delete(model);
    }

    public List<EventModel> SelectAll()
    {
        return _eventAccess.SelectAll();
    }
    public EventModel? SelectAllById(long id)
    {
        return _eventAccess.SelectAllById((int)id);
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