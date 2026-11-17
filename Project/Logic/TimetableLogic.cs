public class TimetableLogic
{
    private readonly TimetableAccess _timeTableAccess = new();

    public TimetableLogic()
    {
    }

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
}