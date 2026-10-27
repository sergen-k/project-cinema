
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

}