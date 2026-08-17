public class FlightResponse
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public FlightStatus Status { get; set; }
    public List<FareClassResponse> FareClasses { get; set; } = new();
}