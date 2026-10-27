using System.Security.Cryptography;

public static class MakeReservation
{

  public static readonly ReservationLogic reservationLogic = new();

  public static void Start(MovieModel movie)
  {
    Console.Clear();
    // tijdelijke suser id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
    // int user_id = 1;
    TimetableAccess timetableAccess = new();
    List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);
    if (timetables.Count == 0)
    {
      Console.WriteLine("Currently there are no planned viewings of this movie. Try again later, press Q to return");

      while (true)
      {
        if (Console.ReadLine() == "Q") break;
      }

      return;
    }

    ConsoleMenu<DateTime> menu = new ConsoleMenu<DateTime>();


    List<DateTime> dates = timetables
    .Select(t => t.HumanReadableShowtime.Date)
    .Distinct()
    .ToList();

    int selectedDateIndex = menu.SelectOption(dates);
    DateTime selectedDate = dates[selectedDateIndex];

    List<DateTime> startTimes = timetables
        .Where(t => t.HumanReadableShowtime.Date == selectedDate.Date)
        .Select(t => t.HumanReadableShowtime)
        .ToList();

    int selectedStartTimeIndex = menu.SelectOption(startTimes);


    TimetableModel timetableModel = timetables.Where(x => x.HumanReadableShowtime == startTimes[selectedStartTimeIndex]).ToList()[0];
    Int64 userId = AccountsLogic.CurrentAccount.Id;


    long nextId = reservationLogic.getNextReservationId();
    ReservationModel reservation = new(nextId, timetableModel.Id, userId);

    reservationLogic.MakeReservation(reservation);


    // check eerst alle timetables die erin zitten daarna wil je dat je alle dagen laat zien van waar die film zich afspeelt daarna wil je de date terug halen en alleen de tijden.
    //  pakt wanneer die film zich afspeelt pol die dag.


  }
  // daarna heb je timetable menu waar ik alle tijden laat zien waaruit je het kan fixen.

}
