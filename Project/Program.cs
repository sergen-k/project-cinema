// See https://aka.ms/new-console-template for more information
Console.CursorVisible = false;
Console.Clear();

List<ConsumableOrderModel> selectedItems = FoodsAndDrinks.Start();

Console.WriteLine($"Returned items: {selectedItems.Count}");

foreach (ConsumableOrderModel item in selectedItems)
{
    Console.WriteLine(
        $"Consumable ID: {item.ConsumableId}, Quantity: {item.Quantity}, Unit Price: €{item.UnitPrice:F2}"
    );
}

Console.WriteLine("Press Enter to continue...");
Console.ReadLine();


Menu.Start();
