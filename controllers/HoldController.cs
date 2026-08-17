using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
[Authorize]
public class HoldController(IHoldServices holdServices) : ControllerBase
{
    [HttpPost("{id}/hold")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> CreateHoldAsync(CreateHoldRequest dto, Guid userId)
    {
        var result = await holdServices.CreateHoldAsync(dto, userId);
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id, Guid userId)
    {
        var hold = await holdServices.GetByIdAsync(id, userId);
        return hold is null ? NotFound(hold) : Ok(hold);
    }
    [HttpPost("{id}/release")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> ReleaseAsync(Guid id, Guid userId)
    {
        var result = await holdServices.ReleaseAsync(id, userId);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
}