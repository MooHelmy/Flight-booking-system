using System.ComponentModel.DataAnnotations;

public class SeatHold
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }

    public int FareClassId { get; set; }
    public FareClass FareClass { get; set; } = null!;

    public int Quantity { get; set; }
    public HoldStatus Status { get; set; }
    public DateTime HeldAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    // Fix #5: نفس فكرة FareClass.RowVersion، بس هنا عشان نمنع
    // اتنين نداء ConfirmAsync متزامنين يأكدوا نفس الـ hold مرتين
    [ConcurrencyCheck]
    public int RowVersion { get; set; }
}
