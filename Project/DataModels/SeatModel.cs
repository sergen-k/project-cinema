
    public class SeatModel {
    public SeatModel(long id, long roomNumber, long seatType)
    {
        Id = id;
        RoomNumber = roomNumber;
        SeatType = seatType;
    }

    public  Int64   Id {get; set;}
    public  Int64   RoomNumber {get; set;}
    public  Int64   SeatType {get; set;}
 }