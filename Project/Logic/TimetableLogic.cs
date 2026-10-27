class TimetableLogic
{
    private readonly TimetableAccess _timeTableAccess = new();

    public TimetableLogic()
    {
    }

    public TimetableModel Select(int id)
    {
        return _timeTableAccess.Select(id);
    }
}