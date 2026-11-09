public static class AdminChangeUserSettings{
    
  public static List<string> MainUserOptions = ["Change user roles","test1","test2"];

  



public static void Start()
    {


        while (true)
        {
             ConsoleMenu<string> userOptions = new();
             userOptions.SelectOption(MainUserOptions);
             
        }
        }
       
}