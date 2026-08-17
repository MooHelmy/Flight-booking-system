public static class ExtensionMethod
{
    public static Flight FlightToEntityMapper(this CreateFlightRequest dto, Guid airlineStaffId)
    {
        return new Flight
        {
            FlightNumber = dto.FlightNumber,
            Origin = dto.Origin,
            Destination = dto.Destination,
            DepartureAt = dto.DepartureAt,
            ArrivalAt = dto.ArrivalAt,
            SalesStartAt = dto.SalesStartAt,
            Status = FlightStatus.Draft,
            AirlineStaffId = airlineStaffId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static FlightResponse FlightToResponseMapper(this Flight flight)
    {
        return new FlightResponse
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            Origin = flight.Origin,
            Destination = flight.Destination,
            DepartureAt = flight.DepartureAt,
            ArrivalAt = flight.ArrivalAt,
            Status = flight.Status,
            FareClasses = flight.FareClasses.Select(fc => fc.FareClassToResponseMapper()).ToList()
        };
    }

    public static FlightListItemResponse FlightToListItemMapper(this Flight flight)
    {
        return new FlightListItemResponse
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            Origin = flight.Origin,
            Destination = flight.Destination,
            DepartureAt = flight.DepartureAt,
            Status = flight.Status,
            StartingPrice = flight.FareClasses.Any() ? flight.FareClasses.Min(fc => fc.Price) : 0
        };
    }

    public static FareClass FareClassToEntityMapper(this CreateFareClassRequest dto)
    {
        return new FareClass
        {
            FlightId = dto.FlightId,
            Name = dto.Name,
            Price = dto.Price,
            TotalSeats = dto.TotalSeats,
            OverbookingLimit = dto.OverbookingLimit,
            BookedSeats = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static FareClassResponse FareClassToResponseMapper(this FareClass fareClass)
    {
        return new FareClassResponse
        {
            Id = fareClass.Id,
            Name = fareClass.Name,
            Price = fareClass.Price,
            AvailableSeats = fareClass.TotalSeats + fareClass.OverbookingLimit - fareClass.BookedSeats
        };
    }

    public static QueueStatusResponse QueueTicketToStatusMapper(this QueueTicket ticket)
    {
        int estimatedSeconds = ticket.Status == QueueStatus.Waiting
            ? ticket.QueuePosition * 10
            : 0;

        return new QueueStatusResponse
        {
            QueuePosition = ticket.QueuePosition,
            Status = ticket.Status,
            EstimatedWaitSeconds = estimatedSeconds,
            ExpiresAt = ticket.ExpiresAt
        };
    }

    public static HoldResponse HoldToResponseMapper(this SeatHold hold)
    {
        int secondsRemaining = hold.Status == HoldStatus.Active
            ? (int)Math.Max(0, (hold.ExpiresAt - DateTime.UtcNow).TotalSeconds)
            : 0;

        return new HoldResponse
        {
            Id = hold.Id,
            Quantity = hold.Quantity,
            TotalPrice = hold.Quantity * hold.FareClass.Price,
            SecondsRemaining = secondsRemaining,
            Status = hold.Status
        };
    }

    public static BookingResponse BookingToResponseMapper(this Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            BookingReference = booking.BookingReference,
            FlightNumber = booking.SeatHold.FareClass.Flight.FlightNumber,
            Origin = booking.SeatHold.FareClass.Flight.Origin,
            Destination = booking.SeatHold.FareClass.Flight.Destination,
            DepartureAt = booking.SeatHold.FareClass.Flight.DepartureAt,
            FareClassName = booking.SeatHold.FareClass.Name,
            Quantity = booking.Quantity,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status,
            ConfirmedAt = booking.ConfirmedAt
        };
    }
}
