public class ConsoleMenu

{
    public string Header {get; set;}

    public ConsoleMenu(string header) => Header = header;
    public ConsoleMenu() => Header = null;

    public string SelectOption(List<string> options)
    {
        if (options == null || options.Count() == 0)
        {
            return null;
        }

        int selectedIndex = 0;



        while (true)
        {
            Console.Clear();
            if (Header != null) {Console.WriteLine(Header);}

            for (int i = 0; i < options.Count; i++)
            {
                if (options[i] == null)
                {
                    Console.WriteLine();
                }
                else if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"> {options[i]} ");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"  {options[i]} ");
                }
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                    if (options[selectedIndex] == null) {selectedIndex--;}
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Count;
                    if (options[selectedIndex] == null) {selectedIndex++;}
                    break;

                case ConsoleKey.Enter:
                    return options[selectedIndex];
            }


        }

    }


}