public static class FoodsAndDrinks
{
    public static List<ConsumableOrderModel> Start()
    {
        ConsumablesLogic logic = new ConsumablesLogic();

        List<ConsumableModel> consumables = logic.GetAll();

        if (consumables.Count == 0)
        {
            Console.WriteLine("No food or drinks available.");
            return new List<ConsumableOrderModel>();
        }

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Foods and Drinks ===");

            for (int i = 0; i < consumables.Count; i++)
            {
                ConsumableModel item = consumables[i];

                Console.WriteLine(
                    $"{i + 1}. {item.Name} ({item.Category}) - €{item.Price:F2}"
                );
            }

            Console.WriteLine("0. Skip / Cancel Foods and Drinks");
            Console.WriteLine("7. Finish selecting");

            Console.Write("Choose an option: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            if (choice == 0)
            {
                Console.WriteLine("Foods and Drinks skipped.");
                return new List<ConsumableOrderModel>();
            }

            if (choice == 7)
            {
                if (logic.GetSelectedItems().Count == 0)
                {
                    Console.WriteLine("No items selected. Choose 0 to skip.");
                    continue;
                }

                int result = ReviewOrder(logic, consumables);

                if (result == 1)
                {
                    return new List<ConsumableOrderModel>(
                        logic.GetSelectedItems()
                    );
                }

                if (result == 2)
                {
                    return new List<ConsumableOrderModel>();
                }

                continue;
            }

            if (choice < 1 || choice > consumables.Count)
            {
                Console.WriteLine("Invalid option.");
                continue;
            }

            ConsumableModel selectedItem = consumables[choice - 1];

            Console.Write("Enter quantity: ");

            int quantity;

            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Please enter a positive number.");
                continue;
            }

            logic.AddItem(selectedItem, quantity);

            Console.WriteLine(
                $"{quantity} x {selectedItem.Name} added successfully!"
            );
        }
    }

    private static void ShowSummary(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        Console.WriteLine();
        Console.WriteLine("=== Order Summary ===");

        List<ConsumableOrderModel> selectedItems = logic.GetSelectedItems();
        
        int number = 1;

        if (selectedItems.Count == 0)
        {
            Console.WriteLine("No food or drinks selected.");
            return;
        }

        foreach (ConsumableOrderModel order in selectedItems)
        {
            foreach (ConsumableModel item in consumables)
            {
                if (order.ConsumableId == item.Id)
                {
                    double subtotal = order.Quantity * order.UnitPrice;

                    Console.WriteLine($"{number}. {order.Quantity} x {item.Name} - €{subtotal:F2}");

                    number++;
                    break;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Total: €{logic.CalculateTotal():F2}");
    }

    private static int ReviewOrder(ConsumablesLogic logic, List<ConsumableModel> consumables)
    {
        while (true)
        {
            ShowSummary(logic, consumables);

            Console.WriteLine();
            Console.WriteLine("1. Confirm and Continue");
            Console.WriteLine("2. Change Quantity");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Add More Items");
            Console.WriteLine("5. Cancel Foods and Drinks");

            Console.Write("Choose an option: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice == 1)
            {
                if (logic.GetSelectedItems().Count == 0)
                {
                    Console.WriteLine("No items to confirm.");
                    continue;
                }

                Console.WriteLine("Foods and Drinks confirmed!");
                return 1;
            }

            if (choice == 4)
            {
                return 3;
            }

            if (choice == 5)
            {
                Console.WriteLine("Foods and Drinks cancelled.");
                return 2;
            }

            if (choice == 2 || choice == 3)
            {
                List<ConsumableOrderModel> selectedItems =
                    logic.GetSelectedItems();

                Console.Write("Enter item number: ");

                int itemNumber;

                if (!int.TryParse(Console.ReadLine(), out itemNumber)
                    || itemNumber < 1
                    || itemNumber > selectedItems.Count)
                {
                    Console.WriteLine("Invalid item number.");
                    continue;
                }

                long consumableId =
                    selectedItems[itemNumber - 1].ConsumableId;

                if (choice == 2)
                {
                    Console.Write("Enter new quantity: ");

                    int quantity;

                    if (!int.TryParse(Console.ReadLine(), out quantity)
                        || quantity <= 0)
                    {
                        Console.WriteLine("Invalid quantity.");
                        continue;
                    }

                    logic.ChangeQuantity(consumableId, quantity);
                    Console.WriteLine("Quantity updated!");
                }
                else
                {
                    logic.RemoveItem(consumableId);
                    Console.WriteLine("Item removed!");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }
}