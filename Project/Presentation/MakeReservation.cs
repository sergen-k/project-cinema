public static class MakeReservation
{
     

    public static void Start()
    {
      

      // tijdelijke user id en movie id, we gaan er vanuit dat iemand is ingelogd en een movie heeft gekozen
      int user_id = 1;
      MovieModel movie = new(1,"The hobbits","","George ",18,"");
      TimetableAccess timetableAccess = new();
      List<TimetableModel> timetables =  timetableAccess.SelectByMovieId(movie);
      

      foreach (TimetableModel Timetable in timetables)
        {
            Console.WriteLine(Timetable.Showtime);
        }
      

        
    }
     // daarna heb je timetable menu waar ik alle tijden laat zien waaruit je het kan fixen.
    
}     