public class SeatOrderLogic
{
    private readonly SeatOrderAccess _SeatOrderAccess = new();

    public SeatOrderLogic()
    {
    }

    public void Write(ReservationModel reservation, SeatModel seat)
    {
        Int64 nextId = _SeatOrderAccess.GetNextSeatOrderId();
        SeatOrderModel toAdd = new(nextId, reservation.Id, seat.Id);
        _SeatOrderAccess.Write(toAdd);
    }

    public SeatOrderModel Select(int id)
    {
        return _SeatOrderAccess.SelectAllById(id);
    }

    public List<SeatOrderModel> SelectForReservation(ReservationModel reservation)
    {
        return _SeatOrderAccess.SelectForReservation(reservation);
    }

}