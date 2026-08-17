using System.ComponentModel.DataAnnotations;

public class FareClass
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int OverbookingLimit { get; set; }
    public int BookedSeats { get; set; }
    public DateTime CreatedAt { get; set; }

    // Concurrency token يدوي — بيتزود +1 مع أي تعديل على BookedSeats
    // عشان نمنع اتنين طلب متزامنين يحجزوا مقاعد فوق بعض (Fix #2)
    [ConcurrencyCheck]
    public int RowVersion { get; set; }
}
