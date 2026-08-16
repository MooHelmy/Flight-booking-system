public class Booking
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SeatHoldId { get; set; }
    public string BookingReference { get; set; } // كود من 6 حروف/أرقام، زي أكواد PNR الحقيقية
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime ConfirmedAt { get; set; }
}
