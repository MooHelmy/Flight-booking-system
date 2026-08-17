public interface IQueueServices
{
    Task<ServicesResponse<QueueStatusResponse>> JoinQueueAsync(int flightId, Guid userId);
    Task<ServicesResponse<QueueStatusResponse>> GetStatusAsync(Guid queueTicketId, Guid userId);
}
