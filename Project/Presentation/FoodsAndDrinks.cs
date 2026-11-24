public static class FoodsAndDrinks
{
    public static List<ConsumableOrderModel> Start()
    {
        ConsumablesLogic logic = new ConsumablesLogic();
 
        List<ConsumableModel> allConsumables = logic.GetAll();
 
        // Organise products by category
        List<ConsumableModel> consumables = new List<ConsumableModel>();
 
        string[] categories = { "Food", "Drinks", "Combos" };
 
        foreach (string category in categories)
        {
            foreach (ConsumableModel item in allConsumables)
            {
                if (IsInCategory(item.Category, category))
                {
                    consumables.Add(item);
                }
            }
        }
 
        if (consumables.Count == 0)
        {
            Console.WriteLine("No food or drinks available.");
            return new List<ConsumableOrderModel>();
        }
 
        while (true)
        {
            ConsoleMenu<ConsumableModel> menu = new();
            menu.AddOption(new InfoOption<ConsumableModel>("=== Foods and Drinks ==="));
            foreach (string category in categories)
            {
                menu.AddOption(new InfoOption<ConsumableModel>(""));
                menu.AddOption(new InfoOption<ConsumableModel>($"=== {category.ToUpper()} ==="));
                bool containsConsumable = false;
                foreach (var item in consumables)
                {
                    if (IsInCategory(item.Category, category))
                    {
                        menu.AddOption(new SelectOption<ConsumableModel>($"{item.Name} - €{item.Price:F2}", item));
                        containsConsumable = true;
                    }
                }
                if (!containsConsumable) { menu.AddOption(new InfoOption<ConsumableModel>($"No {(category == "Food" || category == "Combos" ? category.ToLower() : $"{category.ToLower()}s")} available.")); }
            }
            
            menu.AddOption(new InfoOption<ConsumableModel>(""));
            menu.AddOption(new SelectOption<ConsumableModel>("Continue", new(-1, "", "", 0.0)));
 
            ConsumableModel option = menu.SelectOption();
 
            if (option == null)
            {
                return new List<ConsumableOrderModel>() { new(-1, 0, 0, 0, 0) };
            }
 
            // "Continue" was selected
            if (option.Id == -1)
            {
                int result = ReviewOrder(logic, consumables);
 
                if (result == 1)
                {
                    if (logic.GetSelectedItems().Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return logic.GetSelectedItems();   
                    }
                }
 
                if (result == 2)
                {
                    return new List<ConsumableOrderModel>();
                }
 
                continue;
            }
 
            // A consumable was selected
            int quantity;
 
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Selected: {option.Name} - €{option.Price:F2}");
                Console.Write("Enter quantity: ");
 
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                {
                    break;
                }
                
                Console.Clear();
                Console.WriteLine("Invalid quantity. Please enter a positive number.");
                Pause();
            }
 
            logic.AddItem(option, quantity);
            
            Console.Clear();
            Console.WriteLine($"{quantity} x {option.Name} added successfully!");
            Pause();
        }
    }
 
    // Check if a product belongs to a category
    private static bool IsInCategory(string itemCategory, string category)
    {
        string value = itemCategory.Trim().ToLower();
 
        if (category == "Food")
        {
            return value == "food" || value == "foods";
        }
 
        if (category == "Drinks")
        {
            return value == "drink" || value == "drinks";
        }
 
        if (category == "Combos")
        {
            return value == "combo" || value == "combos";
        }
 
        return false;
    }
 
    // Wait for a key press so messages stay visible before the menu redraws
    private static void Pause()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
 
    private static void ShowSummary(ConsoleMenu<string> menu, ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        menu.AddOption(new InfoOption<string>("=== Order Summary ==="));
 
        List<ConsumableOrderModel> selectedItems = logic.GetSelectedItems();
 
        int number = 1;
 
        if (selectedItems.Count == 0)
        {
            menu.AddOption(new InfoOption<string>("No food or drinks selected."));
            return;
        }
 
        foreach (ConsumableOrderModel order in selectedItems)
        {
            foreach (ConsumableModel item in consumables)
            {
                if (order.ConsumableId == item.Id)
                {
                    double subtotal = order.Quantity * order.UnitPrice;
 
                    menu.AddOption(new InfoOption<string>($"{number}. {order.Quantity} x {item.Name} - €{subtotal:F2}"));
 
                    number++;
                    break;
                }
            }
        }
 
        menu.AddOption(new InfoOption<string>(""));
        menu.AddOption(new InfoOption<string>($"Total: €{logic.CalculateTotal():F2}"));
    }
 
    // Let the user pick one of the currently selected items. Returns null when they go back.
    private static ConsumableModel SelectItem(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        ConsoleMenu<ConsumableModel> menu = new();
        menu.AddOption(new InfoOption<ConsumableModel>("=== Select an item ==="));
 
        foreach (ConsumableOrderModel order in logic.GetSelectedItems())
        {
            foreach (ConsumableModel item in consumables)
            {
                if (order.ConsumableId == item.Id)
                {
                    double subtotal = order.Quantity * order.UnitPrice;
 
                    menu.AddOption(new SelectOption<ConsumableModel>($"{order.Quantity} x {item.Name} - €{subtotal:F2}", item));
                    break;
                }
            }
        }
 
        menu.AddOption(new SelectOption<ConsumableModel>("Back", new(-1, "", "", 0.0)));
 
        ConsumableModel selected = menu.SelectOption();
 
        if (selected == null || selected.Id == -1)
        {
            return null;
        }
 
        return selected;
    }
 
    private static int ReviewOrder(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        while (true)
        {
            ConsoleMenu<string> menu = new();
 
            ShowSummary(menu, logic, consumables);
 
            menu.AddOption(new InfoOption<string>(""));
            menu.AddOption(new SelectOption<string>("Confirm and Continue", "confirm"));
            menu.AddOption(new SelectOption<string>("Change Quantity", "change"));
            menu.AddOption(new SelectOption<string>("Remove Item", "remove"));
            menu.AddOption(new SelectOption<string>("Add More Items", "add"));
 
            string choice = menu.SelectOption();
 
            // Going back without choosing an option returns to the item menu
            if (choice == null || choice == "add")
            {
                return 3;
            }
 
            if (choice == "confirm")
            {
                return 1;
            }
 
            // Change quantity or remove item
            if (logic.GetSelectedItems().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No items selected.");
                Pause();
                continue;
            }
 
            ConsumableModel selectedItem = SelectItem(logic, consumables);
 
            if (selectedItem == null)
            {
                continue;
            }
 
            long consumableId = selectedItem.Id;
 
            if (choice == "change")
            {
                Console.Clear();
                Console.WriteLine($"Selected: {selectedItem.Name}");
                Console.Write("Enter new quantity: ");
 
                int quantity;
 
                if (!int.TryParse(Console.ReadLine(), out quantity)
                    || quantity <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid quantity.");
                    Pause();
                    continue;
                }
                
                Console.Clear();
                logic.ChangeQuantity(consumableId, quantity);
                Console.WriteLine("Quantity updated!");
                Pause();
            }
            else
            {
                Console.Clear();
                logic.RemoveItem(consumableId);
                Console.WriteLine("Item removed!");
                Pause();
            }
        }
    }
}