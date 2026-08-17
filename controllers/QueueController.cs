using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
[Authorize]
public class QueueController(IQueueServices queueServices) : ControllerBase
{
    [HttpPost("{id}/join")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> JoinQueueAsync(int id, Guid userId)
    {
        var result = await queueServices.JoinQueueAsync(id, userId);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
    [HttpGet("{id}/status")]
    public async Task<ActionResult> GetStatusAsync(Guid id, Guid userId)
    {
        var result = await queueServices.GetStatusAsync(id, userId);
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
}