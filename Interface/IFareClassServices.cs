public interface IFareClassServices
{
    Task<ServicesResponse<int>> CreateAsync(CreateFareClassRequest dto, Guid airlineStaffId);
}
