using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
[Authorize]
public class FareClassController(IFareClassServices fareClassServices) : ControllerBase
{

    [HttpPost]
    [Authorize(Roles = "Manager,AirlineStaff")]
    public async Task<ActionResult> CreateAsync(CreateFareClassRequest dto, Guid airlineStaffId)
    {
        var result = await fareClassServices.CreateAsync(dto, airlineStaffId);
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
}