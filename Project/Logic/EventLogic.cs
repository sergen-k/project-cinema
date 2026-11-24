public  class EventLogic
{
    private  EventAccess _eventAccess = new();

    public  void CreateEvent(EventModel model)
    {
        _eventAccess.Write(model);
    }

    public  List<EventModel> GetAllEvents()
    {
        return _eventAccess.SelectAll();
    }

    public  EventModel GetEventById(int id)
    {
        return _eventAccess.SelectAllById(id);
    }

    public  void UpdateEvent(EventModel model)
    {
        _eventAccess.Update(model);
    }

    public  void DeleteEvent(EventModel model)
    {
        _eventAccess.Delete(model);
    }
}