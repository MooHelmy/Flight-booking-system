public class FlightListItemResponse
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureAt { get; set; }
    public FlightStatus Status { get; set; }
    public decimal StartingPrice { get; set; }
}