using System.Security.Cryptography.X509Certificates;

public class ViewReservation
{
    private readonly ReservationLogic _reservationLogic = new();
    private readonly TimetableLogic _timetableLogic = new();
    private readonly SeatOrderLogic _seatOrderLogic = new();
    private readonly SeatLogic _seatLogic = new();
    private readonly MovieLogic _movieLogic = new();

    private List<(Int64, string, DateTime, double)> _reservations = [];
    private int _pageNumber = 0;

    public void Start()
    {

        UserModel user = new(1, "jogndoe", "sdwd", "wd");
        FetchData(user);

        Console.WriteLine("overview of your reservations:");
        while (true)
        {
            Console.Clear();
            PrintTable();
            PrintMenu();
            string userInput = MenuInput();
            switch (userInput)
            {
                case "Q": return;
                case "+":
                    if (10 * (_pageNumber + 1) <= _reservations.Count) _pageNumber++;
                    break;
                case "-": if (_pageNumber - 1 >= 0) _pageNumber--; break;
            }

        }
    }


    public void PrintTable()
    {
        int min = Math.Min(Math.Max(0, _pageNumber * 10), _reservations.Count);
        int max = Math.Min(_pageNumber * 10 + 9, _reservations.Count);

        Console.WriteLine("ReservationNumber; Movie title; Date; Price ");

        foreach (var x in _reservations[min..max])
        {
            Console.WriteLine($"{x.Item1}; {x.Item2}; {x.Item3}; {x.Item4}");
        }
    }

    public void PrintMenu()
    {
        Console.WriteLine(" Press +/- to swap pages. Press Q to exit. ");
    }


    public string MenuInput()
    {
        string[] validOptions = { "Q", "+", "-" };
        while (true)
        {
            string userInput = Console.ReadLine()!;
            if (validOptions.Contains(userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Please enter one of the valid options: Q, +, - to continue");
            }
        }
    }

    public void FetchData(UserModel user)
    {
        List<ReservationModel> AllReservations = _reservationLogic.GetByUser(user);
        foreach (ReservationModel reservation in AllReservations)
        {
            // fetch movie
            TimetableModel timetable = _timetableLogic.Select((int)reservation.MovieShowingId);
            MovieModel movie = _movieLogic.Select((int)timetable.MovieId);


            // fetch seats for price
            double totalCost = 0;
            List<SeatOrderModel> seatOrders = _seatOrderLogic.SelectForReservation(reservation);
            List<SeatModel> seats = [];

            foreach (SeatOrderModel seatOrder in seatOrders)
            {
                SeatModel seat = _seatLogic.SelectFromSeatOrder(seatOrder);
                seats.Add(seat);
            }

            foreach (SeatModel seat in seats)
            {
                totalCost += seat.SeatCost;
            }


            _reservations.Add((reservation.Id, movie.Title, timetable.HumanReadableShowtime, totalCost));
        }
    }
}
