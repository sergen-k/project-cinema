public class PaymentModel
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public string PaymentNumber { get; set; }
    public string Method { get; set; }
    public string Status { get; set; }
    public double Amount { get; set; }

    public PaymentModel(long id, long reservationId, string paymentNumber, string method, string status, double amount)
    {
        Id = id;
        ReservationId = reservationId;
        PaymentNumber = paymentNumber;
        Method = method;
        Status = status;
        Amount = amount;
    }
}
