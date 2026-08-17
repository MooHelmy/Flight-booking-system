public interface IQueueServices
{
    Task<QueueStatusResponse> JoinQueueAsync(int flightId, Guid userId);
    Task<QueueStatusResponse> GetStatusAsync(Guid queueTicketId, Guid userId);
}
