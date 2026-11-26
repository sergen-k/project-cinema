public class Finances
{
    private static List<OptionBase> _options =  new();

    


    public static void Start()
    { 
    _options.Add(new SelectOption("User Finances"));
    _options.Add(new SelectOption("Movie Finances"));

      ConsoleMenu Menu = new();
     
      Menu.SetOptions(_options);
      Menu.SelectOption();
     _options.Clear();
    
   //TODO je wilt dat een finance rol op deze pagina kan komen 
           
   //TODO als je een user finance gaat pakken moet je alle tables checken 

    }





  public static void UserFinances()
  {
     // alles van de user finances moet hierin en dan de checks doe je bij de logic. 

     // pak eerst alle users, 

   
  }


 public static void MovieFinances()
  {
     // pak all de movies waar een timetable in zit en vanuit bestaat. 
     ConsoleMenu timetablemenu = new();
     TimetableLogic timetableLogic = new();
     List<TimetableModel> AllTimeTables = timetableLogic.SelectAll();
     _options.Clear();

     foreach( TimetableModel AllTimetable in AllTimeTables)
    {
      _options.Add(new SelectOption(AllTimetable.MovieId.ToString()));
    }
     
     

 
     

   
   
      

  }

}