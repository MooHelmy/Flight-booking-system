public class FlightResponse
{
    public Guid Id { get; set; }
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public FlightStatus Status { get; set; }
    public List<FareClassResponse> FareClasses { get; set; }
}
// FlightResponse  // ده وظيفته تسمح لـ Client بإرجاع Flight من الـ FareClasses