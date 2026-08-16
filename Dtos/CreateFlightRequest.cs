public class CreateFlightRequest
{
    public string FlightNumber { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public DateTime SalesStartAt { get; set; }
}
// CreateFlightRequest  // ده وظيفته تسمح لـ Client بإنشاء Flight جديد