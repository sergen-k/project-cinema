class SeatLogic
{
    private readonly SeatAccess _SeatAccess = new();

    public SeatLogic()
    {
    }

    public SeatModel Select(int id)
    {
        return _SeatAccess.SelectAllById(id);
    }

    public SeatModel SelectFromSeatOrder(SeatOrderModel model)
    {
        return _SeatAccess.SelectFromSeatOrder(model);
    }

    public List<SeatModel> SelectByRoom(int model)
    {
        return _SeatAccess.SelectByRoom(model);
    }


    public List<SeatModel> SelectOrderedSeatsForViewing(TimetableModel model)
    {
        return _SeatAccess.SelectOrderedSeatsForViewing(model);
    }



}