public class EventTimetableModel
{
    public EventTimetableModel(long id, long eventId, long timetableId)
    {
        Id = id;
        EventId = eventId;
        TimetableId = timetableId;
    }

    public long Id { get; set; }
    public long EventId { get; set; }
    public long TimetableId { get; set; }
}
