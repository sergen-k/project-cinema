public class ConsumableOrderLogic
{
    private static readonly ConsumableOrderAccess consumableOrderAccess = new();

    public void SaveForReservation(long reservationId, List<ConsumableOrderModel> selectedItems)
    {   consumableOrderAccess.SaveForReservation(reservationId, selectedItems);
        
    }

    public  List<ConsumableOrderModel> GetByReservation(ReservationModel reservation)
    {
       return consumableOrderAccess.GetByReservation(reservation);
    }
}