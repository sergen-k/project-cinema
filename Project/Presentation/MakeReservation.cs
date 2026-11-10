
public static class MakeReservation
{

    public static readonly ReservationLogic reservationLogic = new();
    public static readonly SeatOrderLogic seatOrderLogic = new();

    public static void Start(MovieModel movie)
    {

        // tijdelijke suser id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
        Console.Clear();
        TimetableAccess timetableAccess = new();
        List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);
        if (timetables.Count == 0)
        {
            Console.WriteLine("Currently there are no planned viewings of this movie. Try again later, press enter to return");
            Console.ReadLine();
            return;
        }

        ConsoleMenu menu = new();

        // makes sure there are no duplicates and that it get turned into a list

        List<DateTime> dates = timetables
        .Select(t => t.HumanReadableStartTime.Date)
        .Distinct()
        .ToList();

        List<string> formatteddates = dates
        .Select(d => d.ToString("dd/MM/yyyy"))
        .ToList();

        // date selection screen
        int selectedDateIndex = formatteddates.IndexOf(menu.SelectOption(formatteddates));
        DateTime selectedDate = dates[selectedDateIndex];

        // Get all timetables for the selected date
        List<TimetableModel> dayTimetables = timetables
            .Where(t => t.HumanReadableStartTime.Date == selectedDate.Date)
            .ToList();

        // Format start and end times
        List<string> formattedTimes = dayTimetables
            .Select(t => $"{t.HumanReadableStartTime:HH:mm} - {t.HumanReadableEndtime:HH:mm}")
            .ToList();

        // Timetable selection screen
        int selectedStartTimeIndex = formattedTimes.IndexOf(menu.SelectOption(formattedTimes));

        // if (selectedStartTimeIndex == -1)
        // {
        //     break;
        // }


        // Get the selected timetable
        TimetableModel selectedTimetable = dayTimetables[selectedStartTimeIndex];
        Console.Clear();
        Console.WriteLine("How many seats would you like to reserve for?");
        int numberOfSeats = AmountOfSeats();

        Console.Clear();
        SeatSelection seatSelection = new();
        List<SeatModel> toReserveSeats =  seatSelection.Start(selectedTimetable, numberOfSeats);
        Console.Clear();

        Console.CursorVisible = true;
        Console.WriteLine("Are you sure you wanna reserve at this time? (Y/N)");
        var userAnswer = Console.ReadKey().Key;
        if (userAnswer == ConsoleKey.Y)
        {
            // says yes finish reservation  if confirmed send them back to the movie catalogue,
            Console.Clear();
            Console.WriteLine($"Thank you for reserving the movie \n{movie.Title} \nat {formattedTimes[selectedStartTimeIndex]} on {formatteddates[selectedDateIndex]}");
            ReservationModel reservation = new(0, selectedTimetable.Id, AccountsLogic.CurrentAccount.Id, numberOfSeats);
            ReservationAccess reservationAccess = new();

            reservationAccess.Write(reservation);
            ReservationModel reservationRefreshed = reservationLogic.GetByUser(AccountsLogic.CurrentAccount).Last();
            
            foreach (SeatModel seat in toReserveSeats)
            {
                seatOrderLogic.Write(reservationRefreshed, seat);
            }


            Console.WriteLine("Press enter to return!");
            Console.ReadLine();
            Console.CursorVisible = false;
            //Moviecatalogue.Start();

        }

        else
        {
            //says no return back to info page of the movie 
            //  MovieInfo.Start(movie);
            Console.CursorVisible = false;
            return;
        }


    }


    public static int AmountOfSeats()
    {   
        int attempts = 0;

        while (true)
        {
            if(attempts >0) Console.WriteLine("Please enter a valid number.");
            string userInput = Console.ReadLine();
            if(int.TryParse(userInput, out int val)) return val;
            attempts++;
        }
    }

}
