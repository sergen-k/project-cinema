static class AdminPanel
{
    const string Banner = """
        ══════════════════════════════════════════════════════════════════════════════

         █████╗ ██████╗ ███╗   ███╗██╗███╗   ██╗    ██████╗  █████╗ ███╗   ██╗███████╗██╗
        ██╔══██╗██╔══██╗████╗ ████║██║████╗  ██║    ██╔══██╗██╔══██╗████╗  ██║██╔════╝██║
        ███████║██║  ██║██╔████╔██║██║██╔██╗ ██║    ██████╔╝███████║██╔██╗ ██║█████╗  ██║
        ██╔══██║██║  ██║██║╚██╔╝██║██║██║╚██╗██║    ██╔═══╝ ██╔══██║██║╚██╗██║██╔══╝  ██║
        ██║  ██║██████╔╝██║ ╚═╝ ██║██║██║ ╚████║    ██║     ██║  ██║██║ ╚████║███████╗███████╗
        ╚═╝  ╚═╝╚═════╝ ╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝    ╚═╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝

        ══════════════════════════════════════════════════════════════════════════════
        
        """;
    public static void Start()
    {
        while (true)
        {
            ConsoleMenu menu = new();
            menu.IsMainMenu = true;
            menu.AddOption(new InfoOption(Banner));
            menu.PopulateOptions(GetOptions());

            string chosenOption = menu.SelectOption();

            Console.Clear();
            switch (chosenOption)
            {
                case "Change User Settings":
                    AdminChangeUserSettings.Start();
                    break;
                case "Add Movie":
                    AdminAddMovie.Start();
                    break;
                case "Manage Screenings":
                    AdminManageScreenings.Start();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;

                case null:
                    break;
            }
            if (chosenOption == null)
            {
                break;
            }
        }
    }

    private static List<string> GetOptions()
    {
        List<string> options = [];
        options.Add("Change User Settings");
        options.Add("Add Movie");
        options.Add("Manage Screenings");
        return options;
    }
}