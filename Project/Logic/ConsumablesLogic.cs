public class ConsumablesLogic
{
    private ConsumableAccess _access = new ConsumableAccess();

    private List<ConsumableOrderModel> _selectedItems = new();

    public List<ConsumableModel> GetAll()
    {
        return _access.GetAll();
    }

    public bool AddItem(ConsumableModel item, int quantity)
    {
        if (quantity <= 0)
        {
            return false;
        }

        foreach (ConsumableOrderModel order in _selectedItems)
        {
            if (order.ConsumableId == item.Id)
            {
                order.Quantity += quantity;
                return true;
            }
        }

        ConsumableOrderModel newOrder = new ConsumableOrderModel(
            0,
            0,
            item.Id,
            quantity,
            item.Price
        );

        _selectedItems.Add(newOrder);

        return true;
    }

    public List<ConsumableOrderModel> GetSelectedItems()
    {
        return _selectedItems;
    }

    public double CalculateTotal()
    {
        double total = 0;

        foreach (ConsumableOrderModel order in _selectedItems)
        {
            total += order.Quantity * order.UnitPrice;
        }

        return total;
    }

    public bool ChangeQuantity(long consumableId, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            return false;
        }

        foreach (ConsumableOrderModel order in _selectedItems)
        {
            if (order.ConsumableId == consumableId)
            {
                order.Quantity = newQuantity;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(long consumableId)
    {
        for (int i = 0; i < _selectedItems.Count; i++)
        {
            if (_selectedItems[i].ConsumableId == consumableId)
            {
                _selectedItems.RemoveAt(i);
                return true;
            }
        }

        return false;
    }
}