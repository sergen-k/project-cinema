
public  class EventTimetableLogic
{
    private  EventTimetableAccess _eventTimetableAccess = new();

    public  void CreateEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Write(model);
    }

    public  EventTimetableModel GetEventTimetableById(int id)
    {
        return _eventTimetableAccess.SelectAllById(id);
    }

    public  List<EventTimetableModel> GetByEventId(int eventId)
    {
        return _eventTimetableAccess.SelectByEventId(eventId);
    }

    public  List<EventTimetableModel> GetByTimetableId(string timetableId)
    {
        return _eventTimetableAccess.SelectByTimetableId(timetableId);
    }

    public  void UpdateEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Update(model);
    }

    public  void DeleteEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Delete(model);
    }

    public List<EventTimetableModel> SelectAll()
    {
        return _eventTimetableAccess.SelectAll();
    }

    public List<EventTimetableModel> SelectAllByMovieId(long movieId)
    {
        return _eventTimetableAccess.SelectAllByMovieId(movieId);
    }
}