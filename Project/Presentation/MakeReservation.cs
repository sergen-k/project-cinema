public static class MakeReservation
{


  public static void Start()
  {

    // tijdelijke suser id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
    int user_id = 1;
    MovieModel movie = new(0, "The Lord of the Rings: Fellowship of the ring", "It's a movie about a lord who wants rings", "Peter Jackson ", "Fantasy", 13, 178.0);
    TimetableAccess timetableAccess = new();
    List<TimetableModel> timetables = timetableAccess.SelectByMovieId(movie);
    ConsoleMenu<string> menu = new ConsoleMenu<string>();

    while (true)
    {
      // makes sure there are no duplicates and that it get turned into a list

      List<DateTime> dates = timetables
      .Select(t => t.HumanReadableStarttime.Date)
      .Distinct()
      .ToList();

      List<string> formatteddates = dates
      .Select(d => d.ToString("dd/MM/yyyy"))
      .ToList();

      // date selection screen
      int selectedDateIndex = menu.SelectOption(formatteddates);
      DateTime selectedDate = dates[selectedDateIndex];

     while (true)
{
    // Get all timetables for the selected date
    List<TimetableModel> dayTimetables = timetables
        .Where(t => t.HumanReadableStarttime.Date == selectedDate.Date)
        .ToList();

    // Format start and end times
    List<string> formattedTimes = dayTimetables
        .Select(t => $"{t.HumanReadableStarttime:HH:mm} - {t.HumanReadableEndtime:HH:mm}")
        .ToList();

    // Timetable selection screen
    int selectedStartTimeIndex = menu.SelectOption(formattedTimes);

        if (selectedStartTimeIndex == -1)
    {
        break;
    }

   
    // Get the selected timetable
    TimetableModel selectedTimetable = dayTimetables[selectedStartTimeIndex];

    Console.WriteLine("Are u Sure u want to Reservate this time Y-N");
    var userAnswer = Console.ReadKey(true).Key;
    if(userAnswer == ConsoleKey.Y)
        {
          // says yes finish reservation  if confirmed send them back to the movie catalogue,
          Console.WriteLine($"Thank you for reservating the movie {movie.Title} at {formattedTimes[selectedStartTimeIndex]} on {formatteddates[selectedDateIndex]}");
          Console.ReadLine();
        //   MovieCatalogue.Start();
           
        }

    else if(userAnswer == ConsoleKey.N){
          //says no return back to info page of the movie 
        //  MovieInfo.Start(movie);
            return;
        }


}


    }
  }
  // daarna heb je timetable menu waar ik alle tijden laat zien waaruit je het kan fixen.
}
