public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }

    public Guid SeatHoldId { get; set; }
    public SeatHold SeatHold { get; set; } = null!;

    // لازم Unique Index في الـ DbContext — راجع قسم 31
    public string BookingReference { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime ConfirmedAt { get; set; }
}
