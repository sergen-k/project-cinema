
    public class SeatOrderModel {
    public SeatOrderModel(long id, long reservationId, long seatId)
    {
        Id = id;
        ReservationId = reservationId;
        SeatId = seatId;
    }

    public  Int64   Id {get; set;}
    public  Int64   ReservationId {get; set;}
    public  Int64   SeatId {get; set;}
 }