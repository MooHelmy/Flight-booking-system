public interface IHoldServices
{
    // Fix #4: كانت بترجع ServicesResponse بس من غير الـ SeatHoldId،
    // يعني العميل مش هيعرف يكمل للدفع. دلوقتي بترجع الـ HoldResponse كامل.
    Task<ServicesResponse<HoldResponse>> CreateHoldAsync(CreateHoldRequest dto, Guid userId);
    Task<HoldResponse> GetByIdAsync(Guid holdId, Guid userId);
    Task<ServicesResponse> ReleaseAsync(Guid holdId, Guid userId);
}