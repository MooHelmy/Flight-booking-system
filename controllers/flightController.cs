using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
[Authorize]
public class flightController(IFlightServices flightServices) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllAsync(string? origin, string? destination, DateTime? date)
    {
        var products = await flightServices.GetAllAsync(origin, destination, date);
        return products.Any() ? Ok(products) : NotFound(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var product = await flightServices.GetByIdAsync(id);
        return product is null ? NotFound(product) : Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult> CreateAsync(CreateFlightRequest dto, Guid airlineStaffId)
    {
        var result = await flightServices.CreateAsync(dto, airlineStaffId);
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
    [HttpPost("{id}/publish")]
    public async Task<ActionResult> PublishAsync(int id, Guid airlineStaffId)
    {
        var result = await flightServices.PublishAsync(id, airlineStaffId);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }


}