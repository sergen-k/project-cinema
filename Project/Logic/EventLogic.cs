public static class EventLogic
{
    private static EventAccess _eventAccess = new();

    public static void CreateEvent(EventModel model)
    {
        _eventAccess.Write(model);
    }

    public static List<EventModel> GetAllEvents()
    {
        return _eventAccess.SelectAll();
    }

    public static EventModel GetEventById(int id)
    {
        return _eventAccess.SelectAllById(id);
    }

    public static void UpdateEvent(EventModel model)
    {
        _eventAccess.Update(model);
    }

    public static void DeleteEvent(EventModel model)
    {
        _eventAccess.Delete(model);
    }
}