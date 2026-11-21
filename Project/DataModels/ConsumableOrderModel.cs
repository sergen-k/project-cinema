public class ConsumableOrderModel
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public long ConsumableId { get; set; }
    public long Quantity { get; set; }
    public double UnitPrice { get; set; }

    public ConsumableOrderModel(long id, long reservationId, long consumableId, long quantity, double unitPrice)
    {
        Id = id;
        ReservationId = reservationId;
        ConsumableId = consumableId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}