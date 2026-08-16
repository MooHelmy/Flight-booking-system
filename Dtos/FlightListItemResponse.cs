public class FlightListItemResponse
{
    public Guid Id { get; set; }
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureAt { get; set; }
    public FlightStatus Status { get; set; }
    public decimal StartingPrice { get; set; }
}
// FlightListItemResponse  // ده وظيفته تسمح لـ Client بإرجاع Flight من غير الـ FareClasses