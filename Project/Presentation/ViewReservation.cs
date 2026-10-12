public class ViewReservation
{
    private readonly ReservationLogic _reservationLogic = new();

    private List<ReservationModel> _reservations = [];
    private int _cursorPosition = 0;

    public void Start()
    {

        UserModel user = new(0, "jogndoe", "sdwd", "wd");
        _reservations = _reservationLogic.GetByUser(user);

        
        Console.WriteLine("overview of your reservations:");

    }


    public void PrintTable()
    {
        Console.WriteLine("|  ReservationNumber  |   Movie title |     Date     |");

    }

}
