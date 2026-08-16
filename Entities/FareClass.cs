public class FareClass
{
    public Guid Id { get; set; }
    public Guid FlightId { get; set; }
    public string Name { get; set; }        // Economy, Business, First
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int OverbookingLimit { get; set; }
    public int BookedSeats { get; set; }
    public byte[] Version { get; set; }      // RowVersion — Optimistic Concurrency
    public DateTime CreatedAt { get; set; }
}
