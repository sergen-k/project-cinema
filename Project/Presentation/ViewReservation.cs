using System.Security.Cryptography.X509Certificates;

public class ViewReservation
{
    private readonly ReservationLogic _reservationLogic = new();
    private readonly TimetableLogic _timetableLogic= new();
    
    private readonly MovieLogic _movieLogic= new();

    private List<ReservationModel> _reservations = [];
    private int _pageNumber = 0;

    public void Start()
    {

        UserModel user = new(1, "jogndoe", "sdwd", "wd");
        _reservations = _reservationLogic.GetByUser(user);

        Console.WriteLine("overview of your reservations:");
        while (true)
        {
            Console.Clear();
            PrintTable();
            PrintMenu();
            string userInput = MenuInput();
            switch (userInput) {
                case "Q": return;
                case "+": 
                    if (10*_pageNumber <= _reservations.Count+10) _pageNumber++;
                    break;
                case "-": if (_pageNumber >0 )_pageNumber--; break;
            }

        } 
    }


    public void PrintTable()
    {
        int min = Math.Min(Math.Max(0, _pageNumber * 10), _reservations.Count);
        int max = Math.Min(_pageNumber * 10 + 9, _reservations.Count);

        List<ReservationModel> sliced = _reservations[min..max];
        Console.WriteLine("ReservationNumber; Movie title; Date; Price ");

        foreach (ReservationModel reservation in sliced)
        {
            TimetableModel timetable = _timetableLogic.Select((int) reservation.MovieShowingId);
            MovieModel movie = _movieLogic.Select((int) timetable.MovieId);

            // TODO: Add price
            Console.WriteLine($"{reservation.Id}; {movie.Name}; {timetable.HumanReadableShowtime}");

        }
    }

    public void PrintMenu()
    {
        Console.WriteLine(" Press +/- to swap pages. Press Q to exit. ");
    }


    public string MenuInput()
    {
        string[] validOptions = {"Q", "+", "-"};
        while (true)
        {
            string userInput = Console.ReadLine()!;
            if (validOptions.Contains(userInput))
            {
                return userInput;
            } else
            {
                Console.WriteLine("Please enter one of the valid options: Q, +, - to continue");
            }
        }
    }

}
