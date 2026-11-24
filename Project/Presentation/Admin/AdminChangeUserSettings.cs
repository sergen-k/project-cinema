using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;

public static class AdminChangeUserSettings
{

  public static List<string> MainUserOptions = ["Change user roles", "Change user info"];
  private static List<OptionBase> _options = new();

  public static void ChangeUserRoles()
  {
    ConsoleMenu menu = new();
    while (true)
    {
      _options.Clear();
      _options.Add(new SelectOption("Show All Users"));
      _options.Add(new SelectOption("Select User by Email"));
      menu.SetOptions(_options);
      int option = menu.SelectOption();
      switch (option)
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

            ConsoleMenu allUserOptions = new();
            _options.Clear();
            allUsers.Remove(allUsers.Find(user => user.Id == UserLogic.CurrentAccount.Id));
            foreach (var user in allUsers)
            {
              _options.Add(new SelectOption($"{user.FirstName} {user.LastName}, {user.Role}"));
            }
            allUserOptions.SetOptions(_options);
            int selectedOption = allUserOptions.SelectOption();
            if (selectedOption == -1)
            {
              break;
            }
            UserModel selectedUser = allUsers[selectedOption];
            // todo put all the roles into a list and from there let them select what role they want the user to be in
            List<Roles> roleList = Enum.GetValues(typeof(Roles))
                        .Cast<Roles>()
                        .ToList();
            ConsoleMenu allRoleOptions = new();
            _options.Clear();
            foreach (var role in roleList)
            {
              _options.Add(new SelectOption(role.ToString()));
            }
            allRoleOptions.SetOptions(_options);
            int roleOption = allRoleOptions.SelectOption();
            if (roleOption == -1) { continue; }
            Roles selectedRole = roleList[roleOption];
            selectedUser.Role = selectedRole.ToString();
            userAccess.Update(selectedUser);
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
      ConsoleMenu menu = new();
      _options.Clear();
      _options.Add(new SelectOption("Change User Roles"));
      _options.Add(new SelectOption("Change User Info"));
      menu.SetOptions(_options);
      int option = menu.SelectOption();

      switch (option)
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