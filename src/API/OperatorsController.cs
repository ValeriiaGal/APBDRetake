using Application.Exceptions;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace RetakeAPBD;

[ApiController]
[Route("api/mobiles")]
public class OperatorsController(IOperatorService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetPhoneNumberDTO>>> GetAllPhoneNumbers()
    {
        try
        {
            var test = await service.GetAllPhoneNumbersAsync();
            return Ok(test);
        }
        catch (ServerException e)
        {
            return Problem(e.Message); //code: 500
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}