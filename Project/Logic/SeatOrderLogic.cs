class SeatOrderLogic
{
    private readonly SeatOrderAccess _SeatOrderAccess = new();

    public SeatOrderLogic()
    {
    }

    public SeatOrderModel Select(int id)
    {
        return _SeatOrderAccess.Select(id);
    }

    public List<SeatOrderModel> SelectForReservation(ReservationModel reservation)
    {
        return _SeatOrderAccess.SelectForReservation(reservation);
    }

}