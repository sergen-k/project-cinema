public class EventTimetableModel
{
    public EventTimetableModel(long id, long eventId, string timetableId)
    {
        Id = id;
        EventId = eventId;
        TimetableId = timetableId;
    }

    public Int64 Id { get; set; }
    public Int64 EventId { get; set; }
    public string TimetableId { get; set; }
}