using System.Data.Common;
using System.Diagnostics.Tracing;
using System.Globalization;

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

    public List<TimetableModel> SelectAll()
    {
        return _timeTableAccess.SelectAll();
    }

    public List<string> TimetableDatesFormatter(List<TimetableModel> timetables, List<EventTimetableModel> eventTimetables)
    {
        List<string> formattedDates = timetables
            .Select(t => t.HumanReadableStartTime.Date)
            .Distinct()
            .Select(date =>
            {
                bool hasEvent = timetables.Any(t => 
                t.HumanReadableStartTime.Date == date && 
                eventTimetables.Any(e => e.TimetableId == t.Id));

                return $"[{(hasEvent ? "X" : " ")}] - {date:dd/MM/yyyy}";
            })
            .ToList();
        return formattedDates;
    }

    public EventModel? TimetableHasEvent(TimetableModel timetable, List<EventTimetableModel> eventTimetables, List<EventModel> events)
    {
        EventTimetableModel? eventTimetable = eventTimetables
                                            .FirstOrDefault(e => e.TimetableId == timetable.Id);
        if (eventTimetable == null)
            return null;
        else
            return events.First(e => e.Id == eventTimetable.EventId);
    }
}