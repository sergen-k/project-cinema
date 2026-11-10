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



}