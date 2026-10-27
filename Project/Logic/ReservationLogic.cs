
public class ReservationLogic
{
    private readonly ReservationAccess _reservationAccess = new();


    public List<ReservationModel> GetByUser(UserModel user)
    {
        if (user is null)
        {
            return [];
        }
        
        return _reservationAccess.GetByUser(user);
    }


    public long getNextReservationId()
    {
        return _reservationAccess.getNextReservationId();
    }
    public void MakeReservation(ReservationModel reservation)
    {
        _reservationAccess.Write(reservation);
    }

}