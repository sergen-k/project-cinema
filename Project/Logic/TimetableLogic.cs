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

    public bool HasSchedulingConflict(int roomNumber, DateTime startTime, DateTime endTime, long excludeTimetableId = 0)
    {
        List<TimetableModel> existingScreenings = _timeTableAccess.SelectByRoom(roomNumber);

        foreach (TimetableModel screening in existingScreenings)
        {
            if (screening.Id == excludeTimetableId) continue;

            bool overlaps = startTime < screening.HumanReadableEndtime && screening.HumanReadableStartTime < endTime;
            if (overlaps) return true;
        }

        return false;
    }
    public bool IsWithinAllowedSchedulingWindow(DateTime startTime)
    {
        return startTime >= DateTime.Now && startTime <= DateTime.Now.AddDays(14);
    }

    public void ScheduleScreening(TimetableModel timetable)
    {
        _timeTableAccess.Write(timetable);
    }

    public void EditScreening(TimetableModel timetable)
    {
        _timeTableAccess.Update(timetable);
    }

    public void CancelScreening(TimetableModel timetable)
    {
        _timeTableAccess.Delete(timetable);
    }

    public List<TimetableModel> SelectByMovieId(MovieModel movie)
    {
        List<TimetableModel> models = _timeTableAccess.SelectByMovieId(movie);;

        List<TimetableModel> modelsToReturn = [];

        foreach (TimetableModel model in models)
        {
            int reservationCount = _timeTableAccess.ReservationCount(model);
            switch (model.RoomNumber)
            {
                case 1: 
                    if (reservationCount < 150)
                    {
                        modelsToReturn.Add(model);
                    }
                break;
            }

        }

        return modelsToReturn;
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
