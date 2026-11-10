using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;

public static class AdminChangeUserSettings{
    
  public static List<string> MainUserOptions = ["Change user roles","Change user info","test2"];

  


  public static void ChangeUserRoles()
  {
    List<string> userRoleViewingOptions = ["show all users","select user by email"];
    while (true)
    {
      ConsoleMenu<string> userRoleOptions = new();
      int selectedOption = userRoleOptions.SelectOption(userRoleViewingOptions);

      switch (selectedOption)
      {
        
        case -1:
        return;

        case 0:
        // print out a list with all the users.
        UserAccess userAccess = new();
        List<UserModel> allUsers = userAccess.SelectAll();
          while (true)
          {
            // converts the user models and the first name and last name and role into a list
            List<string> userOptions = allUsers
            .Select(user => $"{user.FirstName} {user.LastName} - {user.Role}")
            .ToList();

           ConsoleMenu<string> allUserOptions = new();
           int selectedUserOption = allUserOptions.SelectOption(userOptions);

            if(selectedUserOption == -1)
            {
              break;
            } 
            // todo put all the roles into a list and from there let them select what role they want the user to be in
            List<Roles> roleList = Enum.GetValues(typeof(Roles))
                        .Cast<Roles>()
                        .ToList();
            ConsoleMenu<Roles> allRoleOptions = new();
            int selectedRole = allRoleOptions.SelectOption(roleList);
            
          } 
          break;
         case 1:
         break;
       
      }
    }
  }



public static void Start()
    {


        while (true)
        {

                       
             ConsoleMenu<string> userOptions = new();
            int selectedOption = userOptions.SelectOption(MainUserOptions);

      // pressed escapekey 

      switch (selectedOption)
      {
        // pressed escape key
        case -1:
        return;

        case 0:
        // change user roles 
        ChangeUserRoles();
        break;

        case 1:
        //change user info
        break;
      }
        
        
             
        }
        }
       
}