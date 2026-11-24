public static class FoodsAndDrinks
{
    private static List<OptionBase> _options = new();

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
            ConsoleMenu menu = new();
            _options.Clear();

            _options.Add(new InfoOption("=== Foods and Drinks ==="));

            foreach (string category in categories)
            {
                _options.Add(new InfoOption(""));
                _options.Add(new InfoOption($"=== {category.ToUpper()} ==="));

                bool containsConsumable = false;

                foreach (var item in consumables)
                {
                    if (IsInCategory(item.Category, category))
                    {
                        _options.Add(new SelectOption($"{item.Name} - €{item.Price:F2}"));
                        containsConsumable = true;
                    }
                }

                if (!containsConsumable)
                {
                    _options.Add(new InfoOption($"No {(category == "Food" || category == "Combos" ? category.ToLower() : $"{category.ToLower()}s")} available."));
                }
            }

            _options.Add(new InfoOption(""));
            _options.Add(new SelectOption("Continue"));

            menu.SetOptions(_options);
            int option = menu.SelectOption();

            // Return to seat ordering
            if (option == -1)
            {
                return new List<ConsumableOrderModel>() { new(-1, 0, 0, 0, 0) };
            }

            // "Continue" was selected
            if (_options[option].Label == "Continue")
            {
                int result = ReviewOrder(logic, consumables);

                // Confirm order
                if (result == 1)
                {
                    if (logic.GetSelectedItems().Count == 0)
                    {
                        return new List<ConsumableOrderModel>();
                    }

                    return logic.GetSelectedItems();
                }

                // Cancel food selection
                if (result == 2)
                {
                    return null;
                }

                // Add more items
                continue;
            }

            // A consumable was selected
            int quantity;

            ConsumableModel selectedItem = allConsumables.First(c =>
                c.Name == _options[option].Label.Split(" - ")[0]);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Selected: {selectedItem.Name} - €{selectedItem.Price:F2}");
                Console.Write("Enter quantity: ");

                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                {
                    break;
                }

                Console.Clear();
                Console.WriteLine("Invalid quantity. Please enter a positive number.");
                Pause();
            }

            logic.AddItem(selectedItem, quantity);

            Console.Clear();
            Console.WriteLine($"{quantity} x {selectedItem.Name} added successfully!");
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

    private static void ShowSummary(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        _options.Clear();
        _options.Add(new InfoOption("=== Order Summary ==="));

        List<ConsumableOrderModel> selectedItems = logic.GetSelectedItems();

        int number = 1;

        if (selectedItems.Count == 0)
        {
            _options.Add(new InfoOption("No food or drinks selected."));
            return;
        }

        foreach (ConsumableOrderModel order in selectedItems)
        {
            foreach (ConsumableModel item in consumables)
            {
                if (order.ConsumableId == item.Id)
                {
                    double subtotal = order.Quantity * order.UnitPrice;

                    _options.Add(new InfoOption($"{number}. {order.Quantity} x {item.Name} - €{subtotal:F2}"));

                    number++;
                    break;
                }
            }
        }

        _options.Add(new InfoOption(""));
        _options.Add(new InfoOption($"Total: €{logic.CalculateTotal():F2}"));
    }

    // Let the user pick one of the currently selected items. Returns null when they go back.
    private static ConsumableModel SelectItem(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        ConsoleMenu menu = new();
        _options.Clear();

        _options.Add(new InfoOption("=== Select an Item ==="));

        foreach (ConsumableOrderModel order in logic.GetSelectedItems())
        {
            foreach (ConsumableModel item in consumables)
            {
                if (order.ConsumableId == item.Id)
                {
                    double subtotal = order.Quantity * order.UnitPrice;

                    _options.Add(new SelectOption($"{order.Quantity} x {item.Name} - €{subtotal:F2}"));
                    break;
                }
            }
        }

        menu.SetOptions(_options);
        int selected = menu.SelectOption();

        if (selected == -1)
        {
            return null;
        }

        string itemName = _options[selected].Label
            .Split(" x ")[1]
            .Split(" - ")[0];

        return consumables.First(c => c.Name == itemName);
    }

    private static int ReviewOrder(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        while (true)
        {
            _options.Clear();
            ConsoleMenu menu = new();

            ShowSummary(logic, consumables);

            _options.Add(new InfoOption(""));
            _options.Add(new SelectOption("Confirm and Continue"));
            _options.Add(new SelectOption("Change Quantity"));
            _options.Add(new SelectOption("Remove Item"));
            _options.Add(new SelectOption("Add More Items"));

            menu.SetOptions(_options);
            int choice = menu.SelectOption();

            // Cancel food selection
            if (choice == -1)
            {
                return 2;
            }

            string selectedOption = _options[choice].Label;

            // Confirm order
            if (selectedOption == "Confirm and Continue")
            {
                return 1;
            }

            // Return to food selection
            if (selectedOption == "Add More Items")
            {
                return 0;
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

            if (selectedOption == "Change Quantity")
            {
                Console.Clear();
                Console.WriteLine($"Selected: {selectedItem.Name}");
                Console.Write("Enter new quantity: ");

                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
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
            else if (selectedOption == "Remove Item")
            {
                Console.Clear();
                logic.RemoveItem(consumableId);
                Console.WriteLine("Item removed!");
                Pause();
            }
        }
    }
}