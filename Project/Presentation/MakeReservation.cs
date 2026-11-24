
using System.Globalization;

public static class MakeReservation
{

    public static readonly ReservationLogic reservationLogic = new();
    public static readonly SeatOrderLogic seatOrderLogic = new();
    public static readonly TimetableLogic timetableLogic = new();
    public static readonly EventLogic eventLogic = new();
    public static readonly ConsumableOrderLogic ConsumableOrderLogic = new();
    private static List<OptionBase> _options = new();

    public static void Start(MovieModel movie, List<EventTimetableModel> eventTimetables)
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
    DatePick:
        List<OptionBase> options = new();
        ConsoleMenu menu = new();
        foreach (var timetable in timetableLogic.TimetableDatesFormatter(timetables, eventTimetables))
        {
            options.Add(new SelectOption(timetable));
        }
        menu.SetOptions(options);
        int selectedOption = menu.SelectOption();
        if (selectedOption == -1) { return; }
        DateTime selectedDate = timetables.Select(t => t.HumanReadableStartTime.Date).Distinct().ToList()[selectedOption];

        List<TimetableModel> dayTimetables = timetables
            .Where(t => t.HumanReadableStartTime.Date == selectedDate.Date)
            .ToList();

        // Format start and end times
        List<EventModel> eventModels = eventLogic.SelectAll();
        List<string> formattedTimes = dayTimetables
            .Select(t => $"[{(timetableLogic.TimetableHasEvent(t, eventTimetables, eventModels) == null ? ' ' : 'X')}] - {t.HumanReadableStartTime:HH:mm} - {t.HumanReadableEndtime:HH:mm} ")
            .ToList();

        // Timetable selection screen
        options.Clear();
        for (int i = 0; i < dayTimetables.Count; i++)
        {
            options.Add(new SelectOption(formattedTimes[i]));
            if (formattedTimes[i][1] == 'X')
            {
                EventModel? linkedEvent = timetableLogic.TimetableHasEvent(dayTimetables[i], eventTimetables, eventModels);
                options.Add(new InfoOption($"└> Event Available - {linkedEvent.Name}"));
            }
        }
        menu.SetOptions(options);
        int selectedTime = menu.SelectOption();
        if (selectedTime == -1) { goto DatePick; }

        TimetableModel timeslot = dayTimetables[selectedTime];
        EventModel? selectedEvent = timetableLogic.TimetableHasEvent(timeslot, eventTimetables, eventModels);

        bool hasDuplicate = reservationLogic.checkDuplicateReservation(timeslot, UserLogic.CurrentAccount);
        if (hasDuplicate)
        {
            Console.WriteLine("You already have a reservation for this timeslot, please select a different time");
            Console.ReadLine();
            goto DatePick;
        }

        if (selectedEvent != null)
        {
            _options.Clear();
            _options.Add(new InfoOption("This timeslot has an event taking place during the screening!"));
            _options.Add(new InfoOption(""));
            _options.Add(new InfoOption($"{selectedEvent.Name}"));
            _options.Add(new InfoOption($"- {selectedEvent.Description}"));
            _options.Add(new InfoOption($"Additional fee per seat: €{selectedEvent.Cost:F2}"));
            _options.Add(new InfoOption(""));
            _options.Add(new InfoOption("Are you sure you want to book reservation?"));
            _options.Add(new SelectOption("Continue"));
            menu.SetOptions(_options);
            if (menu.SelectOption() == -1) { goto DatePick; }
        }

    SeatOrdering:
        Console.Clear();
        Console.WriteLine("How many seats would you like to reserve for?");
        int numberOfSeats = AmountOfSeats();

        Console.Clear();
        SeatSelection seatSelection = new();
        List<SeatModel> toReserveSeats = seatSelection.Start(timeslot, numberOfSeats, selectedEvent);
        if (toReserveSeats == null) { return; }
        Console.Clear();

        double totalPrice = toReserveSeats.Sum(x => x.SeatCost);
        // Timetable selection screen

        Console.CursorVisible = true;

        // Select Foods and Drinks
        List<ConsumableOrderModel> selectedItems = FoodsAndDrinks.Start();
        if (selectedItems != null && selectedItems.Count > 0 && selectedItems[0].Id == -1)
        {
            goto SeatOrdering;
        }

        double foodTotal = 0;

        if (selectedItems != null)
        {
            foreach (ConsumableOrderModel item in selectedItems)
            {
                foodTotal += item.Quantity * item.UnitPrice;
            }
        }

        double totalAmount = totalPrice + foodTotal;
        if (selectedEvent != null)
            totalPrice += toReserveSeats.Count * selectedEvent.Cost;
        Console.Clear();

        Console.WriteLine("=== Confirm Your Reservation ===");
        Console.WriteLine($"Movie: {movie.Title}");
        Console.WriteLine($"Date: {timeslot.HumanReadableStartTime:dd/MM/yyyy}");
        Console.WriteLine($"Time: {timeslot.HumanReadableStartTime:HH:mm} - {timeslot.HumanReadableEndtime:HH:mm}");

        Console.WriteLine("== Seats == ");
        string seatInfo = "";
        foreach (var item in toReserveSeats)
        {
            var result = SeatRoomA.GetSeatInfo(item);
            if (result != null)
            {
                (int row, int col) = ((int, int))result;
                switch (item.SeatType)
                {
                    case 1:
                        seatInfo += $"Normal Seat: Row {row}, Seat {col}, Price: €{item.SeatCost:F2} \n";
                        break;
                    case 2:
                        seatInfo += $"Premium Seat: Row {row}, Seat {col}, Price: €{item.SeatCost:F2}\n";
                        break;
                    case 3:
                        seatInfo += $"Deluxe Seat: Row {row}, Seat {col}, Price: €{item.SeatCost:F2}\n";
                        break;
                }

            }
        }
        if (selectedEvent != null)
            seatInfo += $"{toReserveSeats.Count} x {selectedEvent.Name}, Price: €{selectedEvent.Cost:F2}\n";

        Console.WriteLine(seatInfo);
        Console.WriteLine("=== Foods and Drinks ===");
        Console.WriteLine($"Foods and Drinks: €{foodTotal:F2}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Total Amount: €{totalAmount+foodTotal:F2}");

        Console.WriteLine();
        Console.WriteLine("Do you want to confirm your reservation? (Y/N)");

        var userAnswer = Console.ReadKey().Key;

        if (userAnswer == ConsoleKey.Y)
        {
            Console.Clear();
            // Console.WriteLine($"Thank you for reserving the movie \n{movie.Title} \nat {timeslot.HumanReadableStartTime:HH:mm} on {timeslot.HumanReadableEndtime:dd/MM/yyyy}");
            // ReservationModel reservation = new(0, timeslot.Id, UserLogic.CurrentAccount.Id, numberOfSeats);

            // Start simulated payment
            string? paymentNumber = ProcessPayment.Start(totalAmount);

            if (paymentNumber == null)
            {
                Console.WriteLine("Payment cancelled.");
                Console.WriteLine("Reservation was not saved.");
                Console.CursorVisible = false;
                return;
            }

            // Save reservation
            ReservationModel reservation = new(0, timeslot.Id, UserLogic.CurrentAccount.Id, numberOfSeats);

            long reservationId = reservationLogic.WriteAndGetId(reservation);
            ReservationModel reservationRefreshed = new(reservationId, timeslot.Id, UserLogic.CurrentAccount.Id, numberOfSeats);

            foreach (SeatModel seat in toReserveSeats)
            {
                seatOrderLogic.Write(reservationRefreshed, seat);
            }
            if (selectedItems != null)
                ConsumableOrderLogic.SaveForReservation(reservationId, selectedItems);

            // Display payment success
            Console.Clear();

            ProcessPayment.ShowPaymentSuccess(paymentNumber, totalAmount);

            Console.WriteLine();
            Console.WriteLine("Press enter to return.");
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
            if (attempts > 0) Console.WriteLine("Please enter a valid number of 1 - 20");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int val) && val >= 1 && val <= 20) return val;
            attempts++;
        }
    }

}
