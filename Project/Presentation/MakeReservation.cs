public static class MakeReservation
{


  public static void Start()
  {




    // tijdelijke suser id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
    // int user_id = 1;
    MovieModel movie = new(0, "The Lord of the Rings: Fellowship of the ring", "It's a movie about a lord who wants rings", "Peter Jackson ", "Fantasy", 13, 178.0);
    TimetableAccess timetableAccess = new();
    List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);
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










    // check eerst alle timetables die erin zitten daarna wil je dat je alle dagen laat zien van waar die film zich afspeelt daarna wil je de date terug halen en alleen de tijden.
    //  pakt wanneer die film zich afspeelt pol die dag.


  }
  // daarna heb je timetable menu waar ik alle tijden laat zien waaruit je het kan fixen.
}
