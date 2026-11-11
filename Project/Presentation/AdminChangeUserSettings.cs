using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;

public static class AdminChangeUserSettings{
    
  public static List<string> MainUserOptions = ["Change user roles","Change user info", "Return"];

  


  public static void ChangeUserRoles()
  {
    List<string> userRoleViewingOptions = ["Show all users","Select user by email", "Return"];
    while (true)
    {
      ConsoleMenu userRoleOptions = new();
      userRoleOptions.PopulateOptions(userRoleViewingOptions);
      string selectedOption = userRoleOptions.SelectOption();

      switch (selectedOption)
      {
        case "Return":
          return;
        case "Show all users":
        // print out a list with all the users.
        UserAccess userAccess = new();
        List<UserModel> allUsers = userAccess.SelectAll();
          while (true)
          {
            // converts the user models and the first name and last name and role into a list
            List<string> userOptions = allUsers
            .Select(user => $"{user.FirstName} {user.LastName} - {user.Role}")
            .ToList();

           ConsoleMenu allUserOptions = new();
           allUserOptions.AddOption(new SelectOption("Return"));
           allUserOptions.PopulateOptions(userOptions);
           string selectedUserOption = allUserOptions.SelectOption();

            if(selectedUserOption == "Return")
            {
              break;
            } 
            // todo put all the roles into a list and from there let them select what role they want the user to be in
            List<Roles> roleList = Enum.GetValues(typeof(Roles))
                        .Cast<Roles>()
                        .ToList();
            ConsoleMenu<Roles> allRoleOptions = new();
            foreach(var role in roleList)
            {
              allRoleOptions.AddOption(new SelectOption<Roles>(role.ToString(), role));
            }
            Roles selectedRole = allRoleOptions.SelectOption();
            
          } 
          break;
         case "Select user by email":
         break;
       
      }
    }
  }



public static void Start()
    {


        while (true)
        {

                       
            ConsoleMenu userOptions = new();
            userOptions.PopulateOptions(MainUserOptions);
            string selectedOption = userOptions.SelectOption();

      // pressed escapekey 

      switch (selectedOption)
      {
        // pressed escape key
        case "Return":
        return;

        case "Change user roles":
        // change user roles 
        ChangeUserRoles();
        break;

        case "Change user info":
        //change user info
        break;
      }
        
        
             
        }
        }
       
}