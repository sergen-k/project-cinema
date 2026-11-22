
public static class MakeReservation
{

    public static readonly ReservationLogic reservationLogic = new();
    public static readonly SeatOrderLogic seatOrderLogic = new();
    public static readonly TimetableLogic timetableLogic = new();
    public static readonly ConsumableOrderLogic ConsumableOrderLogic = new();

    public static void Start(MovieModel movie)
    {

        // tijdelijke suser id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
        Console.Clear();
        List<TimetableModel> timetables = timetableLogic.SelectByMovieId(movie);
        if (timetables.Count == 0)
        {
            Console.WriteLine("Currently there are no planned viewings of this movie. Try again later, press enter to return");
            Console.ReadLine();
            return;
        }

        ConsoleMenu<int> menu = new();

        // makes sure there are no duplicates and that it get turned into a list
        TimetableModel selectedTimetable;
        string selectedDateString;
        string selectedTimeString;

        while (true)
        {
            List<DateTime> dates = timetables
                .Select(t => t.HumanReadableStartTime.Date)
                .Distinct()
                .ToList();

            List<string> formatteddates = dates
            .Select(d => d.ToString("dd/MM/yyyy"))
            .ToList();

            // date selection screen
            menu.ResetOptions();
            foreach (string formatteddate in formatteddates)
                menu.AddOption(new SelectOption<int>(formatteddate, formatteddates.IndexOf(formatteddate)));
            int selectedDateIndex = menu.SelectOption();
            if (selectedDateIndex == -1) {return;}
            DateTime selectedDate = dates[selectedDateIndex];

            // date selection screen

            // Get all timetables for the selected date
            List<TimetableModel> dayTimetables = timetables
                .Where(t => t.HumanReadableStartTime.Date == selectedDate.Date)
                .ToList();

            // Format start and end times
            List<string> formattedTimes = dayTimetables
                .Select(t => $"room: {t.RoomNumber}, {t.HumanReadableStartTime:HH:mm} - {t.HumanReadableEndtime:HH:mm} ")
                .ToList();

            // Timetable selection screen

            menu.ResetOptions();
            foreach (string formattedTime in formattedTimes)
                menu.AddOption(new SelectOption<int>(formattedTime, formattedTimes.IndexOf(formattedTime)));
            int selectedStartTimeIndex = menu.SelectOption();
            if (selectedStartTimeIndex == -1) {return;}

            // if (selectedStartTimeIndex == -1)
            // {
            //     break;
            // }

            var timeslot = dayTimetables[selectedStartTimeIndex];

            bool hasDuplicate = reservationLogic.checkDuplicateReservation(timeslot, AccountsLogic.CurrentAccount);

            if (hasDuplicate)
            {
                Console.WriteLine("You already have a reservation for this timeslot, please select a different time");
                Console.ReadLine();
            }
            else
            {
                // Get the selected timetable
                selectedTimetable = dayTimetables[selectedStartTimeIndex];
                selectedTimeString = formattedTimes[selectedStartTimeIndex];
                selectedDateString = formatteddates[selectedDateIndex];
                break;
            }
        }

        Console.Clear();
        Console.WriteLine("How many seats would you like to reserve for?");
        int numberOfSeats = AmountOfSeats();

        Console.Clear();

        MovieRoom movieRoom = selectedTimetable.RoomNumber switch
        {
           1 => new SeatRoomA(),
           2 => new SeatRoomB(),
        };

        SeatSelection seatSelection = new(movieRoom);
        List<SeatModel> toReserveSeats = seatSelection.Start(selectedTimetable, numberOfSeats);
        Console.Clear();

        double totalPrice = toReserveSeats.Sum(x => x.SeatCost);
        // Timetable selection screen

        Console.CursorVisible = true;

        // Select Foods and Drinks
        List<ConsumableOrderModel> selectedItems = FoodsAndDrinks.Start();
        Console.Clear();

        Console.WriteLine("=== Confirm Your Reservation ===");
        Console.WriteLine($"Movie: {movie.Title}");
        Console.WriteLine($"Date: {selectedTimetable.HumanReadableStartTime:dd/MM/yyyy}");
        Console.WriteLine($"Time: {selectedTimetable.HumanReadableStartTime:HH:mm} - {selectedTimetable.HumanReadableEndtime:HH:mm}");

        Console.WriteLine("== Seats == ");
       string seatInfo = ""; 
        foreach (var item in toReserveSeats)
        {
            var result = movieRoom.GetSeatInfo(item);
            if (result != null)
            {
                (int row, int col) = ((int, int))result;
                switch(item.SeatType)
                {
                    case 1: 
                        seatInfo += $"normal seat: row {row} seat {col}, price {item.SeatCost} \n";
                    break;
                    case 2: 
                        seatInfo += $"premium seat: row {row} seat {col}, price {item.SeatCost}\n";
                    break;
                    case 3: 
                        seatInfo += $"deluxe seat: row {row} seat {col}, price {item.SeatCost}\n";
                    break;
                }
                
            }
        }

        seatInfo += $"total price {totalPrice}";
        Console.WriteLine(seatInfo);


        Console.WriteLine($"Food and Drink items: {selectedItems.Count}");

        Console.WriteLine();
        Console.WriteLine("Do you want to confirm your reservation? (Y/N)");

        var userAnswer = Console.ReadKey().Key;
        if (userAnswer == ConsoleKey.Y)
        {
            // says yes finish reservation  if confirmed send them back to the movie catalogue,
            Console.Clear();
            Console.WriteLine($"Thank you for reserving the movie \n{movie.Title} \nat {selectedTimetable.HumanReadableStartTime:HH:mm} on {selectedTimetable.HumanReadableEndtime:dd/MM/yyyy}");
            ReservationModel reservation = new(0, selectedTimetable.Id, AccountsLogic.CurrentAccount.Id, numberOfSeats);
            
            long reservationId = reservationLogic.WriteAndGetId(reservation);
            ReservationModel reservationRefreshed = new(reservationId, selectedTimetable.Id, AccountsLogic.CurrentAccount.Id, numberOfSeats);

            foreach (SeatModel seat in toReserveSeats)
            {

                seatOrderLogic.Write(reservationRefreshed, seat);
            }

            ConsumableOrderLogic.SaveForReservation(reservationId, selectedItems);


            Console.WriteLine("Press enter to return!");
            Console.ReadLine();
            Console.CursorVisible = false;

        }

        else
        {
            Console.CursorVisible = false;
            return;
        }


    }


    public static int AmountOfSeats()
    {
        int attempts = 0;

        while (true)
        {
            if (attempts > 0) Console.WriteLine("Please enter a valid number of at least 1.");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int val) && val >= 1) return val;
            attempts++;
        }
    }

}
