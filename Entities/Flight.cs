public class Flight
{
    public Guid Id { get; set; }
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public DateTime SalesStartAt { get; set; }
    public FlightStatus Status { get; set; }
    public Guid AirlineStaffId { get; set; }
    public DateTime CreatedAt { get; set; }
}
