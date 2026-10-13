class TimetableLogic
{
    private readonly TimetableAccess _timeTableAccess = new();

    public TimetableLogic()
    {
    }

    public TimetableModel select(int id)
    {
        return _timeTableAccess.Select(id);
    }
}