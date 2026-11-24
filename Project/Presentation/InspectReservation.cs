public class InspectReservation
{
    private readonly TimetableLogic _timetableLogic = new();
    private readonly SeatOrderLogic _seatOrderLogic = new();
    private readonly SeatLogic _seatLogic = new();
    private readonly MovieLogic _movieLogic = new();

    private readonly ConsumableOrderLogic _consumableOrderLogic = new();
    private readonly ConsumablesLogic _consumablesLogic = new();

    public void Start(ReservationModel reservation)
    {

        Console.Clear();

        TimetableModel timetable = _timetableLogic.Select((int)reservation.MovieShowingId);
        MovieModel movie = _movieLogic.Select((int)timetable.MovieId);


        // fetch seats for price
        double totalCost = 0;
        List<SeatModel> seats = GetSeats(reservation);

        Console.WriteLine("=== Your Reservation ===");
        Console.WriteLine($"Movie: {movie.Title}");
        Console.WriteLine($"Date: {timetable.HumanReadableStartTime:dd/MM/yyyy}");
        Console.WriteLine($"Time: {timetable.HumanReadableStartTime:HH:mm} - {timetable.HumanReadableEndtime:HH:mm}");

        Console.WriteLine("== Seats ==");
        string seatInfo = "";
        foreach (var item in seats)
        {
            var result = SeatRoomA.GetSeatInfo(item);
            if (result != null)
            {
                (int row, int col) = ((int, int))result;
                totalCost += item.SeatCost;
                switch (item.SeatType)
                {
                    case 1:
                        seatInfo += $"Normal Seat: Row {row} Seat {col}, Price {item.SeatCost:F2} \n";
                        break;
                    case 2:
                        seatInfo += $"Premium Seat: Row {row} Seat {col}, Price {item.SeatCost:F2}\n";
                        break;
                    case 3:
                        seatInfo += $"Deluxe Seat: Row {row} Seat {col}, Price {item.SeatCost:F2}\n";
                        break;
                }

            }
        }

        List<ConsumableOrderModel> consumableModels = GetConsumableOrders(reservation);
        Console.WriteLine(seatInfo);

        List<string> foods = [];
        List<string> drinks = [];

        Func<long, string, double, string> infoLine = (x, y, z) => $" {x} x {y}, Price: {z}";

        foreach (var item in consumableModels)
        {
            ConsumableModel consumable = _consumablesLogic.GetItemByOrder(item);
            totalCost+= item.UnitPrice*item.Quantity;

            switch (consumable.Category)
            {
                case "Food": foods.Add(infoLine(item.Quantity, consumable.Name, item.UnitPrice*item.Quantity)); break;
                case "Drink": drinks.Add(infoLine(item.Quantity, consumable.Name, item.UnitPrice*item.Quantity)); break;
            }
        }

        if (drinks.Count > 0)
        {
            Console.WriteLine("== Food ==");
            foreach (string food in foods)
            {
                System.Console.WriteLine(food);
            }
        }

        if (drinks.Count > 0)
        {
            Console.WriteLine("== Drinks ==");
            foreach (string drink in drinks)
            {
                System.Console.WriteLine(drink);
            }
        }

        System.Console.WriteLine($"Total Price: {totalCost:F2}");

        Console.ReadKey();
        // Console.WriteLine($"Food and Drink items: {selectedItems.Count}");
    }

    private List<SeatModel> GetSeats(ReservationModel reservation)
    {
        List<SeatOrderModel> seatOrders = _seatOrderLogic.SelectForReservation(reservation);
        List<SeatModel> seats = [];
        foreach (var seatOrder in seatOrders)
        {
            SeatModel model = _seatLogic.SelectFromSeatOrder(seatOrder);
            seats.Add(model);
        }

        return seats;
    }

    private List<ConsumableOrderModel> GetConsumableOrders(ReservationModel reservation)
    {
        return _consumableOrderLogic.GetByReservation(reservation);


    }

}