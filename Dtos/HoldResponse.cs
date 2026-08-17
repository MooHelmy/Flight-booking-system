public class HoldResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public int SecondsRemaining { get; set; }
    public HoldStatus Status { get; set; }
}