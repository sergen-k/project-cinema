
public static class MakeReservation
{
    public static readonly ReservationLogic reservationLogic = new();
    public static readonly SeatOrderLogic seatOrderLogic = new();

    public static void Start(MovieModel movie)
    {
        Console.Clear();

        TimetableAccess timetableAccess = new();
        List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);

        if (timetables.Count == 0)
        {
            Console.WriteLine("Currently there are no planned viewings of this movie.");
            Console.WriteLine("Press enter to return.");
            Console.ReadLine();
            return;
        }

        ConsoleMenu<int> menu = new();

        TimetableModel selectedTimetable;

        while (true)
        {
            List<DateTime> dates = timetables
                .Select(t => t.HumanReadableStartTime.Date)
                .Distinct()
                .ToList();

            List<string> formattedDates = dates
                .Select(d => d.ToString("dd/MM/yyyy"))
                .ToList();

            menu.ResetOptions();

            foreach (string formattedDate in formattedDates)
            {
                menu.AddOption(new SelectOption<int>(
                    formattedDate,
                    formattedDates.IndexOf(formattedDate)));
            }

            int selectedDateIndex = menu.SelectOption();
            DateTime selectedDate = dates[selectedDateIndex];

            List<TimetableModel> dayTimetables = timetables
                .Where(t => t.HumanReadableStartTime.Date == selectedDate.Date)
                .ToList();

            List<string> formattedTimes = dayTimetables
                .Select(t => $"{t.HumanReadableStartTime:HH:mm} - {t.HumanReadableEndtime:HH:mm}")
                .ToList();

            menu.ResetOptions();

            foreach (string formattedTime in formattedTimes)
            {
                menu.AddOption(new SelectOption<int>(
                    formattedTime,
                    formattedTimes.IndexOf(formattedTime)));
            }

            int selectedStartTimeIndex = menu.SelectOption();

            TimetableModel timeslot = dayTimetables[selectedStartTimeIndex];

            bool hasDuplicate = reservationLogic.checkDuplicateReservation(
                timeslot, AccountsLogic.CurrentAccount);

            if (hasDuplicate)
            {
                Console.WriteLine("You already have a reservation for this timeslot.");
                Console.WriteLine("Please select a different time.");
                Console.ReadLine();
            }
            else
            {
                selectedTimetable = timeslot;
                break;
            }
        }

        // Select seats
        Console.Clear();
        Console.WriteLine("How many seats would you like to reserve?");
        int numberOfSeats = AmountOfSeats();

        Console.Clear();

        SeatSelection seatSelection = new();
        List<SeatModel> toReserveSeats =
            seatSelection.Start(selectedTimetable, numberOfSeats);

        if (toReserveSeats == null || toReserveSeats.Count != numberOfSeats)
        {
            Console.WriteLine("Seat selection was not completed.");
            Console.CursorVisible = false;
            return;
        }

        // Calculate ticket total
        double ticketTotal = 0;

        foreach (SeatModel seat in toReserveSeats)
        {
            ticketTotal += seat.SeatCost;
        }

        // Select foods and drinks
        Console.Clear();
        Console.CursorVisible = true;

        List<ConsumableOrderModel> selectedItems = FoodsAndDrinks.Start();

        // Calculate foods and drinks total
        double foodTotal = 0;

        foreach (ConsumableOrderModel item in selectedItems)
        {
            foodTotal += item.Quantity * item.UnitPrice;
        }

        // Calculate final total
        double totalAmount = ticketTotal + foodTotal;

        // Show reservation summary
        Console.Clear();

        Console.WriteLine("=== CONFIRM YOUR RESERVATION ===");
        Console.WriteLine();

        Console.WriteLine($"Movie: {movie.Title}");
        Console.WriteLine($"Date: {selectedTimetable.HumanReadableStartTime:dd/MM/yyyy}");
        Console.WriteLine($"Time: {selectedTimetable.HumanReadableStartTime:HH:mm} - {selectedTimetable.HumanReadableEndtime:HH:mm}");
        Console.WriteLine($"Seats: {numberOfSeats}");

        Console.WriteLine();
        Console.WriteLine($"Ticket total: €{ticketTotal:F2}");

        Console.WriteLine();
        Console.WriteLine("=== FOODS AND DRINKS ===");

        if (selectedItems.Count == 0)
        {
            Console.WriteLine("No foods or drinks selected.");
        }
        else
        {
            ConsumablesLogic consumablesLogic = new();
            List<ConsumableModel> products = consumablesLogic.GetAll();

            foreach (ConsumableOrderModel order in selectedItems)
            {
                foreach (ConsumableModel product in products)
                {
                    if (order.ConsumableId == product.Id)
                    {
                        double subtotal = order.Quantity * order.UnitPrice;

                        Console.WriteLine(
                            $"{order.Quantity} x {product.Name} - €{subtotal:F2}");

                        break;
                    }
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Foods and Drinks total: €{foodTotal:F2}");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"TOTAL AMOUNT: €{totalAmount:F2}");

        Console.WriteLine();
        Console.WriteLine("Do you want to confirm your reservation? (Y/N)");

        ConsoleKey userAnswer = Console.ReadKey(true).Key;

        if (userAnswer != ConsoleKey.Y)
        {
            Console.WriteLine("Reservation cancelled.");
            Console.CursorVisible = false;
            return;
        }

        // Validate payment details before saving reservation
        Console.Clear();

        string? paymentMethod = ProcessPayment.Start(totalAmount);

        if (paymentMethod == null)
        {
            Console.WriteLine("Reservation was not saved.");
            Console.CursorVisible = false;
            return;
        }

        // Create reservation
        ReservationModel reservation = new(
            0,
            selectedTimetable.Id,
            AccountsLogic.CurrentAccount.Id,
            numberOfSeats
        );

        ReservationAccess reservationAccess = new();

        long reservationId = reservationAccess.WriteAndGetId(reservation);

        ReservationModel reservationRefreshed = new(
            reservationId,
            selectedTimetable.Id,
            AccountsLogic.CurrentAccount.Id,
            numberOfSeats
        );

        // Save selected seats
        foreach (SeatModel seat in toReserveSeats)
        {
            seatOrderLogic.Write(reservationRefreshed, seat);
        }

        // Save selected foods and drinks
        ConsumableOrderAccess consumableOrderAccess = new();

        consumableOrderAccess.SaveForReservation(
            reservationId, selectedItems);

        // Save payment and show payment number
        Console.Clear();

        bool paymentSuccess = ProcessPayment.Finish(
            reservationId, paymentMethod, totalAmount);

        if (!paymentSuccess)
        {
            Console.WriteLine("Payment was not completed.");
            Console.WriteLine("The reservation may already be saved.");
            Console.WriteLine("Please contact staff before trying again.");
        }

        Console.WriteLine();
        Console.WriteLine("Press enter to return.");
        Console.ReadLine();

        Console.CursorVisible = false;
    }

    public static int AmountOfSeats()
    {
        int attempts = 0;

        while (true)
        {
            if (attempts > 0)
            {
                Console.WriteLine("Please enter a valid number.");
            }

            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int val) && val > 0)
            {
                return val;
            }

            attempts++;
        }
    }
}
