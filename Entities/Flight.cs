public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public DateTime SalesStartAt { get; set; }
    public FlightStatus Status { get; set; }
    public Guid AirlineStaffId { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<FareClass> FareClasses { get; set; } = new List<FareClass>();
}
