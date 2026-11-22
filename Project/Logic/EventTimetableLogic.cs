
public static class EventTimetableLogic
{
    private static EventTimetableAccess _eventTimetableAccess = new();

    public static void CreateEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Write(model);
    }

    public static EventTimetableModel GetEventTimetableById(int id)
    {
        return _eventTimetableAccess.SelectAllById(id);
    }

    public static List<EventTimetableModel> GetByEventId(int eventId)
    {
        return _eventTimetableAccess.SelectByEventId(eventId);
    }

    public static List<EventTimetableModel> GetByTimetableId(string timetableId)
    {
        return _eventTimetableAccess.SelectByTimetableId(timetableId);
    }

    public static void UpdateEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Update(model);
    }

    public static void DeleteEventTimetable(EventTimetableModel model)
    {
        _eventTimetableAccess.Delete(model);
    }
}